namespace MWC3.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using MWC3.DAL;
    using MWC3.Models;
    using MWC3.Helpers;

    public class BaseController : Controller
    {
        public readonly ApplicationDbContext Db = new ApplicationDbContext();
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }

        public BaseController()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Db));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Db));
        }

        public BaseController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public string LanguageCode = string.Empty;

        public void PopulateUserName()
        {
            ViewData["UserName"] = this.GetUserName();
        }

        public string GetUserName()
        {
            var userName = string.Empty;
            if (User.Identity.IsAuthenticated)
            {
                userName = User.Identity.Name;
            }
            return userName;
        }

        public string GetUserId()
        {
            var userId = string.Empty;
            if (User != null && User.Identity.IsAuthenticated)
            {
                var userName = this.GetUserName();
                var user = Db.Users.FirstOrDefault(x => userName != null && x.UserName == userName);
                if (user != null)
                {
                    userId = user.Id;
                }
            }
            return userId;
        }

        public void PopulateColorList()
        {
            var list = this.Db.GrapeColors.Where(g => g.LanguageCode == "en").ToList();
            ViewData["GrapeColors"] = new SelectList(list, "ColorId", "Name");
        }

        public void PopulateParentGrapeList()
        {
            var list = this.Db.Grapes.Where(g => g.ParentId == 0).OrderBy(g => g.Name).ToList();
            list.Insert(0, new Grape { Id = 0, Name = "No parent grape" });
            ViewData["ParentGrapes"] = new SelectList(list, "Id", "Name");
        }

        public void PopulateGrapeList()
        {
            var list = this.Db.Grapes.ToList();
            ViewData["Grapes"] = new SelectList(list.OrderBy(l=>l.Name), "Id", "Name");
        }

        public void PopulateCountryList()
        {
            LanguageCode = LanguageHelper.GetLanguageFromCookie(this.HttpContext);
            var list = this.Db.Countries.Where(g => g.ParentId == 0).ToList();

            var languageList = this.Db.Countries.Where(g => g.ParentId > 0 && g.LanguageCode.ToLower() == LanguageCode.ToLower()).ToList();

            foreach (var country in languageList)
            {
                list.Find(c => c.Id == country.ParentId).Name = country.Name;
            }

            ViewData["Countries"] = new SelectList(list.OrderBy(g => g.Name), "Id", "Name");
        }

        public void PopulateCountryList(int id)
        {
            LanguageCode = LanguageHelper.GetLanguageFromCookie(this.HttpContext);
            var list = this.Db.Countries.Where(g => g.ParentId == 0).ToList();

            var languageList = this.Db.Countries.Where(g => g.ParentId > 0 && g.LanguageCode.ToLower() == LanguageCode.ToLower()).ToList();

            foreach (var country in languageList)
            {
                list.Find(c => c.Id == country.ParentId).Name = country.Name;
            }

            var selectList = new SelectList(list.OrderBy(g => g.Name), "Id", "Name", id);

            ViewData["Countries"] = selectList;
        }


        public void PopulateParentCountryList()
        {
            var list = this.Db.Countries.Where(g => g.ParentId == 0).OrderBy(g => g.Name).ToList();
            list.Insert(0, new Country { Id = 0, Name = "No parent country" });
            ViewData["ParentCountries"] = new SelectList(list, "Id", "Name");
        }

        public void PopulateEnabledLanguages()
        {
            var list = this.Db.LanguageInfo.Where(l => l.Enabled);
            ViewData["LanguageCodes"] = new SelectList(list, "LanguageCode", "LanguageCode");
        }

        public void PopulateQualificationList()
        {
            var list = this.Db.Qualifications.ToList();
            list.Insert(0, new Qualification { Id = 0, Name = "No qualification" });
            ViewData["Qualifications"] = new SelectList(list, "Id", "Name");
        }

        public void PopulateQualificationList(int countryId)
        {
            var list = this.Db.Qualifications.Where(q => q.CountryId == countryId).ToList();
            list.Insert(0, new Qualification { Id = 0, Name = "No qualification" });
            ViewData["Qualifications"] = new SelectList(list, "Id", "Name");
        }

        public void PopulateRegionList()
        {
            var list = this.Db.Regions.OrderBy(r => r.Name).ToList();
            // list.Insert(0, new Region { Id = 0, Name = "No region" });
            ViewData["Regions"] = new SelectList(list, "Id", "Name");
        }

        public void PopulateRegionList(int countryId)
        {
            var list = this.Db.Regions.Where(r => r.CountryId == countryId).ToList();
            list.Insert(0, new Region { Id = 0, Name = "No region" });
            ViewData["Regions"] = new SelectList(list, "Id", "Name");
        }

        public void PopulateParentWineColorList()
        {
            var list = this.Db.WineColors.Where(g => g.ParentId == 0).OrderBy(g => g.Name).ToList();
            list.Insert(0, new WineColor { Id = 0, Name = "No parent wine color" });
            ViewData["ParentWineColors"] = new SelectList(list, "Id", "Name");
        }

        public void PopulateWineColorList()
        {
            LanguageCode = LanguageHelper.GetLanguageFromCookie(this.HttpContext);
            var list = this.Db.WineColors.Where(g => g.ParentId == 0).OrderBy(g => g.Name).ToList();

            var wineColorList = this.Db.WineColors.Where(g => g.ParentId > 0 && g.LanguageCode.ToLower() == LanguageCode.ToLower()).ToList();

            foreach (var wineColor in wineColorList)
            {
                list.Find(w => w.Id == wineColor.ParentId).Name = wineColor.Name;
            }
            
            ViewData["WineColors"] = new SelectList(list, "Id", "Name");
        }

        public void PopulateBusinessList()
        {
            var list = this.Db.Businesses.Where(g => g.IsProducer).OrderBy(g => g.Name).ToList();
            ViewData["Businesses"] = new SelectList(list, "Id", "Name");
        }

        public void PopulateMultiplierList()
        {
            var item1 = new SelectListItem { Text = "+", Value = "1" };
            var item2 = new SelectListItem { Text = "-", Value = "-1" };
            var list = new List<SelectListItem> { item1, item2 };
            ViewData["Multipliers"] = new SelectList(list, "Value", "Text");
        }

        public void PopulateParentMultiplierList()
        {
            var list = this.Db.TransactionTypes.Where(g => g.ParentId == 0).OrderBy(g => g.Name).ToList();
            list.Insert(0, new TransactionType { Id = 0, Name = "No parent transaction type" });
            ViewData["ParentTransactionTypes"] = new SelectList(list, "Id", "Name");
        }

        public void PopulateTransactionTypesList()
        {
            LanguageCode = LanguageHelper.GetLanguageFromCookie(this.HttpContext);
            var list = this.Db.TransactionTypes.Where(g => g.ParentId == 0).ToList();

            var languageList = this.Db.TransactionTypes.Where(g => g.ParentId > 0 && g.LanguageCode.ToLower() == LanguageCode.ToLower()).ToList();

            foreach (var item in languageList)
            {
                list.Find(c => c.Id == item.ParentId).Name = item.Name;
            }

            // add to viewdata
            ViewData["TransactionTypes"] = new SelectList(list.OrderBy(g => g.Name), "Id", "Name");
        }

        public void PopulateTransactionTypesList(int selectedId)
        {
            // get language code
            LanguageCode = LanguageHelper.GetLanguageFromCookie(this.HttpContext);

            // get transactiontypes
            var list = this.Db.TransactionTypes.Where(g => g.ParentId == 0).ToList();

            // get translations
            var languageList = this.Db.TransactionTypes.Where(g => g.ParentId > 0 && g.LanguageCode.ToLower() == LanguageCode.ToLower()).ToList();

            // translate
            foreach (var item in languageList)
            {
                list.Find(c => c.Id == item.ParentId).Name = item.Name;
            }

            // create selectlist
            var exportList = new SelectList(list.OrderBy(g => g.Name), "Id", "Name", selectedId);

            // add to viewdata
            ViewData["TransactionTypes"] = exportList;
        }

        public void PopulateTransactionTypesList(bool goingIn)
        {
            // get language code
            LanguageCode = LanguageHelper.GetLanguageFromCookie(this.HttpContext);

            // get transactiontypes
            var list = goingIn ? 
                this.Db.TransactionTypes.Where(g => g.ParentId == 0 && g.Multiplier > 0).ToList() : 
                this.Db.TransactionTypes.Where(g => g.ParentId == 0 && g.Multiplier < 0).ToList();

            // get translations
            var languageList = this.Db.TransactionTypes.Where(g => g.ParentId > 0 && g.LanguageCode.ToLower() == LanguageCode.ToLower()).ToList();

            // translate
            foreach (var item in languageList)
            {
                var x = list.FirstOrDefault(c => c.Id == item.ParentId);
                if (x != null)
                {
                    x.Name = item.Name;
                }
            }

            // add to viewdata
            ViewData["TransactionTypes"] = new SelectList(list.OrderBy(g => g.Name), "Id", "Name");
        }

        public void PopulateYearList()
        {
            var list = new List<SelectListItem>();
            for (int i = 0; i < 100; i++)
            {
                var year = DateTime.Now.Year - i;
                var item = new SelectListItem
                           {
                               Selected = false, 
                               Text = year.ToString(CultureInfo.InvariantCulture), 
                               Value = year.ToString(CultureInfo.InvariantCulture)
                           };
                list.Add(item);
            }

            // add to viewdata
            ViewData["Years"] = new SelectList(list, "Value", "Text");
        }

        public void PopulateUserId()
        {
            // add to viewdata
            ViewData["UserId"] = this.GetUserId();
        }

        public void PopulateBottleTypeList()
        {
            var list = Db.BottleTypes.ToList();

            // add to viewdata
            ViewData["bottleTypes"] = new SelectList(list, "Id", "Name");
        }
    }
}
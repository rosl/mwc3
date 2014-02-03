

namespace MWC3.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    
    using MWC3.DAL;
    using MWC3.Models;
    using MWC3.Helpers;

    public class BaseController : Controller
    {
        public readonly ApplicationDbContext Db = new ApplicationDbContext();

        public string LanguageCode = string.Empty;

        public BaseController()
        {
        }

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
            if (User.Identity.IsAuthenticated)
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
            LanguageCode = LanguageHelper.GetLanguageCookie(this.HttpContext);
            var list = this.Db.Countries.Where(g => g.ParentId == 0).ToList();

            var languageList = this.Db.Countries.Where(g => g.ParentId > 0 && g.LanguageCode.ToLower() == LanguageCode.ToLower()).ToList();

            foreach (var country in languageList)
            {
                list.Find(c => c.Id == country.ParentId).Name = country.Name;
            }

            ViewData["Countries"] = new SelectList(list.OrderBy(g => g.Name), "Id", "Name");
        }

        public void PopulateParentCountryList()
        {
            LanguageCode = LanguageHelper.GetLanguageCookie(this.HttpContext);
            var list = this.Db.Countries.Where(g => g.ParentId == 0).ToList();

            var languageList =
                this.Db.Countries.Where(g => g.ParentId > 0 && g.LanguageCode.ToLower() == LanguageCode.ToLower()).ToList();

            foreach (var country in languageList)
            {
                list.Find(c => c.Id == country.ParentId).Name = country.Name;
            }

            // list.Insert(0, new Country { Id = 0, Name = "No parent country" });
            ViewData["ParentCountries"] = new SelectList(list.OrderBy(g => g.Name), "Id", "Name");
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
            LanguageCode = LanguageHelper.GetLanguageCookie(this.HttpContext);
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
            LanguageCode = LanguageHelper.GetLanguageCookie(this.HttpContext);
            var list = this.Db.TransactionTypes.Where(g => g.ParentId == 0).ToList();

            var languageList = this.Db.TransactionTypes.Where(g => g.ParentId > 0 && g.LanguageCode.ToLower() == LanguageCode.ToLower()).ToList();

            foreach (var item in languageList)
            {
                list.Find(c => c.Id == item.ParentId).Name = item.Name;
            }

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
            ViewData["Years"] = new SelectList(list, "Value", "Text");
        }

        public void PopulateUserId()
        {
            ViewData["UserId"] = this.GetUserId();
        }

        public void PopulateBottleTypeList()
        {
            var list = Db.BottleTypes.ToList();
            ViewData["bottleTypes"] = new SelectList(list, "Id", "Name");
        }
    }
}
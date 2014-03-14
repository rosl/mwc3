namespace MWC3.Controllers
{
    using System.Web.Mvc;

    public class LanguageController : BaseController
    {
        //
        // GET: /Language/
        public RedirectResult Index(string languageCode)
        {
            Helpers.LanguageHelper.SetLanguageToCookie(this.HttpContext);
            Helpers.LanguageHelper.SetCultureToCookie(this.HttpContext);
            Helpers.LanguageHelper.SetCurrentCulture(this.HttpContext);

            // Session["Culture"] = new CultureInfo(Helpers.LanguageHelper.GetCultureFromCookie(this.HttpContext));


            var urlReferrer = this.Request.UrlReferrer;

            if (urlReferrer != null)
            {
                return this.Redirect(urlReferrer.ToString());
            }

            return this.Redirect("/");
        }
    }
}
using System.Web.Mvc;

namespace MWC3.Controllers
{
    public class LanguageController : BaseController
    {
        //
        // GET: /Language/
        public RedirectResult Index(string languageCode)
        {
            Helpers.LanguageHelper.SetLanguageCookie(this.HttpContext);
            Helpers.LanguageHelper.SetCultureCookie(this.HttpContext);
            Helpers.LanguageHelper.SetCurrentCulture(this.HttpContext);
            var urlReferrer = this.Request.UrlReferrer;
            if (urlReferrer != null)
            {
                return this.Redirect(urlReferrer.ToString());
                
            }
            return this.Redirect("/");
        }
    }
}
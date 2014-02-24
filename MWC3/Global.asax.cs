using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MWC3
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Web;

    using MWC3.Helpers;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            LanguageHelper.SetCurrentCulture(new HttpContextWrapper(HttpContext.Current));

            if (HttpContext.Current.Session != null)
            {
                CultureInfo cultureInfo = (CultureInfo)this.Session["Culture"];
                if (cultureInfo == null)
                {
                    string langName = "en";

                    if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
                        langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);

                    cultureInfo = new CultureInfo(langName);
                    this.Session["Culture"] = cultureInfo;
                }

                //Finally setting culture for each request
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
            }
        }

    }
}

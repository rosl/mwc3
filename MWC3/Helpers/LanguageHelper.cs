using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MWC3.Helpers
{
    using System.Globalization;
    using System.Threading;

    using Microsoft.Ajax.Utilities;

    public static class LanguageHelper
    {
        public static List<string> LanguageCodeList = new List<string> { "en", "nl" };
        public static List<string> CultureCodeList = new List<string> { "en-US", "nl-NL" };

        public static string DefaultLanguageCode = LanguageCodeList[0];
        public static string DefaultCultureCode = CultureCodeList[0];

        const string CultureCookieName = "_cc";
        const string LanguageCookieName = "_lc";

        public static void SetLanguageCookie(HttpContextBase context)
        {
            var selectedLanguageCode = context.Request.QueryString["lc"];
            var cookie = context.Request.Cookies[LanguageCookieName];

            // get language
            string languageCode = DefaultLanguageCode;
            if (selectedLanguageCode != null)
            {
                if (LanguageCodeList.Any(l => l.ToLower() == languageCode.ToLower()))
                {
                    languageCode = selectedLanguageCode;   // update cookie value
                }
            }

            // set cookie
            if (cookie != null)
            {
                cookie.Value = languageCode;
            }
            else
            {
                cookie = new HttpCookie(LanguageCookieName)
                         {
                             Value = languageCode,
                             Expires = DateTime.Now.AddYears(1)
                         };
            }

            context.Response.Cookies.Add(cookie);
        }


        public static string GetLanguageCookie(HttpContextBase context)
        {
            var currentLanguageCode = string.Empty;

            // get language code from cookie
            if (context != null)
            {
                var cookie = context.Request.Cookies[LanguageCookieName];
                if (cookie != null)
                {
                    currentLanguageCode = cookie.Value;
                }

                // check if language code is valid
                if (LanguageCodeList.Any(l => l.ToLower() == currentLanguageCode.ToLower()))
                {
                    return currentLanguageCode;
                }
            }

            // return default language code
            return DefaultLanguageCode;
        }

        public static void SetCultureCookie(HttpContextBase context)
        {
            var selectedCode = context.Request.QueryString["cc"];
            var cookie = context.Request.Cookies[CultureCookieName];

            // get language
            string cultureCode = DefaultCultureCode;
            if (selectedCode != null)
            {
                if (CultureCodeList.Any(l => l.ToLower() == cultureCode.ToLower()))
                {
                    cultureCode = selectedCode;   // update cookie value
                }
            }

            // set cookie
            if (cookie != null)
            {
                cookie.Value = cultureCode;
            }
            else
            {
                cookie = new HttpCookie(CultureCookieName)
                {
                    Value = cultureCode,
                    Expires = DateTime.Now.AddYears(1)
                };
            }

            context.Response.Cookies.Add(cookie);

            var ci = CultureInfo.GetCultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        public static void SetCurrentCulture(HttpContextBase context)
        {
            var cultureCode = GetCultureCookie(context);
            var ci = CultureInfo.GetCultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        public static string GetCultureCookie(HttpContextBase context)
        {
            // const string CookieName = "_cc";
            var currentCode = string.Empty;

            // get language code from cookie
            if (context != null)
            {
                var cookie = context.Request.Cookies[CultureCookieName];
                if (cookie != null)
                {
                    currentCode = cookie.Value;
                }

                // check if language code is valid
                if (CultureCodeList.Any(l => l.ToLower() == currentCode.ToLower()))
                {
                    return currentCode;
                }
            }

            // return default language code
            return DefaultCultureCode;
        }
    }
}
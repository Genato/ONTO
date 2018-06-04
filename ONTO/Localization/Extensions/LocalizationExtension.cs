using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ONTO.Localization.Extensions
{
    /// <summary>
    /// Set localization.
    /// </summary>
    public class LocalizationExtension
    {
        /// <summary>
        /// Apply locale to current thread for current user
        /// </summary>
        /// <param name="lang">locale name</param>
        public static void SetLang()
        {
            // Get locale from route values
            string lang = (string)HttpContext.Current.Request.RequestContext.RouteData.Values["lang"];

            // If we haven't found appropriate culture - seet default locale then
            if (lang == null || lang.Contains("lang") == false)
                lang = ConfigurationManager.AppSettings["DEFAULT_LANGUAGE"];

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
        }
    }
}
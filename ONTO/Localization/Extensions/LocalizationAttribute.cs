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
    /// Set localiztion via custom "ActionFilterAttribute", filter is applied globally in "FilterConfig.cs"
    /// </summary>
    public class LocalizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Get locale from route values
            string lang = (string)filterContext.RouteData.Values["lang"];

            // If we haven't found appropriate culture - seet default locale then
            if (lang.Contains("lang"))
                lang = DefaultLang;

            SetLang(lang);
        }

        /// <summary>
        /// Apply locale to current thread
        /// </summary>
        /// <param name="lang">locale name</param>
        private void SetLang(string lang)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
        }

        private string DefaultLang { get { return ConfigurationManager.AppSettings["DEFAULT_LANGUAGE"]; } }
    }
}
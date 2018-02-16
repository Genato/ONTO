using ONTO.Localization.Extensions;
using System.Web;
using System.Web.Mvc;

namespace ONTO
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());

            //Custom filters
            filters.Add(new LocalizationAttribute());
        }
    }
}

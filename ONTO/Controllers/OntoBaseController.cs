using ONTO.Localization.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ONTO.Controllers
{
    /// <summary>
    /// Base conntroller for ONTO application that inherites standard MVC Controller. Here we handles things to avoid DRY in every controller (e.g. exception handling, localization, etc...)
    /// </summary>
    public class OntoBaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            //TODO
            //Replace identity messages with localized messages

            //Set locale for current user
            LocalizationExtension.SetLang();

            return base.BeginExecuteCore(callback, state);
        }

        /// <summary>
        /// Exception handling.
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }
    }
}
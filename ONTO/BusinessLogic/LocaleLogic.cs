using ONTO.DAL;
using ONTO.Models.ONTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONTO.BusinessLogic
{
    public class LocaleLogic
    {
        public LocaleLogic(LocaleDAL localeDAL)
        {
            _localeDAL = localeDAL;
        }

        /// <summary>
        /// Set localization for current user, send userID of currently logged in user.
        /// </summary>
        /// <param name="userID">/param>
        public void SetLocalizationForCurrentUser(string userID)
        {
            HttpContext.Current.Request.RequestContext.RouteData.Values["lang"] = _localeDAL.GetLoacleByUserID(userID)._Localization;
        }

        /// <summary>
        /// Get all localization from DB as a List
        /// </summary>
        /// <returns></returns>
        public List<Locale> GetLocalizations() => _localeDAL.GetAll<Locale>();

        //Private members

        private LocaleDAL _localeDAL { get; set; }
    }
}
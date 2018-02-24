using ONTO.DAL;
using ONTO.Models.ONTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONTO.BusinessLogic
{
    public class LocaleLogic : OntoLogic
    {
        public LocaleLogic(LocaleDAL localeDAL)
        {
            _localeDAL = localeDAL;
        }

        /// <summary>
        /// Set localization for current user.
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

        // Overriden methods //

        /// <summary>
        /// Create new Locale (Adds new language to application).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override int CreateEntity<T>(T entity)
        {
            Locale userSettings = (Locale)Convert.ChangeType(entity, typeof(Locale));

            _localeDAL.CreateEntity(userSettings);

            return _localeDAL.UpdateDatabase();
        }

        /// <summary>
        /// Save Locale changes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override int SaveEntity<T>(T entity)
        {
            Locale tmpLocale = (Locale)Convert.ChangeType(entity, typeof(Locale));

            Locale newLocale = _localeDAL.GetByID<Locale>(tmpLocale.ID);
            newLocale._Localization = tmpLocale._Localization;

            return _localeDAL.UpdateDatabase();
        }

        //Private members

        private LocaleDAL _localeDAL { get; set; }
    }
}
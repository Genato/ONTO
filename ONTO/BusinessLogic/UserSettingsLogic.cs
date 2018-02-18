using ONTO.DAL;
using ONTO.DbContexts;
using ONTO.Models.ONTOModels;
using ONTO.ViewModels.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONTO.BusinessLogic
{
    public class UserSettingsLogic : OntoLogic
    {
        public UserSettingsLogic(UserSettingsDAL userSettingsDAL)
        {
            _userSettingsDAL = userSettingsDAL;
        }

        /// <summary>
        /// Get UserSettings by userID <para/>
        /// NOTE: userID is ID of IdentityUser from {identity}.{Users} table.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserSettings GetByUserID(string userID)
        {
            return _userSettingsDAL.GetByUserID(userID);
        }

        /// <summary>
        /// Save UserProfileSettings for current user by UserID (Not by UserProfileSettings.ID !) and returns number of rows afected (Number of rows should be 1 as we update settings for current user).
        /// </summary>
        /// <param name="userSettings"></param>
        /// <returns></returns>
        public int SaveUserSettings(UserSettings userSettings)
        {
            UserSettings _userSettings = _userSettingsDAL.GetByUserID(userSettings.UserID);
            _userSettings.LocalizationID = userSettings.LocalizationID;

            return _userSettingsDAL.UpdateDatabase();
        }

        // Overriden methods //

        /// <summary>
        /// Create UserSettings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override int CreateEntity<T>(T entity)
        {
            UserSettings userSettings = (UserSettings)Convert.ChangeType(entity, typeof(UserSettings));

            _userSettingsDAL.CreateEntity(userSettings);

            return _userSettingsDAL.UpdateDatabase();
        }

        /// <summary>
        /// Save UserSettings changes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override int SaveEntity<T>(T entity)
        {
            UserSettings tmpUserSettings = (UserSettings)Convert.ChangeType(entity, typeof(UserSettings));

            UserSettings newuserSettings = _userSettingsDAL.GetByUserID(tmpUserSettings.UserID);
            newuserSettings.LocalizationID = tmpUserSettings.LocalizationID;

            return _userSettingsDAL.UpdateDatabase();
        }

        //Private members
        private UserSettingsDAL _userSettingsDAL { get; set; }
    }
}
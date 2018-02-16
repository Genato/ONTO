using ONTO.DAL;
using ONTO.DbContexts;
using ONTO.Models.ONTOModels;
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

        //Private members
        private UserSettingsDAL _userSettingsDAL { get; set; }
    }
}
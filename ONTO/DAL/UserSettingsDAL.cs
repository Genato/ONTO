using ONTO.DbContexts;
using ONTO.Models.ONTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONTO.DAL
{
    /// <summary>
    /// Class that handles CRUD operations on UserSettings entity in database
    /// </summary>
    public class UserSettingsDAL : OntoDAL
    {
        public UserSettingsDAL(OntoDbContext ontoDbContext) : base(ontoDbContext) { }

        /// <summary>
        /// Get UserSettings by userID.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserSettings GetByUserID(string userID)
        {
            _UserSettings = (from userSettings in _ontoDbContext.UserSettings
                                where userSettings.UserID == userID
                                select userSettings).First();

            return _UserSettings;
        }

        public void CreateUserSettings(UserSettings userSettings)
        {
            _ontoDbContext.UserSettings.Add(userSettings);
        }

        //////////////////////
        // Overriden members//
        //////////////////////

        /// <summary>
        /// Get UserSettings by ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public override T GetByID<T>(int id)
        {
            UserSettings userSettings = _ontoDbContext.UserSettings.Find(id);

            return (T)Convert.ChangeType(userSettings, typeof(UserSettings));
        }

        /// <summary>
        /// Add UserSettings to DbSet. (Call DbSet.SaveChanges() to insert it into database)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public override void CreateEntity<T>(T entity)
        {
            _ontoDbContext.UserSettings.Add((UserSettings)Convert.ChangeType(entity, typeof(UserSettings)));
        }

        //Private members
        public UserSettings _UserSettings { get; set; }
    }
}
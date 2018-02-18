using ONTO.DbContexts;
using ONTO.Models.ONTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONTO.DAL
{
    /// <summary>
    /// Class that handles CRUD operations on Localization entity in database
    /// </summary>
    public class LocaleDAL : OntoDAL
    {
        public LocaleDAL(OntoDbContext ontoDbContext) : base(ontoDbContext) { }

        public Locale GetLoacleByUserID(string userID)
        {
            var localization = from locale in _ontoDbContext.Localization
                               join userSettings in _ontoDbContext.UserSettings on locale.ID equals userSettings.LocalizationID
                               where userSettings.UserID == userID
                               select locale;

            return localization.First();
        }

        //////////////////////
        // Overriden members//
        //////////////////////

        /// <summary>
        /// Get Locale by ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public override T GetByID<T>(int id)
        {
            Locale userSettings = _ontoDbContext.Localization.Find(id);

            return (T)Convert.ChangeType(userSettings, typeof(Locale));
        }

        /// <summary>
        /// Add Locale to DbSet. (Call DbSet.SaveChanges() to insert it into database)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public override void CreateEntity<T>(T entity)
        {
            _ontoDbContext.Localization.Add((Locale)Convert.ChangeType(entity, typeof(Locale)));
        }
    }
}
﻿using ONTO.DbContexts;
using ONTO.Models.ONTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONTO.DAL
{
    public abstract class OntoDAL
    {
        protected OntoDAL(OntoDbContext ontoDbContext)
        {
            _ontoDbContext = ontoDbContext;
        }

        protected OntoDbContext _ontoDbContext { get; set; }

        ////////////////////
        // Public members //
        ////////////////////

        public abstract T GetByID<T>(int id) where T : class;

        //Implemented virtual methods (override if you wish).
        //Some of the DB entities don't have some of the columns, 
        //that's why some methods are virtual so that classes that inherite from OntoDAL doesn't ahave to implement every method.
        
        public virtual T GetByName<T>(string name) where T : class { return null; }

        public virtual List<T> GetAll<T>() where T : class => _ontoDbContext.Set<T>().ToList();

        /// <summary>
        /// Method just call DbContext.SaveChanges()
        /// </summary>
        /// <returns></returns>
        public virtual int UpdateDatabase() => _ontoDbContext.SaveChanges();

        /// <summary>
        /// Method just call DbContext.SaveChangesAsync()
        /// </summary>
        /// <returns></returns>
        public virtual Task<int> UpdateDatabaseAsync() => _ontoDbContext.SaveChangesAsync();
    }
}
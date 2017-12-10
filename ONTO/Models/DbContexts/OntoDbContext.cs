﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ONTO.Models.ONTOModels
{
    public class KatalogKljučnihBrojevaOtpadaDBContext : DbContext
    {
        public KatalogKljučnihBrojevaOtpadaDBContext()
            : base("PostgreSQLCnnection")
        {
        }

        public DbSet<KatalogKljučnihBrojevaOtpada> KatalogKljučnihBrojevaOtpada { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("onto");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KatalogKljučnihBrojevaOtpada>().ToTable("Katalog_Kljucnih_Brojeva_Otpada");
        }
    }
}
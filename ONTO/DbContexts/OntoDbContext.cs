using ONTO.Models;
using ONTO.Models.ONTOModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ONTO.DbContexts
{
    public class OntoDbContext : DbContext
    {
        public OntoDbContext()
            : base("PostgreONTOConnection")
        {
        }

        public DbSet<KatalogKljučnihBrojevaOtpada> KatalogKljučnihBrojevaOtpada { get; set; }
        public DbSet<PrateciList> PrateciList { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<Locale> Localization { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("onto");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KatalogKljučnihBrojevaOtpada>().ToTable("Katalog_Kljucnih_Brojeva_Otpada");
            modelBuilder.Entity<PrateciList>().ToTable("Prateci_List");
            modelBuilder.Entity<UserSettings>().ToTable("User_Settings");
            modelBuilder.Entity<Locale>().ToTable("Localization").Property(p => p._Localization).HasColumnName("Localization");
        }
    }
}
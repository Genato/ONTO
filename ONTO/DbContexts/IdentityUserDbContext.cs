using Microsoft.AspNet.Identity.EntityFramework;
using ONTO.Models;
using ONTO.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ONTO.DbContexts
{
    public class IdentityUserDbContext : IdentityDbContext<OntoIdentityUser>
    {
        public IdentityUserDbContext()
            : base("PostgreONTOIdentityConnection", throwIfV1Schema: false)
        {
        }

        public static IdentityUserDbContext Create()
        {
            return new IdentityUserDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("identity");

            base.OnModelCreating(modelBuilder);

            //Users
            modelBuilder.Entity<OntoIdentityUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");

            //Roles
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
        }
    }
}
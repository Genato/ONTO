using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ONTO.DbContexts;
using ONTO.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONTO.Identity
{
    public class OntoIdentityRoleManager : RoleManager<OntoIdentityRole>
    {
        public OntoIdentityRoleManager(IRoleStore<OntoIdentityRole, string> roleStore) : base(roleStore) { }

        public static OntoIdentityRoleManager Create(IdentityFactoryOptions<OntoIdentityRoleManager> options, IOwinContext context)
        {
            OntoIdentityRoleManager ontoIdentityRoleManager = new OntoIdentityRoleManager(new RoleStore<OntoIdentityRole>(context.Get<IdentityUserDbContext>()));

            return ontoIdentityRoleManager;
        }
    }
}
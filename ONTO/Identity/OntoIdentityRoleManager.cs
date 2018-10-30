using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ONTO.DbContexts;
using ONTO.Identity.Extensions;
using ONTO.Models.IdentityModels;
using ONTO.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ONTO.Identity
{
    public class OntoIdentityRoleManager : RoleManager<OntoIdentityRole>
    {
        public OntoIdentityRoleManager(IRoleStore<OntoIdentityRole, string> roleStore) : base(roleStore) { }

        public async Task<bool> DeleteRoleAsync(string roleName)
        {
            OntoIdentityRole role = await this.FindByNameAsync(roleName);

            return DeleteAsync(role).Result.Succeeded ? true : false;
        }

        public async Task<bool> EditRoleAsync(EditRoleViewModel editRoleViewModel)
        {
            OntoIdentityRole oldRole = await this.FindByIdAsync(editRoleViewModel.RoleID);

            oldRole.Name = editRoleViewModel.RoleName;

            return UpdateAsync(oldRole).Result.Succeeded ? true : false;
        }

        public static OntoIdentityRoleManager Create(IdentityFactoryOptions<OntoIdentityRoleManager> options, IOwinContext context)
        {
            OntoIdentityRoleManager ontoIdentityRoleManager = new OntoIdentityRoleManager(new RoleStore<OntoIdentityRole>(context.Get<IdentityUserDbContext>()));

            ontoIdentityRoleManager.RoleValidator = new CustomRoleValidator(ontoIdentityRoleManager);

            return ontoIdentityRoleManager;
        }
    }
}
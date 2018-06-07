using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ONTO.Models;
using ONTO.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ONTO.Models.OntoIdentityUser;
using ONTO.ViewModels.AccountViewModels;
using System.Threading.Tasks;
using ONTO.Identity.Extensions;

namespace ONTO.Identity
{
    /// <summary>
    /// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    /// </summary>
    public class OntoIdentityUserManager : UserManager<OntoIdentityUser>
    {
        public OntoIdentityUserManager(IUserStore<OntoIdentityUser> store) : base(store) { }

        /// <summary>
        /// We override this function because we implemeted custom validator "CustomUserValidator" and when we try to add user to role it validate email and says that user email already exists.
        /// So we need to intercept and validate email before it goes to custom validator.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public override Task<IdentityResult> AddToRoleAsync(string userId, string role)
        {
            string emailToken = this.GenerateEmailConfirmationToken(userId);

            this.ConfirmEmail(userId, emailToken);

            return base.AddToRoleAsync(userId, role);
        }

        //public override Task<IdentityResult> RemoveFromRoleAsync(string userId, string role)
        //{
        //    string emailToken = this.GenerateEmailConfirmationToken(userId);

        //    this.ConfirmEmail(userId, emailToken);

        //    return base.RemoveFromRoleAsync(userId, role);  
        //}

        /// <summary>
        /// Method updates all user properties (It doesn't update password !)
        /// </summary>
        /// <param name="profileViewModel"></param>
        public async Task<IdentityResult> UpdateUser(IdentityProfileViewModel profileViewModel)
        {
            OntoIdentityUser ontoIdentityUser = this.FindById(HttpContext.Current.User.Identity.GetUserId());
            ontoIdentityUser.Email = profileViewModel.NewEmail == null ? profileViewModel.CurrentEmail : profileViewModel.NewEmail;
            ontoIdentityUser.UserName = profileViewModel.NewEmail == null ? profileViewModel.CurrentEmail : profileViewModel.NewEmail;

            IdentityResult result = await this.UpdateAsync(ontoIdentityUser);

            return result;
        }

        /// <summary>
        /// Method only updates user password
        /// </summary>
        /// <param name="changePasswordViewModel"></param>
        public async Task<IdentityResult> UpdateUserPassword(ChangePasswordViewModel changePasswordViewModel)
        {            
            return await this.ChangePasswordAsync(HttpContext.Current.User.Identity.GetUserId(), changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);
        }

        public static OntoIdentityUserManager Create(IdentityFactoryOptions<OntoIdentityUserManager> options, IOwinContext context)
        {
            var manager = new OntoIdentityUserManager(new UserStore<OntoIdentityUser>(context.Get<IdentityUserDbContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new CustomUserValidator(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            
            // Configure validation logic for passwords
            manager.PasswordValidator = new CustomPasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = false
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            var dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<OntoIdentityUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }
}
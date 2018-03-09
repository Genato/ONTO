using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using ONTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ONTO.Identity
{
    // Configure the application sign-in manager which is used in this application.
    public class OntoIdentitySignInManager : SignInManager<OntoIdentityUser, string>
    {
        public OntoIdentitySignInManager(OntoIdentityUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(OntoIdentityUser user)
        {
            return user.GenerateUserIdentityAsync((OntoIdentityUserManager)UserManager);
        }

        public static OntoIdentitySignInManager Create(IdentityFactoryOptions<OntoIdentitySignInManager> options, IOwinContext context)
        {
            return new OntoIdentitySignInManager(context.GetUserManager<OntoIdentityUserManager>(), context.Authentication);
        }
    }
}
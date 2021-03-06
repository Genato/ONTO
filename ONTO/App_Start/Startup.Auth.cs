﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using ONTO.Identity;
using ONTO.Models;
using ONTO.DbContexts;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ONTO.Models.IdentityModels;

namespace ONTO
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(IdentityUserDbContext.Create);
            app.CreatePerOwinContext<OntoIdentityUserManager>(OntoIdentityUserManager.Create);
            app.CreatePerOwinContext<OntoIdentitySignInManager>(OntoIdentitySignInManager.Create);
            app.CreatePerOwinContext<OntoIdentityRoleManager>(OntoIdentityRoleManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/{lang}/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<OntoIdentityUserManager, OntoIdentityUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
        }
    }
}
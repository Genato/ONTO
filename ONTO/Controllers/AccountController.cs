using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ONTO.Identity;
using ONTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ONTO.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //// GET: User
        //public new ActionResult Profile()
        //{
        //    return View();
        //}

        // POST: /Account/Register
        //[HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public new async Task<ActionResult> Profile()
        {
            //if (ModelState.IsValid)
            //{
                var user = new ApplicationUser { UserName = "druidhr@gmail.com", Email = "druidhr@gmail.com" };
                var result = await UserManager.CreateAsync(user, "genato1510");
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                //AddErrors(result);
            //}

            // If we got this far, something failed, redisplay form
            return View("ERROR");
        }

    }
}
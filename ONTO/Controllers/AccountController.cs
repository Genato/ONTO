using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ONTO.BusinessLogic;
using ONTO.DbContexts;
using ONTO.Identity;
using ONTO.Models;
using ONTO.Models.ONTOModels;
using ONTO.ViewModels.AccountViewModels;
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
        public AccountController(ApplicationSignInManager signInManager, ApplicationUserManager userManager, IAuthenticationManager authenticationManager, UserSettingsLogic userSettingsLogic, LocaleLogic localeLogic)
        {
            _SignInManager = signInManager;
            _UserManager = userManager;
            _AuthenticationManager = authenticationManager;
            _UserSettingsLogic = userSettingsLogic;
            _LocaleLogic = localeLogic;
        }

        // GET: User
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                var _user = new OntoIdentityUser { UserName = user.Email, Email = user.Email };
                var result = await _UserManager.CreateAsync(_user, user.Password);

                if (result.Succeeded)
                {
                    await _SignInManager.SignInAsync(_user, isPersistent: false, rememberBrowser: false);

                    OntoIdentityUser ontoUser = _UserManager.Find(user.Email, user.Password);
                    _LocaleLogic.SetLocalizationForCurrentUser(ontoUser.Id);

                    return RedirectToAction("Index", "Home");
                }

                _UserSettingsLogic.AddErrors(ModelState, result);
            }

            // If we got this far, something failed, redisplay form
            return View("ERROR", ModelState);
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var result = await _SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            if(result == SignInStatus.Failure)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            // Successfully logged in
            OntoIdentityUser ontoUser = _UserManager.Find(model.Email, model.Password);
            _LocaleLogic.SetLocalizationForCurrentUser(ontoUser.Id);

            return RedirectToAction("Index", "Home");
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public new ActionResult Profile()
        {
            ProfileViewModel profileViewModel = new ProfileViewModel()
            {
                Localization = _LocaleLogic.GetLocalizations()
            };

            return View(profileViewModel);
        }

        [HttpPost]
        public new ActionResult Profile(ProfileViewModel profileViewModel)
        {
            //TODO
            //Save selected localization to DB to table UserSettings
            UserSettings userSettings = new UserSettings()
            {
                LocalizationID = profileViewModel.SelectedLocale,
                UserID = User.Identity.GetUserId()
            };

            _UserSettingsLogic.SaveUserSettings(userSettings);

            profileViewModel.Localization = _LocaleLogic.GetLocalizations();

            return View(profileViewModel);
        }

        ///Private members section
        private ApplicationSignInManager _SignInManager { get; set; }
        private ApplicationUserManager _UserManager { get; set; }
        private IAuthenticationManager _AuthenticationManager { get; set; }
        private UserSettingsLogic _UserSettingsLogic { get; set; }
        private LocaleLogic _LocaleLogic { get; set; }

        //Overriden methods
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }
    }
}
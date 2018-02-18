using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ONTO.BusinessLogic;
using ONTO.DbContexts;
using ONTO.Identity;
using ONTO.Localization;
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
            RegisterViewModel registerViewModel = new RegisterViewModel()
            {
                Localization = _LocaleLogic.GetLocalizations()
            };

            return View(registerViewModel);
        }

        /// <summary>
        /// POST: /Account/Register <para/>
        /// Action register new user to {schema}.{table} => {identity}.{Users} and UserSettings to {onto}.{User_Settings}. <para/>
        /// It also sets localization for currently created and loged in user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

                    //Create UserSettings and set localization for him
                    OntoIdentityUser ontoUser = _UserManager.Find(user.Email, user.Password);

                    UserSettings userSettings = new UserSettings()
                    {
                        UserID = ontoUser.Id,
                        LocalizationID = user.SelectedLocale
                    };

                    _UserSettingsLogic.CreateEntity(userSettings);
                    _LocaleLogic.SetLocalizationForCurrentUser(ontoUser.Id);

                    return RedirectToAction("Index", "Home");
                }

                _UserSettingsLogic.AddErrors(ModelState, result);
            }

            // If we got this far, something failed, redisplay form
            user.Localization = _LocaleLogic.GetLocalizations();

            return View(user);
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// POST: /Account/Login <para/>
        /// Action login user and sets localization for it.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            
            var result = await _SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            if(result == SignInStatus.Failure)
            {
                ModelState.AddModelError("", ErrorMsg.InvaliLoginAttempt);
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
            OntoIdentityUser ontoUser = _UserManager.FindById(User.Identity.GetUserId());

            ProfileViewModel profileViewModel = new ProfileViewModel()
            {
                Localization = _LocaleLogic.GetLocalizations(),
                CurrentEmail = ontoUser.Email,
                SelectedLocale = _UserSettingsLogic.GetByUserID(ontoUser.Id).LocalizationID
            };

            return View(profileViewModel);
        }

        /// <summary>
        /// Save UserSettings changes to {onto}.{User_Settings} table and OntoIdenityUser changes to {identity}.{Users} table.
        /// </summary>
        /// <param name="profileViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async new Task<ActionResult> Profile(ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid == false)
            {
                profileViewModel.Localization = _LocaleLogic.GetLocalizations();
                return View(profileViewModel);
            }

            IdentityResult identityResult = await _UserManager.UpdateUser(profileViewModel);

            if (identityResult.Succeeded == false)
                _UserSettingsLogic.AddErrors(ModelState, identityResult);

            UserSettings userSettings = new UserSettings()
            {
                LocalizationID = profileViewModel.SelectedLocale,
                UserID = User.Identity.GetUserId()
            };

            _UserSettingsLogic.SaveUserSettings(userSettings);
            _LocaleLogic.SetLocalizationForCurrentUser(User.Identity.GetUserId());

            profileViewModel.Localization = _LocaleLogic.GetLocalizations();

            return RedirectToAction("Profile");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if(ModelState.IsValid == false)
                return View("Error", ModelState);

            IdentityResult identityResult = await _UserManager.UpdateUserPassword(changePasswordViewModel);

            if (identityResult.Succeeded == false)
                _UserSettingsLogic.AddErrors(ModelState, identityResult);

            return View();
        }

        ///Private members section
        private ApplicationSignInManager _SignInManager { get; set; }
        private ApplicationUserManager _UserManager { get; set; }
        private IAuthenticationManager _AuthenticationManager { get; set; }
        private UserSettingsLogic _UserSettingsLogic { get; set; }
        private LocaleLogic _LocaleLogic { get; set; }

        //Overriden methods

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ModelStateDictionary modelstate = ModelState;

            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// Exception handling.
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }
    }
}
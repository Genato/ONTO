using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ONTO.BusinessLogic;
using ONTO.Identity;
using ONTO.Localization;
using ONTO.Models;
using ONTO.Models.ONTOModels;
using ONTO.ViewModels.AccountViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ONTO.Controllers
{
    public class AccountController : OntoBaseController
    {
        public AccountController(OntoIdentitySignInManager signInManager, OntoIdentityUserManager userManager, IAuthenticationManager authenticationManager, UserSettingsLogic userSettingsLogic, LocaleLogic localeLogic)
        {
            _SignInManager = signInManager;
            _UserManager = userManager;
            _AuthenticationManager = authenticationManager;
            _UserSettingsLogic = userSettingsLogic;
            _LocaleLogic = localeLogic;
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
                return View(model);
            
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
        public ActionResult IdentityProfileSettings()
        {
            OntoIdentityUser ontoUser = _UserManager.FindById(User.Identity.GetUserId());

            IdentityProfileViewModel profileViewModel = new IdentityProfileViewModel()
            {
                CurrentEmail = ontoUser.Email,
            };

            return View(profileViewModel);
        }

        /// <summary>
        /// Save OntoIdenityUser changes to {identity}.{Users} table.
        /// </summary>
        /// <param name="profileViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IdentityProfileSettings(IdentityProfileViewModel identityProfileViewModel)
        {
            if (ModelState.IsValid == false)
                return View(identityProfileViewModel);

            IdentityResult identityResult = await _UserManager.UpdateUser(identityProfileViewModel);

            if (identityResult.Succeeded == false)
                _UserSettingsLogic.AddErrors(ModelState, identityResult);
            
            return RedirectToAction("IdentityProfileSettings");
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
                return View();

            IdentityResult identityResult = await _UserManager.UpdateUserPassword(changePasswordViewModel);

            if (identityResult.Succeeded == false)
                _UserSettingsLogic.AddErrors(ModelState, identityResult);

            return View();
        }

        [HttpGet]
        public ActionResult UserAppSettings()
        {
            OntoIdentityUser ontoUser = _UserManager.FindById(User.Identity.GetUserId());

            UserAppSettingsViewModel userAppSettings = new UserAppSettingsViewModel()
            {
                Localization = _LocaleLogic.GetLocalizations(),
                SelectedLocale = _UserSettingsLogic.GetByUserID(ontoUser.Id).LocalizationID
            };

            return View(userAppSettings);
        }

        /// <summary>
        /// Save UserSettings changes to {onto}.{User_Settings} table
        /// </summary>
        /// <param name="userSettings"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserAppSettings(UserAppSettingsViewModel userAppSettingsViewModel)
        {
            if (ModelState.IsValid == false)
            {
                userAppSettingsViewModel.Localization = _LocaleLogic.GetLocalizations();
                return View(userAppSettingsViewModel);
            }

            UserSettings userSettings = new UserSettings()
            {
                LocalizationID = userAppSettingsViewModel.SelectedLocale,
                UserID = User.Identity.GetUserId()
            };

            //Save User app settings and set localization for current user
            _UserSettingsLogic.SaveUserSettings(userSettings);
            _LocaleLogic.SetLocalizationForCurrentUser(User.Identity.GetUserId());

            return RedirectToAction("UserAppSettings");
        }

        ///Private members section
        private OntoIdentitySignInManager _SignInManager { get; set; }
        private OntoIdentityUserManager _UserManager { get; set; }
        private IAuthenticationManager _AuthenticationManager { get; set; }
        private UserSettingsLogic _UserSettingsLogic { get; set; }
        private LocaleLogic _LocaleLogic { get; set; }
    }
}
using Microsoft.AspNet.Identity;
using ONTO.BusinessLogic;
using ONTO.Identity;
using ONTO.Models;
using ONTO.Models.IdentityModels;
using ONTO.Models.ONTOModels;
using ONTO.ViewModels.AccountViewModels;
using ONTO.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ONTO.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : OntoBaseController
    {
        public AdminController(OntoIdentityRoleManager roleManager, LocaleLogic localeLogic, OntoIdentityUserManager userManager, UserSettingsLogic userSettingsLogic)
        {
            _UserSettingsLogic = userSettingsLogic;
            _UserManager = userManager;
            _LocaleLogic = localeLogic;
            _RoleManager = roleManager;
        }

        [HttpGet]
        public ActionResult AdminSettings()
        {
            return View();
        }

        [HttpGet]
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
        public async Task<ActionResult> Register(RegisterViewModel user)
        {
            user.Localization = _LocaleLogic.GetLocalizations();

            if (ModelState.IsValid == false)
                return View(user);

            OntoIdentityUser _user = new OntoIdentityUser { UserName = user.Email, Email = user.Email };
            var result = await _UserManager.CreateAsync(_user, user.Password);

            if (result.Succeeded == false)
            {
                _UserSettingsLogic.AddErrors(ModelState, result);
                return View(user);
            }

            //Create UserSettings and set localization for currently created user
            OntoIdentityUser ontoUser = _UserManager.Find(user.Email, user.Password);

            UserSettings userSettings = new UserSettings()
            {
                UserID = ontoUser.Id,
                LocalizationID = user.SelectedLocale
            };

            _UserSettingsLogic.CreateEntity(userSettings);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            OntoIdentityRole role = new OntoIdentityRole(createRoleViewModel.RoleName);

            var result = await _RoleManager.CreateAsync(role);

            if (result.Succeeded == false)
            {
                _UserSettingsLogic.AddErrors(ModelState, result);
                return View();
            }

            return View();
        }

        [HttpGet]
        public ActionResult ListRoles()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ManageRoles()
        {
            return View();
        }

        private UserSettingsLogic _UserSettingsLogic { get; set; }
        private OntoIdentityRoleManager _RoleManager { get; set; }
        private OntoIdentityUserManager _UserManager { get; set; }
        private LocaleLogic _LocaleLogic { get; set; }
    }
}
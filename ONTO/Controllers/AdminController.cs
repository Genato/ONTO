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
using PagedList;
using ONTO.Localization;

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

        /// <summary>
        /// This function/action
        /// </summary>
        /// <param name="createRoleViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult ManageRoles(int pageNumber = 1, int pageSize = 5)
        {
            ManageRolesViewModel manageRolesViewModel = new ManageRolesViewModel()
            {
                PagedListOfRoles = _RoleManager.Roles.OrderBy(x => x.Name).ToPagedList(pageNumber, pageSize)
            };

            return View(manageRolesViewModel);
        }

        /// <summary>
        /// This action delete role.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<JsonResult> DeleteRole(string roleName)
        {
            bool result = await _RoleManager.DeleteRoleAsync(roleName);

            string resultMessage = result ? SuccessMsg.RoleDeletedSuccesfully : ErrorMsg.RoleDeletedError;

            return Json(resultMessage);
        }

        [HttpGet]
        public async Task<ActionResult> EditRole(string roleName)
        {
            OntoIdentityRole ontoIdentityRole = await _RoleManager.FindByNameAsync(roleName);

            EditRoleViewModel editRoleViewModel = new EditRoleViewModel()
            {
                RoleID = ontoIdentityRole.Id,
                RoleName = ontoIdentityRole.Name
            };

            return View(editRoleViewModel);
        }

        /// <summary>
        /// This action edit role name.
        /// </summary>
        /// <param name="ontoIdentityRole"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<JsonResult> EditRole(EditRoleViewModel editRoleViewModel)
        {
            bool result = await _RoleManager.EditRoleAsync(editRoleViewModel);

            string resultMessage = result ? SuccessMsg.RoleEditSuccesfully : ErrorMsg.RoleEditError;

            return Json(resultMessage);
        }

        [HttpGet]
        public ActionResult ManageUserRole(string roleName)
        {
            return View(new ManageUserRolesViewModel() { RoleName = roleName });
        }

        [HttpPost]
        public async Task<JsonResult> AddUserToRole(ManageUserRolesViewModel manageUserRolesViewModel)
        {
            OntoIdentityUser ontoIdentityUser = await _UserManager.FindByNameAsync(manageUserRolesViewModel.UserName);

            IdentityResult result = await _UserManager.AddToRoleAsync(ontoIdentityUser.Id, manageUserRolesViewModel.RoleName);

            string resultMessage = result.Succeeded ? SuccessMsg.UserAddedToRoleSuccesfuly : ErrorMsg.UserAddedToRoleError;

            return Json(resultMessage);
        }

        private UserSettingsLogic _UserSettingsLogic { get; set; }
        private OntoIdentityRoleManager _RoleManager { get; set; }
        private OntoIdentityUserManager _UserManager { get; set; }
        private LocaleLogic _LocaleLogic { get; set; }
    }
}
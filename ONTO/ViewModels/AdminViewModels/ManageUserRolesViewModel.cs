using Microsoft.AspNet.Identity.EntityFramework;
using ONTO.Localization;
using ONTO.Models;
using ONTO.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ONTO.ViewModels.AdminViewModels
{
    public class ManageUserRolesViewModel
    {
        [Display(Name = nameof(Labels.UserName), ResourceType = typeof(Labels))]
        public string UserName { get; set; }
        [Display(Name = nameof(Labels.RoleName), ResourceType = typeof(Labels))]
        public string RoleName { get; set; }

        public string RoleID { get; set; }

        public List<OntoIdentityUser> ListOfUser { get; set; }
    }
}
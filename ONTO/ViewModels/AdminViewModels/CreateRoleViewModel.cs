using ONTO.Localization;
using ONTO.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ONTO.ViewModels.AdminViewModels
{
    public class CreateRoleViewModel
    {
        [Display(Name = nameof(Labels.RoleName), ResourceType = typeof(Labels))]
        public string RoleName { get; set; }
    }
}
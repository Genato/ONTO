using Microsoft.AspNet.Identity;
using ONTO.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ONTO.ViewModels.AccountViewModels
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = nameof(Labels.CurrentPassword), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceName = nameof(ErrorMsg.PasswordIsRequired), ErrorMessageResourceType = typeof(ErrorMsg))]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = nameof(Labels.NewPassword), ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = nameof(ErrorMsg.MinPasswordLength), ErrorMessageResourceType = typeof(ErrorMsg))]
        [Required(ErrorMessageResourceName = nameof(ErrorMsg.ConfirmPasswordIsRequired), ErrorMessageResourceType = typeof(ErrorMsg))]
        public string NewPassword { get; set; }
    }
}
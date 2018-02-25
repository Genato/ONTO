using ONTO.Localization;
using ONTO.Models.ONTOModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ONTO.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = nameof(Labels.Email), ResourceType = typeof(Labels))]
        [EmailAddress(ErrorMessageResourceName = nameof(ErrorMsg.EmailInvalidFormat), ErrorMessageResourceType = typeof(ErrorMsg))]
        [Required(ErrorMessageResourceName = nameof(ErrorMsg.EmailMustBeSpecified), ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = nameof(Labels.Password), ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = nameof(ErrorMsg.MinPasswordLength), ErrorMessageResourceType = typeof(ErrorMsg))]
        [Required(ErrorMessageResourceName = nameof(ErrorMsg.PasswordIsRequired), ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = nameof(Labels.ConfirmPassword), ResourceType = typeof(Labels))]
        [Compare("Password", ErrorMessageResourceName = nameof(ErrorMsg.PasswordsDontMatch), ErrorMessageResourceType = typeof(ErrorMsg))]
        [Required(ErrorMessageResourceName = nameof(ErrorMsg.ConfirmPasswordIsRequired), ErrorMessageResourceType = typeof(ErrorMsg))]
        public string ConfirmPassword { get; set; }

        [Display(Name = nameof(Labels.SelectLanguage), ResourceType = typeof(Labels))]
        public List<Locale> Localization { get; set; }

        public int SelectedLocale { get; set; }
    }
}
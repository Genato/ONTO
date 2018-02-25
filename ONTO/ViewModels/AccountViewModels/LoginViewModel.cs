using ONTO.Localization;
using System.ComponentModel.DataAnnotations;

namespace ONTO.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Display(Name = nameof(Labels.Email), ResourceType = typeof(Labels))]
        [EmailAddress(ErrorMessageResourceName = nameof(ErrorMsg.EmailInvalidFormat), ErrorMessageResourceType = typeof(ErrorMsg))]
        [Required(ErrorMessageResourceName = nameof(ErrorMsg.EmailMustBeSpecified), ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = nameof(Labels.Password), ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceName = nameof(ErrorMsg.PasswordIsRequired), ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Password { get; set; }

        [Display(Name = nameof(Labels.RememberMe), ResourceType = typeof(Labels))]
        public bool RememberMe { get; set; }
    }
}

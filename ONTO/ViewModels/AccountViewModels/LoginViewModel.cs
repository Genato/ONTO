using ONTO.Localization;
using System.ComponentModel.DataAnnotations;

namespace ONTO.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = nameof(Labels.Email), ResourceType = typeof(Labels))]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Labels.Password), ResourceType = typeof(Labels))]
        public string Password { get; set; }

        [Display(Name = nameof(Labels.RememberMe), ResourceType = typeof(Labels))]
        public bool RememberMe { get; set; }
    }
}

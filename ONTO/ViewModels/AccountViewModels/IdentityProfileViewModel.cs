using ONTO.Localization;
using ONTO.Models.ONTOModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ONTO.ViewModels.AccountViewModels
{
    public class IdentityProfileViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = nameof(Labels.CurrentEmail), ResourceType = typeof(Labels))]
        public string CurrentEmail { get; set; }

        [EmailAddress]
        [Display(Name = nameof(Labels.NewEmail), ResourceType = typeof(Labels))]
        public string NewEmail { get; set; }
    }
}
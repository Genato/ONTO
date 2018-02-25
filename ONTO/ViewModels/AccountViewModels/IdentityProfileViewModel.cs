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
        [Display(Name = nameof(Labels.CurrentEmail), ResourceType = typeof(Labels))]
        [EmailAddress(ErrorMessageResourceName = nameof(ErrorMsg.EmailInvalidFormat), ErrorMessageResourceType = typeof(ErrorMsg))]
        [Required(ErrorMessageResourceName = nameof(ErrorMsg.CurrentEmailIsRequired), ErrorMessageResourceType = typeof(ErrorMsg))]
        public string CurrentEmail { get; set; }

        [Display(Name = nameof(Labels.NewEmail), ResourceType = typeof(Labels))]
        [EmailAddress(ErrorMessageResourceName = nameof(ErrorMsg.EmailInvalidFormat), ErrorMessageResourceType = typeof(ErrorMsg))]
        [Required(ErrorMessageResourceName = nameof(ErrorMsg.NewEmailIsRequired), ErrorMessageResourceType = typeof(ErrorMsg))]
        public string NewEmail { get; set; }
    }
}
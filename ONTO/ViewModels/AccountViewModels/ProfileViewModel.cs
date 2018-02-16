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
    public class ProfileViewModel
    {
        [Required]
        [Display(Name = nameof(Messages.LocalizationLabel), ResourceType = typeof(Messages))]
        public List<Locale> Localization { get; set; }

        public int SelectedLocale { get; set; }
    }
}
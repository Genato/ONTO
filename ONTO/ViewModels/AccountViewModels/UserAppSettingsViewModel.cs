using Microsoft.AspNet.Identity;
using ONTO.Localization;
using ONTO.Models;
using ONTO.Models.ONTOModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ONTO.ViewModels.AccountViewModels
{
    public class UserAppSettingsViewModel
    {
        [Display(Name = nameof(Labels.SelectLanguage), ResourceType = typeof(Labels))]
        public List<Locale> Localization { get; set; }

        public int SelectedLocale { get; set; }
    }
}
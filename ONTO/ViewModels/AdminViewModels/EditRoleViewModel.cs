using Microsoft.AspNet.Identity.EntityFramework;
using ONTO.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONTO.ViewModels.AdminViewModels
{
    public class EditRoleViewModel
    {
        public OntoIdentityRole OntoIdentityRole { get; set; }
        public List<IdentityUserRole> UserRole { get; set; }
    }
}
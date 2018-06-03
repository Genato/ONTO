using ONTO.Models.IdentityModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONTO.ViewModels.AdminViewModels
{
    public class ManageRolesViewModel
    {
        public IPagedList<OntoIdentityRole> PagedListOfRoles { get; set; }
    }
}
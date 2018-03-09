using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONTO.Models.IdentityModels
{
    public class OntoIdentityRole : IdentityRole
    {
        public OntoIdentityRole() { }

        public OntoIdentityRole(string roleName) : base(roleName) { }
    }
}
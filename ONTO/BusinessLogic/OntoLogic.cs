using Microsoft.AspNet.Identity;
using ONTO.DAL;
using ONTO.DbContexts;
using ONTO.Models.ONTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ONTO.BusinessLogic
{
    public abstract class OntoLogic
    {

        // Public members

        /// <summary>
        /// Add errors to modelstate from IdentityResults. ModelState can be passed to ERROR view.
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="result"></param>
        public void AddErrors(ModelStateDictionary modelState, IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError("error", error);
            }
        }
    }
}
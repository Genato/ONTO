using Microsoft.AspNet.Identity;
using ONTO.Localization;
using ONTO.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ONTO.Identity.Extensions
{
    public class CustomRoleValidator : RoleValidator<OntoIdentityRole>
    {
        public CustomRoleValidator(RoleManager<OntoIdentityRole, string> roleManager) : base(roleManager)
        {
            _RoleManager = roleManager;
            _Errors = new List<string>();
        }

        public override Task<IdentityResult> ValidateAsync(OntoIdentityRole role)
        {
            ValidateRoleName(role);

            IdentityResult result = _Errors.Count > 0 ? new IdentityResult(_Errors.ToArray()) : IdentityResult.Success;

            return Task.FromResult(result);
        }

        private void ValidateRoleName(OntoIdentityRole role)
        {
            if (string.IsNullOrWhiteSpace(role.Name))
            {
                _Errors.Add(ErrorMsg.RoleNameMustBeSpecified);

                return;
            }

            Task<OntoIdentityRole> _role = _RoleManager.FindByNameAsync(role.Name);

            if (_role.Result != null && _role.Result.Name == role.Name)
                _Errors.Add(ErrorMsg.RoleAllreadyExists);

        }

        private List<string> _Errors { get; set; }
        public RoleManager<OntoIdentityRole, string> _RoleManager { get; set; }
    }
}
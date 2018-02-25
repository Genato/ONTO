using Microsoft.AspNet.Identity;
using ONTO.Localization;
using ONTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ONTO.Identity.Extensions
{
    /// <summary>
    /// Custom user validator. <para/>
    /// NOTE: This class inherited and override "UserValidator<T> ValidateAsync(T user)", because standard Identity validation does not support Globalization/Localization
    /// </summary>
    public class CustomUserValidator : UserValidator<OntoIdentityUser>
    {
        public CustomUserValidator(UserManager<OntoIdentityUser, string> manager) : base(manager)
        {
            _Errors = new List<string>();
            _Manager = manager;
        }

        public override Task<IdentityResult> ValidateAsync(OntoIdentityUser user)
        {
            ValidateEmail(user);

            IdentityResult result = _Errors.Count > 0 ? new IdentityResult(_Errors.ToArray()) : IdentityResult.Success;

            return Task.FromResult(result);
        }

        /// <summary>
        /// Method that adds localization for Identity user validation.
        /// </summary>
        /// <param name="user"></param>
        private void ValidateEmail(OntoIdentityUser user)
        {
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                _Errors.Add(ErrorMsg.EmailMustBeSpecified);
                return;
            }

            Task<OntoIdentityUser> _user =  _Manager.FindByEmailAsync(user.Email);

            if (_user.Result != null && _user.Result.Id != user.Id)
                _Errors.Add(ErrorMsg.EmailAllreadyExists);
        }

        private List<string> _Errors { get; set; }
        public UserManager<OntoIdentityUser, string> _Manager { get; set; }
    }
}
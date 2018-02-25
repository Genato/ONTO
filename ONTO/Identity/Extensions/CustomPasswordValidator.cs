using Microsoft.AspNet.Identity;
using ONTO.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ONTO.Identity.Extensions
{
    /// <summary>
    /// Custom Identity password validator. <para/>
    /// NOTE: This class inherited and override "PasswordValidator ValidateAsync(string password)", because standard Identity validation does not support Globalization/Localization
    /// </summary>
    public class CustomPasswordValidator : PasswordValidator
    {
        public CustomPasswordValidator()
        {
            _Errors = new List<string>();
        }

        public override Task<IdentityResult> ValidateAsync(string password)
        {
            ValidatePassword(password);

            IdentityResult result = _Errors.Count > 0 ? new IdentityResult(_Errors.ToArray()) : IdentityResult.Success;

            return Task.FromResult(result);
        }

        /// <summary>
        /// Method that adds localization for Identity password validation.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                _Errors.Add(ErrorMsg.PasswordIsRequired);
                return;
            }

            if (Regex.IsMatch(password, @"[a-z]+") == false)
                _Errors.Add(ErrorMsg.PasswordRequireLowerCase);

            if (Regex.IsMatch(password, @"[0-9]+") == false)
                _Errors.Add(ErrorMsg.PasswordRequireDigit);
        }

        private List<string> _Errors { get; set; }
    }
}
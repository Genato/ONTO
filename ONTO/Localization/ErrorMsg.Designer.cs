﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ONTO.Localization {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorMsg {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMsg() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ONTO.Localization.ErrorMsg", typeof(ErrorMsg).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The confirm password field is required !.
        /// </summary>
        public static string ConfirmPasswordIsRequired {
            get {
                return ResourceManager.GetString("ConfirmPasswordIsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The current email field is required !.
        /// </summary>
        public static string CurrentEmailIsRequired {
            get {
                return ResourceManager.GetString("CurrentEmailIsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is already an account associated with the email you entered !.
        /// </summary>
        public static string EmailAllreadyExists {
            get {
                return ResourceManager.GetString("EmailAllreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email address is not in right format !.
        /// </summary>
        public static string EmailInvalidFormat {
            get {
                return ResourceManager.GetString("EmailInvalidFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email address must be specified !.
        /// </summary>
        public static string EmailMustBeSpecified {
            get {
                return ResourceManager.GetString("EmailMustBeSpecified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Combination of email address and password is not correct !.
        /// </summary>
        public static string InvaliLoginAttempt {
            get {
                return ResourceManager.GetString("InvaliLoginAttempt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password must be at least 6 characters long !.
        /// </summary>
        public static string MinPasswordLength {
            get {
                return ResourceManager.GetString("MinPasswordLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The new email field is required !.
        /// </summary>
        public static string NewEmailIsRequired {
            get {
                return ResourceManager.GetString("NewEmailIsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password field is required !.
        /// </summary>
        public static string PasswordIsRequired {
            get {
                return ResourceManager.GetString("PasswordIsRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Passwords must have at least one digit (&apos;0&apos;-&apos;9&apos;)..
        /// </summary>
        public static string PasswordRequireDigit {
            get {
                return ResourceManager.GetString("PasswordRequireDigit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Passwords must have at least one lowercase (&apos;a&apos;-&apos;z&apos;)..
        /// </summary>
        public static string PasswordRequireLowerCase {
            get {
                return ResourceManager.GetString("PasswordRequireLowerCase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password and confirmation password do not match !.
        /// </summary>
        public static string PasswordsDontMatch {
            get {
                return ResourceManager.GetString("PasswordsDontMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Role with the same name already exists !.
        /// </summary>
        public static string RoleAllreadyExists {
            get {
                return ResourceManager.GetString("RoleAllreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Role name must be specified !.
        /// </summary>
        public static string RoleNameMustBeSpecified {
            get {
                return ResourceManager.GetString("RoleNameMustBeSpecified", resourceCulture);
            }
        }
    }
}

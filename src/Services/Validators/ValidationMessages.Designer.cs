﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Services.Validators {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ValidationMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ValidationMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Services.Validators.ValidationMessages", typeof(ValidationMessages).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Account with AccountId &apos;{0}&apos; not found!.
        /// </summary>
        internal static string AccountNotFound {
            get {
                return ResourceManager.GetString("AccountNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Double entry for values &apos;{0}, {1}, {2}&apos;.
        /// </summary>
        internal static string DoubleEntry {
            get {
                return ResourceManager.GetString("DoubleEntry", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; is not a valid date time for {1}.
        /// </summary>
        internal static string InvalidDateTime {
            get {
                return ResourceManager.GetString("InvalidDateTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {PropertyName} is invalid.
        /// </summary>
        internal static string InvalidField {
            get {
                return ResourceManager.GetString("InvalidField", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {PropertyName} should be {MaxLength} characters long.
        /// </summary>
        internal static string Length {
            get {
                return ResourceManager.GetString("Length", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Meter Reading with MeterReadingId &apos;{0}&apos; not found!.
        /// </summary>
        internal static string MeterReadingNotFound {
            get {
                return ResourceManager.GetString("MeterReadingNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {PropertyName} should be greater or equal to {ComparisonValue}.
        /// </summary>
        internal static string MustBeGreaterOrEqual {
            get {
                return ResourceManager.GetString("MustBeGreaterOrEqual", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {PropertyName} should be lower or equal to {ComparisonValue}.
        /// </summary>
        internal static string MustBeLowerOrEqual {
            get {
                return ResourceManager.GetString("MustBeLowerOrEqual", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; is not a valid number for {1}.
        /// </summary>
        internal static string NotANumber {
            get {
                return ResourceManager.GetString("NotANumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {PropertyName} should be between {From} and {To}.
        /// </summary>
        internal static string RangeBetween {
            get {
                return ResourceManager.GetString("RangeBetween", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {PropertyName} is Required.
        /// </summary>
        internal static string RequiredField {
            get {
                return ResourceManager.GetString("RequiredField", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{PropertyValue}&apos; is not a valid value for {PropertyName}.
        /// </summary>
        internal static string ValidValue {
            get {
                return ResourceManager.GetString("ValidValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; is not a valid value for {1}.
        /// </summary>
        internal static string ValidValueCustom {
            get {
                return ResourceManager.GetString("ValidValueCustom", resourceCulture);
            }
        }
    }
}

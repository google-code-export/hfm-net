﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HFM.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("HFM.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Failed to clear cache file &apos;{0}&apos;..
        /// </summary>
        internal static string CacheFileDeleteFailed {
            get {
                return ResourceManager.GetString("CacheFileDeleteFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to create or clear the data cache folder..
        /// </summary>
        internal static string CacheSetupFailed {
            get {
                return ResourceManager.GetString("CacheSetupFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Primary UI failed to initialize..
        /// </summary>
        internal static string FailedToInitUI {
            get {
                return ResourceManager.GetString("FailedToInitUI", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Single Instance IPC channel failed to register..
        /// </summary>
        internal static string IpcRegisterFailed {
            get {
                return ResourceManager.GetString("IpcRegisterFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mono version detection failed..
        /// </summary>
        internal static string MonoDetectFailed {
            get {
                return ResourceManager.GetString("MonoDetectFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your version of Mono is too old.  Please update to 2.8 or later..
        /// </summary>
        internal static string MonoTooOld {
            get {
                return ResourceManager.GetString("MonoTooOld", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to signal first instance of HFM.NET.  Please try starting HFM.NET again before reporting this issue..
        /// </summary>
        internal static string RemotingCallFailed {
            get {
                return ResourceManager.GetString("RemotingCallFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to HFM.NET detected an instance of HFM.NET already running on this computer but failed to connect with it.  If you&apos;re sure there is not an existing instance of HFM.NET running then you may safely ignore this warning and allow HFM.NET to start.  Note that running more than one instance of HFM.NET on the same computer will cause problems.  Do you want to allow HFM.NET to start?.
        /// </summary>
        internal static string RemotingFailedQuestion {
            get {
                return ResourceManager.GetString("RemotingFailedQuestion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User preferences failed to initialize.  The user.config file is likely corrupt.  Start with the &apos;/r&apos; switch to reset the user preferences..
        /// </summary>
        internal static string UserPreferencesFailed {
            get {
                return ResourceManager.GetString("UserPreferencesFailed", resourceCulture);
            }
        }
    }
}

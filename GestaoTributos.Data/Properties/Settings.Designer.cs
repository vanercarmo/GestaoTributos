﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestaoTributos.Data.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://web20.senior.com.br:38821/g5-senior-services/sapiens_Synccom_senior_g5_co" +
            "_mct_ctb_gerarlotectb")]
        public string GestaoTributos_Data_wsLoteContabil_g5_senior_services {
            get {
                return ((string)(this["GestaoTributos_Data_wsLoteContabil_g5_senior_services"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://web20.senior.com.br:38821/g5-senior-services/sapiens_Synccom_senior_g5_co" +
            "_mct_ctb_importacaolctctb")]
        public string GestaoTributos_Data_wsLancamentoContabil_g5_senior_services {
            get {
                return ((string)(this["GestaoTributos_Data_wsLancamentoContabil_g5_senior_services"]));
            }
        }
    }
}

﻿#pragma checksum "D:\SESSION 3\PROG INTERFACE\GitHub\projet_prog\prog_final\prog_final\DialogAdmin.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E0A718D2F0D728716D369FBEA7E7B14F08F740175CE83180C144E911D0D71E46"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace prog_final
{
    partial class DialogAdmin : 
        global::Microsoft.UI.Xaml.Controls.ContentDialog, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2408")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // DialogAdmin.xaml line 36
                {
                    this.stckpnl_admin = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 3: // DialogAdmin.xaml line 57
                {
                    this.stckpnl_adherent = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 4: // DialogAdmin.xaml line 82
                {
                    this.loginbtn = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.loginbtn).Click += this.connexionbtn;
                }
                break;
            case 5: // DialogAdmin.xaml line 90
                {
                    this.closeBtn = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.closeBtn).Click += this.closeBtn_Click;
                }
                break;
            case 6: // DialogAdmin.xaml line 58
                {
                    this.tbx_userAdherent = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            case 7: // DialogAdmin.xaml line 63
                {
                    this.messageErr_tbx_userAdherent = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 8: // DialogAdmin.xaml line 37
                {
                    this.tbx_userAdmin = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            case 9: // DialogAdmin.xaml line 42
                {
                    this.pwd_userAdmin = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.PasswordBox>(target);
                }
                break;
            case 10: // DialogAdmin.xaml line 47
                {
                    this.messageErr_tbx_userAdmin = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 11: // DialogAdmin.xaml line 14
                {
                    this.cbxAdmin = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.CheckBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.CheckBox)this.cbxAdmin).Checked += this.gestion_checkbox_admin;
                }
                break;
            case 12: // DialogAdmin.xaml line 20
                {
                    this.cbxAdherent = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.CheckBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.CheckBox)this.cbxAdherent).Checked += this.gestion_checkbox_adherent;
                }
                break;
            case 13: // DialogAdmin.xaml line 26
                {
                    this.messageErr_checkbx = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2408")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

﻿#pragma checksum "C:\Users\timor\Downloads\SzkolaJazdy_v5\SzkolaJazdy\SzkolaJazdy\Rejestracja.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0C988D5C30E43CC198648E6A5B3098D7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SzkolaJazdy
{
    partial class Rejestracja : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Rejestracja.xaml line 38
                {
                    this.btnWyslijDane = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnWyslijDane).Click += this.WyslanieDanych;
                }
                break;
            case 3: // Rejestracja.xaml line 36
                {
                    this.tbEmail = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4: // Rejestracja.xaml line 32
                {
                    this.tbTelefon = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5: // Rejestracja.xaml line 28
                {
                    this.tbNazwisko = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6: // Rejestracja.xaml line 24
                {
                    this.tbImie = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 7: // Rejestracja.xaml line 20
                {
                    this.tbHaslo = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 8: // Rejestracja.xaml line 16
                {
                    this.tbLogin = (global::Windows.UI.Xaml.Controls.TextBox)(target);
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}


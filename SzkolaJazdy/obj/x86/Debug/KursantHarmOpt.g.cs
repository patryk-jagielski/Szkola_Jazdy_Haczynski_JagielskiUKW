﻿#pragma checksum "C:\Users\timor\Downloads\SzkolaJazdy_v5\SzkolaJazdy\SzkolaJazdy\KursantHarmOpt.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2FF139A141BAA047477EC888319B65A3"
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
    partial class KursantHarmOpt : 
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
            case 2: // KursantHarmOpt.xaml line 12
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element2 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element2).Click += this.Cofanie;
                }
                break;
            case 3: // KursantHarmOpt.xaml line 53
                {
                    this.SelectHarmKursanta = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 5: // KursantHarmOpt.xaml line 41
                {
                    this.btnOdswiezTabele = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnOdswiezTabele).Click += this.OdswiezTabeleKursanci;
                }
                break;
            case 6: // KursantHarmOpt.xaml line 45
                {
                    this.btnAnulujJazdy = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnAnulujJazdy).Click += this.AnulujJazdy;
                }
                break;
            case 7: // KursantHarmOpt.xaml line 35
                {
                    this.btnZapisanieNaJazdy = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnZapisanieNaJazdy).Click += this.ZapisanieNaJazdy;
                }
                break;
            case 8: // KursantHarmOpt.xaml line 19
                {
                    this.cdpDzien = (global::Windows.UI.Xaml.Controls.CalendarDatePicker)(target);
                }
                break;
            case 9: // KursantHarmOpt.xaml line 21
                {
                    this.cbGodzina = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 10: // KursantHarmOpt.xaml line 30
                {
                    this.cbInstruktor = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
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

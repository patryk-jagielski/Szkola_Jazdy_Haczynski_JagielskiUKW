#pragma checksum "C:\Users\timor\Downloads\SzkolaJazdy_v5\SzkolaJazdy\SzkolaJazdy\InstruktorHarmOpt.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9ED297FE26F5C8F9494405722A8D250D"
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
    partial class InstruktorHarmOpt : 
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
            case 2: // InstruktorHarmOpt.xaml line 12
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element2 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element2).Click += this.Cofanie;
                }
                break;
            case 3: // InstruktorHarmOpt.xaml line 14
                {
                    this.btnOdswiezTabele = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnOdswiezTabele).Click += this.OdswiezTabeleKursanci;
                }
                break;
            case 4: // InstruktorHarmOpt.xaml line 32
                {
                    this.SelectHarmInstr = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 6: // InstruktorHarmOpt.xaml line 26
                {
                    this.btnAktualizujStatus = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnAktualizujStatus).Click += this.AktualizujStatus;
                }
                break;
            case 7: // InstruktorHarmOpt.xaml line 22
                {
                    this.chbNieukończone = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                }
                break;
            case 8: // InstruktorHarmOpt.xaml line 23
                {
                    this.chbUkończone = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                }
                break;
            case 9: // InstruktorHarmOpt.xaml line 24
                {
                    this.chbAnulowane = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
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


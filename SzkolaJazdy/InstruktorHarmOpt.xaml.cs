using DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace SzkolaJazdy
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class InstruktorHarmOpt : Page
    {
        public string daneLoginIns;
        public string daneInstruktorId;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                daneLoginIns = e.Parameter.ToString();
            }
            base.OnNavigatedTo(e);
        }
        public InstruktorHarmOpt()
        {
            this.InitializeComponent();
        }

        private void Cofanie(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(InstruktorPage), daneLoginIns);
        }

        private void OdswiezTabeleKursanci(object sender, RoutedEventArgs e)
        {
            daneInstruktorId = Data.GetInsIdWhereLogin(daneLoginIns)[0];
            SelectHarmInstr.ItemsSource = Data.GetHarmJazdWhereIns(daneInstruktorId);
        }

        private void AktualizujStatus(object sender, RoutedEventArgs e)
        {
            if (chbUkończone.IsChecked == true)
            {
                Data.HarmJazdStatus(((ContentControl)chbUkończone).Content.ToString(), SelectHarmInstr.SelectedItem.ToString());
            }
            else if (chbNieukończone.IsChecked == true)
            {
                Data.HarmJazdStatus(((ContentControl)chbNieukończone).Content.ToString(), SelectHarmInstr.SelectedItem.ToString());
            }
            else if(chbAnulowane.IsChecked == true)
            {
                Data.HarmJazdStatus(((ContentControl)chbAnulowane).Content.ToString(), SelectHarmInstr.SelectedItem.ToString());
            }
        }
    }
}

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
using DataAccess;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace SzkolaJazdy
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class KursantDaneUzOpt : Page
    {
        public string daneLoginKur;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                daneLoginKur = e.Parameter.ToString();
            }
            base.OnNavigatedTo(e);
        }
        public KursantDaneUzOpt()
        {
            this.InitializeComponent();
        }

        private void OdswiezTabeleKursanci(object sender, RoutedEventArgs e)
        {
            SelectDaneKursanta.ItemsSource = Data.GetDataKursanciWhere(daneLoginKur);
            tbLogin.Text = Data.GetDataKursanciWhere(daneLoginKur)[0];
            tbHaslo.Text = Data.GetDataKursanciWhere(daneLoginKur)[1];
            tbImie.Text = Data.GetDataKursanciWhere(daneLoginKur)[2];
            tbNazwisko.Text = Data.GetDataKursanciWhere(daneLoginKur)[3];
            tbTelefon.Text = Data.GetDataKursanciWhere(daneLoginKur)[4];
            tbEmail.Text = Data.GetDataKursanciWhere(daneLoginKur)[5];

        }

        private void Cofanie(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(KursantPage), daneLoginKur);
        }

        private void AktualizujDane(object sender, RoutedEventArgs e)
        {
            Data.UpdateRekordKur(tbLogin.Text, tbHaslo.Text, tbImie.Text, tbNazwisko.Text, tbTelefon.Text, tbEmail.Text, daneLoginKur);
            daneLoginKur = tbLogin.Text;
        }
    }
}

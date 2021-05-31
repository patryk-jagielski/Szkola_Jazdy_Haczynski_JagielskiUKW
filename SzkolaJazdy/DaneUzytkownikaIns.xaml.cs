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
    public sealed partial class DaneUzytkownikaIns : Page    
    {
        private string daneLoginKur;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                daneLoginKur = e.Parameter.ToString();
            }
            base.OnNavigatedTo(e);
        }
        public DaneUzytkownikaIns()
        {
            this.InitializeComponent();
        }

        private void OdswiezTabeleInstruktor(object sender, RoutedEventArgs e)
        {
            SelectDaneInstruktora.ItemsSource = Data.GetDataInstruktorWhere(daneLoginKur);
            tbLogin.Text = Data.GetDataInstruktorWhere(daneLoginKur)[0];
            tbHaslo.Text = Data.GetDataInstruktorWhere(daneLoginKur)[1];
            tbImie.Text = Data.GetDataInstruktorWhere(daneLoginKur)[2];
            tbNazwisko.Text = Data.GetDataInstruktorWhere(daneLoginKur)[3];
            tbTelefon.Text = Data.GetDataInstruktorWhere(daneLoginKur)[4];
            tbEmail.Text = Data.GetDataInstruktorWhere(daneLoginKur)[5];
        }

        private void Cofanie(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(InstruktorPage), daneLoginKur);
        }

        private void AktualizujDane(object sender, RoutedEventArgs e)
        {
            Data.UpdateRekordIns(tbLogin.Text, tbHaslo.Text, tbImie.Text, tbNazwisko.Text, tbTelefon.Text, tbEmail.Text, daneLoginKur);
            daneLoginKur = tbLogin.Text;
        }
    }
}

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
    public sealed partial class PojazdyAdminOption : Page
    {
        public PojazdyAdminOption()
        {
            this.InitializeComponent();
        }

        private void OdswiezTabelePojazdy(object sender, RoutedEventArgs e)
        {
            SelectPojazdy.ItemsSource = Data.GetDataPojazdy();
        }

        private void DodajPojazd(object sender, RoutedEventArgs e)
        {
            Data.AddDataPojazdy(tbTypPojazdu.Text, tbMarka.Text, tbModelPoj.Text, tbRokPro.Text, tbSkrzBie.Text, tbStaPoj.Text, "1");
        }

        private void ZmienStatusPojazdu(object sender, RoutedEventArgs e)
        {
            Data.UpdateDataPojazdy(tbStaPoj.Text, tbPojazdId.Text);
        }

        private void UsunPojazd(object sender, RoutedEventArgs e)
        {
            Data.DeleteDataPojazdy(tbPojazdId.Text);
        }

        private void Cofanie(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminPage));
        }
    }
}

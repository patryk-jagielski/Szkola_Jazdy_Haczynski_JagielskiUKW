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
    public sealed partial class InstruktorzyAdminOpt : Page
    {
        public InstruktorzyAdminOpt()
        {
            this.InitializeComponent();
            cbNazIns.ItemsSource = Data.GetNazwiskaInstruktorzy();
        }

        private void Cofanie(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminPage));
        }

        private void OdswiezTabeleInstruktorzy(object sender, RoutedEventArgs e)
        {
            if (cbNazIns.SelectedItem != null)
            {
                SelectInstruktorzy.ItemsSource = Data.GetDataInsWhereNaz(cbNazIns.SelectedItem.ToString());
            }
            else
            {
                SelectInstruktorzy.ItemsSource = Data.GetDataInstruktorzy();
            }
        }

        private void DodajInstruktora(object sender, RoutedEventArgs e)
        {
            Data.AddDataInstruktorzy(tbLogin.Text, tbHaslo.Text, tbImie.Text, tbNazwisko.Text, tbTelefon.Text, tbEmail.Text, tbStatus.Text, "1");
        }

        private void ZmienStatusInstruktora(object sender, RoutedEventArgs e)
        {
            Data.UpdateDataStatusIns(tbStatus.Text, tbInstruktorId.Text);
        }

        private void UsunInstruktora(object sender, RoutedEventArgs e)
        {
            Data.DeleteDataIns(tbInstruktorId.Text);
        }
    }
}

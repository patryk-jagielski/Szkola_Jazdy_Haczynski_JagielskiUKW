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
    public sealed partial class KursanciAdminOpt : Page
    {
        private string kursantId;
        public KursanciAdminOpt()
        {
            this.InitializeComponent();
            cbNazKurs.ItemsSource = Data.GetDataKursNaz();
        }

        private void Cofanie(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminPage));    
        }

        private void OdswiezTabeleKursanci(object sender, RoutedEventArgs e)
        {
            

            if (cbNazKurs.SelectedItem != null)
            {
                kursantId = Data.GetIdKursantWhereNaz(cbNazKurs.SelectedItem.ToString())[0];
                SelectKursantow.ItemsSource = Data.GetDataKursanciWhereNaz(kursantId);
                SelectHarmKursanta.ItemsSource = Data.GetHarmJazdWhere(kursantId);
            }
            else
            {
                SelectKursantow.ItemsSource = Data.GetDataKursanci();
                SelectHarmKursanta.ItemsSource = Data.GetHarmJazd();
            }
            
        }
    }
}

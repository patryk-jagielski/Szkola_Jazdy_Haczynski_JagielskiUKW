using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using SzkolaJazdy;
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

namespace DataAccess
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class InstruktorPojOpt : Page
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
        public InstruktorPojOpt()
        {
            this.InitializeComponent();
        }

        private void Cofanie(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(InstruktorPage), daneLoginIns);
        }

        private void OdswiezTabelePojazdy(object sender, RoutedEventArgs e)
        {
            SelectPojazdy.ItemsSource = Data.GetDataPojazdy();
        }

        private void ZmienStatusPojazdu(object sender, RoutedEventArgs e)
        {
            Data.UpdateDataPojazdy(tbStaPoj.Text, tbPojId.Text);
        }
    }
}

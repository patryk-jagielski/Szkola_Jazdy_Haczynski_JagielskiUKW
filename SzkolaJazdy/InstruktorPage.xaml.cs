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
    public sealed partial class InstruktorPage : Page
    {
        public string daneLogin;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            { 
                tbPowitanie.Text = $"{e.Parameter.ToString()}";
                daneLogin = e.Parameter.ToString();
            }
            base.OnNavigatedTo(e);
        }
        public InstruktorPage()
        {
            this.InitializeComponent();
        }

        private void PrzejscieDoKursanciInsOpt(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(InstruktorKursOpt), daneLogin);
        }

        private void PrzejscieDoPojazdyInsOpt(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(InstruktorPojOpt), daneLogin);
        }

        private void PrzejscieDoHarmonogramInsOpt(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(InstruktorHarmOpt), daneLogin);
        }

        private void Wylogowanie(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void DaneUzytkownikaIns(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DaneUzytkownikaIns), daneLogin);
        }
    }
}

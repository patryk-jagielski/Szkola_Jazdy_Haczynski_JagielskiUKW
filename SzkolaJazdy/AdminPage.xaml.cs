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
    public sealed partial class AdminPage : Page
    {
        public AdminPage()
        {
            this.InitializeComponent();
        }

        private void Wylogowanie(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void PrzejscieDoInstruktorzyOpt(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(InstruktorzyAdminOpt));
        }

        private void PrzejscieDoKursanciOpt(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(KursanciAdminOpt));
        }

        private void PrzejscieDoPojazdyOpt(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PojazdyAdminOption));
        }
    }
}

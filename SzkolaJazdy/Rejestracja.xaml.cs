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
    public sealed partial class Rejestracja : Page
    {
        public Rejestracja()
        {
            this.InitializeComponent();
        }

        private void WyslanieDanych(object sender, RoutedEventArgs e)
        {
            Data.AddDataKursanci(tbLogin.Text, tbHaslo.Password.ToString(), tbImie.Text, tbNazwisko.Text, tbTelefon.Text, tbEmail.Text, 1);
            this.Frame.Navigate(typeof(MainPage)); 
        }
    }
}

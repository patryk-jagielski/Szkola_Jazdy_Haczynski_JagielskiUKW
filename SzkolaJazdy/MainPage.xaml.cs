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

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace SzkolaJazdy
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Logowanie(object sender, RoutedEventArgs e)
        {
            string sprawdzenieKursanci = Data.LoginInDataKursanci(login.Text, haslo.Password.ToString())[0];
            string sprawdzenieInstruktorzy = Data.LoginInDataInstruktorzy(login.Text, haslo.Password.ToString())[0];
            string sprawdzenieAdministratorzy = Data.LoginInDataAdmin(login.Text, haslo.Password.ToString())[0];

            if (sprawdzenieKursanci == "1")
            {
                this.Frame.Navigate(typeof(KursantPage), login.Text);
            }
            else if(sprawdzenieInstruktorzy == "1")
            {
                this.Frame.Navigate(typeof(InstruktorPage), login.Text);
            }
            else if (sprawdzenieAdministratorzy == "1")
            {
                this.Frame.Navigate(typeof(AdminPage), login.Text);
            }
            else
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("Błędny login lub hasło.");
                await messageDialog.ShowAsync();
            }
        }

        private void Rejestracja(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Rejestracja));
        }
    }
}

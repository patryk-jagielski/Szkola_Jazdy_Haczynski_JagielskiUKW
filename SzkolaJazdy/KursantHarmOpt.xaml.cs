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
    public sealed partial class KursantHarmOpt : Page
    {
        public string daneLoginKur;
        public string daneInstruktorId;
        public string daneKursantId;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                daneLoginKur = e.Parameter.ToString();
            }
            base.OnNavigatedTo(e);
        }
        public KursantHarmOpt()
        {
            this.InitializeComponent();
            cbInstruktor.ItemsSource = Data.GetNazwiskaInstruktorzy();    
        }

        private void Cofanie(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(KursantPage), daneLoginKur);
        }

        private void OdswiezTabeleKursanci(object sender, RoutedEventArgs e)
        {
            daneKursantId = Data.GetIdKursantWhere(daneLoginKur)[0];
            SelectHarmKursanta.ItemsSource = Data.GetHarmJazdWhere(daneKursantId);
        }

        async private void ZapisanieNaJazdy(object sender, RoutedEventArgs e)
        {
            daneInstruktorId = Data.GetIdInstruktorWhere(cbInstruktor.SelectedItem.ToString())[0];
            daneKursantId = Data.GetIdKursantWhere(daneLoginKur)[0];
            if (Data.CheckStatusJazd(daneKursantId)[0] == "1")
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("Nie można zapisać się na jazdy, z powodu nieukończenia poprzednich.");
                await messageDialog.ShowAsync();
            }
            else if(Data.CheckDateAndHour(daneInstruktorId, cdpDzien.Date.Value.Day.ToString() + "." + cdpDzien.Date.Value.Month.ToString() + "." +
                    cdpDzien.Date.Value.Year.ToString(), ((ContentControl)cbGodzina.SelectedItem).Content.ToString())[0] == "1")
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("Nie można zapisać się na jazdy, ponieważ wybrana data oraz godzina jest już zajęta.");
                await messageDialog.ShowAsync();
            }
            else
            {
                Data.AddDataHarmJazdKurs(cdpDzien.Date.Value.Day.ToString() + "." + cdpDzien.Date.Value.Month.ToString() + "." +
                    cdpDzien.Date.Value.Year.ToString(), ((ContentControl)cbGodzina.SelectedItem).Content.ToString(), "Nieukończone", daneKursantId, daneInstruktorId);
            }
            

        }

        private async void AnulujJazdy(object sender, RoutedEventArgs e)
        {
            if (Data.CheckStatusJazd(daneKursantId)[0] == "0")
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("Jazdy zostały już ukończone lub anulowane.");
                await messageDialog.ShowAsync();
            }
            else
            {
                Data.HarmJazdStatus("Anulowane", SelectHarmKursanta.SelectedItem.ToString()); 
            }
              
        }
    }
}

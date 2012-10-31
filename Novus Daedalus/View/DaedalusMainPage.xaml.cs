using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Novus_Daedalus.View
{
    /// <summary>
    /// Logica di interazione per DaedalusMainPage.xaml
    /// </summary>
    public partial class DaedalusMainPage : Page
    {

        public DaedalusMainPage()
        {
            InitializeComponent();

        }

        private void MainPageLoaded(object sender, RoutedEventArgs e)
        {
            Benvenuto_label.Content = "Benvenuto " + ((Model.utente)Application.Current.Properties["User"]).persona.Nome;
            if (Application.Current.Properties["Scheda"] != null)
                Scheda_Label.Content = "scheda numero: ";
            else
                Scheda_Label.Content = "nessuna scheda selezionata";
        }

        private void PersoneButtonClick(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["Scheda"] == null)
            {
                MessageBox.Show("Non hai selezionato alcuna scheda, premi il pulsante \"Apri scheda\" " +
                    "oppure il pulsante \"Nuova iscrizione\"");
                return;
            }
        }

        private void LaboratorioButtonClick(object sender, RoutedEventArgs e)
        {
            /*if (Application.Current.Properties["Scheda"] == null)
            {
                MessageBox.Show("Non hai selezionato alcuna scheda, premi il pulsante \"Apri scheda\" " +
                    "oppure il pulsante \"Nuova iscrizione\"");
                return;
            }*/
            new View.Laboratorio_Selezione_Modalità(this).Show();
        }

        private void SeguitiButtonClick(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["Scheda"] == null)
            {
                MessageBox.Show("Non hai selezionato alcuna scheda, premi il pulsante \"Apri scheda\" " +
                    "oppure il pulsante \"Nuova iscrizione\"");
                return;
            }
        }

        private void AttiButtonClick(object sender, RoutedEventArgs e)
        {
            /*if (Application.Current.Properties["Scheda"] == null)
            {
                MessageBox.Show("Non hai selezionato alcuna scheda, premi il pulsante \"Apri scheda\" " +
                    "oppure il pulsante \"Nuova iscrizione\"");
                return;
            }*/
            new Selezione_Tipo_Atti().Show();
        }

        private void MagazzinoButtonClick(object sender, RoutedEventArgs e)
        {
            /*if (Application.Current.Properties["Scheda"] == null)
            {
                MessageBox.Show("Non hai selezionato alcuna scheda, premi il pulsante \"Apri scheda\" " +
                    "oppure il pulsante \"Nuova iscrizione\"");
                return;
            }
            else
            {
                NavigationService.Navigate(new View.Magazzino());
            }*/
            NavigationService.Navigate(new View.Magazzino());
        }

        private void ApriSchedaButtonClick(object sender, RoutedEventArgs e)
        {
            new View.ApriScheda().Show();
        }

        private void LogoutButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["User"] = null;
            NavigationService.GoBack();
        }

        private void OfficinaButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new View.Officina());
        }

        private void Informazioni_Generali_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["Scheda"] == null)
            {
                MessageBox.Show("Non hai selezionato alcuna scheda, premi il pulsante \"Apri scheda\" " +
                    "oppure il pulsante \"Nuova iscrizione\"");
                return;
            }
        }
    }
}

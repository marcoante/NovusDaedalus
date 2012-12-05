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
        private Model.novus_daedalus_dbEntities db_connection;
        private Model.scheda scheda = null;
        ApriScheda apriSchedaPage = null;
        string stato_pagina = "scheda";

        public DaedalusMainPage()
        {
            InitializeComponent();

        }

        private void MainPageLoaded(object sender, RoutedEventArgs e)
        {
            Benvenuto_label.Content = "Benvenuto " + ((Model.utente)Application.Current.Properties["User"]).persona.Nome;
            if (scheda != null)
            {
                Scheda_Label.Content = "scheda numero: " + scheda.NumeroRegistro;
            }
            else
                Scheda_Label.Content = "nessuna scheda selezionata";

            db_connection = new Model.novus_daedalus_dbEntities();
            

            //inizializzazione scadenze importanti
        }

        private void PersoneButtonClick(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["Scheda"] == null)
            {
                MessageBox.Show("Non hai selezionato alcuna scheda, premi il pulsante \"Apri scheda\" " +
                    "oppure il pulsante \"Nuova iscrizione\"");
                return;
            }
            else
            {
                Persone.PersoneMainWindow persone_window = new Persone.PersoneMainWindow();
                persone_window.ShowDialog();
            }
        }

        private void LaboratorioButtonClick(object sender, RoutedEventArgs e)
        {
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
            if (Application.Current.Properties["Scheda"] == null)
            {
                MessageBox.Show("Non hai selezionato alcuna scheda, premi il pulsante \"Apri scheda\" " +
                    "oppure il pulsante \"Nuova iscrizione\"");
                return;
            }
            else
                new Selezione_Tipo_Atti().ShowDialog();
        }

        private void MagazzinoButtonClick(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["Scheda"] == null)
            {
                MessageBox.Show("Non hai selezionato alcuna scheda, premi il pulsante \"Apri scheda\" " +
                    "oppure il pulsante \"Nuova iscrizione\"");
                return;
            }
            else
                NavigationService.Navigate(new View.Magazzino());
        }

        private void ApriSchedaButtonClick(object sender, RoutedEventArgs e)
        {
            apriSchedaPage = new View.ApriScheda();
            apriSchedaPage.evento_scheda_selezionata += new SchedaSelezionataHandler(SelezioneSchedaHandler);
            apriSchedaPage.ShowDialog();
        }

        void SelezioneSchedaHandler(object sender)
        {
            //inizializzazione pagina
            //la variabile scheda contiene l'entità scheda prelevata dal DB in base all'ID preso da Application.Current.Properties["Scheda"]);
            scheda = db_connection.scheda.Find((int)Application.Current.Properties["Scheda"]);
            Scheda_Label.Content = "Scheda selezionata: " + scheda.NumeroRegistro;
            //i pulsanti nascosti sono resi visibili
            /*Persone_Button.Visibility = Visibility.Visible;
            Atti_Button.Visibility = Visibility.Visible;
            Seguiti_Button.Visibility = Visibility.Visible;
            Posta_Button.Visibility = Visibility.Visible;
            Informazioni_Generali_Button.Visibility = Visibility.Visible;
            Cose_button.Visibility = Visibility.Visible;
            Magazzino_Button.Visibility = Visibility.Visible;*/
            Visible_Button();
        }

        private void LogoutButtonClick(object sender, RoutedEventArgs e)
        {
            //la variabile globale utente è impostata a NULL
            Application.Current.Properties["User"] = null;
            //la variabile scheda è impostata a NULL
            scheda = null;
            //la variabile globale scheda(ID della scheda aperta) è impostata a NULL
            Application.Current.Properties["Scheda"] = null;
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

        private void NuovaIscrizioneButtonClick(object sender, RoutedEventArgs e)
        {
            NuovaIscrizione.MainWindow nuova_iscrizione_window = new NuovaIscrizione.MainWindow();
            nuova_iscrizione_window.ShowDialog();
        }

        private void Posta_Button_Click(object sender, RoutedEventArgs e)
        {
            Posta.Posta_MainWindow posta_window = new Posta.Posta_MainWindow();
            posta_window.ShowDialog();
        }

        private void Impostazioni_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Impostazioni.Impostazioni());
        }

        private void Esci_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Switch_button_Click(object sender, RoutedEventArgs e)
        {
            if (stato_pagina.Equals("scheda"))
            {
                stato_pagina = "gestione";
                Switch_button.Content = "scheda";
                Officina_Button.Visibility = Visibility.Visible;
                Collapse_Button();
            }
            else
            {
                stato_pagina = "scheda";
                Switch_button.Content = "gestione";
                Officina_Button.Visibility = Visibility.Collapsed;
                Visible_Button();
            }
        }

        private void Collapse_Button()
        {
            Persone_Button.Visibility = Visibility.Collapsed;
            Atti_Button.Visibility = Visibility.Collapsed;
            Seguiti_Button.Visibility = Visibility.Collapsed;
            Posta_Button.Visibility = Visibility.Collapsed;
            Informazioni_Generali_Button.Visibility = Visibility.Collapsed;
            Cose_button.Visibility = Visibility.Collapsed;
            Magazzino_Button.Visibility = Visibility.Collapsed;
            Laboratorio_Button.Visibility = Visibility.Collapsed;
        }

        private void Visible_Button()
        {
            if (scheda != null)
            {
                Persone_Button.Visibility = Visibility.Visible;
                Atti_Button.Visibility = Visibility.Visible;
                Seguiti_Button.Visibility = Visibility.Visible;
                Posta_Button.Visibility = Visibility.Visible;
                Informazioni_Generali_Button.Visibility = Visibility.Visible;
                Cose_button.Visibility = Visibility.Visible;
                Magazzino_Button.Visibility = Visibility.Visible;
                Laboratorio_Button.Visibility = Visibility.Visible;
            }
            else
            {
                Laboratorio_Button.Visibility = Visibility.Visible;
            }
        }
    }
}

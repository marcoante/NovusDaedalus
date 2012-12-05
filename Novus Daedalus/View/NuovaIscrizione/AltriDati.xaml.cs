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

namespace Novus_Daedalus.View.NuovaIscrizione
{
    /// <summary>
    /// Logica di interazione per AltriDati.xaml
    /// </summary>
    public partial class AltriDati : Page
    {
        // Binding sources per i controlli XAML
        // ------------------------------------
        Model.scheda scheda_binding_source;
        Model.notizia_reato notizia_reato;
        // ------------------------------------

        NuovaIscrizione nuova_iscrizione_data;

        private Model.novus_daedalus_dbEntities db_connection;

        public AltriDati()
        {
            InitializeComponent();

            nuova_iscrizione_data = (NuovaIscrizione)Application.Current.Properties["nuova_iscrizione"];
            scheda_binding_source = new Model.scheda();
            notizia_reato = new Model.notizia_reato();
            scheda_binding_source.iscrizione = new Model.iscrizione();
        }

        private void AltriDatiLoaded(object sender, RoutedEventArgs e)
        {
            db_connection = new Model.novus_daedalus_dbEntities();
            Scheda_Grid.DataContext = scheda_binding_source;
            DataIscrizione_Grid.DataContext = scheda_binding_source.iscrizione;
            Iscrizione_Grid1.DataContext = scheda_binding_source.iscrizione;
            Iscrizione_Grid2.DataContext = scheda_binding_source.iscrizione;
        }

        private void AnnullaButtonClick(object sender, RoutedEventArgs e)
        {
            //Si ottiene la finestra corrente e si chiude
            Window closing_window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "NuovaIscrizioneMainWindow");
            closing_window.Close();
        }

        private void IndietroButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CreaSchedaButtonClick(object sender, RoutedEventArgs e)
        {
            // Si controlla la correttezza dei campi inseriti dall'utente
            if (!scheda_binding_source.IsValid || !scheda_binding_source.iscrizione.IsValid)
            {
                MessageBox.Show("Alcuni dati obbligatori sono mancanti.");
                return;
            }

            notizia_reato.Fonte = fonteComboBox.Text;
            notizia_reato.Specie = specieComboBox.Text;
            scheda_binding_source.DataRegistrazione = System.DateTime.Now.Date;
            if (TribRadioButton.IsChecked == true) scheda_binding_source.iscrizione.Ufficio = TribRadioButton.Content.ToString();
            else scheda_binding_source.iscrizione.Ufficio = TribMinRadioButton.Content.ToString();
            scheda_binding_source.iscrizione.Referente = referenteComboBox.Text;
            scheda_binding_source.iscrizione.Stato = statoComboBox.Text;
            scheda_binding_source.iscrizione.ArchiviazioneImmediata = archiviazioneImmediataComboBox.Text;
            scheda_binding_source.iscrizione.IndaginiPG = indaginiPGComboBox.Text;
         
            // Si aggiorna il database
            db_connection.notizia_reato.Add(notizia_reato);
            db_connection.scheda.Add(scheda_binding_source);
            foreach (Model.persona p in nuova_iscrizione_data.Persone_indagate_list)
            {
                p.scheda = scheda_binding_source;
                if (p.indagato.difensore != null)
                    p.indagato.difensore.persona.scheda = scheda_binding_source;
                if (p.indagato.difensore3 != null)
                    p.indagato.difensore3.persona.scheda = scheda_binding_source;
                db_connection.persona.Add(p);
            }

            foreach(Model.persona p in nuova_iscrizione_data.Persone_offese_list)
            {
                p.scheda = scheda_binding_source;
                db_connection.persona.Add(p);
            }

            foreach (Model.reato r in nuova_iscrizione_data.Reati_list)
            {
                r.scheda = scheda_binding_source;
                db_connection.reato.Add(r);
            }

            foreach (Model.PersonaReato pr in nuova_iscrizione_data.Persone_reati_ass)
            {
                pr.scheda = scheda_binding_source;
                db_connection.PersonaReato.Add(pr);

                if (pr.persona.indagato != null)
                {
                    Model.Iscrive iscrive_ass = new Model.Iscrive();
                    iscrive_ass.IdPm = 1;
                    iscrive_ass.notizia_reato = notizia_reato;
                    iscrive_ass.indagato = pr.persona.indagato;
                    iscrive_ass.reato = pr.reato;
                    iscrive_ass.iscrizione = scheda_binding_source.iscrizione;

                    db_connection.Iscrive.Add(iscrive_ass);
                }
            }
            db_connection.SaveChanges();
            // Si imposta la variabile di sessione con l'id della scheda
            Application.Current.Properties["Scheda"] = scheda_binding_source.Id;

            //Si ottiene la finestra corrente e si chiude
            MainWindow closing_window = (MainWindow)Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "NuovaIscrizioneMainWindow");
            closing_window.Close();
            closing_window.On_evento_scheda_creata();
        }
    }
}

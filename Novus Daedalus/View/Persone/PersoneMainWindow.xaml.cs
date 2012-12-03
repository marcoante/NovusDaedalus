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
using System.Windows.Shapes;

namespace Novus_Daedalus.View.Persone
{
    /// <summary>
    /// Logica di interazione per PersoneMainWindow.xaml
    /// </summary>
    public partial class PersoneMainWindow : Window
    {
        private Model.novus_daedalus_dbEntities db_connection;

        // La scheda di riferimento
        private Model.scheda scheda;

        // L'elenco delle persone
        private List<Model.persona> persona_binding_source;

        public PersoneMainWindow()
        {
            InitializeComponent();
        }

        private void PersoneMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            db_connection = new Model.novus_daedalus_dbEntities();
            persona_binding_source = new List<Model.persona>();
            scheda = db_connection.scheda.Find((int)Application.Current.Properties["Scheda"]);

            foreach (Model.persona p in scheda.persona)
            {
                if (p.Ruolo == "indagato" || p.Ruolo == "persona offesa")
                    persona_binding_source.Add(p);
            }

            System.Windows.Data.CollectionViewSource personaViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("personaViewSource")));
            // Caricare i dati impostando la proprietà CollectionViewSource.Source:
            // personaViewSource.Source = [origine dati generica]
            personaViewSource.Source = persona_binding_source;
        }

        private void DatiButtonClick(object sender, RoutedEventArgs e)
        {
            Model.persona selezione = (Model.persona)personaDataGrid.SelectedItem;
            if (selezione.indagato == null)
            {
                MostraPersona dati_persona_window = new MostraPersona(selezione);
                dati_persona_window.ShowDialog();
            }
            else
            {
                MostraIndagato dati_indagato_window = new MostraIndagato(selezione);
                dati_indagato_window.ShowDialog();
            }
        }

        private void RimuoviButtonClick(object sender, RoutedEventArgs e)
        {
            Model.persona selezione = (Model.persona)personaDataGrid.SelectedItem;
            if (selezione.indagato != null)
            {
                MessageBox.Show("Non è possibile rimuovere un indagato.");
                return;
            }
            // Si rimuove la persona selezionata dal database
            db_connection.persona.Remove(selezione);

            db_connection.SaveChanges();

            // Si aggiorna l'elenco delle persone
            persona_binding_source.Remove(selezione);
            personaDataGrid.SelectedItem = null;
            personaDataGrid.Items.Refresh();
        }

        private void ChiudiButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ModificaButtonClick(object sender, RoutedEventArgs e)
        {
            Model.persona selezione = (Model.persona)personaDataGrid.SelectedItem;
            if (selezione == null) MessageBox.Show("Devi selezionare una persona dalla lista, prima di poterla modificare.");
            else
            {
                if (selezione.indagato == null)
                {
                    SetDatiPersona modifica_window = new SetDatiPersona(selezione);
                    // Si registra l'handler per la modifica di una persona
                    modifica_window.evento_p_modificata += new SetPersonaHandler(PersonaModificataHandler);
                    modifica_window.ShowDialog();
                    return;
                }
                SetDatiIndagato modifica_indagato_window = new SetDatiIndagato(selezione);
                modifica_indagato_window.evento_p_modificata += new SetIndagatoHandler(IndagatoModificatoHandler);
                modifica_indagato_window.ShowDialog();
            }

        }

        void PersonaModificataHandler(object sender, DatiPEventArgs dati_evento)
        {
            // Si aggiorna l'elenco delle persone
            persona_binding_source.Clear();
            db_connection = new Model.novus_daedalus_dbEntities();
            scheda = db_connection.scheda.Find((int)Application.Current.Properties["Scheda"]);

            foreach (Model.persona p in scheda.persona)
            {
                if (p.Ruolo != "Difensore")
                    persona_binding_source.Add(p);
            }
            personaDataGrid.SelectedItem = persona_binding_source.Find(item => item.Id == dati_evento.Nuova_persona.Id);
            personaDataGrid.Items.Refresh();
        }

        void IndagatoModificatoHandler(object sender, DatiIndagatoEventArgs dati_evento)
        {
            // Si aggiorna l'elenco delle persone
            persona_binding_source.Clear();
            db_connection = new Model.novus_daedalus_dbEntities();
            scheda = db_connection.scheda.Find((int)Application.Current.Properties["Scheda"]);

            foreach (Model.persona p in scheda.persona)
            {
                if (p.Ruolo != "Difensore")
                    persona_binding_source.Add(p);
            }
            personaDataGrid.SelectedItem = persona_binding_source.Find(item => item.Id == dati_evento.Nuova_persona.Id);
            personaDataGrid.Items.Refresh();
        }

        private void AggiungiButtonClick(object sender, RoutedEventArgs e)
        {
            SetDatiPersona mod_persona_win = new SetDatiPersona();
            SetDatiIndagato mod_indagato_win = new SetDatiIndagato();
            mod_persona_win.evento_p_creata += new SetPersonaHandler(PersonaModificataHandler);
            mod_indagato_win.evento_p_creata += new SetIndagatoHandler(IndagatoModificatoHandler);

            AggiungiPersona aggiungi_p_win = new AggiungiPersona(mod_persona_win, mod_indagato_win);
            aggiungi_p_win.ShowDialog();
        }
    }
}

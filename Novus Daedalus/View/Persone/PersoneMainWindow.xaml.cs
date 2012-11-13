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

        private Model.scheda scheda;

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
            //personaViewSource.Source = scheda.persona;
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
            //selezione = (Model.persona)personaDataGrid.SelectedItem;
            //if (selezione.indagato != null)
            //{
            //    MessageBox.Show("Non è possibile rimuovere un indagato.");
            //    return;
            //}
            //scheda.persona.Remove(selezione);
            //List<Model.persona_atto> pa_ass_list = db_connection.persona_atto.Where(item => item.IdScheda == scheda.Id && item.CodiceFiscalePersona == selezione.CodiceFiscale).ToList();
            //foreach (Model.persona_atto pa in pa_ass_list)
            //{
            //    db_connection.persona_atto.Remove(pa);
            //}

            //List<Model.persona_cosa> pc_ass_list = db_connection.persona_cosa.Where(item => item.IdScheda == scheda.Id && item.CodiceFiscalePersona == selezione.CodiceFiscale).ToList();
            //foreach (Model.persona_cosa pc in pc_ass_list)
            //{
            //    db_connection.persona_cosa.Remove(pc);
            //}

            //List<Model.persona_reato> pr_ass_list = db_connection.persona_reato.Where(item => item.IdScheda == scheda.Id && item.CodiceFiscalePersona == selezione.CodiceFiscale).ToList();
            //foreach (Model.persona_reato pr in pr_ass_list)
            //{
            //    db_connection.persona_reato.Remove(pr);
            //}

            //db_connection.SaveChanges();

            //personaDataGrid.SelectedItem = null;
            //personaDataGrid.Items.Refresh();
        }

        private void ChiudiButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ModificaButtonClick(object sender, RoutedEventArgs e)
        {
            //selezione = (Model.persona)personaDataGrid.SelectedItem;
            //if (selezione == null) MessageBox.Show("Devi selezionare una persona dalla lista, prima di poterla modificare.");
            //else
            //{
            //    if (selezione.indagato == null)
            //    {
            //        SetDatiPersona modifica_window = new SetDatiPersona(selezione);                // Si registra l'handler per la modifica di una persona offesa
            //        modifica_window.evento_p_modificata += new SetPersonaHandler(PersonaModificataHandler);
            //        modifica_window.ShowDialog();
            //        return;
            //    }
            //    MessageBox.Show("La funzione di modifica indagato non è ancora disponibile.");
            //}

        }

        void PersonaModificataHandler(object sender)
        {
           // UpdatePersonaViewSource();

        }

        //private void UpdatePersonaViewSource()
        //{
        //    db_connection = new Model.novus_daedalus_dbEntities();
        //    scheda = db_connection.scheda.Find((int)Application.Current.Properties["Scheda"]);

        //    System.Windows.Data.CollectionViewSource personaViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("personaViewSource")));
        //    // Caricare i dati impostando la proprietà CollectionViewSource.Source:
        //    // personaViewSource.Source = [origine dati generica]
        //    personaViewSource.Source = scheda.persona;
        //    if (selezione != null)
        //    {
        //        Model.persona selected_item = scheda.persona.Where(item => item.CodiceFiscale == selezione.CodiceFiscale).First();
        //        personaDataGrid.SelectedItem = selected_item;
        //        personaDataGrid.Items.Refresh();
        //    }
        //}
    }
}

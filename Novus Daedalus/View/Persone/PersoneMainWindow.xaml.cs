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
        Model.novus_daedalus_dbEntities db_connection;

        Model.scheda scheda;


        public PersoneMainWindow()
        {
            InitializeComponent();
        }

        private void PersoneMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            db_connection = new Model.novus_daedalus_dbEntities();
            scheda = db_connection.scheda.Find((int)Application.Current.Properties["Scheda"]);

            System.Windows.Data.CollectionViewSource personaViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("personaViewSource")));
            // Caricare i dati impostando la proprietà CollectionViewSource.Source:
            // personaViewSource.Source = [origine dati generica]
            personaViewSource.Source = scheda.persona;
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
            scheda.persona.Remove(selezione);
            List<Model.persona_atto> pa_ass_list = db_connection.persona_atto.Where(item => item.IdScheda == scheda.Id && item.CodiceFiscalePersona == selezione.CodiceFiscale).ToList();
            foreach (Model.persona_atto pa in pa_ass_list)
            {
                db_connection.persona_atto.Remove(pa);
            }

            List<Model.persona_cosa> pc_ass_list = db_connection.persona_cosa.Where(item => item.IdScheda == scheda.Id && item.CodiceFiscalePersona == selezione.CodiceFiscale).ToList();
            foreach (Model.persona_cosa pc in pc_ass_list)
            {
                db_connection.persona_cosa.Remove(pc);
            }

            List<Model.persona_reato> pr_ass_list = db_connection.persona_reato.Where(item => item.IdScheda == scheda.Id && item.CodiceFiscalePersona == selezione.CodiceFiscale).ToList();
            foreach (Model.persona_reato pr in pr_ass_list)
            {
                db_connection.persona_reato.Remove(pr);
            }

            db_connection.SaveChanges();

            personaDataGrid.SelectedItem = null;
            personaDataGrid.Items.Refresh();
        }

        private void ChiudiButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

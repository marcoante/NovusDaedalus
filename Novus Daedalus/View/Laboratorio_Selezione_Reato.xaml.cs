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

namespace Novus_Daedalus.View
{
    /// <summary>
    /// Logica di interazione per Laboratorio_Selezione_Reato.xaml
    /// </summary>
    public partial class Laboratorio_Selezione_Reato : Window
    {
        /* Riferimento alla pagina Laboratorio_Autocomposizione che lancia questa finestra,
         utilizzato per bloccare tale pagina*/
        private Laboratorio_Autocomposizione Laboratorio_Pagina = null;

        // Eventi che segnalano alla pagina "LaboratorioAutocomposizione" la selezione di un reato
        public event ReatoSelezionatoHandler evento_reato_selezionato;

        // Lista dei reati da inserire nel data grid
        private List<Model.reato> reati_binding_source;

        public Laboratorio_Selezione_Reato()
        {
            InitializeComponent();
        }

        private void Laboratorio_Selezione_Reato1_Loaded_1(object sender, RoutedEventArgs e)
        {
            List<Model.reato> reati_list = new List<Model.reato>();
            Model.reato r = new Model.reato();
            r.Codice = "codice reato";
            r.NomenIuris = "aet. 1";
            reati_list.Add(r);
            reati_binding_source = reati_list;

            Novus_Daedalus.novus_daedalus_dbDataSet novus_daedalus_dbDataSet = ((Novus_Daedalus.novus_daedalus_dbDataSet)(this.FindResource("novus_daedalus_dbDataSet")));
            // Carica i dati nella tabella reato. Se necessario, è possibile modificare questo codice.
            Novus_Daedalus.novus_daedalus_dbDataSetTableAdapters.reatoTableAdapter novus_daedalus_dbDataSetreatoTableAdapter = new Novus_Daedalus.novus_daedalus_dbDataSetTableAdapters.reatoTableAdapter();
            novus_daedalus_dbDataSetreatoTableAdapter.Fill(novus_daedalus_dbDataSet.reato);
            System.Windows.Data.CollectionViewSource reatoViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("reatoViewSource")));
            //reatoViewSource.View.MoveCurrentToFirst();
            reatoViewSource.Source = reati_binding_source;
        }

        private void Apri_Button_Click(object sender, RoutedEventArgs e)
        {
            On_evento_reato_selezionato(new DatiReatoEventArgs((Model.reato)reatoDataGrid.SelectedItem));
            //chiudo questa finestra
            this.Close();
        }

        protected virtual void On_evento_reato_selezionato(DatiReatoEventArgs e)
        {
            if (evento_reato_selezionato != null) evento_reato_selezionato(this, e);
        }

        private void Chiudi_Button_Click(object sender, RoutedEventArgs e)
        {
            /*
             * il sistema torna alla Deadalus_Main_Page
             */
            //Laboratorio_Pagina.NavigationService.GoBack();
            this.Close();
        }
    }

    // Gestione degli eventi per la selezione di un reato
    public delegate void ReatoSelezionatoHandler(object sender, DatiReatoEventArgs e);

    public class DatiReatoEventArgs : EventArgs
    {
        Model.reato reato_selezionato;

        public DatiReatoEventArgs(Model.reato reato)
        {
            reato_selezionato = reato;
        }

        public Model.reato Reato_selezionato
        {
            get { return reato_selezionato; }
            set { reato_selezionato = value; }
        }
    }
}

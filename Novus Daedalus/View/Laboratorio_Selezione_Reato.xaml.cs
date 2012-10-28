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

        public Laboratorio_Selezione_Reato(Laboratorio_Autocomposizione pagina)
        {
            InitializeComponent();
            this.Laboratorio_Pagina = pagina;
            Laboratorio_Pagina.IsEnabled = false;
        }

        private void Laboratorio_Selezione_Reato1_Loaded_1(object sender, RoutedEventArgs e)
        {

            Novus_Daedalus.novus_daedalus_dbDataSet novus_daedalus_dbDataSet = ((Novus_Daedalus.novus_daedalus_dbDataSet)(this.FindResource("novus_daedalus_dbDataSet")));
            // Carica i dati nella tabella reato. Se necessario, è possibile modificare questo codice.
            Novus_Daedalus.novus_daedalus_dbDataSetTableAdapters.reatoTableAdapter novus_daedalus_dbDataSetreatoTableAdapter = new Novus_Daedalus.novus_daedalus_dbDataSetTableAdapters.reatoTableAdapter();
            novus_daedalus_dbDataSetreatoTableAdapter.Fill(novus_daedalus_dbDataSet.reato);
            System.Windows.Data.CollectionViewSource reatoViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("reatoViewSource")));
            reatoViewSource.View.MoveCurrentToFirst();
        }

        private void Apri_Button_Click(object sender, RoutedEventArgs e)
        {
            //imposto la variabile reato della pagina di provenienza

            //rendo la pagina di provenienza di nuovo attiva
            Laboratorio_Pagina.IsEnabled = true;
            this.Close();
        }

        private void Chiudi_Button_Click(object sender, RoutedEventArgs e)
        {
            /*senza rendere la pagina di provenienza di nuovo attiva,
             * il sistema torna alla Deadalus_Main_Page
             */
            Laboratorio_Pagina.NavigationService.GoBack();
            this.Close();
        }
    }
}

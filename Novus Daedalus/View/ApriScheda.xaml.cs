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
    /// Logica di interazione per Apri_Scheda.xaml
    /// </summary>
    public partial class ApriScheda : Window
    {
        //Connessione al db
        Model.novus_daedalus_dbEntities DB_connection = null;

        // Eventi che segnalano alla pagina "DaedalusMainPage" la selezione di una scheda
        public event SchedaSelezionataHandler evento_scheda_selezionata;

        public ApriScheda()
        {
            InitializeComponent();
        }

        private void ApriSchedaLoaded(object sender, RoutedEventArgs e)
        {
            DB_connection = new Model.novus_daedalus_dbEntities();

            System.Windows.Data.CollectionViewSource schedaViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("schedaViewSource")));
            // Caricare i dati impostando la proprietà CollectionViewSource.Source:
            // schedaViewSource.Source = [origine dati generica]
            schedaViewSource.Source = DB_connection.scheda.ToList();
        }

        private void AnnullaButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ApriButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["Scheda"] = ((Model.scheda)schedaDataGrid.SelectedItem).Id;
            On_evento_scheda_selezionata();
            this.Close();
        }

        protected virtual void On_evento_scheda_selezionata()
        {
            if (evento_scheda_selezionata!= null) evento_scheda_selezionata(this);
        }
    }

    // Gestione degli eventi per la selezione di un reato
    public delegate void SchedaSelezionataHandler(object sender);
}

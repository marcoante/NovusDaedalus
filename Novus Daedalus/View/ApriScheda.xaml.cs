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
        public ApriScheda()
        {
            InitializeComponent();
        }

        private void ApriSchedaLoaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource schedaViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("schedaViewSource")));
            // Caricare i dati impostando la proprietà CollectionViewSource.Source:
            // schedaViewSource.Source = [origine dati generica]
            schedaViewSource.Source = ((Model.novus_daedalus_dbEntities)Application.Current.Properties["db_connection"]).scheda.ToList();
        }

        private void AnnullaButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ApriButtonClick(object sender, RoutedEventArgs e)
        {
        }
    }
}

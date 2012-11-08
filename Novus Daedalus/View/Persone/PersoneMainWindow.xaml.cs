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

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            db_connection = new Model.novus_daedalus_dbEntities();
            scheda = db_connection.scheda.Find((int)Application.Current.Properties["Scheda"]);

            System.Windows.Data.CollectionViewSource personaViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("personaViewSource")));
            // Caricare i dati impostando la proprietà CollectionViewSource.Source:
            // personaViewSource.Source = [origine dati generica]
            personaViewSource.Source = scheda.persona;
        }
    }
}

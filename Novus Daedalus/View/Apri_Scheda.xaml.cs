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
    public partial class Apri_Scheda : Window
    {
        private Controller.Scheda scheda_controller;

        public Apri_Scheda()
        {
            InitializeComponent();
            scheda_controller = new Controller.Scheda();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource schedaViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("schedaViewSource")));
            // Caricare i dati impostando la proprietà CollectionViewSource.Source:
            // schedaViewSource.Source = [origine dati generica]
            schedaViewSource.Source = scheda_controller.binding_source;
        }

        private void Annulla_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Apri_Button_Click(object sender, RoutedEventArgs e)
        {
            scheda_controller.Apri();
        }
    }
}

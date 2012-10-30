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

namespace Novus_Daedalus.View
{
    /// <summary>
    /// Logica di interazione per Laboratorio_Autocomposizione.xaml
    /// </summary>
    public partial class Laboratorio_Autocomposizione : Page
    {
        /*variabile che indica il tipo di reato selezionato, se null nessun reato è stato
        selezionato, quindi bisogna ritornare errore
        */ 
        private Model.reato reato = null;

        public Laboratorio_Autocomposizione()
        {
            InitializeComponent();
        }

        private void Laboratorio_Autocomposizione_Loaded(object sender, RoutedEventArgs e)
        {
            new Laboratorio_Selezione_Reato(this).Show();
        }

        private void Chiudi_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Menù_principale_Button_Click(object sender, RoutedEventArgs e)
        {
            new Laboratorio_Avviso_Chiusura(this).Show();
        }
    }
}

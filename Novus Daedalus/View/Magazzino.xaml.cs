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
    /// Logica di interazione per Magazzino.xaml
    /// </summary>
    public partial class Magazzino : Page
    {
        public Magazzino()
        {
            InitializeComponent();
        }

        private void Chiudi_Magazzino_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Esame_Imputazioni_Button_Click(object sender, RoutedEventArgs e)
        {
            new Magazzino_Selezione_Imputazioni(this, "esame").Show();
        }

        private void Modifica_Button_Click(object sender, RoutedEventArgs e)
        {
            new Magazzino_Selezione_Imputazioni(this, "modifica").Show();
        }
    }
}

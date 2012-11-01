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
    /// Logica di interazione per Selezione_Tipo_Atti.xaml
    /// </summary>
    public partial class Selezione_Tipo_Atti : Window
    {
        public Selezione_Tipo_Atti()
        {
            InitializeComponent();
        }

        private void Sviluppo_Click(object sender, RoutedEventArgs e)
        {
            Griglia.RowDefinitions[1].Height = new GridLength(172, GridUnitType.Star);
        }

        private void Gestione_Click(object sender, RoutedEventArgs e)
        {
            Griglia.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
        }

        private void Conclusione_Click(object sender, RoutedEventArgs e)
        {
            Griglia.RowDefinitions[1].Height = new GridLength(0, GridUnitType.Star);
        }
    }
}

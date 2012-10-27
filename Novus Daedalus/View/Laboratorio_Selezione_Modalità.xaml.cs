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
    /// Logica di interazione per Laboratorio_Selezione_Modalità.xaml
    /// </summary>
    public partial class Laboratorio_Selezione_Modalità : Window
    {
        DaedalusMainPage Main_Page_Istance = null;

        public Laboratorio_Selezione_Modalità(DaedalusMainPage Main_Page_Istance)
        {
            InitializeComponent();
            this.Main_Page_Istance = Main_Page_Istance;
        }

        private void Autocomposizione_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Page_Istance.NavigationService.Navigate(new Laboratorio_Autocomposizione());
            this.Close();
        }

        private void Assemblaggio_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Page_Istance.NavigationService.Navigate(new Laboratorio_Assemblaggio());
            this.Close();
        }
    }
}

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
    /// Logica di interazione per Officina.xaml
    /// </summary>
    public partial class Officina : Page
    {
        private Controller.Officina Officina_Controller;
        public Officina()
        {
            InitializeComponent();
            Officina_Controller = new Controller.Officina();

        }

        private void Chiudi_Button_Click(object sender, RoutedEventArgs e)
        {
            Officina_Controller.chiudi(NavigationService);
        }
    }
}

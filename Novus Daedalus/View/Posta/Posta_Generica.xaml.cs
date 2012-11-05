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

namespace Novus_Daedalus.View.Posta
{
    /// <summary>
    /// Logica di interazione per Posta_Generica.xaml
    /// </summary>
    public partial class Posta_Generica : Page
    {
        public Posta_Generica()
        {
            InitializeComponent();
        }

        private void Indietro_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

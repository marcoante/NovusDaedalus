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
    /// Logica di interazione per Posta_Selezione_Tipo.xaml
    /// </summary>
    public partial class Posta_Selezione_Tipo : Page
    {
        public Posta_Selezione_Tipo()
        {
            InitializeComponent();
        }

        private void Ufficio_Giudiziario_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Posta_Ufficio_Giudiziario());
        }

        private void Generica_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Posta_Generica());
        }
    }
}

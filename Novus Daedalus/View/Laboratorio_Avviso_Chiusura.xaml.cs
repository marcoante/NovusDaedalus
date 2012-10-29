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
    /// Logica di interazione per Laboratorio_Avviso_Chiusura.xaml
    /// </summary>
    public partial class Laboratorio_Avviso_Chiusura : Window
    {
        private Laboratorio_Autocomposizione pagina = null;

        public Laboratorio_Avviso_Chiusura(Laboratorio_Autocomposizione pagina_laboratorio)
        {
            InitializeComponent();
            pagina = pagina_laboratorio;
            //disabilito la pagina di provenienza per prevenire modifiche
            pagina.IsEnabled = false;
        }

        private void Chiudi_Button_Click(object sender, RoutedEventArgs e)
        {
            pagina.NavigationService.GoBack();
            this.Close();
        }
    }
}

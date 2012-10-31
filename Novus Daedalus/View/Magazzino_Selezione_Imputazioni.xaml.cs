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
    /// Logica di interazione per Magazzino_Selezione_Imputazioni.xaml
    /// </summary>
    public partial class Magazzino_Selezione_Imputazioni : Window
    {
        private Magazzino pagina = null;
        public Magazzino_Selezione_Imputazioni(Magazzino pagina)
        {
            InitializeComponent();
            this.pagina = pagina;
        }

        private void Magazzino_Selezione_Imputazioni_Loaded(object sender, RoutedEventArgs e)
        {
            //disabilito la pagina di provenienza
            pagina.IsEnabled = false;
        }

        private void Annulla_Button_Click(object sender, RoutedEventArgs e)
        {
            //riabilito la pagina di provenienza
            pagina.IsEnabled = true;

            //chiudo questa finestra
            this.Close();
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            //riabilito la pagina di provenienza
            pagina.IsEnabled = true;
            
            //lancio la schermata per l'esame delle imputazioni (AGGIUNGERE PARAMTERI)
            new Magazzino_Esame_Imputazioni().Show();

            //chiudo questa finestra
            this.Close();
        }
    }
}

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

namespace Novus_Daedalus.View.Impostazioni
{
    /// <summary>
    /// Logica di interazione per Impostazioni.xaml
    /// </summary>
    public partial class Impostazioni : Page
    {
        public Impostazioni()
        {
            InitializeComponent();
        }

        private void Chiudi_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Sedi_Dibattimento_Button_Click(object sender, RoutedEventArgs e)
        {
            SediDibattimenti.SediDibattimenti_MainWindow Impostazione_Sedi_Dibattimento = new SediDibattimenti.SediDibattimenti_MainWindow();
            Impostazione_Sedi_Dibattimento.ShowDialog();
        }

        private void Qualità_Utente_Button_Click(object sender, RoutedEventArgs e)
        {
            Utente.Utente_MainWindow Impostazione_Utente = new Utente.Utente_MainWindow();
            Impostazione_Utente.ShowDialog();
        }

        private void Referenti_Button_Click(object sender, RoutedEventArgs e)
        {
            Referenti.MainWindow Impostazione_Referenti = new Referenti.MainWindow();
            Impostazione_Referenti.ShowDialog();
        }
    }
}

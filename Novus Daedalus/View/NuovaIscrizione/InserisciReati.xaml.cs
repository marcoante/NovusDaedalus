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

namespace Novus_Daedalus.View.NuovaIscrizione
{
    /// <summary>
    /// Logica di interazione per InserisciReati.xaml
    /// </summary>
    public partial class InserisciReati : Page
    {
        private NuovaIscrizione nuova_iscrizione_data;

        public InserisciReati()
        {
            InitializeComponent();
        }

        public InserisciReati(NuovaIscrizione nuova_iscrizione_data)
        {
            InitializeComponent();
            this.nuova_iscrizione_data = nuova_iscrizione_data;
        }

        private void AggiungiButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void ModificaButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void RimuoviButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void IndietroButtonClick(object sender, RoutedEventArgs e)
        {
            nuova_iscrizione_data.Reati_list.Clear();
            NavigationService.GoBack();
        }

        private void AnnullaButtonClick(object sender, RoutedEventArgs e)
        {
            //Si ottiene la finestra corrente e si chiude
            Window closing_window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "NuovaIscrizioneMainWindow");
            closing_window.Close();
        }

        private void AvantiButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}

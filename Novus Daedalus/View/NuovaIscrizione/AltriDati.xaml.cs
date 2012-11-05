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
    /// Logica di interazione per AltriDati.xaml
    /// </summary>
    public partial class AltriDati : Page
    {
        Model.scheda scheda_binding_source;
        Model.notizia_reato notizia_reato_binding_source;

        NuovaIscrizione nuova_iscrizione_data;

        public AltriDati()
        {
            InitializeComponent();
            nuova_iscrizione_data = (NuovaIscrizione)Application.Current.Properties["nuova_iscrizione"];
            scheda_binding_source = new Model.scheda();
            notizia_reato_binding_source = new Model.notizia_reato();
        }

        private void AltriDatiLoaded(object sender, RoutedEventArgs e)
        {
            Window_Grid.DataContext = scheda_binding_source;
            Fonte_Grid.DataContext = notizia_reato_binding_source;
            Specie_Grid.DataContext = notizia_reato_binding_source;
        }

        private void AnnullaButtonClick(object sender, RoutedEventArgs e)
        {
            //Si ottiene la finestra corrente e si chiude
            Window closing_window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "NuovaIscrizioneMainWindow");
            closing_window.Close();
        }

        private void IndietroButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CreaSchedaButtonClick(object sender, RoutedEventArgs e)
        {
            nuova_iscrizione_data.Nuova_scheda = scheda_binding_source;
            nuova_iscrizione_data.Notizia_reato = notizia_reato_binding_source;
            //nuova_iscrizione_data.Scheda_SaveChanges();
        }
    }
}

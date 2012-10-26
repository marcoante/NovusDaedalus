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
    /// Logica di interazione per DaedalusMainPage.xaml
    /// </summary>
    public partial class DaedalusMainPage : Page
    {
        private Controller.Daedalus_Main_Page Main_Page_Controller;
        public DaedalusMainPage()
        {
            InitializeComponent();
            this.Benvenuto_label.Content = "benvenuto " + ((Model.utenti) Application.Current.Properties["User"]).persona.Nome;
            Main_Page_Controller = new Controller.Daedalus_Main_Page();
        }

        private void Persone_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Page_Controller.Apri_Persone();
        }

        private void Laboratorio_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Page_Controller.Apri_Laboratorio();
        }

        private void Seguiti_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Page_Controller.Apri_Seguiti();
        }

        private void Atti_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Page_Controller.Apri_Atti();
        }

        private void Magazzino_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Page_Controller.Apri_Magazzino();
        }

        private void Apri_Scheda_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Page_Controller.Apri_Scheda();
        }

        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Page_Controller.Logout(NavigationService);
        }

        private void Officina_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Page_Controller.Apri_Officina(NavigationService);
        }
    }
}

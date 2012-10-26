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

namespace Novus_Daedalus
{
    /// <summary>
    /// Logica di interazione per Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private Controller.Login login_controller;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            login_controller = new Controller.Login();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            login_controller.execute_login(Username_Textbox.Text, Password_Textbox.Password, this.NavigationService);
        }

        private void Crea_utente_button_Click(object sender, RoutedEventArgs e)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider hash_provider = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashing_data = System.Text.Encoding.ASCII.GetBytes("ao");
            hashing_data = hash_provider.ComputeHash(hashing_data);
            String password_md5_hash = System.Text.Encoding.ASCII.GetString(hashing_data);

            //inserimento utente prova
            Model.utenti utente_prova = new Model.utenti();
            utente_prova.username = "mariorossi";
            utente_prova.password = password_md5_hash;
            utente_prova.CF = "ciao";
            //

            ((Model.novus_daedalus_dbEntities)Application.Current.Properties["db_connection"]).utenti.Add(utente_prova);
            ((Model.novus_daedalus_dbEntities)Application.Current.Properties["db_connection"]).SaveChanges();
        }
    }
}

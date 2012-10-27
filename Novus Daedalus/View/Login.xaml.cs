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
    /// Logica di interazione per Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            Model.utente user_query_result = new Model.utente();

            System.Security.Cryptography.MD5CryptoServiceProvider hash_provider = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashing_data = System.Text.Encoding.ASCII.GetBytes(Password_Textbox.Password);
            hashing_data = hash_provider.ComputeHash(hashing_data);
            String password_md5_hash = System.Text.Encoding.ASCII.GetString(hashing_data);

            user_query_result = ((Model.novus_daedalus_dbEntities)Application.Current.Properties["db_connection"]).utente.Find(Username_Textbox.Text);
            if (user_query_result != null)
            {
                if (user_query_result.Password.Equals(password_md5_hash))
                {
                    //salvataggio utente
                    Application.Current.Properties["User"] = user_query_result;
                    NavigationService.Navigate(new View.DaedalusMainPage());   
                }
                else MessageBox.Show("username o password errate");
            }
            else MessageBox.Show("username o password errate");
        }

        private void CreaUtenteButtonClick(object sender, RoutedEventArgs e)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider hash_provider = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashing_data = System.Text.Encoding.ASCII.GetBytes("ao");
            hashing_data = hash_provider.ComputeHash(hashing_data);
            String password_md5_hash = System.Text.Encoding.ASCII.GetString(hashing_data);

            //inserimento utente prova
            Model.utente utente_prova = new Model.utente();
            utente_prova.Username = "mariorossi";
            utente_prova.Password = password_md5_hash;
            utente_prova.CodiceFiscaleUtente = "ciao";
            utente_prova.persona = ((Model.novus_daedalus_dbEntities)Application.Current.Properties["db_connection"]).persona.Find(utente_prova.CodiceFiscaleUtente);
            //

            ((Model.novus_daedalus_dbEntities)Application.Current.Properties["db_connection"]).utente.Add(utente_prova);
            ((Model.novus_daedalus_dbEntities)Application.Current.Properties["db_connection"]).SaveChanges();
        }
    }
}

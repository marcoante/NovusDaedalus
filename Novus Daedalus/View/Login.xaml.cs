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
        private Model.novus_daedalus_dbEntities db_connection;
        //private Novus_Daedalus.PasswordHash.PasswordHash pass_hash;

        public Login()
        {
            InitializeComponent();
            db_connection = new Model.novus_daedalus_dbEntities();
            //pass_hash = new PasswordHash.PasswordHash();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            Model.utente user_query_result = new Model.utente();

            /*System.Security.Cryptography.MD5CryptoServiceProvider hash_provider = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashing_data = System.Text.Encoding.ASCII.GetBytes(Password_Textbox.Password);
            hashing_data = hash_provider.ComputeHash(hashing_data);
            String password_md5_hash = System.Text.Encoding.ASCII.GetString(hashing_data);*/

            user_query_result = db_connection.utente.Find(Username_Textbox.Text);
            if (user_query_result != null)
            {
                /*if (user_query_result.Password.Equals(password_md5_hash))*/
                if (Novus_Daedalus.PasswordHash.PasswordHash.ValidatePassword(Password_Textbox.Password, user_query_result.Password))
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
            /*System.Security.Cryptography.MD5CryptoServiceProvider hash_provider = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashing_data = System.Text.Encoding.ASCII.GetBytes("ao");
            hashing_data = hash_provider.ComputeHash(hashing_data);
            String password_md5_hash = System.Text.Encoding.ASCII.GetString(hashing_data);*/
            string password = Novus_Daedalus.PasswordHash.PasswordHash.CreateHash("ao");

            //inserimento utente prova
            Model.utente utente_prova = new Model.utente();
            utente_prova.Username = "mariorossi";
            //utente_prova.Password = password_md5_hash;
            utente_prova.Password = password;
            utente_prova.Id = 1;
            utente_prova.persona = db_connection.persona.Find(utente_prova.Id);
            //

            db_connection.utente.Add(utente_prova);
            db_connection.SaveChanges();
        }

        private void Esci_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

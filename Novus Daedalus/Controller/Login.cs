using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;
using System.Windows.Navigation;

namespace Novus_Daedalus.Controller
{
    public class Login
    {
        public void execute_login(string username, string password, NavigationService navigator)
        {
            Model.utenti u = new Model.utenti();

            System.Security.Cryptography.MD5CryptoServiceProvider hash_provider = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashing_data = System.Text.Encoding.ASCII.GetBytes(password);
            hashing_data = hash_provider.ComputeHash(hashing_data);
            String password_md5_hash = System.Text.Encoding.ASCII.GetString(hashing_data);

            u = ((Model.novus_daedalus_dbEntities)Application.Current.Properties["db_connection"]).utenti.Find(username);
            if (u != null)
            {
                if (u.password.Equals(password_md5_hash))
                {
                    //salvataggio utente
                    Application.Current.Properties["User"] = u;
                    navigator.Navigate(new View.DaedalusMainPage());   
                }
                else MessageBox.Show("username o password errate");
            }
            else MessageBox.Show("username o password errate");
        }   
    }
}

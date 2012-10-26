using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Novus_Daedalus.Controller
{
    class Daedalus_Main_Page
    {
        public void Apri_Persone()
        {
            if (Application.Current.Properties["Scheda"] == null)
            {
                MessageBox.Show("Non hai selezionato alcuna scheda, premi il pulsante \"Apri scheda\" " +
                    "oppure il pulsante \"Nuova iscrizione\"");
                return;
            }
        }

        public void Apri_Atti()
        {
            if (Application.Current.Properties["Scheda"] == null)
            {
                MessageBox.Show("Non hai selezionato alcuna scheda, premi il pulsante \"Apri scheda\" " +
                    "oppure il pulsante \"Nuova iscrizione\"");
                return;
            }
        }

        public void Apri_Magazzino()
        {
            if (Application.Current.Properties["Scheda"] == null)
            {
                MessageBox.Show("Non hai selezionato alcuna scheda, premi il pulsante \"Apri scheda\" " +
                    "oppure il pulsante \"Nuova iscrizione\"");
                return;
            }
        }

        public void Apri_Seguiti()
        {
            if (Application.Current.Properties["Scheda"] == null)
            {
                MessageBox.Show("Non hai selezionato alcuna scheda, premi il pulsante \"Apri scheda\" " +
                    "oppure il pulsante \"Nuova iscrizione\"");
                return;
            }
        }

        public void Apri_Laboratorio()
        {
            if (Application.Current.Properties["Scheda"] == null)
            {
                MessageBox.Show("Non hai selezionato alcuna scheda, premi il pulsante \"Apri scheda\" " +
                    "oppure il pulsante \"Nuova iscrizione\"");
                return;
            }
        }

        public void Apri_Scheda()
        {
            new View.Apri_Scheda().Show();
        }

        public void Apri_Officina(NavigationService navigator)
        {
            navigator.Navigate(new View.Officina());
        }

        public void Logout(NavigationService navigator)
        {
            Application.Current.Properties["User"] = null;
            navigator.GoBack();
        }
    }
}

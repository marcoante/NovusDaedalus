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

        private SetDatiReato set_dati_reato_window;

        private AltriDati altri_dati_window;

        public InserisciReati()
        {
            InitializeComponent();
        }

        private void InserisciReatiLoaded(object sender, RoutedEventArgs e)
        {
            nuova_iscrizione_data = (NuovaIscrizione)Application.Current.Properties["nuova_iscrizione"];
            nuova_iscrizione_data.Nuova_scheda = null;
            nuova_iscrizione_data.Notizia_reato = null;

            System.Windows.Data.CollectionViewSource reatiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("reatiViewSource")));
            reatiViewSource.Source = nuova_iscrizione_data.Reati_list;
        }


        private void AggiungiButtonClick(object sender, RoutedEventArgs e)
        {
            set_dati_reato_window = new SetDatiReato();
            // Si registra l'handler per l'inserimento di un nuovo reato
            set_dati_reato_window.evento_reato_creato += new SetReatoHandler(ReatoCreatoHandler);
            set_dati_reato_window.ShowDialog();
        }

        void ReatoCreatoHandler(object sender, DatiReatoEventArgs dati_evento)
        {
            // Si aggiunge il reato nelle informazioni sulla nuova iscrizione
            nuova_iscrizione_data.Reati_list.Add(dati_evento.Nuovo_reato);
            // Si creano le associazioni tra il reato e le persone associate
            foreach (Model.persona p in dati_evento.Persone_indagate_associate)
            {
                Model.PersonaReato ass = new Model.PersonaReato();
                ass.persona = p;
                ass.reato = dati_evento.Nuovo_reato;
                nuova_iscrizione_data.Persone_reati_ass.Add(ass);
            }

            foreach (Model.persona p in dati_evento.Persone_offese_associate)
            {
                Model.PersonaReato ass = new Model.PersonaReato();
                ass.persona = p;
                ass.reato = dati_evento.Nuovo_reato;
                nuova_iscrizione_data.Persone_reati_ass.Add(ass);
            }
            // Si aggiorna il data grid contenete l'elenco dei nuovi indagati
            reatoDataGrid.SelectedItem = dati_evento.Nuovo_reato;
            reatoDataGrid.Items.Refresh();
        }



        private void ModificaButtonClick(object sender, RoutedEventArgs e)
        {
            if (reatoDataGrid.SelectedItem == null) MessageBox.Show("Devi selezionare un reato dalla lista, prima di poterlo modificare.");
            else
            {
                set_dati_reato_window = new SetDatiReato((Model.reato)reatoDataGrid.SelectedItem);
                // Si registra l'handler per la modifica di un reato
                set_dati_reato_window.evento_reato_modificato += new SetReatoHandler(ReatoModificatoHandler);
                set_dati_reato_window.ShowDialog();
            }
        }

        void ReatoModificatoHandler(object sender, DatiReatoEventArgs dati_evento)
        {
            // Si rimuove dall'elenco l'oggetto corrispondente al reato da modificare
            nuova_iscrizione_data.Reati_list.Remove((Model.reato) reatoDataGrid.SelectedItem);
            // Si inserisce nell'elenco il reato modificato
            nuova_iscrizione_data.Reati_list.Add(dati_evento.Nuovo_reato);
            // Si aggiornano le associazioni tra il reato e le persone
            foreach (Model.persona p in nuova_iscrizione_data.Persone_indagate_list)
            {
                nuova_iscrizione_data.Persone_reati_ass.RemoveAll(r => r.reato.NomenIuris == dati_evento.Reato_originale.NomenIuris && r.persona.Equals(p));
            }
            foreach (Model.persona p in dati_evento.Persone_indagate_associate)
            {
                Model.PersonaReato ass = new Model.PersonaReato();
                ass.persona = p;
                ass.reato = dati_evento.Nuovo_reato;
                nuova_iscrizione_data.Persone_reati_ass.Add(ass);
            }

            foreach (Model.persona p in nuova_iscrizione_data.Persone_offese_list)
            {
                nuova_iscrizione_data.Persone_reati_ass.RemoveAll(r => r.reato.NomenIuris == dati_evento.Reato_originale.NomenIuris && r.persona.Equals(p));
            }
            foreach (Model.persona p in dati_evento.Persone_offese_associate)
            {
                Model.PersonaReato ass = new Model.PersonaReato();
                ass.persona = p;
                ass.reato = dati_evento.Nuovo_reato;
                nuova_iscrizione_data.Persone_reati_ass.Add(ass);
            }
            // Si aggiorna il data grid contenete l'elenco dei nuovi indagati
            reatoDataGrid.SelectedItem = dati_evento.Nuovo_reato;
            reatoDataGrid.Items.Refresh();
        }



        private void RimuoviButtonClick(object sender, RoutedEventArgs e)
        {
            if (reatoDataGrid.SelectedItem == null) MessageBox.Show("Devi selezionare un reato dalla lista, prima di poterlo rimuovere.");
            else
            {
                // Si rimuove dall'elenco il reato selezionato
                Model.reato reato_da_rimuovere = (Model.reato)reatoDataGrid.SelectedItem;
                nuova_iscrizione_data.Reati_list.Remove(reato_da_rimuovere);
                nuova_iscrizione_data.Persone_reati_ass.RemoveAll(item => item.reato.NomenIuris == reato_da_rimuovere.NomenIuris);
                // Si aggiorna il data grid contenete l'elenco dei nuovi reati
                reatoDataGrid.SelectedItem = null;
                reatoDataGrid.Items.Refresh();

            }
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
            if (checkReatiIndagati() == false)
                return;
            if (checkReatiOffesi() == false)
                return;
            altri_dati_window = new AltriDati();
            NavigationService.Navigate(altri_dati_window);
        }

        public bool checkReatiIndagati()
        {
            foreach (Model.persona p in nuova_iscrizione_data.Persone_indagate_list)
            {
                if (nuova_iscrizione_data.Persone_reati_ass.Find(item => item.persona == p) == null)
                {
                    MessageBox.Show("Attenzione, non è stato associato alcun reato a carico dell'indagato " +
                        p.Nome + " " + p.Cognome);
                    return false;
                }
            }
            return true;
        }

        public bool checkReatiOffesi()
        {
            foreach (Model.persona p in nuova_iscrizione_data.Persone_offese_list)
            {
                if (nuova_iscrizione_data.Persone_reati_ass.Find(item => item.persona == p) == null)
                {
                    MessageBox.Show("Attenzione, non è stato associato alcun reato alla persona offesa " +
                        p.Nome + " " + p.Cognome);
                    return false;
                }
            }
            return true;
        }
    }
}

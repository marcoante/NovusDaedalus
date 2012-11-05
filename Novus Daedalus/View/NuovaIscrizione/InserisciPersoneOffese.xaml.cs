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
    /// Logica di interazione per InserisciPersoneOffese.xaml
    /// </summary>
    public partial class InserisciPersoneOffese : Page
    {
        // Informazioni sulla nuova iscrizione, oggetto usato per il passaggio di dati tra pagine
        private NuovaIscrizione nuova_iscrizione_data;
        // Lista delle persone offese da inserire nel data grid
        private List<Model.persona> persone_offese_binding_source;
        // Finestra per l'inserimento dei dati di una persona offesa
        private SetDatiPO set_dati_po_window;

        public InserisciPersoneOffese()
        {
            InitializeComponent();
        }

        private void InserisciPersoneOffeseLoaded(object sender, RoutedEventArgs e)
        {
            nuova_iscrizione_data = (NuovaIscrizione)Application.Current.Properties["nuova_iscrizione"];
            persone_offese_binding_source = nuova_iscrizione_data.Persone_offese_list;

            nuova_iscrizione_data.Reati_list.Clear();
            nuova_iscrizione_data.Persone_reati_ass.Clear();

            System.Windows.Data.CollectionViewSource persona_offesaViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("persona_offesaViewSource")));
            // Caricare i dati impostando la proprietà CollectionViewSource.Source:
            // notizia_reatoViewSource.Source = [origine dati generica]
            persona_offesaViewSource.Source = persone_offese_binding_source;
        }

        private void AggiungiButtonClick(object sender, RoutedEventArgs e)
        {
            set_dati_po_window = new SetDatiPO();
            // Si registra l'handler per l'inserimento di una nuova persona offesa
            set_dati_po_window.evento_po_creata += new SetPOHandler(POCreataHandler);
            set_dati_po_window.ShowDialog();
        }

        void POCreataHandler(object sender, DatiPOEventArgs dati_evento)
        {
            // Si aggiunge la persona offesa nelle informazioni sulla nuova iscrizione
            nuova_iscrizione_data.Persone_offese_list.Add(dati_evento.Nuova_persona_offesa);
            // Si aggiorna il data grid contenete l'elenco delle persone offese
            persona_offesaDataGrid.SelectedItem = dati_evento.Nuova_persona_offesa;
            persona_offesaDataGrid.Items.Refresh();
        }

        private void ModificaButtonClick(object sender, RoutedEventArgs e)
        {
            if (persona_offesaDataGrid.SelectedItem == null) MessageBox.Show("Devi selezionare una persona offesa dalla lista, prima di poterla modificare.");
            else
            {
                set_dati_po_window = new SetDatiPO((Model.persona)persona_offesaDataGrid.SelectedItem);
                // Si registra l'handler per la modifica di una persona offesa
                set_dati_po_window.evento_po_modificata += new SetPOHandler(POModificataHandler);
                set_dati_po_window.ShowDialog();
            }
        }

        void POModificataHandler(object sender, DatiPOEventArgs dati_evento)
        {
            // Si rimuove dall'elenco l'oggetto corrispondente alla persona offesa da modificare
            nuova_iscrizione_data.Persone_offese_list.RemoveAll(item => item.CodiceFiscale == dati_evento.Persona_offesa_originale.CodiceFiscale);
            // Si inserisce nell'elenco la persona offesa modificata
            nuova_iscrizione_data.Persone_offese_list.Add(dati_evento.Nuova_persona_offesa);
            // Si aggiorna il data grid contenete l'elenco delle persone offese
            persona_offesaDataGrid.SelectedItem = dati_evento.Nuova_persona_offesa;
            persona_offesaDataGrid.Items.Refresh();
        }

        private void RimuoviButtonClick(object sender, RoutedEventArgs e)
        {
            if (persona_offesaDataGrid.SelectedItem == null) MessageBox.Show("Devi selezionare una persona offesa dalla lista, prima di poterla rimuovere.");
            else
            {
                // Si rimuove dall'elenco la persona offesa selezionata
                Model.persona persona_da_rimuovere = (Model.persona)persona_offesaDataGrid.SelectedItem;
                nuova_iscrizione_data.Persone_offese_list.RemoveAll(item => item.CodiceFiscale == persona_da_rimuovere.CodiceFiscale);
                // Si aggiorna il data grid contenete l'elenco dei nuovi indagati
                persona_offesaDataGrid.SelectedItem = null;
                persona_offesaDataGrid.Items.Refresh();
            }
        }

        private void AnnullaButtonClick(object sender, RoutedEventArgs e)
        {
            //Si ottiene la finestra corrente e si chiude
            Window closing_window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "NuovaIscrizioneMainWindow");
            closing_window.Close();
        }

        private void IndietroButtonClick(object sender, RoutedEventArgs e)
        {
            nuova_iscrizione_data.Persone_offese_list.Clear();
            NavigationService.GoBack();
        }

        private void AvantiButtonClick(object sender, RoutedEventArgs e)
        {
            InserisciReati reati_window = new InserisciReati();
            NavigationService.Navigate(reati_window);
        }
    }
}

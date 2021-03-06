﻿using System;
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
    /// Logica di interazione per InserisciIndagati.xaml
    /// </summary>
    public partial class InserisciIndagati : Page
    {
        // Informazioni sulla nuova iscrizione, oggetto usato per il passaggio di dati tra pagine
        private NuovaIscrizione nuova_iscrizione_data;
        // Finestra per l'inserimento dei dati di un indagato
        private SetDatiIndagato set_dati_indagato_window;

        public InserisciIndagati()
        {
            InitializeComponent();
        }

        private void InserisciIndagatiLoaded(object sender, RoutedEventArgs e)
        {
            nuova_iscrizione_data = (NuovaIscrizione)Application.Current.Properties["nuova_iscrizione"];

            nuova_iscrizione_data.Reati_list.Clear();
            nuova_iscrizione_data.Persone_reati_ass.Clear();
            nuova_iscrizione_data.Persone_offese_list.Clear();

            System.Windows.Data.CollectionViewSource personaViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("personaViewSource")));
            // Caricare i dati impostando la proprietà CollectionViewSource.Source:
            // notizia_reatoViewSource.Source = [origine dati generica]
            personaViewSource.Source = nuova_iscrizione_data.Persone_indagate_list;
        }

        private void AggiungiIndagatoButtonClick(object sender, RoutedEventArgs e)
        {
            set_dati_indagato_window = new SetDatiIndagato();
            // Si registra l'handler per l'inserimento di un nuovo indagato
            set_dati_indagato_window.evento_indagato_creato += new SetIndagatoHandler(IndagatoCreatoHandler);
            set_dati_indagato_window.ShowDialog();
        }

        void IndagatoCreatoHandler(object sender, DatiIndagatoEventArgs dati_evento)
        {
            // Si aggiunge l'indagato nelle informazioni sulla nuova iscrizione
            nuova_iscrizione_data.Persone_indagate_list.Add(dati_evento.Nuova_persona_indagata);
            // Si aggiorna il data grid contenete l'elenco dei nuovi indagati
            personaDataGrid.SelectedItem = dati_evento.Nuova_persona_indagata;
            personaDataGrid.Items.Refresh();
        }


        private void ModificaButtonClick(object sender, RoutedEventArgs e)
        {
            if (personaDataGrid.SelectedItem == null) MessageBox.Show("Devi selezionare un indagato dalla lista, prima di poterlo modificare.");
            else
            {
                set_dati_indagato_window = new SetDatiIndagato((Model.persona)personaDataGrid.SelectedItem);
                // Si registra l'handler per la modifica di un indagato
                set_dati_indagato_window.evento_indagato_modificato += new SetIndagatoHandler(IndagatoModificatoHandler);
                set_dati_indagato_window.ShowDialog();
            }
        }

        void IndagatoModificatoHandler(object sender, DatiIndagatoEventArgs dati_evento)
        {
            // Si rimuove dall'elenco l'oggetto corrispondente all'indagato da modificare
            nuova_iscrizione_data.Persone_indagate_list.Remove((Model.persona)personaDataGrid.SelectedItem);
            // Si inserisce nell'elenco l'indagato modificato
            nuova_iscrizione_data.Persone_indagate_list.Add(dati_evento.Nuova_persona_indagata);
            // Si aggiorna il data grid contenete l'elenco dei nuovi indagati
            personaDataGrid.SelectedItem = dati_evento.Nuova_persona_indagata;
            personaDataGrid.Items.Refresh();
        }


        private void RimuoviButtonClick(object sender, RoutedEventArgs e)
        {
            if (personaDataGrid.SelectedItem == null) MessageBox.Show("Devi selezionare un indagato dalla lista, prima di poterlo rimuovere.");
            else
            {
                // Si rimuove dall'elenco la persona indagata selezionata
                nuova_iscrizione_data.Persone_indagate_list.Remove((Model.persona)personaDataGrid.SelectedItem);
                // Si aggiorna il data grid contenete l'elenco dei nuovi indagati
                personaDataGrid.SelectedItem = null;
                personaDataGrid.Items.Refresh();

            }
        }

        private void AnnullaButtonClick(object sender, RoutedEventArgs e)
        {
            //Si ottiene la finestra corrente e si chiude
            Window closing_window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "NuovaIscrizioneMainWindow");
            closing_window.Close();
        }

        private void AvantiButtonClick(object sender, RoutedEventArgs e)
        {
            InserisciPersoneOffese po_window = new InserisciPersoneOffese();
            NavigationService.Navigate(po_window);
        }
          
    }
}

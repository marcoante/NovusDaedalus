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
using System.Windows.Shapes;

namespace Novus_Daedalus.View.NuovaIscrizione
{
    /// <summary>
    /// Logica di interazione per SetDatiReato.xaml
    /// </summary>
    [System.ComponentModel.DefaultEvent("evento_reato_creato")]
    public partial class SetDatiReato : Window
    {
        private bool modalità_modifica;
        // Copia del reato da modificare, usata solo nel caso di modalità modifica
        private Model.reato reato_originale;

        // Eventi che segnalano alla pagina "InserisciReati" la creazione o la modifica di un reato
        public event SetReatoHandler evento_reato_creato;
        public event SetReatoHandler evento_reato_modificato;

        // Binding sources per i controlli XAML
        // ------------------------------------
        private List<ReatoIndagati> indagati_binding_source;
        private List<ReatoPO> po_binding_source;
        private Model.reato reato_binding_source;
        // ------------------------------------

        private NuovaIscrizione nuova_iscrizione_data;

        private Model.novus_daedalus_dbEntities db_connection;

        // Costruttore per la modalità di creazione di un nuovo reato
        public SetDatiReato()
        {
            InitializeComponent();
            nuova_iscrizione_data = (NuovaIscrizione)Application.Current.Properties["nuova_iscrizione"];
            indagati_binding_source = new List<ReatoIndagati>();
            po_binding_source = new List<ReatoPO>();

            reato_binding_source = new Model.reato();
            reato_binding_source.Data = System.DateTime.Now;
            modalità_modifica = false;
        }

        // Costruttore per la modalità modifica di un reato già esistente
        public SetDatiReato(Model.reato reato)
        {
            InitializeComponent();
            nuova_iscrizione_data = (NuovaIscrizione)Application.Current.Properties["nuova_iscrizione"];
            indagati_binding_source = new List<ReatoIndagati>();
            po_binding_source = new List<ReatoPO>();

            reato_originale = reato;

            reato_binding_source = new Model.reato();
            reato_binding_source.NomenIuris = reato_originale.NomenIuris;
            reato_binding_source.Codice = reato_originale.Codice;
            reato_binding_source.Data = reato_originale.Data;
            reato_binding_source.Luogo = reato_originale.Luogo;

            modalità_modifica = true;
        }


        private void SetDatiReatoLoaded(object sender, RoutedEventArgs e)
        {
            // Si aggiornano i controlli XAML
            Dati_Reato_Grid.DataContext = reato_binding_source;

            foreach (Model.persona p in nuova_iscrizione_data.Persone_indagate_list)
            {
                ReatoIndagati ri = new ReatoIndagati();
                ri.PersonaIndagata = p;
                if (modalità_modifica == true
                    && nuova_iscrizione_data.Persone_reati_ass.Find(r => r.reato.NomenIuris == reato_originale.NomenIuris && r.persona.Equals(p)) != null)
                    ri.IsSelected = true;
                else
                    ri.IsSelected = false;
                indagati_binding_source.Add(ri);
            }
            Reato_Indagati_List_View.DataContext = indagati_binding_source;

            foreach (Model.persona p in nuova_iscrizione_data.Persone_offese_list)
            {
                ReatoPO rp = new ReatoPO();
                rp.PersonaOffesa = p;
                if (modalità_modifica == true
                    && nuova_iscrizione_data.Persone_reati_ass.Find(r => r.reato.NomenIuris == reato_originale.NomenIuris && r.persona.Equals(p)) != null)
                    rp.IsSelected = true;
                else
                    rp.IsSelected = false;
                po_binding_source.Add(rp);
            }
            if (indagati_binding_source.Count == 0)
                chkAllIndagati.IsChecked = false;
            else
                chkAllIndagati.IsChecked = true;
            foreach (ReatoIndagati ri in indagati_binding_source)
            {
                if (ri.IsSelected == false)
                    chkAllIndagati.IsChecked = false;
            }

            if (po_binding_source.Count == 0)
                chkAllPO.IsChecked = false;
            else
                chkAllPO.IsChecked = true;
            foreach (ReatoPO rp in po_binding_source)
            {
                if (rp.IsSelected == false)
                    chkAllPO.IsChecked = false;
            }

            Reato_PO_List_View.DataContext = po_binding_source;

            db_connection = new Model.novus_daedalus_dbEntities();

            System.Windows.Data.CollectionViewSource tavola_reatiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tavola_reatiViewSource")));
            // Caricare i dati impostando la proprietà CollectionViewSource.Source:
            // tavola_reatiViewSource.Source = [origine dati generica]
            tavola_reatiViewSource.Source = db_connection.tavola_reati.ToList();
            if (modalità_modifica == true)
                descrizione_reatoComboBox.SelectedItem = db_connection.tavola_reati.Find(reato_binding_source.Codice);
        }

        private void chkAllIndagatiClick(object sender, RoutedEventArgs e)
        {
            foreach (ReatoIndagati ri in indagati_binding_source)
            {
                if (chkAllIndagati.IsChecked == true)
                    ri.IsSelected = true;
                else
                    ri.IsSelected = false;
            }
            Reato_Indagati_List_View.Items.Refresh();
        }

        private void chkAllPOClick(object sender, RoutedEventArgs e)
        {
            foreach (ReatoPO rp in po_binding_source)
            {
                if (chkAllPO.IsChecked == true)
                    rp.IsSelected = true;
                else
                    rp.IsSelected = false;
            }
            Reato_PO_List_View.Items.Refresh();
        }

        private void chkIndagatoClick(object sender, RoutedEventArgs e)
        {
            bool selected_all = true;

            if (((CheckBox)sender).IsChecked == false)
            {
                chkAllIndagati.IsChecked = false;
                selected_all = false;
            }
            else
            {
                foreach (ReatoIndagati ri in indagati_binding_source)
                {
                    if (ri.IsSelected == false)
                        selected_all = false;
                }
            }
            if (selected_all)
                chkAllIndagati.IsChecked = true;
        }

        private void chkPOClick(object sender, RoutedEventArgs e)
        {
            bool selected_all = true;

            if (((CheckBox)sender).IsChecked == false)
            {
                chkAllPO.IsChecked = false;
                selected_all = false;
            }
            else
            {
                foreach (ReatoPO rp in po_binding_source)
                {
                    if (rp.IsSelected == false)
                        selected_all = false;
                }
            }
            if (selected_all)
                chkAllPO.IsChecked = true;
        }


        private void AnnullaButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void InserisciButtonClick(object sender, RoutedEventArgs e)
        {
            // Si controlla se tutti i dati obbligatori di un reato sono presenti
            if (!reato_binding_source.IsValid)
            {
                MessageBox.Show("Uno o più dati del reato sono mancanti.");
                return;
            }

            // Si impostano alcuni campi del reato, a seconda delle selezioni dell'utente
            Model.tavola_reati reato_selezionato = (Model.tavola_reati)descrizione_reatoComboBox.SelectedItem;
            reato_binding_source.NomenIuris = reato_selezionato.NomenIuris;
            reato_binding_source.Codice = reato_selezionato.Codice;
            reato_binding_source.Descrizione = reato_selezionato.Descrizione;

            // Se il nuovo reato ha un codice già presente nell'elenco dei nuovi reati
            // si mostra un messaggio di errore
            Model.reato find_result_reato;
            find_result_reato = nuova_iscrizione_data.Reati_list.Find(item => item.NomenIuris == reato_binding_source.NomenIuris);
            if (find_result_reato != null)
            {
                if (modalità_modifica == false)
                {
                    MessageBox.Show("Il reato con Nomen Iuris \"" +
                        reato_binding_source.NomenIuris +
                        "\" è già stato inserito.");
                    return;
                }
                else if (find_result_reato.NomenIuris != reato_originale.NomenIuris)
                {
                    MessageBox.Show("Il reato con Nomen Iuris \"" +
                        reato_binding_source.NomenIuris +
                        "\" è già stato inserito.");
                    return;
                }
            }

            DatiReatoEventArgs event_data;
            if (modalità_modifica) event_data = new DatiReatoEventArgs(reato_binding_source, reato_originale);
            else event_data = new DatiReatoEventArgs(reato_binding_source);

            // Si aggiornano le associazioni tra i reati e le persone
            foreach (ReatoIndagati ri in indagati_binding_source)
            {
                if (ri.IsSelected == true)
                {
                    event_data.Persone_indagate_associate.Add(ri.PersonaIndagata);
                }
            }

            foreach (ReatoPO rp in po_binding_source)
            {
                if (rp.IsSelected == true)
                {
                    event_data.Persone_offese_associate.Add(rp.PersonaOffesa);
                }
            }

            // Se si è in modalità modifica si invoca l'evento reato modificato,
            // che verrà gestito dalla pagina "Inserisci reati"
            // Altrimenti si invoca l'evento reato creato, che verrà gestito
            // sempre dalla pagina "Inserisci reati"
            if (modalità_modifica)
                On_evento_reato_modificato(event_data);
            else
                On_evento_reato_creato(event_data);
            Close();
        }


        protected virtual void On_evento_reato_creato(DatiReatoEventArgs e)
        {
            if (evento_reato_creato != null) evento_reato_creato(this, e);
        }

        protected virtual void On_evento_reato_modificato(DatiReatoEventArgs e)
        {
            if (evento_reato_modificato != null) evento_reato_modificato(this, e);
        }
    }





    // Gestione degli eventi per la creazione e la modifica di un reato
    public delegate void SetReatoHandler(object sender, DatiReatoEventArgs e);

    // Argomento degli handler
    public class DatiReatoEventArgs : EventArgs
    {
        private Model.reato nuovo_reato;
        // Copia del reato prima della modifiche, usata solo nella modalità modifica
        private Model.reato reato_originale;

        private List<Model.persona> persone_indagate_associate;

        private List<Model.persona> persone_offese_associate;

        public DatiReatoEventArgs(Model.reato nuovo_reato)
        {
            this.nuovo_reato = nuovo_reato;
            persone_indagate_associate = new List<Model.persona>();
            persone_offese_associate = new List<Model.persona>();
        }

        public DatiReatoEventArgs(Model.reato nuovo_reato, Model.reato reato_originale)
        {
            this.nuovo_reato = nuovo_reato;
            this.reato_originale = reato_originale;
            persone_indagate_associate = new List<Model.persona>();
            persone_offese_associate = new List<Model.persona>();
        }

        public Model.reato Nuovo_reato
        {
            get { return nuovo_reato; }
            set { nuovo_reato = value; }
        }

        public Model.reato Reato_originale
        {
            get { return reato_originale; }
            set { reato_originale = value; }
        }

        public List<Model.persona> Persone_indagate_associate
        {
            get { return persone_indagate_associate; }
            set { persone_indagate_associate = value; }
        }

        public List<Model.persona> Persone_offese_associate
        {
            get { return persone_offese_associate; }
            set { persone_offese_associate = value; }
        }
    }
}

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

        private List<ReatoIndagati> indagati_binding_source;
        private NuovaIscrizione nuova_iscrizione_data;

        private Model.reato reato_binding_source;

        public SetDatiReato()
        {
            InitializeComponent();
            nuova_iscrizione_data = (NuovaIscrizione)Application.Current.Properties["nuova_iscrizione"];
            indagati_binding_source = new List<ReatoIndagati>();

            reato_binding_source = new Model.reato();
            modalità_modifica = false;
        }

        // Costruttore per la modalità modifica di un reato già esistente
        public SetDatiReato(Model.reato reato)
        {
            InitializeComponent();
            nuova_iscrizione_data = (NuovaIscrizione)Application.Current.Properties["nuova_iscrizione"];
            indagati_binding_source = new List<ReatoIndagati>();

            reato_originale = reato;
            nomenIurisComboBox.Text = reato_originale.NomenIuris;

            reato_binding_source = new Model.reato();
            reato_binding_source.NomenIuris = reato_originale.NomenIuris;
            reato_binding_source.Codice = reato_originale.Codice;
            reato_binding_source.Data = reato_originale.Data;
            reato_binding_source.Luogo = reato_originale.Luogo;

            modalità_modifica = true;
        }


        private void SetDatiReatoLoaded(object sender, RoutedEventArgs e)
        {

            Dati_Reato_Grid.DataContext = reato_binding_source;

            foreach (Model.persona p in nuova_iscrizione_data.Persone_indagate_list)
            {
                ReatoIndagati ri = new ReatoIndagati();
                ri.PersonaIndagata = p;
                if (modalità_modifica == true
                    && nuova_iscrizione_data.Persone_reati_ass.Find(r => r.reato.NomenIuris == reato_originale.NomenIuris && r.persona.CodiceFiscale == p.CodiceFiscale) != null)
                    ri.IsSelected = true;
                else
                    ri.IsSelected = false;
                indagati_binding_source.Add(ri);
            }
            Reato_Indagati_List_View.DataContext = indagati_binding_source;
        }


        private void AnnullaButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void InserisciButtonClick(object sender, RoutedEventArgs e)
        {
            // Si impostano alcuni campi del reato, a seconda delle selezioni dell'utente
            reato_binding_source.NomenIuris = nomenIurisComboBox.Text;

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
                    MessageBox.Show("Il reatoo con Nomen Iuris \"" +
                        reato_binding_source.NomenIuris +
                        "\" è già stato inserito.");
                    return;
                }
            }

            DatiReatoEventArgs event_data;
            if (modalità_modifica) event_data = new DatiReatoEventArgs(reato_binding_source, reato_originale);
            else event_data = new DatiReatoEventArgs(reato_binding_source);

            foreach (ReatoIndagati ri in indagati_binding_source)
            {
                if (ri.IsSelected == true)
                {
                    event_data.Persone_indagate_associate.Add(ri.PersonaIndagata);
                }
            }

            // Se si è in modalità modifica si invoca l'evento reato modificato,
            // che verrà gestito dalla pagina "Inserisci reati"
            // Altrimenti si invoca l'evento reato creato, che verrà gestito
            // sempre dalla pagina "Inserisci reati"
            if (modalità_modifica)
            {

                On_evento_reato_modificato(event_data);
            }
            else On_evento_reato_creato(event_data);
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

        public DatiReatoEventArgs(Model.reato nuovo_reato)
        {
            this.nuovo_reato = nuovo_reato;
            persone_indagate_associate = new List<Model.persona>();
        }

        public DatiReatoEventArgs(Model.reato nuovo_reato, Model.reato reato_originale)
        {
            this.nuovo_reato = nuovo_reato;
            this.reato_originale = reato_originale;
            persone_indagate_associate = new List<Model.persona>();
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
    }
}

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
    /// Logica di interazione per SetDatiIndagato.xaml
    /// </summary>
    [System.ComponentModel.DefaultEvent("evento_indagato_creato")]
    public partial class SetDatiIndagato : Window
    {
        private bool modalità_modifica;
        // Copia dell'indagato da modificare, usata solo nel caso di modalità modifica
        private Model.indagato indagato_originale;

        // Eventi che segnalano alla pagina "InserisciIndagati" la creazione o la modifica di un indagato
        public event SetIndagatoHandler evento_indagato_creato;
        public event SetIndagatoHandler evento_indagato_modificato;

        // Dati sulla nuova iscrizione
        private NuovaIscrizione nuova_iscrizione_data;

        private Model.persona persona_indagata_binding_source;
        private Model.indagato indagato_binding_source;

        // Costruttore per la modalità di creazione nuovo indagato
        public SetDatiIndagato(NuovaIscrizione nuova_iscrizione_data)
        {
            InitializeComponent();

            this.nuova_iscrizione_data = nuova_iscrizione_data;

            Model.persona p = new Model.persona();
            Model.indagato i = new Model.indagato();
            i.persona = p;
            p.indagato = i;
            p.Ruolo = "indagato";
            p.Sesso = true;

            persona_indagata_binding_source = p;
            indagato_binding_source = i;

            modalità_modifica = false;
        }

        // Costruttore per la modalità modifica di un indagato già esistente
        public SetDatiIndagato(NuovaIscrizione nuova_iscrizione_data, Model.persona persona_indagata)
        {
            InitializeComponent();

            this.nuova_iscrizione_data = nuova_iscrizione_data;
            this.indagato_originale = persona_indagata.indagato;

            Model.persona p = new Model.persona(persona_indagata);
            Model.indagato i = new Model.indagato(persona_indagata.indagato);
            i.persona = p;
            p.indagato = i;

            if (p.Sesso == true) sessoMRadioButton.IsChecked = true;
            else sessoFRadioButton.IsChecked = true;
            statoComboBox.Text = i.Stato;
            precedenti_penaliComboBox.Text = i.PrecedentiPenali;

            persona_indagata_binding_source = p;
            indagato_binding_source = i;

            modalità_modifica = true;
        }


        private void SetDatiIndagatoLoaded(object sender, RoutedEventArgs e)
        {
            Dati_Persona_Grid.DataContext = persona_indagata_binding_source;
            Dati_Indagato_Grid.DataContext = indagato_binding_source;
        }

        private void InserisciButtonClick(object sender, RoutedEventArgs e)
        {
            // Si controlla se tutti i dati obbligatori di una persona indagata sono presenti
            if (!indagato_binding_source.persona.IsValid)
            {
                MessageBox.Show("Uno o più dati anagrafici dell'indagato sono mancanti.");
                return;
            }
            // Se il nuovo indagato ha un codice fiscale già presente nell'elenco dei nuovi indagati
            // si mostra un messaggio di errore
            Model.persona find_result_persona;
            find_result_persona = nuova_iscrizione_data.Persone_indagate_list.Find(item => item.CodiceFiscale == indagato_binding_source.persona.CodiceFiscale);
            if (find_result_persona != null)
            {
                if (modalità_modifica == false)
                {
                    MessageBox.Show("L'indagato con Codice Fiscale \"" +
                        indagato_binding_source.persona.CodiceFiscale +
                        "\" è già stato inserito.");
                    return;
                }
                else if (find_result_persona.CodiceFiscale != indagato_originale.persona.CodiceFiscale)
                {
                    MessageBox.Show("L'indagato con Codice Fiscale \"" +
                        indagato_binding_source.persona.CodiceFiscale +
                        "\" è già stato inserito.");
                    return;
                }
            }

            // Si impostano alcuni campi dell'indagato, a seconda delle selezioni dell'utente
            indagato_binding_source.CodiceFiscale = persona_indagata_binding_source.CodiceFiscale;
            indagato_binding_source.Stato = statoComboBox.Text;
            if (sessoMRadioButton.IsChecked == true) indagato_binding_source.persona.Sesso = true;
            else indagato_binding_source.persona.Sesso = false;
            indagato_binding_source.PrecedentiPenali = precedenti_penaliComboBox.Text;

            // Se si è in modalità modifica si invoca l'evento indagato modificato,
            // che verrà gestito dalla pagina "Inserisci indagati"
            // Altrimenti si invoca l'evento indagato creato, che verrà gestito
            // sempre dalla pagina "Inserisci indagati"
            if (modalità_modifica) On_evento_indagato_modificato(new DatiIndagatoEventArgs(persona_indagata_binding_source, indagato_originale.persona));
            else On_evento_indagato_creato(new DatiIndagatoEventArgs(persona_indagata_binding_source));
            Close();
        }

        private void AnnullaButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }


        protected virtual void On_evento_indagato_creato(DatiIndagatoEventArgs e)
        {
            if (evento_indagato_creato != null) evento_indagato_creato(this, e);
        }

        protected virtual void On_evento_indagato_modificato(DatiIndagatoEventArgs e)
        {
            if (evento_indagato_modificato != null) evento_indagato_modificato(this, e);
        }
    }


    // Gestione degli eventi per la creazione e la modifica di una persona indagata
    public delegate void SetIndagatoHandler(object sender, DatiIndagatoEventArgs e);

    // Argomento degli handler
    public class DatiIndagatoEventArgs : EventArgs
    {
        private Model.persona nuova_persona_indagata;
        // Copia della persona indagata prima della modifiche, usata solo nella modalità modifica
        private Model.persona persona_indagata_originale;

        public DatiIndagatoEventArgs(Model.persona nuova_persona_indagata)
        {
            this.nuova_persona_indagata = nuova_persona_indagata;
        }

        public DatiIndagatoEventArgs(Model.persona nuova_persona_indagata, Model.persona persona_indagata_originale)
        {
            this.nuova_persona_indagata = nuova_persona_indagata;
            this.persona_indagata_originale = persona_indagata_originale;
        }

        public Model.persona Nuova_persona_indagata
        {
            get { return nuova_persona_indagata; }
            set { nuova_persona_indagata = value; }
        }

        public Model.persona Persona_indagata_originale
        {
            get { return persona_indagata_originale; }
            set { persona_indagata_originale = value; }
        }
    }
}
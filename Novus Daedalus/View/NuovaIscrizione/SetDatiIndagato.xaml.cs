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

        // Binding sources per i controlli xaml
        // -----------------------------------
        private Model.persona persona_indagata_binding_source;
        private Model.indagato indagato_binding_source;

        private Model.difensore difensore1;
        private Model.difensore difensore2;
        // -----------------------------------

        // Costruttore per la modalità di creazione nuovo indagato
        public SetDatiIndagato()
        {
            InitializeComponent();

            nuova_iscrizione_data = (NuovaIscrizione)Application.Current.Properties["nuova_iscrizione"];

            Model.persona p = new Model.persona();
            Model.indagato i = new Model.indagato();
            i.persona = p;
            p.indagato = i;
            p.Ruolo = "indagato";
            p.Sesso = "M";
            p.NumeroEscussioni = 0;

            persona_indagata_binding_source = p;
            indagato_binding_source = i;

            difensore1 = new Model.difensore();
            difensore2 = new Model.difensore();
            difensore1.persona = new Model.persona();
            difensore2.persona = new Model.persona();
            difensore1.persona.Ruolo = "Difensore";
            difensore2.persona.Ruolo = "Difensore";

            modalità_modifica = false;
        }

        // Costruttore per la modalità modifica di un indagato già esistente
        public SetDatiIndagato(Model.persona persona_indagata)
        {
            InitializeComponent();

            nuova_iscrizione_data = (NuovaIscrizione)Application.Current.Properties["nuova_iscrizione"];
            this.indagato_originale = persona_indagata.indagato;

            Model.persona p = new Model.persona(persona_indagata);
            Model.indagato i = new Model.indagato(persona_indagata.indagato);
            i.persona = p;
            p.indagato = i;

            if (p.Sesso == "M") sessoMRadioButton.IsChecked = true;
            else sessoFRadioButton.IsChecked = true;
            statoComboBox.Text = i.Stato;
            precedenti_penaliComboBox.Text = i.PrecedentiPenali;

            persona_indagata_binding_source = p;
            indagato_binding_source = i;

            difensore1 = new Model.difensore();
            difensore2 = new Model.difensore();
            if (persona_indagata.indagato.difensore != null)
                difensore1.persona = new Model.persona(persona_indagata.indagato.difensore.persona);
            else
            {
                difensore1.persona = new Model.persona();
                difensore1.persona.Ruolo = "Difensore";
            }

            if (persona_indagata.indagato.difensore3 != null)
                difensore2.persona = new Model.persona(persona_indagata.indagato.difensore3.persona);
            else
            {
                difensore2.persona = new Model.persona();
                difensore2.persona.Ruolo = "Difensore";
            }

            modalità_modifica = true;
        }


        private void SetDatiIndagatoLoaded(object sender, RoutedEventArgs e)
        {
            Dati_Persona_Grid.DataContext = persona_indagata_binding_source;
            Dati_Indagato_Grid.DataContext = indagato_binding_source;
            Dif1_Grid.DataContext = difensore1.persona;
            Dif2_Grid.DataContext = difensore2.persona;
        }

        private void InserisciButtonClick(object sender, RoutedEventArgs e)
        {
            // Si controlla se tutti i dati obbligatori di una persona indagata sono presenti
            if (!indagato_binding_source.persona.IsValid)
            {
                MessageBox.Show("Uno o più dati anagrafici dell'indagato sono mancanti.");
                return;
            }

            if (nomeDif1TextBox.Text != null && nomeDif1TextBox.Text != "")
            {
                if (difensore1.persona.IsValid)
                    indagato_binding_source.difensore = difensore1;
                else
                {
                    MessageBox.Show("Uno o più dati anagrafici del primo difensore sono mancanti.");
                    return;
                }
            }

            if (nomeDif2TextBox.Text != null && nomeDif2TextBox.Text != "")
            {
                if (difensore2.persona.IsValid)
                    indagato_binding_source.difensore3 = difensore2;
                else
                {
                    MessageBox.Show("Uno o più dati anagrafici del secondo difensore sono mancanti.");
                    return;
                }
            }

            // Si impostano alcuni campi dell'indagato, a seconda delle selezioni dell'utente
            indagato_binding_source.Stato = statoComboBox.Text;
            if (sessoMRadioButton.IsChecked == true) indagato_binding_source.persona.Sesso = "M";
            else indagato_binding_source.persona.Sesso = "F";
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
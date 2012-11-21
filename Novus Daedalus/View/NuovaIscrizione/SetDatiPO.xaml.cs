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
    /// Logica di interazione per SetDatiPO.xaml
    /// </summary>
    [System.ComponentModel.DefaultEvent("evento_po_creata")]
    public partial class SetDatiPO : Window
    {
        private bool modalità_modifica;
        // Copia della persona offesa da modificare, usata solo nel caso di modalità modifica
        private Model.persona po_originale;

        // Eventi che segnalano alla pagina "SetDatiReato" la creazione o la modifica di una persona offesa
        public event SetPOHandler evento_po_creata;
        public event SetPOHandler evento_po_modificata;

        // Dati sulla nuova iscrizione
        private NuovaIscrizione nuova_iscrizione_data;

        private Model.persona po_binding_source;

        // Costruttore per la modalità di creazione nuova persona offesa
        public SetDatiPO()
        {
            InitializeComponent();

            nuova_iscrizione_data = (NuovaIscrizione)Application.Current.Properties["nuova_iscrizione"];

            Model.persona p = new Model.persona();
            Model.persona_offesa i = new Model.persona_offesa();
            i.persona = p;
            p.persona_offesa = i;
            p.Ruolo = "persona offesa";
            p.Sesso = "M";
            p.NumeroEscussioni = 0;
            p.persona_offesa.AvvisoArchiviazione = false;

            po_binding_source = p;
            modalità_modifica = false;
        }

        // Costruttore per la modalità modifica di un indagato già esistente
        public SetDatiPO(Model.persona persona_offesa)
        {
            InitializeComponent();

            nuova_iscrizione_data = (NuovaIscrizione)Application.Current.Properties["nuova_iscrizione"];
            this.po_originale = persona_offesa;

            Model.persona p = new Model.persona(persona_offesa);
            Model.persona_offesa i = new Model.persona_offesa(persona_offesa.persona_offesa);
            i.persona = p;
            p.persona_offesa = i;

            if (p.Sesso == "M") sessoMRadioButton.IsChecked = true;
            else sessoFRadioButton.IsChecked = true;

            po_binding_source = p;
            modalità_modifica = true;
        }


        private void SetDatiPOLoaded(object sender, RoutedEventArgs e)
        {
            PO_Grid.DataContext = po_binding_source;
            AvvArch_Grid.DataContext = po_binding_source.persona_offesa;
        }

        private void InserisciButtonClick(object sender, RoutedEventArgs e)
        {
            // Si controlla se tutti i dati obbligatori di una persona offesa siano presenti
            if (!po_binding_source.IsValid)
            {
                MessageBox.Show("Uno o più dati anagrafici della persona offesa sono mancanti.");
                return;
            }

            // Si impostano alcuni campi della persona offesa, a seconda delle selezioni dell'utente
            if (sessoMRadioButton.IsChecked == true) po_binding_source.Sesso = "M";
            else po_binding_source.Sesso = "F";

            // Se si è in modalità modifica si invoca l'evento persona offesa modificata,
            // che verrà gestito dalla pagina "SetDatiReato"
            // Altrimenti si invoca l'evento persona offesa creata, che verrà gestito
            // sempre dalla pagina "SetDatiReato"
            if (modalità_modifica) On_evento_po_modificata(new DatiPOEventArgs(po_binding_source, po_originale));
            else On_evento_po_creata(new DatiPOEventArgs(po_binding_source));
            Close();
        }

        private void Annulla_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        protected virtual void On_evento_po_creata(DatiPOEventArgs e)
        {
            if (evento_po_creata != null) evento_po_creata(this, e);
        }

        protected virtual void On_evento_po_modificata(DatiPOEventArgs e)
        {
            if (evento_po_modificata != null) evento_po_modificata(this, e);
        }
    }


    // Gestione degli eventi per la creazione e la modifica di una persona offesa
    public delegate void SetPOHandler(object sender, DatiPOEventArgs e);

    // Argomento degli handler
    public class DatiPOEventArgs : EventArgs
    {
        private Model.persona nuova_persona_offesa;
        // Copia della persona offesa prima della modifiche, usata solo nella modalità modifica
        private Model.persona persona_offesa_originale;

        public DatiPOEventArgs(Model.persona nuova_persona_offesa)
        {
            this.nuova_persona_offesa = nuova_persona_offesa;
        }

        public DatiPOEventArgs(Model.persona nuova_persona_offesa, Model.persona persona_offesa_originale)
        {
            this.nuova_persona_offesa = nuova_persona_offesa;
            this.persona_offesa_originale = persona_offesa_originale;
        }

        public Model.persona Nuova_persona_offesa
        {
            get { return nuova_persona_offesa; }
            set { nuova_persona_offesa = value; }
        }

        public Model.persona Persona_offesa_originale
        {
            get { return persona_offesa_originale; }
            set { persona_offesa_originale = value; }
        }
    }
}

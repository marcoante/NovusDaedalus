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

namespace Novus_Daedalus.View.Persone
{
    /// <summary>
    /// Logica di interazione per SetDatiIndagato.xaml
    /// </summary>
    public partial class SetDatiIndagato : Window
    {
        private bool modalità_modifica;

        // Copia della persona da modificare, usata solo nel caso di modalità modifica
        private Model.persona p_originale;

        // Eventi che segnalano alla finestra "PersoneMainWindow" la creazione o la modifica di una persona
        public event SetIndagatoHandler evento_p_creata;
        public event SetIndagatoHandler evento_p_modificata;

        private List<PersonaReati> reati_binding_source;

        Model.novus_daedalus_dbEntities db_connection;

        Model.scheda scheda;

        private Model.persona p_binding_source;
        private Model.indagato i_binding_source;

        private Model.difensore difensore1;

        public SetDatiIndagato()
        {
            InitializeComponent();

            Model.persona p = new Model.persona();
            Model.indagato i = new Model.indagato();
            i.persona = p;
            p.indagato = i;
            p.Ruolo = "indagato";
            p.Sesso = "M";
            p.NumeroEscussioni = 0;

            p_binding_source = p;
            i_binding_source = i;

            difensore1 = new Model.difensore();
            difensore1.persona = new Model.persona();
            difensore1.persona.Ruolo = "Difensore";

            modalità_modifica = false;
        }

        public SetDatiIndagato(Model.persona p)
        {
            InitializeComponent();
            this.p_originale = p;

            if (p_originale.indagato.difensore == null)
            {
                difensore1 = new Model.difensore();
                difensore1.persona = new Model.persona();
                difensore1.persona.Ruolo = "Difensore";
            }

            //Model.persona p_copia = new Model.persona(p);
            //if (p.Ruolo == "persona offesa")
            //{
            //    Model.persona_offesa i = new Model.persona_offesa(p.persona_offesa);
            //    i.persona = p_copia;
            //    p_copia.persona_offesa = i;
            //}
            //if (p.Ruolo == "persona informata")
            //{
            //    Model.persona_informata i = new Model.persona_informata(p.persona_informata);
            //    i.persona = p_copia;
            //    p_copia.persona_informata = i;
            //}

            //if (p_copia.Sesso == "M") sessoMRadioButton.IsChecked = true;
            //else sessoFRadioButton.IsChecked = true;

            modalità_modifica = true;
        }

        private void SetDatiIndagato_Loaded(object sender, RoutedEventArgs e)
        {
            reati_binding_source = new List<PersonaReati>();
            db_connection = new Model.novus_daedalus_dbEntities();
            if (modalità_modifica)
            {
                p_binding_source = db_connection.persona.Find(p_originale.Id);
                i_binding_source = db_connection.indagato.Find(p_originale.indagato.Id);
                statoComboBox.Text = p_originale.indagato.Stato;
                precedenti_penaliComboBox.Text = p_originale.indagato.PrecedentiPenali;
                if (p_binding_source.Sesso == "M") sessoMRadioButton.IsChecked = true;
                else sessoFRadioButton.IsChecked = true;
            }
            scheda = db_connection.scheda.Find((int)Application.Current.Properties["Scheda"]);
            if(i_binding_source.difensore != null)
            {
                difensore1 = i_binding_source.difensore;
            }
            difensore1.persona.IdScheda = scheda.Id;

            DatiPersona_Grid.DataContext = p_binding_source;
            Dati_Indagato_Grid.DataContext = i_binding_source;
            Dif1_Grid.DataContext = difensore1.persona;

            if (modalità_modifica == false)
                p_binding_source.scheda = scheda;

            foreach (Model.reato r in scheda.reato)
            {
                PersonaReati pr = new PersonaReati();
                pr.Reato = r;
                if (modalità_modifica == true
                    && p_binding_source.PersonaReato.Any(item => item.IdPersona == p_binding_source.Id && item.IdReato == r.Id && item.IdScheda == scheda.Id) == true)
                    pr.IsSelected = true;
                else
                    pr.IsSelected = false;
                reati_binding_source.Add(pr);
            }
            if (reati_binding_source.Count == 0)
                chkAllReati.IsChecked = false;
            else
                chkAllReati.IsChecked = true;
            foreach (PersonaReati pr in reati_binding_source)
            {
                if (pr.IsSelected == false)
                    chkAllReati.IsChecked = false;
            }
            Persona_Reati_List_View.DataContext = reati_binding_source;
        }

        private void chkAllReatiClick(object sender, RoutedEventArgs e)
        {
            foreach (PersonaReati pr in reati_binding_source)
            {
                if (chkAllReati.IsChecked == true)
                    pr.IsSelected = true;
                else
                    pr.IsSelected = false;
            }
            Persona_Reati_List_View.Items.Refresh();
        }

        private void chkReatoClick(object sender, RoutedEventArgs e)
        {
            bool selected_all = true;

            if (((CheckBox)sender).IsChecked == false)
            {
                chkAllReati.IsChecked = false;
                selected_all = false;
            }
            else
            {
                foreach (PersonaReati pr in reati_binding_source)
                {
                    if (pr.IsSelected == false)
                        selected_all = false;
                }
            }
            if (selected_all)
                chkAllReati.IsChecked = true;
        }

        private void AnnullaButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            if (modalità_modifica == false)
                db_connection.persona.Add(p_binding_source);

            // Si controlla se tutti i dati obbligatori di una persona siano presenti
            if (!p_binding_source.IsValid)
            {
                MessageBox.Show("Uno o più dati anagrafici della persona sono mancanti.");
                return;
            }

            if(reati_binding_source.Find(item => item.IsSelected == true) == null)
            {
                {
                    MessageBox.Show("Attenzione, non è stato associato alcun reato a carico dell'indagato.");
                    return;
                }
            }

            // Si impostano alcuni campi della persona, a seconda delle selezioni dell'utente
            if (sessoMRadioButton.IsChecked == true) p_binding_source.Sesso = "M";
            else p_binding_source.Sesso = "F";
            i_binding_source.Stato = statoComboBox.Text;
            i_binding_source.PrecedentiPenali = precedenti_penaliComboBox.Text;

            if (nomeDif1TextBox.Text != null && nomeDif1TextBox.Text != "" && i_binding_source.difensore == null)
            {
                if (difensore1.persona.IsValid)
                {
                    i_binding_source.difensore = difensore1;
                }
            }


            DatiIndagatoEventArgs event_data;
            if (modalità_modifica) event_data = new DatiIndagatoEventArgs(p_binding_source, p_originale);
            else event_data = new DatiIndagatoEventArgs(p_binding_source);

            p_binding_source.PersonaReato.Clear();
            foreach (PersonaReati pr in reati_binding_source)
            {
                if (pr.IsSelected == true)
                {
                    Model.PersonaReato nuovo_pr = new Model.PersonaReato();
                    nuovo_pr.scheda = scheda;
                    nuovo_pr.persona = p_binding_source;
                    nuovo_pr.reato = pr.Reato;
                    p_binding_source.PersonaReato.Add(nuovo_pr);
                }
            }
            db_connection.SaveChanges();


            // Se si è in modalità modifica si invoca l'evento persona modificata,
            // Altrimenti si invoca l'evento persona creata
            if (modalità_modifica)
                On_evento_p_modificata(event_data);
            else
                On_evento_p_creata(event_data);
            Close();
        }

        protected virtual void On_evento_p_modificata(DatiIndagatoEventArgs e)
        {
            if (evento_p_modificata != null) evento_p_modificata(this, e);
        }

        protected virtual void On_evento_p_creata(DatiIndagatoEventArgs e)
        {
            if (evento_p_creata != null) evento_p_creata(this, e);
        }

    }

    // Gestione degli eventi per la creazione e la modifica di una persona
    public delegate void SetIndagatoHandler(object sender, DatiIndagatoEventArgs e);


    // Argomento degli handler
    public class DatiIndagatoEventArgs : EventArgs
    {
        private Model.persona nuova_persona;
        // Copia della persona prima della modifiche, usata solo nella modalità modifica
        private Model.persona persona_originale;

        public DatiIndagatoEventArgs(Model.persona nuova_persona)
        {
            this.nuova_persona = nuova_persona;
        }

        public DatiIndagatoEventArgs(Model.persona nuova_persona, Model.persona persona_originale)
        {
            this.nuova_persona = nuova_persona;
            this.persona_originale = persona_originale;
        }

        public Model.persona Nuova_persona
        {
            get { return nuova_persona; }
            set { nuova_persona = value; }
        }

        public Model.persona Persona_originale
        {
            get { return persona_originale; }
            set { persona_originale = value; }
        }
    }
}

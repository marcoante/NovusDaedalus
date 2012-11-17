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
    /// Logica di interazione per SetDatiPersona.xaml
    /// </summary>
    public partial class SetDatiPersona : Window
    {
        private bool modalità_modifica;

        // Copia della persona da modificare, usata solo nel caso di modalità modifica
        private Model.persona p_originale;

        // Eventi che segnalano alla finestra "PersoneMainWindow" la creazione o la modifica di una persona
        public event SetPersonaHandler evento_p_creata;
        public event SetPersonaHandler evento_p_modificata;

        Model.novus_daedalus_dbEntities db_connection;

        private Model.persona p_binding_source;

        public SetDatiPersona()
        {
            InitializeComponent();

            Model.persona p = new Model.persona();
            p.Sesso = "M";
            p.NumeroEscussioni = 0;

            p_binding_source = p;
            modalità_modifica = false;
        }

        public SetDatiPersona(Model.persona p)
        {
            //InitializeComponent();
            //db_connection = new Model.novus_daedalus_dbEntities();
            //this.p = db_connection.persona.Find(p.CodiceFiscale);

            //if (this.p.Sesso == true) sessoMRadioButton.IsChecked = true;
            //else sessoFRadioButton.IsChecked = true;

            //modalità_modifica = true;
        }

        private void SetDatiPersona_Loaded(object sender, RoutedEventArgs e)
        {
            DatiPersona_Grid.DataContext = p_originale;
            System.Windows.Data.CollectionViewSource personaViewSource1 = ((System.Windows.Data.CollectionViewSource)(this.FindResource("personaViewSource1")));
            // Caricare i dati impostando la proprietà CollectionViewSource.Source:
            // personaViewSource1.Source = [origine dati generica]
        }

        private void AnnullaButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            // Si controlla se tutti i dati obbligatori di una persona sono presenti
            //if (!p.IsValid)
            //{
            //    MessageBox.Show("Uno o più dati anagrafici della persona sono mancanti.");
            //    return;
            //}
            //if (sessoMRadioButton.IsChecked == true) p.Sesso = true;
            //else p.Sesso = false;
            //db_connection.SaveChanges();

            //if (modalità_modifica) On_evento_p_modificata();
            //else On_evento_p_creata();
            //Close();
        }

        protected virtual void On_evento_p_modificata()
        {
            if (evento_p_modificata != null) evento_p_modificata(this);
        }

        protected virtual void On_evento_p_creata()
        {
            if (evento_p_creata != null) evento_p_creata(this);
        }
    }

    // Gestione degli eventi per la creazione e la modifica di una persona
    public delegate void SetPersonaHandler(object sender);
}

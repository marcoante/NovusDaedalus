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
    /// Logica di interazione per MostraIndagato.xaml
    /// </summary>
    public partial class MostraIndagato : Window
    {
        private Model.persona p;

        public MostraIndagato()
        {
            InitializeComponent();
        }

        public MostraIndagato(Model.persona p)
        {
            InitializeComponent();
            this.p = p;
        }

        private void MostraIndagato_Loaded(object sender, RoutedEventArgs e)
        {
            //db_connection = new Model.novus_daedalus_dbEntities();

            Window_Grid.DataContext = p;
            if (p.Sesso == "M")
                sessoTextBlock.Text = "M";
            else
                sessoTextBlock.Text = "F";

            DatiIndagato_Grid.DataContext = p.indagato;

            if (p.indagato.difensore != null)
            {
                difensore1TextBlock.Text = p.indagato.difensore.persona.Nome + " " + p.indagato.difensore.persona.Cognome;
            }
            if (p.indagato.difensore3 != null)
            {
                difensore2TextBlock.Text = p.indagato.difensore3.persona.Nome + " " + p.indagato.difensore3.persona.Cognome;
            }

            List<Model.reato> reati_collegati = new List<Model.reato>();
            foreach (Model.PersonaReato pr in p.PersonaReato)
            {
                reati_collegati.Add(pr.reato);
            }
            Reati_List_View.DataContext = reati_collegati;
        }

        private void ChiudiButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

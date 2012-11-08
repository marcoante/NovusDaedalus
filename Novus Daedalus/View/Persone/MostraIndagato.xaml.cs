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
        private Model.novus_daedalus_dbEntities db_connection;

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
            db_connection = new Model.novus_daedalus_dbEntities();

            Window_Grid.DataContext = p;
            if (p.Sesso == true)
                sessoTextBlock.Text = "M";
            else
                sessoTextBlock.Text = "F";

            DatiIndagato_Grid.DataContext = p.indagato;

            List<Model.reato> reati_collegati = new List<Model.reato>();
            foreach (Model.persona_reato pr in p.persona_reato)
            {
                reati_collegati.Add(db_connection.reato.Find(pr.IdReato));
            }
            Reati_List_View.DataContext = reati_collegati;
        }

        private void ChiudiButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

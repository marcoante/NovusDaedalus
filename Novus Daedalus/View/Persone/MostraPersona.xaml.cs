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
    /// Logica di interazione per MostraPersona.xaml
    /// </summary>
    public partial class MostraPersona : Window
    {
        private Model.persona p;

        public MostraPersona()
        {
            InitializeComponent();
        }

        public MostraPersona(Model.persona p)
        {
            InitializeComponent();
            this.p = p;
        }

        private void MostraPersona_Loaded(object sender, RoutedEventArgs e)
        {

            Window_Grid.DataContext = p;
            if (p.Sesso == "M")
                sessoTextBlock.Text = "M";
            else
                sessoTextBlock.Text = "F";

            List<Model.reato> reati_collegati = new List<Model.reato>();
            foreach(Model.PersonaReato pr in p.PersonaReato)
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

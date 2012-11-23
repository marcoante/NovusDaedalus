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
    /// Logica di interazione per AggiungiPersona.xaml
    /// </summary>
    public partial class AggiungiPersona : Window
    {
        private SetDatiPersona mod_persona_win;
        private SetDatiIndagato mod_indagato_win;

        public AggiungiPersona()
        {
            InitializeComponent();
        }

        public AggiungiPersona(SetDatiPersona mod_persona_win, SetDatiIndagato mod_indagato_win)
        {
            InitializeComponent();
            this.mod_persona_win = mod_persona_win;
            this.mod_indagato_win = mod_indagato_win;
        }

        private void AnnullaButtonClick(object sender, RoutedEventArgs e)
        {
            mod_persona_win.Close();
            mod_indagato_win.Close();
            Close();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
            if (Ruolo_ComboBox.Text == "Indagato")
            {
                mod_persona_win.Close();
                mod_indagato_win.ShowDialog();
            }
            else
            {
                mod_indagato_win.Close();
                mod_persona_win.Ruolo = Ruolo_ComboBox.Text;
                mod_persona_win.ShowDialog();
            }
        }
    }
}

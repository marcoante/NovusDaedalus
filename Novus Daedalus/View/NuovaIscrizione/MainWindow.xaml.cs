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
using System.Windows.Navigation;

namespace Novus_Daedalus.View.NuovaIscrizione
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        // Eventi che segnalano alla pagina "DaedalusMainPage" la creazione di una scheda
        public event SchedaCreataHandler evento_scheda_creata;

        public MainWindow()
        {
            InitializeComponent();
            Application.Current.Properties["nuova_iscrizione"] = new NuovaIscrizione();
        }

        public virtual void On_evento_scheda_creata()
        {
            if (evento_scheda_creata != null) evento_scheda_creata(this);
        }
    }

    // Gestione degli eventi per la creazione di una scheda
    public delegate void SchedaCreataHandler(object sender);
}

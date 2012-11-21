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

namespace Novus_Daedalus.View
{
    /// <summary>
    /// Logica di interazione per Magazzino_Esame_Imputazioni.xaml
    /// </summary>
    public partial class Magazzino_Esame_Imputazioni : Window
    {
        public Magazzino_Esame_Imputazioni()
        {
            InitializeComponent();
        }

        private void Magazzino_Esame_Imputazioni_Loaded(object sender, RoutedEventArgs e)
        {
            //Codice eseguito al caricamento della pagina(DA AGGIORNARE)
            Capo_Imputazione_Label.Content = "Esame del capo A di imputazione";
            Testo_Imputazione_Label.Content = "Testo dell'imputazione di prova";
            Persone_Accusate_Label.Content = "Paolo Bianchi";
            Elementi_da_provare_Label.Content = "Tutti gli elementi da provare";
        }

        private void Chiudi_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

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

namespace Novus_Daedalus
{
    /// <summary>
    /// Logica di interazione per MetroIndex.xaml
    /// </summary>
    public partial class MetroIndex : Window
    {
        public MetroIndex()
        {
            InitializeComponent();
        }

        private void ChiudiButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Novus_Daedalus.View.Impostazioni.SediDibattimenti
{
    /// <summary>
    /// Logica di interazione per SediDibattimenti1.xaml
    /// </summary>
    public partial class SediDibattimenti1 : Page
    {
        public SediDibattimenti1()
        {
            InitializeComponent();
        }

        private void Aggiungi_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SediDibattimenti2());
        }
    }
}

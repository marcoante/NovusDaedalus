using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Novus_Daedalus
{
    /// <summary>
    /// Logica di interazione per App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            this.Properties["db_connection"] = new Model.novus_daedalus_dbEntities();
            this.Properties["User"] = null;
            this.Properties["Scheda"] = null;

            this.Properties["nuova_iscrizione_data"] = null;
        }
    }
}

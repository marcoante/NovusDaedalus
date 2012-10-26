using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Novus_Daedalus.Controller
{
    class Scheda
    {
        public List<Model.scheda> binding_source;

        public Scheda()
        {
           binding_source = ((Model.novus_daedalus_dbEntities)Application.Current.Properties["db_connection"]).scheda.ToList();
        }

        public void Apri()
        {
        }
    }
}

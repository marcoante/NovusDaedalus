using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Novus_Daedalus.Controller
{
    class Officina
    {
        public void chiudi(NavigationService navigator)
        {
            navigator.GoBack();
        }
    }
}

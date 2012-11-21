using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novus_Daedalus.Model
{
    public partial class persona_informata
    {
        // Costruttore vuoto
        public persona_informata() { }

        // Costruttore per la copia di una persona informata
        public persona_informata(Model.persona_informata i)
        {
            this.Id = i.Id;
        }
    }
}

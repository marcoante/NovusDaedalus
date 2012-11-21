using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novus_Daedalus.Model
{
    public partial class persona_offesa
    {
        // Costruttore vuoto
        public persona_offesa() { }

        // Costruttore per la copia di una persona offesa
        public persona_offesa(Model.persona_offesa i)
        {
            this.Id = i.Id;
            this.AvvisoArchiviazione = i.AvvisoArchiviazione;
        }
    }
}

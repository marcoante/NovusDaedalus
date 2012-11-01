using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novus_Daedalus.Model
{
    public partial class indagato
    {
        // Costruttore per la copia di un indagato
        public indagato(indagato i)
        {
            this.CodiceFiscale = i.CodiceFiscale;
            this.Stato = i.Stato;
            this.PrecedentiPenali = i.PrecedentiPenali;
            this.QualitàDifesa = i.QualitàDifesa;
            this.Difensore1 = i.Difensore1;
            this.Difensore2 = i.Difensore2;
        }
    }
}
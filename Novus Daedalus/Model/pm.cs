//------------------------------------------------------------------------------
// <auto-generated>
//    Codice generato da un modello.
//
//    Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//    Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Novus_Daedalus.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class pm
    {
        public pm()
        {
            this.iscrizione = new HashSet<iscrizione>();
        }
    
        public string CodiceFiscale { get; set; }
    
        public virtual ICollection<iscrizione> iscrizione { get; set; }
        public virtual persona persona { get; set; }
    }
}

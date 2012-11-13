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
    
    public partial class scheda
    {
        public scheda()
        {
            this.atto = new HashSet<atto>();
            this.AttoCosa = new HashSet<AttoCosa>();
            this.cosa = new HashSet<cosa>();
            this.persona = new HashSet<persona>();
            this.PersonaAtto = new HashSet<PersonaAtto>();
            this.PersonaCosa = new HashSet<PersonaCosa>();
            this.PersonaReato = new HashSet<PersonaReato>();
            this.reato = new HashSet<reato>();
            this.ReatoAtto = new HashSet<ReatoAtto>();
            this.ReatoCosa = new HashSet<ReatoCosa>();
        }
    
        public int Id { get; set; }
        public string NumeroRegistro { get; set; }
        public string PosizioneFascicolo { get; set; }
        public Nullable<System.DateTime> DataRegistrazione { get; set; }
        public Nullable<int> IdIscrizione { get; set; }
    
        public virtual ICollection<atto> atto { get; set; }
        public virtual ICollection<AttoCosa> AttoCosa { get; set; }
        public virtual ICollection<cosa> cosa { get; set; }
        public virtual iscrizione iscrizione { get; set; }
        public virtual ICollection<persona> persona { get; set; }
        public virtual ICollection<PersonaAtto> PersonaAtto { get; set; }
        public virtual ICollection<PersonaCosa> PersonaCosa { get; set; }
        public virtual ICollection<PersonaReato> PersonaReato { get; set; }
        public virtual ICollection<reato> reato { get; set; }
        public virtual ICollection<ReatoAtto> ReatoAtto { get; set; }
        public virtual ICollection<ReatoCosa> ReatoCosa { get; set; }
    }
}

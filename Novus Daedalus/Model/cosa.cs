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
    
    public partial class cosa
    {
        public cosa()
        {
            this.AttoCosa = new HashSet<AttoCosa>();
            this.PersonaCosa = new HashSet<PersonaCosa>();
            this.ReatoCosa = new HashSet<ReatoCosa>();
        }
    
        public int Id { get; set; }
        public string Qualità { get; set; }
        public string Origine { get; set; }
        public string Acquisizione { get; set; }
        public string Descrizione { get; set; }
        public string Evento { get; set; }
        public string AttoOrigine { get; set; }
        public Nullable<int> IdScheda { get; set; }
    
        public virtual ICollection<AttoCosa> AttoCosa { get; set; }
        public virtual ICollection<PersonaCosa> PersonaCosa { get; set; }
        public virtual ICollection<ReatoCosa> ReatoCosa { get; set; }
        public virtual scheda scheda { get; set; }
    }
}

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
    
    public partial class notizia_reato
    {
        public notizia_reato()
        {
            this.Iscrive = new HashSet<Iscrive>();
        }
    
        public int Id { get; set; }
        public string Specie { get; set; }
        public string Fonte { get; set; }
    
        public virtual ICollection<Iscrive> Iscrive { get; set; }
    }
}

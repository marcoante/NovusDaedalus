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
    
    public partial class consulente
    {
        public int Id { get; set; }
        public string Professione { get; set; }
        public string AmbienteAttività { get; set; }
    
        public virtual persona persona { get; set; }
    }
}

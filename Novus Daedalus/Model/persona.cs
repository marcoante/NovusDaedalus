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
    
    public partial class persona
    {
        public persona()
        {
            this.PersonaAtto = new HashSet<PersonaAtto>();
            this.PersonaCosa = new HashSet<PersonaCosa>();
            this.PersonaReato = new HashSet<PersonaReato>();
            this.utente = new HashSet<utente>();
        }
    
        public int Id { get; set; }
        public string CodiceFiscale { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Sesso { get; set; }
        public string LuogoNascita { get; set; }
        public Nullable<System.DateTime> DataNascita { get; set; }
        public string Nazionalità { get; set; }
        public string Domicilio { get; set; }
        public string Ruolo { get; set; }
        public Nullable<int> NumeroEscussioni { get; set; }
        public Nullable<int> IdScheda { get; set; }
    
        public virtual collaboratore collaboratore { get; set; }
        public virtual consulente consulente { get; set; }
        public virtual difensore difensore { get; set; }
        public virtual gip gip { get; set; }
        public virtual indagato indagato { get; set; }
        public virtual interprete interprete { get; set; }
        public virtual ICollection<PersonaAtto> PersonaAtto { get; set; }
        public virtual ICollection<PersonaCosa> PersonaCosa { get; set; }
        public virtual ICollection<PersonaReato> PersonaReato { get; set; }
        public virtual persona_informata persona_informata { get; set; }
        public virtual persona_offesa persona_offesa { get; set; }
        public virtual pg pg { get; set; }
        public virtual pm pm { get; set; }
        public virtual scheda scheda { get; set; }
        public virtual ICollection<utente> utente { get; set; }
    }
}

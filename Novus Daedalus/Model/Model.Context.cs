﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class novus_daedalus_dbEntities : DbContext
    {
        public novus_daedalus_dbEntities()
            : base("name=novus_daedalus_dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<atto> atto { get; set; }
        public DbSet<collaboratore> collaboratore { get; set; }
        public DbSet<consulente> consulente { get; set; }
        public DbSet<cosa> cosa { get; set; }
        public DbSet<difensore> difensore { get; set; }
        public DbSet<gip> gip { get; set; }
        public DbSet<indagato> indagato { get; set; }
        public DbSet<interprete> interprete { get; set; }
        public DbSet<iscrizione> iscrizione { get; set; }
        public DbSet<notizia_reato> notizia_reato { get; set; }
        public DbSet<persona> persona { get; set; }
        public DbSet<persona_informata> persona_informata { get; set; }
        public DbSet<persona_offesa> persona_offesa { get; set; }
        public DbSet<pm> pm { get; set; }
        public DbSet<polizia_giudiziaria> polizia_giudiziaria { get; set; }
        public DbSet<reato> reato { get; set; }
        public DbSet<scheda> scheda { get; set; }
        public DbSet<utenti> utenti { get; set; }
    }
}

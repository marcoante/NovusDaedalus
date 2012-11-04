﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Novus_Daedalus.View.NuovaIscrizione
{
    // Classe che contiene le informazioni su una nuova iscrizione
    public class NuovaIscrizione
    {
        // Lista delle persona indagate
        private List<Model.persona> persone_indagate_list;

        // Lista dei reati da inserire nella scheda
        private List<Model.reato> reati_list;

        private List<Model.persona_reato> persone_reati_ass;

        // La nuova scheda
        private Model.scheda nuova_scheda;

        // Connessione al db
        private Model.novus_daedalus_dbEntities db_connection = new Model.novus_daedalus_dbEntities();


        public NuovaIscrizione()
        {
            persone_indagate_list = new List<Model.persona>();
            reati_list = new List<Model.reato>();
            persone_reati_ass = new List<Model.persona_reato>();
            nuova_scheda = new Model.scheda();
            nuova_scheda.DataRegistrazione = DateTime.Now.Date;
            db_connection.scheda.Add(nuova_scheda);
        }

        public List<Model.persona> Persone_indagate_list
        {
            get { return persone_indagate_list; }
        }

        public List<Model.reato> Reati_list
        {
            get { return reati_list; }
        }

        public List<Model.persona_reato> Persone_reati_ass
        {
            get { return persone_reati_ass; }
        }



        public void PersoneIndagate_SaveChanges()
        {
            foreach (Model.persona p in persone_indagate_list)
            {
                Model.persona persona_query_result = db_connection.persona.Find(p.CodiceFiscale);
                PersonaIndagataObject_Update(p, persona_query_result);
                if (persona_query_result != null)
                {
                    Model.indagato indagato_query_result = db_connection.indagato.Find(p.indagato.CodiceFiscale);
                    IndagatoObject_Update(p.indagato, indagato_query_result);
                }
            }
        }

        private void PersonaIndagataObject_Update(Model.persona nuova_persona, Model.persona vecchia_persona)
        {
            if (vecchia_persona == null)
            {
                try
                {
                    db_connection.persona.Add(nuova_persona);
                    db_connection.indagato.Add(nuova_persona.indagato);
                    nuova_scheda.persona.Add(nuova_persona);
                    db_connection.SaveChanges();
                }
                catch (Exception ex)
                {
                    db_connection.persona.Remove(nuova_persona);
                    throw ex;
                }
            }
            else
            {
                //db_connection.persona.Attach(nuova_persona);
                //db_connection.Entry(nuova_persona).State = System.Data.EntityState.Modified;
                vecchia_persona.Nome = nuova_persona.Nome;
                vecchia_persona.Cognome = nuova_persona.Cognome;
                vecchia_persona.Sesso = nuova_persona.Sesso;
                vecchia_persona.LuogoNascita = nuova_persona.LuogoNascita;
                vecchia_persona.DataNascita = nuova_persona.DataNascita;
                vecchia_persona.Nazionalità = nuova_persona.Nazionalità;
                vecchia_persona.Domicilio = nuova_persona.Domicilio;
                vecchia_persona.NumeroEscussioni = nuova_persona.NumeroEscussioni;

                //vecchia_persona.collaboratore = nuova_persona.collaboratore;
                //vecchia_persona.difensore = nuova_persona.difensore;
                //vecchia_persona.gip = nuova_persona.gip;
                //vecchia_persona.indagato = nuova_persona.indagato;
                //vecchia_persona.persona_atto = nuova_persona.persona_atto;
                //vecchia_persona.persona_cosa = nuova_persona.persona_cosa;
                //vecchia_persona.persona_reato = nuova_persona.persona_reato;
                //vecchia_persona.persona_informata = nuova_persona.persona_informata;
                //vecchia_persona.persona_offesa = nuova_persona.persona_offesa;
                //vecchia_persona.pm = nuova_persona.pm;
                //vecchia_persona.utente = nuova_persona.utente;
                //vecchia_persona.scheda = nuova_persona.scheda;
                nuova_scheda.persona.Add(vecchia_persona);
                db_connection.SaveChanges();
            }
        }

        private void IndagatoObject_Update(Model.indagato nuovo_indagato, Model.indagato vecchio_indagato)
        {
            if (vecchio_indagato == null)
            {
                try
                {
                    db_connection.indagato.Add(nuovo_indagato);
                    db_connection.SaveChanges();
                }
                catch (Exception ex)
                {
                    db_connection.indagato.Remove(nuovo_indagato);
                    throw ex;
                }
            }
            else
            {
                vecchio_indagato.Stato = nuovo_indagato.Stato;
                vecchio_indagato.PrecedentiPenali = nuovo_indagato.PrecedentiPenali;
                vecchio_indagato.QualitàDifesa = nuovo_indagato.QualitàDifesa;
                vecchio_indagato.Difensore1 = nuovo_indagato.Difensore1;
                vecchio_indagato.Difensore2 = nuovo_indagato.Difensore2;
                db_connection.SaveChanges();
            }
        }


        public void Reati_SaveChanges()
        {
            foreach (Model.reato r in reati_list)
            {
                db_connection.reato.Add(r);
            }
            foreach (Model.persona_reato pr in Persone_reati_ass)
            {
                db_connection.persona_reato.Add(pr);
            }
            db_connection.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Novus_Daedalus.Model
{
    public partial class persona : IDataErrorInfo
    {
        // Costruttore per la copia di una persona
        public persona(persona p)
        {
            this.CodiceFiscale = p.CodiceFiscale;
            this.Nome = p.Nome;
            this.Cognome = p.Cognome;
            this.Sesso = p.Sesso;
            this.LuogoNascita = p.LuogoNascita;
            this.DataNascita = p.DataNascita;
            this.Nazionalità = p.Nazionalità;
            this.Domicilio = p.Domicilio;
            this.Ruolo = p.Ruolo;
            this.NumeroEscussioni = p.NumeroEscussioni;
        }

        // Messaggi di errore per i controlli sull'input dell'utente
        #region IDataErrorInfo Members

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "CodiceFiscale")
                {
                    if (string.IsNullOrEmpty(CodiceFiscale))
                        result = "Il Codice Fiscale è un campo obbligatorio";
                }

                if (columnName == "Nome")
                {
                    if (string.IsNullOrEmpty(Nome))
                        result = "Il Nome è un campo obbligatorio";
                }

                if (columnName == "Cognome")
                {
                    if (string.IsNullOrEmpty(Cognome))
                        result = "Il Cognome è un campo obbligatorio";
                }
                return result;

            }
        }

        #endregion

        // Funzione di utility, restituisce true se i campi obbligatori
        // sono presenti nell'oggetto persona, restituisce false altrimenti
        public bool IsValid
        {
            get
            {
                if (this.CodiceFiscale == null || this.CodiceFiscale == ""
                    || this.Nome == null || this.Nome == ""
                    || this.Cognome == null || this.Cognome == "") return false;

                return true;
            }
        }
    }
}
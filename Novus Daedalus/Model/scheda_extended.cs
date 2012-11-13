using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Novus_Daedalus.Model
{
    public partial class scheda : IDataErrorInfo
    {

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

                if (columnName == "NumeroRegistro")
                {
                    if (string.IsNullOrEmpty(NumeroRegistro))
                        result = "Il Numero registro è un campo obbligatorio";
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
                if (this.NumeroRegistro == null || this.NumeroRegistro == "")
                    return false;

                return true;
            }
        }
    }
}

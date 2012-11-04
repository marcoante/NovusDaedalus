using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace Novus_Daedalus.Model
{
    public partial class reato : IDataErrorInfo
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
                if (columnName == "Luogo")
                {
                    if (string.IsNullOrEmpty(Luogo))
                        result = "Il Luogo è un campo obbligatorio";
                }

                if (columnName == "Data")
                {
                    DateTime date_time;
                    if (DateTime.TryParse(Data.ToString(), out date_time) == false
                        || Data == null)
                        result = "La Data è un campo obbligatorio";
                }
                return result;

            }
        }

        #endregion

        // Funzione di utility, restituisce true se i campi obbligatori
        // sono presenti nell'oggetto reato, restituisce false altrimenti
        public bool IsValid
        {
            get
            {
                DateTime date_time;
                if (this.Luogo == null || this.Luogo == ""
                    || this.Data == null || DateTime.TryParse(Data.ToString(), out date_time) == false) return false;

                return true;
            }
        }
    }
}

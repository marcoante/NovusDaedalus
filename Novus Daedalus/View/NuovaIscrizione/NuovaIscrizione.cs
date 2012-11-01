using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novus_Daedalus.View.NuovaIscrizione
{
    // Classe che contiene le informazioni su una nuova iscrizione
    public class NuovaIscrizione
    {
        // Lista delle persona indagate
        private List<Model.persona> persone_indagate_list;
        //private List<Model.reato> reati_list;

        public NuovaIscrizione()
        {
            persone_indagate_list = new List<Model.persona>();
        }

        public List<Model.persona> Persone_indagate_list
        {
            get { return persone_indagate_list; }
            //set { persone_indagate_list = value; }
        }
    }
}
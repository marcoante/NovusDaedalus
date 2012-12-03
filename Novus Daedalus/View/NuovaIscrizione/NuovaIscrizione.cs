using System;
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

        // Lista delle persone offese
        private List<Model.persona> persone_offese_list;

        // Lista dei reati da inserire nella scheda
        private List<Model.reato> reati_list;

        private List<Model.PersonaReato> persone_reati_ass;

        // La nuova scheda
        private Model.scheda nuova_scheda;

        private Model.notizia_reato notizia_reato;

        // Connessione al db
        private Model.novus_daedalus_dbEntities db_connection = new Model.novus_daedalus_dbEntities();


        public NuovaIscrizione()
        {
            persone_indagate_list = new List<Model.persona>();
            persone_offese_list = new List<Model.persona>();
            reati_list = new List<Model.reato>();
            persone_reati_ass = new List<Model.PersonaReato>();
        }

        public List<Model.persona> Persone_indagate_list
        {
            get { return persone_indagate_list; }
        }

        public List<Model.persona> Persone_offese_list
        {
            get { return persone_offese_list; }
        }

        public List<Model.reato> Reati_list
        {
            get { return reati_list; }
        }

        public List<Model.PersonaReato> Persone_reati_ass
        {
            get { return persone_reati_ass; }
        }

        public Model.scheda Nuova_scheda
        {
            get { return nuova_scheda; }
            set { nuova_scheda = value; }
        }

        public Model.notizia_reato Notizia_reato
        {
            get { return notizia_reato; }
            set { notizia_reato = value; }
        }

    }
}
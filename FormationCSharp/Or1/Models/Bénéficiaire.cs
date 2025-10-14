using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Or.Models
{
    public class Bénéficiaire
    {
        public long MaCarte { get; set; }
        public int IdtCpt { get; set; }
        public string PrenomClient { get; set; }
        public string NomClient { get; set; }

        public Bénéficiaire(long numclient, int idtcpt, string prenom, string nom)
        {
            MaCarte = numclient;   
            IdtCpt = idtcpt;
            PrenomClient = prenom;
            NomClient = nom;
        }
    }
}

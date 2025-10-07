using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argent1
{
    internal class Transactions
    {
        public int identifiant_t { get; private set; }
        public DateTime Horodatage { get; private set; }
        public decimal Montant { get; private set; }
        public int Expéditeur { get; private set; }
        public int Destinataire { get; private set; }
        public enum Etat { OK, KO }
        public Etat Statut { get; private set; }

        public Transactions(int identifiant, DateTime horodatage, decimal montant, int expéditeur, int destinataire)
        {
            identifiant_t = identifiant;
            Horodatage = horodatage;
            Montant = montant;
            Expéditeur = expéditeur;
            Destinataire = destinataire;
            Statut = Etat.KO;

        }
        public void OK()
        {
            // donner un statut ok de la transaction
            Statut = Etat.OK;
        }
    }
}

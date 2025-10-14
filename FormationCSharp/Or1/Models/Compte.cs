using System.Collections.Generic;

namespace Or.Models
{
    public enum TypeCompte { Courant, Livret }

    public struct MessErreur
    {
        public bool Condition;
        public string message;
        public decimal type;
        public long numero;
    }
    
    public class Compte
    {
        public int Id { get; set; }
        public long IdentifiantCarte { get; set; }
        public TypeCompte TypeDuCompte { get; set; }
        public decimal Solde { get; private set; }

        public Compte(int id, long identifiantCarte, TypeCompte type, decimal soldeInitial)
        {
            Id = id;
            IdentifiantCarte = identifiantCarte;
            TypeDuCompte = type;
            Solde = soldeInitial;
        }

        /// <summary>
        /// Action de dépôt d'argent sur le compte bancaire
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>Statut du dépôt</returns>
        public MessErreur EstDepotValide(Transaction transaction)
        {
            MessErreur messErreur = new MessErreur();

            if (transaction.Montant > 0)
            {
                messErreur.Condition = true;
            }
            else
            {
                messErreur.Condition = false;
                messErreur.message = "Le dépôt n'est pas valide";
            }
            return messErreur;
        }

        /// <summary>
        /// Action de retrait d'argent sur le compte bancaire
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>Statut du retrait</returns>
        public MessErreur EstRetraitValide(Transaction transaction)
        {
            MessErreur messErreur = new MessErreur();

            if (EstRetraitAutorise(transaction.Montant).Condition)
            {
                messErreur.Condition = true;
            }
            else
            {
                messErreur.Condition = false;
                messErreur.message = EstRetraitAutorise(transaction.Montant).message;
            }
            return messErreur;  
        }

        private MessErreur EstRetraitAutorise(decimal montant)
        {
            MessErreur messErreur = new MessErreur();
            messErreur.Condition = (Solde >= montant && montant > 0);

            if (messErreur.Condition == false)
            {
                messErreur.message = "le retrait est non autorisé";
            }
            return messErreur;
        }
       

    }
}

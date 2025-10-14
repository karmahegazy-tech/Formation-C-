using Or.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using static Or.Business.SqlRequests;

namespace Or.Models
{

    public class Carte
    {
        public long Id { get; set; }
        public decimal Plafond { get; set; }
        public string PrenomClient { get; set; }
        public string NomClient { get; set; }
        public List<int> ListComptesId { get; set; }
        public List<Transaction> Historique { get; private set; }

        public Carte(long id, string prenom, string nom, decimal plafondMax = 0)
        {
            Id = id;
            PrenomClient = prenom;
            NomClient = nom;
            Plafond = plafondMax == 0 ? 500 : plafondMax;
            ListComptesId = new List<int>();
            Historique = new List<Transaction>();
        }

        public void AlimenterHistoriqueEtListeComptes(List<Transaction> hist, List<int> comptesId)
        {
            ListComptesId = comptesId;
            Historique = hist;
        }

        public void AjoutTransactionValidee(Transaction transac)
        {
            Historique.Add(transac);
        }

        // -------------------------------------------------------------------------------------------------------------
        //                              Contraintes sur les retraits et virements
        // -------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Est-ce que le retrait (retrait simple, virement) est il autorisé au niveau de la carte ? 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="Expediteur"></param>
        /// <param name="Destinataire"></param>
        /// <returns></returns>
        public MessErreur EstRetraitAutoriseNiveauCarte(Transaction transaction, Compte Expediteur, Compte Destinataire)
        {
            MessErreur messErreur = new MessErreur();
            messErreur.Condition = (EstOperationAutoriseeContraintesComptes(Expediteur, Destinataire).Condition && EstEligibleMaximumRetraitHebdomadaire(transaction.Montant, transaction.Horodatage).Condition);

            //alimentation du message d'erreur
            if (EstEligibleMaximumRetraitHebdomadaire(transaction.Montant, transaction.Horodatage).Condition == false)
            {
                messErreur.message = EstEligibleMaximumRetraitHebdomadaire(transaction.Montant, transaction.Horodatage).message;
            }
            else if (EstOperationAutoriseeContraintesComptes(Expediteur, Destinataire).Condition == false)
            {
                messErreur.message = EstOperationAutoriseeContraintesComptes(Expediteur, Destinataire).message;
            }
            return messErreur;
        }

        /// <summary>
        /// Test d'éligibilité par rapport au plafond maximal de la carte
        /// </summary>
        /// <param name="montant"></param>
        /// <param name="dateEffet"></param>
        /// <returns></returns>
        public MessErreur EstEligibleMaximumRetraitHebdomadaire(decimal montant, DateTime dateEffet)
        {
            MessErreur messErreur = new MessErreur();
            List<Transaction> retraitsHisto = Historique.Where(x => (x.Horodatage > dateEffet.AddDays(-10)) && ListComptesId.Contains(x.Expediteur)).Select(x => x).ToList();
            decimal sommeHisto = montant + retraitsHisto.Sum(x => x.Montant);

            //aimentation des resultats
            messErreur.Condition = (sommeHisto < Plafond);
            messErreur.type = retraitsHisto.Sum(x => x.Montant);
            //cas d'erreur sur le plafond
            if (sommeHisto > Plafond)
            {
                messErreur.message = "Le plafond sur 10 jours est atteint";
            }
            return messErreur;
        }

        /// <summary>
        /// Est-ce que les contraintes sur les comptes bancaires sont respectées ? 
        /// </summary>
        /// <param name="Expediteur"></param>
        /// <param name="Destinataire"></param>
        /// <returns></returns>
        private MessErreur EstOperationAutoriseeContraintesComptes(Compte Expediteur, Compte Destinataire)
        {
            MessErreur messErreur = new MessErreur();

            // Est-ce que la transaction demandée est possible ?
            if (Tools.EstTransactionExterieure(Expediteur.Id, Destinataire.Id))
            {
                messErreur.message = "Un numéro de compte est mal transcrit à la transaction";
                messErreur.Condition = false;
            }

            // Opération Interne 
            if (EstOperationInterne(Expediteur.Id, Destinataire.Id).Condition)
            {
                messErreur.Condition = true;

            }
            else if (EstOperationInterne(Expediteur.Id, Destinataire.Id).Condition == false)
            {
                messErreur.Condition = false;
                messErreur.message = EstOperationInterne(Expediteur.Id, Destinataire.Id).message;

            }

            // Opération externe
            else
            {
                messErreur.Condition = EstOperationExterneAutorise(Expediteur, Destinataire).Condition;
                if (messErreur.Condition == false)
                {
                    messErreur.message = EstOperationExterneAutorise(Expediteur, Destinataire).message;
                }
            }
            return messErreur;
        }

        /// <summary>
        /// Le compte appartient-il à la carte ? 
        /// </summary>
        /// <param name="idtCpt"></param>
        /// <returns></returns>
        public MessErreur EstComptePresent(int idtCpt)
        {
            MessErreur messErreur = new MessErreur();

            messErreur.Condition = ListComptesId.Exists(x => x == idtCpt);
            if (messErreur.Condition == false)
            {
                messErreur.message = "Le compte n'appartient pas à une carte";
            }
            return messErreur;
        }

        /// <summary>
        /// Est ce qu'il s'agit d'une opération interne possible en principe ? 
        /// </summary>
        /// <param name="cptExt"></param>
        /// <param name="cptDest"></param>
        /// <returns></returns>
        private MessErreur EstOperationInterne(int cptExt, int cptDest)
        {
            MessErreur messErreur = new MessErreur();

            Operation operation = Tools.TypeTransaction(cptExt, cptDest);
            messErreur.Condition =
               (
                operation == Operation.DepotSimple ||
                operation == Operation.RetraitSimple ||
               (operation == Operation.InterCompte && EstComptePresent(cptExt).Condition && EstComptePresent(cptDest).Condition)
               );
            if (messErreur.Condition == false)
            {
                messErreur.message = "L'Opération interne n'est pas possible";
            }
            return messErreur;
        }

        /// <summary>
        /// S'agit il d'une opération inter-compte externe possible ? 
        /// </summary>
        /// <param name="Expediteur"></param>
        /// <param name="Destinataire"></param>
        /// <returns></returns>
        private MessErreur EstOperationExterneAutorise(Compte Expediteur, Compte Destinataire)
        {
            MessErreur messErreur = new MessErreur();

            Operation operation = Tools.TypeTransaction(Expediteur.Id, Destinataire.Id);

            messErreur.Condition = (operation == Operation.InterCompte && Expediteur.TypeDuCompte == TypeCompte.Courant && Destinataire.TypeDuCompte == TypeCompte.Courant);

            if (messErreur.Condition == false)
            {
                messErreur.message = "L'Opération inter-compte pas autorisée";
            }
            return messErreur;
        }

    }
}
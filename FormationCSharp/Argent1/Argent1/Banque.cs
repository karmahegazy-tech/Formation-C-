using System;
using System.Collections.Generic;

namespace Argent1
{
    public struct TypeExistence
    {
        public bool Resultat;
        public string type;
        public long numcarte;
        public int indexExp;
        public int indexDes;
        public decimal _Plafond;
    }

    // Je te rajoute un RoleCompte pour rendre plus simple la compréhension de q et 'E' ou 'D'
    public enum RoleCompte
    {
        Expéditeur,
        Destinataire
    }
     
    internal class Banque
    {
        // Intéressant, une manière possible de stocker ta liste de Cartes et Comptes aurait pu être d'utiliser un dictionnaire
        public List<CarteBancaire> Carte { get; set; }
        public List<CompteBancaire> Compte { get; set; }
        public List<Transactions> transaction { get; set; }

        // variable de verification de type de compte
        const string courant = "Courant";
        const string livret = "Livret";

        public Banque(List<CarteBancaire> carte, List<CompteBancaire> compte, List<Transactions> _transaction)
        {
            Carte = carte;
            Compte = compte;;
            transaction = _transaction;
        }

        // Bonne idée de refactoriser cette méthode - beaucoup trop longue et difficilement maintenable, tu pourrais avoir des méthodes privées pour chaque 'bloc'  
        public void MainTraitement()
        {
            long carteExp = 0;

            for (int i = 0; i < transaction.Count; i++)
            {
                // Question bête, que représente q et la valeur E ?!? 
                RoleCompte q = RoleCompte.Expéditeur;
                //char q = 'E';
                decimal montant = transaction[i].Montant;
                string operation = null;
                int index = 0;

                // J'ai déplacé les deux constantes comme attributs 

                //Cas 1: UN DEPOT MM CARTE
                if (EstDepot(transaction[i].Expéditeur, transaction[i].Destinataire))
                //if ((transaction[i].Expéditeur == 0) && (transaction[i].Destinataire != 0))
                {
                    //regarder si le compte/carte destinataire existe
                    int numCompte = transaction[i].Destinataire;
                    if (Check(numCompte, q, montant).Resultat == true)
                    {
                        //changement solde Expediteur 
                        operation = "dépôt";
                        index = Check(numCompte, q, montant).indexExp;
                        Compte[index].ApplicationTransation(operation, montant);

                        //changement solde Destinataire 
                        // Etrange d'appliquer le retrait sur le compte pour un Dépôt 
                        operation = "retrait";
                        index = Check(numCompte, q, montant).indexDes;
                        Compte[index].ApplicationTransation(operation, montant);

                        //changement du status
                        transaction[i].OK();
                    }

                }
                //Cas 2: TRANSACTION NORMAL
                else if (EstVirement(transaction[i].Expéditeur, transaction[i].Destinataire))
                //else if ((transaction[i].Expéditeur != 0) && (transaction[i].Destinataire != 0))
                {

                    //regarder si le compte/carte expediteur existe et si le montant est valide
                    int NumCompte = transaction[i].Expéditeur;

                    // 1) Cas où le compte expéditeur est COURANT
                    if ((Check(NumCompte, q, montant).Resultat == true) && (Check(NumCompte, q, montant).type == courant))
                    {
                        // verfication des transactions faite pour le compte expediteur 
                        DateTime DateComparé = transaction[i].Horodatage;
                        if ((Check(NumCompte, q, montant)._Plafond - CumulJours(NumCompte, DateComparé)) > montant)
                        {
                            carteExp = Check(NumCompte, q, montant).numcarte;
                            NumCompte = transaction[i].Destinataire;
                            q = RoleCompte.Destinataire; // plus simple, non ? 

                            // Cas où le compte destinataire existe et, est COURANT
                            if ((Check(NumCompte, q, montant).Resultat == true) && (Check(NumCompte, q, montant).type == courant))
                            {
                                //changement solde Expediteur 
                                operation = "dépôt";
                                index = Check(NumCompte, q, montant).indexExp;
                                Compte[index].ApplicationTransation(operation, montant);

                                //changement solde Destinataire 
                                operation = "retrait";
                                index = Check(NumCompte, q, montant).indexDes;
                                Compte[index].ApplicationTransation(operation, montant);

                                //changement du status
                                transaction[i].OK();
                            }
                            // Cas où le compte destinataire existe et, est LIVRET 
                            else if ((Check(NumCompte, q, montant).Resultat == true) && (Check(NumCompte, q, montant).type == livret) && (carteExp == Check(NumCompte, q, montant).numcarte))
                            {
                                //changement solde Expediteur 
                                operation = "dépôt";
                                index = Check(NumCompte, q, montant).indexExp;
                                Compte[index].ApplicationTransation(operation, montant);

                                //changement solde Destinataire 
                                operation = "retrait";
                                index = Check(NumCompte, q, montant).indexDes;
                                Compte[index].ApplicationTransation(operation, montant);

                                //changement du status
                                transaction[i].OK();
                            }
                        }
                    }
                    // 2) Cas où le compte expéditeur est LIVRET
                    else if ((Check(NumCompte, q, montant).Resultat == true) && (Check(NumCompte, q, montant).type == livret))
                    {
                        // verfication des transactions faite pour le compte expediteur 
                        DateTime DateComparé = transaction[i].Horodatage;
                        if ((Check(NumCompte, q, montant)._Plafond - CumulJours(NumCompte, DateComparé)) > montant)
                        {
                            carteExp = Check(NumCompte, q, montant).numcarte;
                            NumCompte = transaction[i].Destinataire;
                            q = RoleCompte.Destinataire;

                            // Cas où le compte destinataire existe, et est COURANT ou LIVRET
                            if ((Check(NumCompte, q, montant).Resultat == true) && (carteExp == Check(NumCompte, q, montant).numcarte))
                            {
                                //changement solde Expediteur 
                                operation = "dépôt";
                                index = Check(NumCompte, q, montant).indexExp;
                                Compte[index].ApplicationTransation(operation, montant);

                                //changement solde Destinataire 
                                operation = "retrait";
                                index = Check(NumCompte, q, montant).indexDes;
                                Compte[index].ApplicationTransation(operation, montant);

                                //changement du status
                                transaction[i].OK();
                            }
                        }
                    }
                }
                //Cas 3: UN RETRAIT MM CARTE
                else if (EstRetrait(transaction[i].Expéditeur, transaction[i].Destinataire)) // c'est plus clair, non ? 
                //else if ((transaction[i].Expéditeur != 0) && (transaction[i].Destinataire == 0))
                {
                    //regarder si le compte/carte expediteur existe et si le montant est valide
                    int NumCompte = transaction[i].Expéditeur;

                    // Cas où le compte expéditeur est COURANT
                    if ((Check(NumCompte, q, montant).Resultat == true) && (Check(NumCompte, q, montant).type == courant))
                    {
                        // verfication des transactions faite pour le compte expediteur 
                        DateTime DateComparé = transaction[i].Horodatage;
                        if ((Check(NumCompte, q, montant)._Plafond - CumulJours(NumCompte, DateComparé)) > montant)
                        {
                            //changement solde Expediteur 
                            operation = "dépôt";
                            index = Check(NumCompte, q, montant).indexExp;
                            Compte[index].ApplicationTransation(operation, montant);

                            //changement du status
                            transaction[i].OK();
                        }
                    }
                }
            }
        }

        private bool EstRetrait(int expediteur, int destinataire)
        {
            return (expediteur != 0) && (destinataire == 0);
        }

        private bool EstDepot(int expediteur, int destinataire)
        {
            return (expediteur == 0) && (destinataire != 0);
        }

        private bool EstVirement(int expediteur, int destinataire)
        {
            return (expediteur != 0) && (destinataire != 0);
        }


        // Check fonctionne bien, mais il fait deux choses, la vérification du compte et de la carte
        public TypeExistence Check(int NumCompte, RoleCompte role, decimal montant)
        {
            TypeExistence resultat = new TypeExistence();
            //regarder si le compte existe
            for (int j = 0; Compte.Count > j; j++)
            {
                // avec un dictionnaire tu n'auras pas besoin de faire un foreach
                if (Compte[j].CompteExiste(NumCompte) == true)
                {

                    // check si le montant de transaction pour l'expediteur peut être retiré (solde)
                    // E expéditeur, D destinataire, q - destinataire ou expéditeur 
                    if (role == RoleCompte.Expéditeur)
                    {
                        if (Compte[j].RetraitOk(montant) == true)
                        {
                            resultat.type = Compte[j].Type;
                            resultat.indexExp = j;
                        }
                    }
                    else
                    {
                        resultat.type = Compte[j].Type;
                        resultat.indexDes = j;
                    }

                    //regarder si le compte appartient à une carte
                    for (int k = 0; Carte.Count > k; k++)
                    {
                        // avec un dico, ce serait plus simple
                        long NumCarte_cb = Compte[k].NumCarte_cb;
                        if (Carte[k].CarteExiste(NumCarte_cb) == true)
                        {
                            // check si le montant de transaction pour l'expediteur peut être retiré (plafond) 
                            if (role == RoleCompte.Expéditeur)
                            {
                                if (Carte[k].PlafondOk(montant) == true)
                                {
                                    resultat.Resultat = true;
                                    resultat.numcarte = Carte[k].NumCarte;
                                    resultat._Plafond = Carte[k].plafond;
                                    break;
                                }
                            }
                            else
                            {

                                resultat.Resultat = true;
                                resultat.numcarte = Carte[k].NumCarte;
                                break;
                            }
                        }
                    }
                    break;
                }
            }

            return resultat;
        }

        public decimal CumulJours(int NumCompte, DateTime DateComparé)
        {
            decimal cumul = 0;
            for (int i = 0; i < transaction.Count; i++)
            {
                int difference = DateComparé.Day - transaction[i].Horodatage.Day;

                // si on trouve une transaction avec le même expéditeur, que la transation soit validée et que ca soit moins que 10 jours
                if ((transaction[i].Expéditeur == NumCompte) && (transaction[i].Statut == Transactions.Etat.OK) && (difference < 10) && (difference > 0) )
                {
                    cumul += transaction[i].Montant;
                }
            }
            return cumul;
        }

    }
}


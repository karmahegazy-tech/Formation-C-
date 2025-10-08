using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Argent1
{
    public struct TypeExistance
    {
        public bool Resultat;
        public string type;
        public long numcarte;
        public int indexExp;
        public int indexDes;
        public decimal _Plafond;
    }

    internal class Banque
    {
        public List<CarteBancaire> Carte { get; set; }
        public List<CompteBancaire> Compte { get; set; }
        public List<Transactions> transaction { get; set; }

        public Banque(List<CarteBancaire> carte, List<CompteBancaire> compte, List<Transactions> _transaction)
        {
            Carte = carte;
            Compte = compte;;
            transaction = _transaction;
        }

        public void MainTraitement()
        {
            long carteExp = 0;

            for (int i = 0; i < transaction.Count; i++)
            {
                char q = 'E';
                decimal montant = transaction[i].Montant;
                string operation = null;
                int index = 0;

                // variable de verfication de type de compte
                const string courant = "Courant";
                const string livret = "Livret";

                //Cas 1: UN DEPOT MM CARTE
                if ((transaction[i].Expéditeur == 0) && (transaction[i].Destinataire != 0))
                {
                    //regarder si le compte/carte destinataire existe
                    int NumCompte = transaction[i].Destinataire;
                    if (Check(NumCompte, q, montant).Resultat == true)
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
                //Cas 2: TRANSACTION NORMAL
                else if ((transaction[i].Expéditeur != 0) && (transaction[i].Destinataire != 0))
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
                            q = 'D';

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
                            q = 'D';

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
                else if ((transaction[i].Expéditeur != 0) && (transaction[i].Destinataire == 0))
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

        public TypeExistance Check(int NumCompte, char q, decimal montant)
        {
            TypeExistance resultat = new TypeExistance();
            //regarder si le compte existe
            for (int j = 0; Compte.Count > j; j++)
            {
                if (Compte[j].CompteExiste(NumCompte) == true)
                {

                    // check si le montant de transaction pour l'expediteur peut être retraiter (solde)
                    if (q == 'E')
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
                        long NumCarte_cb = Compte[k].NumCarte_cb;
                        if (Carte[k].CarteExiste(NumCarte_cb) == true)
                        {
                            // check si le montant de transaction pour l'expediteur peut être retraiter (plafond) 
                            if (q == 'E')
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

                // si on trouve une transaction avec le même expéditeur, que la transation soit validée ett que ca soit moins que 10 jours
                if ((transaction[i].Expéditeur == NumCompte) && (transaction[i].Statut == Transactions.Etat.OK) && (difference < 10) && (difference > 0) )
                {
                    cumul += transaction[i].Montant;
                }
            }
            return cumul;
        }

    }
}


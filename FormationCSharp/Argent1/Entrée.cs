using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Argent1
{
    internal class Entrée
    {
        public List<Transactions> LectureTransaction(string Transactions)
        {
            // creation d'une liste de la transaction
            List<Transactions> transaction = new List<Transactions> { };

            using (FileStream file = new FileStream("Transactions.csv", FileMode.Open, FileAccess.Read))
            {
                //La route de monl fichier input
                StreamReader sr = new StreamReader(Transactions);

                //Lecture sequentielle de mon fichier
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] phrase = line.Split(';');

                    //IGNIORE : si la transaction n'a pas d'expéditeur ou un destinataire
                    if (string.IsNullOrEmpty(phrase[3]) || string.IsNullOrEmpty(phrase[4]))
                    {
                        line = sr.ReadLine();
                    }

                    transaction.Add(new Transactions(int.Parse(phrase[0]), DateTime.Parse(phrase[1]), Decimal.Parse(phrase[2]), int.Parse(phrase[3]), int.Parse(phrase[4])));
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            return transaction;
        }
        public List<CompteBancaire> LectureCompte(string Comptes)
        {
            // creation d'une liste des comptes
            List<CompteBancaire> Compte = new List<CompteBancaire> { };

            using (FileStream file = new FileStream("Comptes.csv", FileMode.Open, FileAccess.Read))
            {
                //La route de mon fichier input
                StreamReader sr = new StreamReader(Comptes);

                //Lecture sequentielle de mon fichier
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] phrase = line.Split(';');

                    // si le valeur du solde n'existe  pas le mettre à zero par defaut
                    if (string.IsNullOrEmpty(phrase[3]))
                    {
                        phrase[3] = "0";
                    }
                    //IGNIORE : si un compte n'a pas de numéro, un type, ou une numéro de carte associé 
                    if (string.IsNullOrEmpty(phrase[0])|| string.IsNullOrEmpty(phrase[1])|| string.IsNullOrEmpty(phrase[3]))
                    {
                        line = sr.ReadLine();
                    }

                    Compte.Add(new CompteBancaire(int.Parse(phrase[0]), long.Parse(phrase[1]), phrase[2], decimal.Parse(phrase[3])));

                    line = sr.ReadLine();
                }
                sr.Close();
            }
            return Compte;

        }
        public List<CarteBancaire> LectureCarte(string Cartes)
        {
            // creation d'une liste des cartes
            List<CarteBancaire> Carte = new List<CarteBancaire> { };

            using (FileStream file = new FileStream("Cartes.csv", FileMode.Open, FileAccess.Read))
            {
                //La route de mon fichier input
                StreamReader sr = new StreamReader(Cartes);

                //Lecture sequentielle de mon fichier
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] phrase = line.Split(';');

                    // si le valeur du plafond n'existe  pas le mettre à 500 par defaut
                    if (string.IsNullOrEmpty(phrase[1]))
                    {
                        phrase[1] = "500";
                    }
                    
                    if (decimal.Parse(phrase[1]) < 500 )
                    {
                        phrase[1] = "500";
                    }
                    else if (decimal.Parse(phrase[1]) > 3000)
                    {
                        phrase[1] = "3000";
                    }

                    //IGNIORE : si la carte n'a pas un numéro  
                    if (string.IsNullOrEmpty(phrase[0]))
                    {
                        line = sr.ReadLine();
                    }

                    Carte.Add(new CarteBancaire(long.Parse(phrase[0]), decimal.Parse(phrase[1])));

                    line = sr.ReadLine();
                }
                sr.Close();
            }
            return Carte;
        }
        

    }
}

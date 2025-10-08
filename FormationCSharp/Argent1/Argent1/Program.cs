using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argent1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Entrée entrée = new Entrée();
            // lecture et creation des listes
            List<Transactions> transaction = entrée.LectureTransaction("Transactions.csv");
            List<CompteBancaire> Compte = entrée.LectureCompte("Comptes.csv");
            List<CarteBancaire> Carte = entrée.LectureCarte("Cartes.csv");

            // traitement du fichier
            Banque banque = new Banque(Carte, Compte, transaction);
            banque.MainTraitement();

            // creation du fichier sortie et son alimentation
            Sortie sortie = new Sortie();
            sortie.CreationSortie(transaction);

            Console.ReadKey();
        }
    }
}

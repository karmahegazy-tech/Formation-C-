using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie2
{
    public static class Morpion
    {
        public static void DisplayMorpion(char[,] tabxo)
        {
            Console.WriteLine("affichage grille de morpion");


            int a = tabxo.GetLength(1);
            int b = tabxo.GetLength(0);


            for (int j = 0; j < a; j++)
            {
                for (int i = 0; i < b; i++)
                {
                    Console.Write($"{tabxo[i, j]} ");

                }
                Console.WriteLine();
            }

            return;
        }

        public static int CheckMorpion(char[,] tabxo)
        {

            char joueur = 'x';

            // verification de la victoire : 3 possibilité

            int a = -1;

            // verification de la victoire sur les lignes
            for (int i = 0; i < 3; i++)
            {
                if (tabxo[i, 0] == joueur && tabxo[i, 1] == joueur && tabxo[i, 2] == joueur)
                {
                    Console.WriteLine($"le joueur {joueur} a gagné !!!");
                    if (joueur == 'x')
                    {
                        a = 1;
                    }
                    else
                    {
                        a = 2;
                    }

                }
            }

            // verification de la victoire sur les colonnes
            for (int i = 0; i < 3; i++)
            {
                if (tabxo[0, i] == joueur && tabxo[1, i] == joueur && tabxo[2, i] == joueur)
                {

                    Console.WriteLine($"le joueur {joueur} a gagné !!!");
                    if (joueur == 'x')
                    {
                        a = 1;
                    }
                    else
                    {
                        a = 2;
                    }
                }

            }
            // verification de la victoire sur les diagonales
            if ((tabxo[0, 0] == joueur && tabxo[1, 1] == joueur && tabxo[2, 2] == joueur) || (tabxo[0, 2] == joueur && tabxo[1, 1] == joueur && tabxo[2, 0] == joueur))
            {

                Console.WriteLine($"le joueur {joueur} a gagné !!!");
                if (joueur == 'x')
                {
                    a = 1;
                }
                else
                {
                    a = 2;
                }

            }

            // cas d'égalité 


            if (a == -1)
            {
                Console.WriteLine("dommage personne a gagné! beheheheheheheh");
            }

            return a;

        }

        
        
    }
}

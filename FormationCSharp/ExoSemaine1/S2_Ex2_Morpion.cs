using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie2
{
    public static class Morpion
    {
        public static void DisplayMorpion(/*typeGrille grille */)
        {
            Console.WriteLine("affichage grille de morpion");

            char[,] tabxo = new char[,] {{ '-', '-', '-' }, { '-', '-', '-' }, { '-', '-', '-' } };

            int a = tabxo.GetLength(1);
            int b = tabxo.GetLength(0);


            for (int j = 0; j < a; j++)
            {
                for (int i = 0; i < b; i++)
                {
                    Console.Write($"{tabxo[i,j]} ");

                }
                Console.WriteLine();
            }
     
            return ;
        }

        public static int CheckMorpion(/*typeGrille grille */)
        {
            //TODO
            return -1;
        }
    }
}

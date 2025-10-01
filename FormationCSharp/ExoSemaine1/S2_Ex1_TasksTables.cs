using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie2
{
    public static class TasksTables
    {
        

        public static int SumTab(int[] tab)
        {
            Console.WriteLine("Somme des élements d'un tableau");
            int i = 0;
            int sum = 0;

            Console.WriteLine($"Somme : {sum}");
            Console.Write("tab : [");

            for (; i < tab.Length;)
            {

                sum += tab[i];
                Console.Write($"{tab[i]} " );
                i++;
            }
            Console.Write("]");
            Console.WriteLine();
            Console.WriteLine($"Somme : {sum}");

            return sum;
        }

        public static int[] OpeTab(int[] tab, char ope, int b)
        {
            Console.WriteLine("Opération sur un tableau");
            Console.Write("res : [");
                       
            int i= 0;

            for (; i < tab.Length;)
            {
                if (ope == '+')
                {
                    tab[i] = tab[i] + b;

                }
                else if (ope == '-')
                {
                    tab[i] = tab[i] - b;

                }
                else if (ope == '*')
                {
                    tab[i] = tab[i] * b;

                }

                else
                {
                    Console.WriteLine(" operation invalide");
                }

                Console.Write($"{tab[i]} ");
                i++;
               
            }

            Console.Write("]");
            Console.WriteLine();

            return tab;
        }

        public static int[] ConcatTab(int[] tab1, int[] tab2)
        {
            Console.WriteLine("Concétenation sur deux tableaux");

            int[] tab12 = new int[tab1.Length + tab2.Length];

            /* affichage de la table 1 */

            Console.Write("tab1 : [");
            for (int i = 0; i < tab1.Length; i++)
            {
                tab12[i] = tab1[i];
                Console.Write($"{tab1[i]} ");
                i++;
            }
            Console.Write("]");
            Console.WriteLine();


            /* affichage de la table 2 */

            Console.Write("tab2 : [");
            for (int i = 0; i < tab2.Length; i++)
            {
                tab12[tab1.Length + i] = tab2[i];
                Console.Write($"{tab2[i]} ");
            }
            Console.Write("]");
            Console.WriteLine();

            /* affichage de la table 1 et la table 2 */

            Console.Write("tab1 et tab2 : [");
            for (int i = 0; i < tab1.Length + tab2.Length; i++)
            {
                Console.Write($"{tab12[i]} ");

            }
            Console.Write("]");
            Console.WriteLine();


            return tab12;
        }

    }
}

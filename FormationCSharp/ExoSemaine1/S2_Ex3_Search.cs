using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Serie2
{
    public static class Search
    {
        public static int LinearSearch(int[] tableau, int valeur)
        {
            int a = -1;
            if (tableau.Length == 0)
            {
                Console.WriteLine("le tableau est vide");
            }
            else
            {
                for (int i = 0; i < tableau.Length; i++)
                {
                    if (tableau[i] == valeur)
                    {
                        Console.WriteLine($"la position de la valeur cherché, en commencant par zéro, est :{i}");
                        a = 1;
                    }

                }
                if (a == -1)
                {
                    Console.WriteLine($"{valeur} n'était pas trouvé");
                }
            }


            return a;
        }

        public static int BinarySearch(int[] tableau, int valeur)
        {
            int haut = tableau.Length;
            int bas = 0;
            int mid = (haut + bas) / 2;

            int a = -1;


            for (int i = 0; i < (tableau.Length/2); i++)
            {
                if (tableau[mid] == valeur)
                {
                    if (a == -1)
                    {
                        Console.WriteLine($"la position de la valeur cherché, en commencant par zéro, est :{mid}");
                        a = 1;
                    }
                }
                else if (tableau[mid] < valeur)
                {
                    if (haut - mid == 1)
                    {
                        mid = haut;
                    }
                    else
                    {
                        bas = mid + 1;
                        mid = (haut + bas) / 2;
                    }
                }
                else
                {
                    if (bas - mid == 1)
                    {
                        mid = bas;
                    }
                    else
                    {
                        haut = mid - 1;
                        mid = (haut + bas) / 2;
                    }
                }
            }

            if (a == -1)
            {
                Console.WriteLine($"{valeur} n'était pas trouvé");
            }

            return a;


        }
    }
}

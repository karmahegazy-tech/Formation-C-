using Serie1;
using Serie2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serie1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* 
            SERIE 1
            ----------------------------------------------ELEMENTARY OPERATION
               int a = 12;
               int b = 5;


                Console.WriteLine("quelle est l'opération que vous souhaitez faire?");
                string texte = Console.ReadLine();
                char operation;
                bool estok = char.TryParse(texte, out operation);

                if (estok)
                {
                    ElementaryOperations.BasicOperation(a, b, operation);
                }



            ----------------------------------------------SPEAKING CLOCK

            
            DateTime dateheure = DateTime.Now;
            TimeSpan heuremaintenant = DateTime.Now.TimeOfDay;
            int heure = DateTime.Now.Hour;
            Console.WriteLine(heuremaintenant);
            Console.WriteLine(dateheure);

            SpeakingClock.GoodDay(heure);
          

            ----------------------------------------------PYRAMID

            int n = 10;
            bool isSmooth = false;
            Pyramid.PyramidConstruction(n, isSmooth);
            */


            /*
            SERIE 2
            ------------------------------------------TASKS TABLES

            -----SOMMES DES ELEMENTS D'UNE TABLE-----
            int[] tab = new int[] { -6, 4, 9 };
            TasksTables.SumTab(tab);


            ---------OPERATION SUR UNE TABLE---------

            int b = 5;

            Console.WriteLine("quelle est l'opération que vous souhaitez faire?");
            string texte = Console.ReadLine();
            char ope;
            bool estok = char.TryParse(texte, out ope);
            int[] tab = new int[] { -6, 4, 9 };

            if (estok)
            {
                TasksTables.OpeTab(tab, ope, b);
            }

            ---------CONCETENATION DES DEUX TABLEAUX---------
            

            int[] tab1 = new int[] { -6, 4, 9 };

            int[] tab2 = new int[] { 1, 1 };

            int[] Tab3 = TasksTables.ConcatTab(tab1, tab2);
            

            ------------------------------------------Morpion
              
            -----Affichage Grille-----
            
             */
    

        Morpion.DisplayMorpion();

            Console.ReadKey();
        }

    }
}



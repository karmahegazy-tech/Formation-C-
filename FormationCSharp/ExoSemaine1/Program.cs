using ExoSemaine1;
using Serie1;
using Serie4;
using Serie3;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace serie1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* 
            //SERIE 1
            //----------------------------------------------ELEMENTARY OPERATION
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



            //----------------------------------------------SPEAKING CLOCK

            
            DateTime dateheure = DateTime.Now;
            TimeSpan heuremaintenant = DateTime.Now.TimeOfDay;
            int heure = DateTime.Now.Hour;
            Console.WriteLine(heuremaintenant);
            Console.WriteLine(dateheure);

            SpeakingClock.GoodDay(heure);
          

            //----------------------------------------------PYRAMID

            int n = 10;
            bool isSmooth = false;
            Pyramid.PyramidConstruction(n, isSmooth);
            */


            /*
            //SERIE2
            //------------------------------------------TASKS TABLES------------------------------------------------------------

            //-----SOMMES DES ELEMENTS D'UNE TABLE-----
              int[] tab = new int[] { -6, 4, 9 };
              TasksTables.SumTab(tab);


            //---------OPERATION SUR UNE TABLE---------

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

            //---------CONCETENATION DES DEUX TABLEAUX---------


            int[] tab1 = new int[] { -6, 4, 9 };

            int[] tab2 = new int[] { 1, 1 };

            int[] Tab3 = TasksTables.ConcatTab(tab1, tab2);


            //------------------------------------------Morpion----------------------------------------------------------------

            // On a utilisée un tableau multi-dimension pour representer le jeu pour pouvoir avoir 9 cases, 3 x 3 
            char[,] tabxo = new char[,] { { '-', 'x', '-' }, { '-', 'x', 'o' }, { '-', 'x', '-' } };

            //-----Affichage Grille-----
            Morpion.DisplayMorpion(tabxo);
         
            //-----Affichage de vainqueur-------
            Morpion.CheckMorpion(tabxo);
            */
            //--------------------------------------------Recherche dans un tableau-------------------------------------------

            //-----Recherche lineaire------
            /*
            int[] tableau = new int[] { -6, 1, 2, 6, 7, 9, 10, 12, 15 };
            Console.WriteLine("quelle est la valeur que tu cherches?");
            string texte = Console.ReadLine();
            int valeur;
            bool estok = int.TryParse(texte, out valeur);

            if (estok)
            {
                Search.BinarySearch(tableau, valeur);

            }

            */
            //SERIE 3 
            //--------------------------------------------------Administrative Tasks-------------------------------------------
            /*
             
            //---------Elimination sedious thoughts------------
            string text = "Bonjour karma, comment vas tu? qu'est ce que tu vas faire aujourd'hui?";
            string[] prohibitedTerms = new string[] { "vas", "karma" };
            AdministrativeTasks.EliminateSeditiousThoughts(text, prohibitedTerms);

            */

            /*
            //-------control format---------
            string line = "Mlle Karma HEGAZY 08";        
            AdministrativeTasks.ControlFormat(line);
            */
            //-----change date-----------
            /*
            string report = "1982-10-09 : Appel suspect de M. Plenko Andrej à M. Dimitrov Nikolai, arrestation des deux suspects le 1982-10-19";
            AdministrativeTasks.ChangeDate(report);
            */

            //----------------------------------------------------CODE CESAR--------------------------------------------------------
            /*
            int x = 3;
            string line = "SALUT COMMENT VAS TU";

            Cesar cesar = new Cesar();
            
            string line2 = cesar.CesarCode(line);   
            cesar.DecryptCesarCode(line2);
            */
            /*
            string line2 = cesar.GeneralCesarCode(line, x);
            cesar.GeneralDecryptCesarCode(line2, x);
            */

            //----------------------------------------------------MORSE CODE---------------------------------------------------------
            //-----letter count
            /*
            Morse morse = new Morse();

            
            string code = "how is life ?";
            
            morse.LettersCount(code);
            
            morse.WordsCount(code);
              
            */
            /*
            StringCollection code = new StringCollection();

            code.Add("===.=.===.=...===.===.===...===.=.=...=.....===.===...===.===.===...=.===.=...=.=.=...=");
            code.Add("=...=.===.=...==...===.===.===...=.===.=");
;
            morse.MorseTranslation(code);

            */
            /*
            StringCollection code = new StringCollection();

            code.Add("===.=.===.=...===.===.===...===.=.=...=.....===.===...===.===.===...=.===.=...=.=.=...=");
            code.Add("===..=.===.=....===.===.===....===.=.=...=......===..===...===.===..===...=.===.=...=..=.=...=");

            morse.EfficientMorseTranslation(code);

            */
            /*
            String sentence = "SALUT HI";

            morse.MorseEncryption(sentence);
            */

            /*
            ClassCouncil.SchoolMeans("Moyenne.csv");
            */
            /*
            char[,] tabxo = new char[,] { { '-', '-', '-' }, { '-', '-', '-' }, { '-', '-', '-' } };

            Morpion.MorpionGame(tabxo);
            */
            Console.ReadKey();
        }

    }
}



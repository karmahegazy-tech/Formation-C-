using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie3
{
    public class Cesar
    {
        private readonly char[,] _cesarTable;

        public Cesar()
        {
            _cesarTable = new char[,]
            {
                { 'A', 'D' },
                { 'B', 'E' },
                { 'C', 'F' },
                { 'D', 'G' },
                { 'E', 'H' },
                { 'F', 'I' },
                { 'G', 'J' },
                { 'H', 'K' },
                { 'I', 'L' },
                { 'J', 'M' },
                { 'K', 'N' },
                { 'L', 'O' },
                { 'M', 'P' },
                { 'N', 'Q' },
                { 'O', 'R' },
                { 'P', 'S' },
                { 'Q', 'T' },
                { 'R', 'U' },
                { 'S', 'V' },
                { 'T', 'W' },
                { 'U', 'X' },
                { 'V', 'Y' },
                { 'W', 'Z' },
                { 'X', 'A' },
                { 'Y', 'B' },
                { 'Z', 'C' }
            };
        }

        public string CesarCode(string line)
        {
           
            StringBuilder line2 = new StringBuilder();
            for (int j = 0; j < line.Length; j++)
            {
                char a = line[j];

                //S'il y a des espaces qu'il les remplace avec des espaces
                //S'il y a des espaces qu'il les remplace avec des espaces
                if (a == ' ')
                {
                    line2.Insert(j, " ");
                }
                //les lettres il les remplace avec le lettre du meme ligne mais du deuxième colonne
                else
                {
                    for (int i = 0; i < 26; i++)
                    {
                        if (a == _cesarTable[i, 1])
                        {
                            line2.Insert(j, _cesarTable[i, 0]);

                        }
                    }
                }

                
            }
            Console.WriteLine(line2.ToString());
            return line2.ToString() ;
        }

        public string DecryptCesarCode(string line2)
        {
          
            StringBuilder line3 = new StringBuilder();
            for (int j = 0; j < line2.Length; j++)
            {
                char a = line2[j];

                //S'il y a des espaces qu'il les remplace avec des espaces
                if (a == ' ')
                {
                    line3.Insert(j, " ");
                }
                //les lettres il les remplace avec le lettre du meme ligne mais du deuxième colonne
                else
                {
                    for (int i = 0; i < 26; i++)
                    {
                        if (a == _cesarTable[i, 0])
                        {
                            line3.Insert(j, _cesarTable[i , 1]);

                        }
                    }
                }


            }
            Console.WriteLine(line3);
            return line3.ToString();
        }

        public string GeneralCesarCode(string line, int x)
        {
        
            StringBuilder line2 = new StringBuilder();
            for (int j = 0; j < line.Length; j++)
            {
                char a = line[j];

                //S'il y a des espaces qu'il les remplace avec des espaces 
                if (a == ' ')
                {
                    line2.Insert(j, " ");
                }
                //les lettres il les remplace avec un décalage donnée
                else
                {
                    for (int i = 0; i < 26; i++)
                    {
                        if (a == _cesarTable[i, 0])
                        {
                            line2.Insert(j, _cesarTable[i + x, 0]);

                        }
                    }
                }


            }
            Console.WriteLine(line2.ToString());
            return line2.ToString();
        }

        public string GeneralDecryptCesarCode(string line2, int x)
        {
           
            StringBuilder line3 = new StringBuilder();
            for (int j = 0; j < line2.Length; j++)
            {
                char a = line2[j];

                //S'il y a des espaces qu'il les remplace avec des espaces
                if (a == ' ')
                {
                    line3.Insert(j, " ");
                }
                //les lettres il les remplace avec un décalage donnée
                else
                {
                    for (int i = 0; i < 26; i++)
                    {
                        if (a == _cesarTable[(i + x) % 26, 0])
                        {
                            line3.Insert(j, _cesarTable[i, 0]);

                        }
                    }
                }


            }
            Console.WriteLine(line3);
            return line3.ToString();
        }
    }
}

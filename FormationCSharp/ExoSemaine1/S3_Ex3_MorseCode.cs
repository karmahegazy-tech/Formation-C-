using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Serie3
{
    public class Morse
    {
        private const string Taah = "===";
        private const string Ti = "=";
        private const string Point = ".";
        private const string PointLetter = "...";
        private const string PointWord = ".....";

        private readonly Dictionary<string, char> _alphabet;

        public Morse()
        {
            _alphabet = new Dictionary<string, char>()
            {
                {$"{Ti}.{Taah}", 'A'},
                {$"{Taah}.{Ti}.{Ti}.{Ti}", 'B'},
                {$"{Taah}.{Ti}.{Taah}.{Ti}", 'C'},
                {$"{Taah}.{Ti}.{Ti}", 'D'},
                {$"{Ti}", 'E'},
                {$"{Ti}.{Ti}.{Taah}.{Ti}", 'F'},
                {$"{Taah}.{Taah}.{Ti}", 'G'},
                {$"{Ti}.{Ti}.{Ti}.{Ti}", 'H'},
                {$"{Ti}.{Ti}", 'I'},
                {$"{Ti}.{Taah}.{Taah}.{Taah}", 'J'},
                {$"{Taah}.{Ti}.{Taah}", 'K'},
                {$"{Ti}.{Taah}.{Ti}.{Ti}", 'L'},
                {$"{Taah}.{Taah}", 'M'},
                {$"{Taah}.{Ti}", 'N'},
                {$"{Taah}.{Taah}.{Taah}", 'O'},
                {$"{Ti}.{Taah}.{Taah}.{Ti}", 'P'},
                {$"{Taah}.{Taah}.{Ti}.{Taah}", 'Q'},
                {$"{Ti}.{Taah}.{Ti}", 'R'},
                {$"{Ti}.{Ti}.{Ti}", 'S'},
                {$"{Taah}", 'T'},
                {$"{Ti}.{Ti}.{Taah}", 'U'},
                {$"{Ti}.{Ti}.{Ti}.{Taah}", 'V'},
                {$"{Ti}.{Taah}.{Taah}", 'W'},
                {$"{Taah}.{Ti}.{Ti}.{Taah}", 'X'},
                {$"{Taah}.{Ti}.{Taah}.{Taah}", 'Y'},
                {$"{Taah}.{Taah}.{Ti}.{Ti}", 'Z'},
                {$"{Ti}{Ti}", '+'},
            };
        }

        public int LettersCount(string code)
        {
            int longeur = 0;

            foreach (char c in code)
            {
                if (char.IsLetter(c))
                {
                    longeur++;
                }
            }
            Console.WriteLine($"La phrase est: {code}");
            Console.WriteLine($"Sa longeur est : {longeur} ");

            return longeur;
        }

        public int WordsCount(string code)
        {

            int longeur = 0;

            foreach (char c in code)
            {
                if (c == ' ')
                {
                    longeur++;
                }
            }
            Console.WriteLine($"La phrase est: {code}");
            Console.WriteLine($"Sa longeur est : {longeur} ");

            return longeur;

        }

        public string MorseTranslation(StringCollection code)
        {
            Console.WriteLine("Traduction Morse :");
            StringBuilder phrase = new StringBuilder();

            foreach (string s in code)
            {
                phrase.Clear();
                int count = 0;

                // remplacer les separateurs mots et lettres (chaines de caractère) par des symboles
                string code_ = s.Replace(PointWord, "?");
                code_ = code_.Replace(PointLetter, "!");

                //séparation des mots
                string[] mot = code_.Split('?');

                foreach (string c in mot)

                {

                    if (count > 0)
                    {
                        phrase.Append(" ");
                    }

                    // séparation des lettres
                    string[] lettre = c.Split('!');
                    for (int i = 0; i < lettre.Length; i++, count++)
                    {

                        if (_alphabet.ContainsKey(lettre[i]))
                        {
                            phrase.Append(_alphabet[lettre[i]]);
                        }
                    }

                }
                Console.WriteLine($"{s} : {phrase}");

            }

            return phrase.ToString();
        }

        public string EfficientMorseTranslation(StringCollection code)
        {
            Console.WriteLine("Traduction Morse :");
            StringBuilder phrase = new StringBuilder();

            foreach (string s in code)
            {
                phrase.Clear();
                int count = 0;

                // remplacer les separateurs mots et lettres (chaines de caractère) par des symboles

                string code_ = s;

                code_ = Regex.Replace(code_, @"\.{5,}", "?");
                code_ = code_.Replace(PointWord, "?");

                code_ = code_.Replace("....", "!");
                code_ = code_.Replace(PointLetter, "!");

                code_ = code_.Replace("..", ".");

                //séparation des mots
                string[] mot = code_.Split('?');

                foreach (string c in mot)

                {

                    if (count > 0)
                    {
                        phrase.Append(" ");
                    }

                    // séparation des lettres
                    string[] lettre = c.Split('!');
                    for (int i = 0; i < lettre.Length; i++, count++)
                    {

                        if (_alphabet.ContainsKey(lettre[i]))
                        {
                            phrase.Append(_alphabet[lettre[i]]);
                        }
                    }

                }
                Console.WriteLine($"{s} : {phrase}");

            }

            return phrase.ToString();
        }

        public string MorseEncryption(string sentence)
        {
            Console.WriteLine("Codage en Morse :");
            StringBuilder phrase = new StringBuilder();

            int count = 0;

            //séparation des mots
            string[] mot = sentence.Split(' ');

            foreach (string c in mot)

            {

                if (count > 0)
                {
                    phrase.Append(PointWord);
                }

                // séparation des lettres
                foreach (char a in c)
                {
                    foreach (KeyValuePair<string, char> kvp in _alphabet)
                    {

                        if (a == kvp.Value)
                        {
                            phrase.Append(kvp.Key);
                        }
                    }
                }
                count++;
            }
            Console.WriteLine($"{sentence} : {phrase.ToString()}");


            return phrase.ToString();
        }
    }
}


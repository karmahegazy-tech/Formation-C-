using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Serie3
{
    public static class AdministrativeTasks
    {
        public static string EliminateSeditiousThoughts(string text, string[] prohibitedTerms)
        {
            Console.WriteLine(text);
            for (int i = 0; i < prohibitedTerms.Length; i++)
            {
                if (text.Contains(prohibitedTerms[i]))
                {
                    string final = text.Replace(prohibitedTerms[i], new string('x', prohibitedTerms[i].Length));
                    text = final;
                }
                
            }
            Console.WriteLine(text);
            return text;
        }

        public static bool ControlFormat(string line)
        {
            Console.WriteLine(" Recencement des résidents : ");
            Console.WriteLine(line);
            string resultat = "ok";
            bool a = true;

            if (line.Substring(0, 4) != "M." && line.Substring(0, 4) != "Mme." && line.Substring(0, 4) != "Mlle")
            {
                if (resultat == "ok")
                {
                    resultat = "ko";

                    a = false;
                }
            }

            foreach (char c in line.Substring(5, 12))
            {
                if (char.IsDigit(c) || char.IsSymbol(c))
                {
                    if (resultat == "ok")
                    {
                        resultat = "ko";

                        a = false;
                    }
                }
            }
            foreach (char c in line.Substring(18, 2))
            {
                if (char.IsLetter(c) || char.IsSymbol(c))
                {
                    if (resultat == "ok")
                    {
                        resultat = "ko";

                        a = false;
                    }
                }
            }
            if (line.Substring(18) == null)
            {

                if (resultat == "ok")
                {
                    resultat = "ko";
                    a = false;
                }
            }
            else if (line.Substring(19) == null)
            {
                if (resultat == "ok")
                {
                    resultat = "ko";
                    a = false;
                }
            }
            Console.WriteLine(resultat);
            return a;

        }
        

        public static string ChangeDate(string report)
        {
            Console.WriteLine("correction des dates");
            Console.WriteLine("rapport en entrée");
            Console.WriteLine(report);

            string pattern = @"\b\d{4}-\d{2}\-\d{2}\b";
          
            MatchCollection ladate = Regex.Matches(report, pattern);
      
            foreach (var a in ladate)
            {     
                if (DateTime.TryParse(a.ToString(), out DateTime date))
                {
                    report = Regex.Replace(report, a.ToString(), date.ToString("dd.MM.yy"));
                }
            }

            Console.WriteLine("rapport en sortie");
            Console.WriteLine(report);

            return report;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie4
{
    public static class ClassCouncil
    {
        public static void SchoolMeans(string input)
        {
            // creation d'une liste avec les differents notes pour avoir la moyenne
            List<double> histoire = new List<double> { };
            List<double> maths = new List<double> { };

            //La route de mon fichier input
            StreamReader sr = new StreamReader(input);

            //Lecture de la premiere ligne de mon fichier
            string line = sr.ReadLine();

            while (line != null)
            {
                string[] phrase = line.Split(';');

                if (phrase[1] == "Histoire")
                {
                    histoire.Add(double.Parse(phrase[2]));
                }
                if (phrase[1] == "Maths")
                {
                    maths.Add(double.Parse(phrase[2]));
                }

                line = sr.ReadLine();
            }
            double average_histoire = histoire.Average();
            double average_maths = maths.Average();

            Console.WriteLine($"la moyenne de la classe en histoire: {average_histoire:00.0}");
            Console.WriteLine($"la moyenne de la classe en Math: {average_maths: 00.0}");

            sr.Close();

            using (FileStream file = new FileStream("Sortie.csv", FileMode.Create, FileAccess.Write))
            {

                using (StreamWriter sw = new StreamWriter(file))
                {

                    sw.WriteLine($"histoire;{average_histoire:00.0}");
                    sw.WriteLine($"maths;{average_maths:00.0}");

                }
            }
        }


    }
}



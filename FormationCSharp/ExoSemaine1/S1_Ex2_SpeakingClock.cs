using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie1
{
    public static class SpeakingClock
    {
        public static string GoodDay(int heure)
        {

            if (0 < heure && heure < 6)
            {
                Console.WriteLine(heure);
                Console.WriteLine("Merveilleuse nuit");
            }
            if (6 < heure && heure < 12)
            {
                Console.WriteLine(heure);
                Console.WriteLine("Bonne matinée");
            }
            if (heure == 12)
            {
                Console.WriteLine(heure);
                Console.WriteLine("bon appétit");
            }
            if (12 < heure && heure < 19)
            {
                Console.WriteLine(heure);
                Console.WriteLine("Profitez de votre après midi");
            }
            if (heure > 18)
            {
                Console.WriteLine(heure);
                Console.WriteLine("passer une bonne soirée");
            }

           
           
            return string.Empty;
        }
    }
}

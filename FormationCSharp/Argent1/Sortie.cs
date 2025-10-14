using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Argent1
{
    internal class Sortie
    {
        public void CreationSortie(List<Transactions> Transaction)
        {
            using (FileStream file = new FileStream("Sortie.csv", FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sr = new StreamWriter(file))
                {
                    for (int i = 0; i < Transaction.Count; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append($"{Transaction[i].identifiant_t};{Transaction[i].Statut}");
                        sr.WriteLine(sb);
                        Console.WriteLine(sb.ToString());
                    }
                    sr.Close();
                }
            } 
        }
    }
}

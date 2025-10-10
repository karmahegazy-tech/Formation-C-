using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Argent1
{
    internal class Sortie
    {
        // Ajout de fichierSortie 
        public void CreationSortie(string fichierSortie, List<Transactions> Transaction)
        {
            using (FileStream file = new FileStream(fichierSortie, FileMode.Create, FileAccess.Write))
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

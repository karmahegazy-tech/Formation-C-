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
        /*
        public void CreationSortie(string Comptes)
        {
            //creation de mon fichier

            using (FileStream file = new FileStream("Sortie.csv", FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    // mettre la premiere ligne avec les soldes des differents comptes
                    using (FileStream file2 = new FileStream("Comptes.csv", FileMode.Open, FileAccess.Read))
                    {
                        // inserer les noms des comptes 
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Sorties ");

                        //inserer les soldes des comptes avant traitements des transactions
                        StringBuilder sc = new StringBuilder();
                        sc.Append("        ");


                        StreamReader sr = new StreamReader(Comptes);
                        string line = sr.ReadLine();
                        while (line != null)
                        {
                            string[] phrase = line.Split(';');
                            sb.Append($"{phrase[1]}  " );
                            sb.Append($"{phrase[3]}  ");

                            line = sr.ReadLine();
                        }
                        sw.WriteLine(sb);
                        sw.WriteLine(sc);
                    }
                }
            }
            
        }
        */

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
                    }
                    sr.Close();
                }
            }
        }
    }
}

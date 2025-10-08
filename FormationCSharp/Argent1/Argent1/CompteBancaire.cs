using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Argent1
{
    internal class CompteBancaire
    {
        public int Identifiant { get; private set; }
        public long NumCarte_cb { get; private set; }
        public string Type { get; private set; }
        public decimal SoldeInitial { get; private set; }

        public CompteBancaire(int identifiant, long numCarte_cb, string type, decimal soldeInitial)
        {
            Identifiant = identifiant;
            NumCarte_cb = numCarte_cb;
            Type = type;
            SoldeInitial = soldeInitial;
        }
        
        //Verifier si le compte existe
        public bool CompteExiste(int NumCompte)
        {
            if (NumCompte == Identifiant)
            {
                return true;
            }
            return false;
        }

        //verifier si le compte peut debiter le montant 
        public bool RetraitOk(decimal montant)
        {
            if (montant < SoldeInitial)
            {
                return true;
            }
            return false;
        }
        //Appliquer l'operation
        public decimal ApplicationTransation(string operation, decimal montant)
        {
            if ((RetraitOk(montant) == true) && (operation == "retrait"))
            {
                SoldeInitial = SoldeInitial - montant;
            }
            if ((RetraitOk(montant) == true) && (operation == "dépôt"))
            {
                SoldeInitial = SoldeInitial + montant;
            }
            return SoldeInitial;
        }
    }
}

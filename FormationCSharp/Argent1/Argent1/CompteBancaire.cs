namespace Argent1
{
    internal class CompteBancaire
    {
        public int Identifiant { get; private set; }
        public long NumCarte_cb { get; private set; }
        public string Type { get; private set; }
        // Plutôt solde non ? 
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
            if ((0 < montant) && (montant <= SoldeInitial))
            {
                return true;
            }
            return false;
        }
        //Appliquer l'operation
        // ce serait intéressant d'utiliser une énumération sur le type d'opération
        public decimal ApplicationTransation(string operation, decimal montant)
        {
            if ((RetraitOk(montant) == true) && (operation == "retrait"))
            {
                SoldeInitial = SoldeInitial - montant;
            }
            // Etrange d'appeler le code de contrôle sur le Retrait avant une opération de dépôt sur l'instance qui reçoit le montant
            if ((RetraitOk(montant) == true) && (operation == "dépôt"))
            {
                SoldeInitial = SoldeInitial + montant;
            }
            return SoldeInitial;
        }
    }
}

namespace Argent1
{
    internal class CarteBancaire
    {
        public long NumCarte { get; set; }
        public decimal plafond { get; set; }

        public CarteBancaire(long numcarte, decimal Plafond) 
        {
            NumCarte = numcarte;    
            plafond = Plafond; 
        }

        //vérifier si le compte appartient à une carte
        public bool CarteExiste(long NumCarte_cb)
        {
            // Tu peux même faire plus simple 
            // return NumCarte_cb == NumCarte;
            if (NumCarte_cb == NumCarte)
            {
                return true;
            }
            return false;
        }

        //vérifier si le montant est en dessous du plafond
        public bool PlafondOk(decimal montant)
        {
            if ((montant < plafond) && (0 < montant))
            {
                return true;
            }
            return false;
        }
    }
}

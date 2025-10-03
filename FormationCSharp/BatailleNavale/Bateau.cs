using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace Bataille_Navale
{
    internal class Bateau
    {
        public string Nom { get; private set; }
        public int Taille { get; private set; }
        public List<Position> Positions { get; private set; }

        public Bateau(string nom, int taille, List<Position> position)
        {
            Nom = nom;
            Taille = taille;
            Positions = position;
        }

        /// <summary>
        /// Case à l'état touché si elle appartient au bateau
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Touché(int x, int y)
        {
            for (int i = 0; i < Positions.Count; i++)
            {
                if (Positions[i].X == x && Positions[i].Y == y)
                {
                    Positions[i].Touché();
                }
            }   
           
        }

        /// <summary>
        /// Le bateau est-il coulé ? 
        /// </summary>
        public bool EstCoulé()
        {
            bool coulé = true;
            for (int i = 0; i < Positions.Count; i++)
            {
                if (Positions[i].Statut != Position.Etat.Touché)
                {
                    coulé = false;
                }
            }
            if (coulé == true)
            {
                for (int i = 0; i < Positions.Count; i++)
                {
                    Positions[i].Coulé();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
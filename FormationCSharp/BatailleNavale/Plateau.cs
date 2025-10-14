using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;

namespace Bataille_Navale
{
    internal class Plateau
    {
        public Position[,] PlateauJeu { get; set; }

        public List<Bateau> Bateaux { get; set; }

        public Plateau(int taille)
        {
            PlateauJeu = new Position[10, 10];
            Bateaux = new List<Bateau>()
            {
               new Bateau("A", 5, new List<Position>()),
               new Bateau("B", 4, new List<Position>()),
               new Bateau("C", 3, new List<Position>()),
               new Bateau("D", 3, new List<Position>()),
               new Bateau("E", 2, new List<Position>())
            };
        }

        public void CreationPlateau()
        {
            bool positionnement = false;
            //bool ligne_bool = false;
            //bool colone_bool = false;

            // Techniquement tu n'as pas besoin de faire cela car c'est fait dans le constructeur
            for (int i = 0; i < PlateauJeu.GetLength(0); i++)
            {
                for (int j = 0; j < PlateauJeu.GetLength(1); j++)
                {
                    PlateauJeu[i, j] = new Position(i,j);
                }
            }
            for (int i = 0; i < Bateaux.Count; i++)
            {
                // Pourquoi tu demandes à l'utilisateur qch ? Il faudrait passer par Random 
                Console.WriteLine($"Donné la position en x du bateau {Bateaux[i].Nom}");
                string lectureA = Console.ReadLine();
                bool bool_x = int.TryParse(lectureA, out int ligne);

                Console.WriteLine($"Donné la position en y du bateau {Bateaux[i].Nom}");
                string lectureB = Console.ReadLine();
                bool bool_y = int.TryParse(lectureB, out int colonne);
                do
                {
                    Console.WriteLine($"Donné la positionnement du bateau {Bateaux[i].Nom}, V OU H");
                    string lectureC = Console.ReadLine();
                    if (lectureC == "V" || lectureC == "H")
                    {
                        positionnement = true;
                    }
                    else
                    {
                        Console.WriteLine("le positionnement est faux");
                    }
                } while (positionnement == false);
            }
           
        }

        public void LancementPartie()
        {
            int cpt = 0;
            while (!FindePartie())
            {
                Console.Clear();
                AfficherPlateau();

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Quelle case visez-vous : (format: ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("ligne");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(",");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("colonne");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(")");
                Console.WriteLine();

                string val = Console.ReadLine();
                string[] position = val.Split(',', '.');

                // Partie à implémenter
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            AfficherPlateau();
            Console.Write($"GG {cpt} coups effectués !");
        }

        /// <summary>
        /// Peut-on placer le navire sur la grille sans qu'il dépasse les bords et qu'il ne touche les autres bateaux ? 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="taille"></param>
        /// <param name="estVertical"></param>
        /// <returns></returns>
        private bool PlacerBateau(int x, int y, int taille, bool estVertical)
        {
            bool place = true;

            //vérifier si le bateau rentre dans le plateau
            // Tu es sur que c'est !Vertical et pas Vertical 
            //if (estVertical != true && (x + taille) < 11)
            if (estVertical && 10 - x < taille)
            {
                place = false;
            }
            //if (estVertical != true && (y + taille) > 11)
            if (!estVertical && 10 - y < taille)
            {
                place = false;
            }

            // vérifier s'il y a des contacts avec d'autres bateaux
            for (int b = 0; b < Bateaux.Count; b++)
            {
                // si le bateau est complétement vide positionner le premier bateau 
                if (b == 0)
                {
                    for (int t = 0; t > taille; t++)
                    {
                        if (estVertical == true)
                        {
                            Bateaux[0].Positions.Add(new Position(x + t, y));
                        }
                        else
                        {
                            Bateaux[0].Positions.Add(new Position(x, y + t));
                        }
                    }
                }
                //verification de la position des restes des bateaux par rapport à mon premier
                else
                {
                    // verification de position pour chaque bateau mise en avant
                    for (int i = 0; i < b; i++)
                    {
                        // verification de poisition par rapport a chaque position d'un bateau
                        for (int j = 0; j < taille; j++)
                        {
                            // C'est vrai, mais il faut penser aussi au fait que les bateaux ne doivent pas être collés.
                            if ((x == Bateaux[i].Positions[j].X) && (y == Bateaux[i].Positions[j].Y))
                            {
                                place = false;
                            }
                        }

                    }

                    if (place == true)
                    {
                        for (int t = 0; t > taille; t++)
                        {
                            if (estVertical == true)
                            {
                                Bateaux[b].Positions.Add(new Position(x + t, y));
                            }
                            else
                            {
                                Bateaux[b].Positions.Add(new Position(x, y + t));
                            }
                        }

                    }


                }
            }
            return true;
        }


        /// <summary>
        /// Choix de la case (x , y) 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Viser(int x, int y)
        {

        }

        /// <summary>
        /// Affichage de l'état de la grille et de la situation de la partie
        /// </summary>
        public void AfficherPlateau()
        {
            List<Position> list = new List<Position>();
            foreach (Bateau b in Bateaux)
            {
                list.AddRange(b.Positions);
                Console.WriteLine($"{b.Nom}: {b.Taille} de long, coulé: {b.EstCoulé()}");
            }

            foreach (Position p in list)
            {
                PlateauJeu.SetValue(p, p.X, p.Y);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("   1 2 3 4 5 6 7 8 9 10");
            int cpt = 0, tmp = 0;
            foreach (Position p in PlateauJeu)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                if (p.X != tmp || cpt == 0)
                {
                    if (cpt > 0)
                    {
                        Console.WriteLine();
                    }
                    Console.Write(string.Format("{0,-3}", ++cpt));
                }

                ConsoleColor foreground;
                switch (p.Statut)
                {
                    case Position.Etat.Plouf:
                        foreground = ConsoleColor.Blue;
                        break;
                    case Position.Etat.Touché:
                        foreground = ConsoleColor.Red;
                        break;
                    case Position.Etat.Coulé:
                        foreground = ConsoleColor.Green;
                        break;
                    default:
                        foreground = ConsoleColor.White;
                        break;
                }
                Console.ForegroundColor = foreground;
                Console.Write((char)p.Statut + " ");

                tmp = p.X;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        /// <summary>
        /// La partie est-elle finie ? 
        /// </summary>
        /// <returns></returns>
        internal bool FindePartie()
        {
            return false;
        }

    }
}
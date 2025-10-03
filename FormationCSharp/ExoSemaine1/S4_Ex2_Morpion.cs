using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie4
{
    public static class Morpion
    {
        public static void MorpionGame(char[,] tabxo)
        {
            char joueur = 'x';
            string lecture = null;
            int compteur = 1;
            bool fin = false;

            for (; compteur < 10; compteur++)
            {
                bool rempli = false;
                bool ligne_ok = false;
                bool colonne_ok = false;
                char ligne = 'a';
                int colonne = 0;
                int l = 0;
                

                do
                {
                    // DEMANDE DE LIGNE JOUEUR 
                    do
                    {
                        Console.WriteLine($"joueur {joueur}, choisissez un ligne entre A et C");

                        // LECTURE DE RETOUR
                        lecture = Console.ReadLine();
                        bool bool_ligne = char.TryParse(lecture, out ligne);

                        // GESTION D'UN RETOUR DIFFERENT DE 0 OU 1 OU 2 
                        if (bool_ligne == false || (ligne != 'A' && ligne != 'B' && ligne != 'C'))
                        {
                            Console.WriteLine("rentrez une valeur valide parmi A ou B ou C");
                        }
                        else
                        {
                            ligne_ok = true;
                        }
                    } while (ligne_ok == false);

                    // DEMANDE DE COLONNE JOUEUR 
                    do
                    {
                        Console.WriteLine($"joueur {joueur}, choisissez une colonne entre 1 et 3");
                        // LECTURE DE RETOUR
                        lecture = Console.ReadLine();
                        bool bool_colonne = int.TryParse(lecture, out colonne);

                        // GESTION D'UN RETOUR DIFFERENT DE 0 OU 1 OU 
                        if (bool_colonne == false || (0 > colonne && colonne > 3))
                        {
                            Console.WriteLine("rentrez une valeur valide entre 1 et 3");
                        }
                        else
                        {
                            colonne_ok = true;
                        }

                    } while (colonne_ok == false);

                    // Insertion du jeu
                    if (ligne == 'A')
                    {
                        l = 0;
                    }
                    else if (ligne == 'B')
                    {
                        l = 1;
                    }
                    else
                    {
                        l = 2;
                    }
                    colonne = colonne - 1;

                    if (tabxo[l, colonne] == '-')
                    {
                        tabxo[l, colonne] = joueur; 

                    }
                    else
                    {
                        Console.WriteLine("la position est déja prise");
                    }

                    //display le jeu actuel
                    DisplayMorpion(tabxo);

                    // regrder si qq'un a gagné
                    int a = CheckMorpion(tabxo, joueur, compteur);
                    if (a == 1 || a == 2)
                    {
                        fin = true;
                    }

                    // changement de joueur
                    if (joueur == 'x') { joueur = 'o'; }
                    else { joueur = 'x'; }

                    rempli = true;
                } while (rempli == false);

                // FIN DU MATCH
                if (fin == true)
                {
                    break;
                }
            }
           
        }

        public static void DisplayMorpion(char[,] tabxo)
        {
            Console.WriteLine("affichage grille de morpion");


            int a = tabxo.GetLength(1);
            int b = tabxo.GetLength(0);


            for (int j = 0; j < a; j++)
            {
                for (int i = 0; i < b; i++)
                {
                    Console.Write($"{tabxo[j, i]} ");

                }
                Console.WriteLine();
            }

            return;
        }

        public static int CheckMorpion(char[,] tabxo, char joueur, int compteur)
        {

            // verification de la victoire : 3 possibilité

            int a = -1;

            // verification de la victoire sur les lignes
            for (int i = 0; i < 3; i++)
            {
                if (tabxo[i, 0] == joueur && tabxo[i, 1] == joueur && tabxo[i, 2] == joueur)
                {
                    Console.WriteLine($"le joueur {joueur} a gagné !!!");
                    if (joueur == 'x')
                    {
                        a = 1;
                        break;
                    }
                    else
                    {
                        a = 2;
                        break;
                    }
                   
                }
            }

            // verification de la victoire sur les colonnes
            for (int i = 0; i < 3; i++)
            {
                if (tabxo[0, i] == joueur && tabxo[1, i] == joueur && tabxo[2, i] == joueur)
                {

                    Console.WriteLine($"le joueur {joueur} a gagné !!!");
                    if (joueur == 'x')
                    {
                        a = 1;
                        break;
                    }
                    else
                    {
                        a = 2;
                        break;
                    }
                    
                }
            }
            // verification de la victoire sur les diagonales
            if ((tabxo[0, 0] == joueur && tabxo[1, 1] == joueur && tabxo[2, 2] == joueur) || (tabxo[0, 2] == joueur && tabxo[1, 1] == joueur && tabxo[2, 0] == joueur))
            {

                Console.WriteLine($"le joueur {joueur} a gagné !!!");
                if (joueur == 'x')
                {
                    a = 1;
                }
                else
                {
                    a = 2;
                }
            }

            // cas d'égalité 


            if (compteur == 9 && (a == -1)) 
            {
                Console.WriteLine("dommage personne a gagné! beheheheheheheh");
            }

            return a;
        }
    }
}



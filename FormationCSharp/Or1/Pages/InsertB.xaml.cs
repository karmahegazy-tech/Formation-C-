using Microsoft.Data.Sqlite;
using Or.Business;
using Or.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Or.Pages
{
    /// <summary>
    /// Logique d'interaction pour InsertB.xaml
    /// </summary>
    public partial class InsertB : PageFunction<long>
    {
        Carte CartePorteur { get; set; }

        public InsertB(long maCarte)
        {
            InitializeComponent();
         
            CartePorteur = SqlRequests.InfosCarte(maCarte);
        }
        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(null);
        }
        private void Rajouter_Bénéficiaire(object sender, RoutedEventArgs e)
        {

            string numeroCompte = numCompte.Text;

            if (int.TryParse(numeroCompte, out int numero))
            {
                string message = SqlRequests.EstBeneficiairePotentiel(CartePorteur.Id, numero).message;
                bool boo = SqlRequests.EstBeneficiairePotentiel(CartePorteur.Id, numero).Condition;
                long numCarte = SqlRequests.EstBeneficiairePotentiel(CartePorteur.Id, numero).numero;
                // si les conditions sur le bénéficiaire est vrai 
                if (boo == true)
                {
                    // ajouter le nouveau bénéficiaire
                    Bénéficiaire bene = new Bénéficiaire(numCarte, numero, "", "");
                    bene.MaCarte = CartePorteur.Id;
                    bene.IdtCpt = numero;
                    bene.PrenomClient = SqlRequests.InfosCarte(numCarte).PrenomClient;
                    bene.NomClient = SqlRequests.InfosCarte(numCarte).NomClient;

                    SqlRequests.AjouterBénéficiaire(bene);
                }
                else
                {
                    // cas d'erreur; si une des conditions sur le bénéficiaire n'est pas rempli
                    MessageBox.Show(message);
                }

            }
            else
            {
                // erreur dans le cas où le valeur inserer n'est pas un chiffre
                MessageBox.Show("Veuillez entrer un numéro");
            }



        }

        private void numCompte_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

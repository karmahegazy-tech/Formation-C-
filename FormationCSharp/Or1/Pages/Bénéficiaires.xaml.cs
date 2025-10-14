using Or.Business;
using Or.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Logique d'interaction pour Bénéficiaire.xaml
    /// </summary>
    public partial class bénéficiaire : PageFunction<long>
    {
        public bénéficiaire(long maCarte)
        {
            InitializeComponent();

            Carte c = SqlRequests.InfosCarte(maCarte);

            Numero.Text = c.Id.ToString();
            Prenom.Text = c.PrenomClient;
            Nom.Text = c.NomClient; 
            listView.ItemsSource = SqlRequests.ListeBénéficiairesAssociesClient(maCarte);
        }
        // Gestion du Bouton retour 
        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(null);
        }
        public void PageFunctionNavigate(PageFunction<long> page)
        {
            page.Return += new ReturnEventHandler<long>(PageFunction_Return);
            NavigationService.Navigate(page);
        }
        public void PageFunction_Return(object sender, ReturnEventArgs<long> e)
        {
            listView.ItemsSource = SqlRequests.ListeBénéficiairesAssociesClient(long.Parse(Numero.Text));
        }
          
        // Gestion du Bouton Ajouter
        private void Rajouter_Bénéficiaire(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new InsertB(long.Parse(Numero.Text)));
        }

        // Gestion du Bouton Supprimer, supprimer et update la table 
        private void Supprimer_beneficiaire(object sender, RoutedEventArgs e)
        {
           SqlRequests.SupprimerBénéficiaire((int)(sender as Button).CommandParameter);
           listView.ItemsSource = SqlRequests.ListeBénéficiairesAssociesClient(long.Parse(Numero.Text));
        }

        // Gestion de la taille du tableau
        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridView gridView = listView.View as GridView;
            if (gridView != null)
            {
                double totalWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;
                gridView.Columns[0].Width = totalWidth * 0.10; // 10%
                gridView.Columns[1].Width = totalWidth * 0.30; // 40%
                gridView.Columns[2].Width = totalWidth * 0.30; // 20%
                gridView.Columns[3].Width = totalWidth * 0.30; // 20%
            }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

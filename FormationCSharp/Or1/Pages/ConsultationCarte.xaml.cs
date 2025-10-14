using MaterialDesignThemes.Wpf;
using Or.Business;
using Or.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Xml;
using System.Xml.Serialization;

namespace Or.Pages
{
    /// <summary>
    /// Logique ht'interaction pour ConsultationCarte.xaml
    /// </summary>
    public partial class ConsultationCarte : PageFunction<long>
    {

        public ConsultationCarte(long numCarte)
        {
            InitializeComponent();

            string fichier = FichierImporter.Text;

            Carte c = SqlRequests.InfosCarte(numCarte);

            Numero.Text = c.Id.ToString();
            Prenom.Text = c.PrenomClient;
            Nom.Text = c.NomClient;

            listView.ItemsSource = SqlRequests.ListeComptesAssociesCarte(numCarte);


        }
        private void GoDetailsCompte(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new DetailsCompte(long.Parse(Numero.Text), (int)(sender as Button).CommandParameter));
        }

        private void GoHistoTransactions(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new HistoriqueTransactions(long.Parse(Numero.Text)));
        }

        private void GoVirement(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new Virement(long.Parse(Numero.Text)));
        }

        private void GoRetrait(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new Retrait(long.Parse(Numero.Text)));
        }

        private void GoDepot(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new Depot(long.Parse(Numero.Text)));
        }

        // Export des transactions des differents comptes d'une carte
        private ExportComptes SerialiserComptesTransaction()
        {
            ExportComptes comptes = new ExportComptes();
            comptes.Comptes = new List<ExportCompte>();
            for (int i = 0; i < SqlRequests.ListeComptesAssociesCarte(1234567890123456).Count; i++)
            {
                ExportCompte exportcompte = new ExportCompte();
                exportcompte.ID = SqlRequests.ListeComptesAssociesCarte(1234567890123456)[i].Id;
                exportcompte.TypeDuCompte = SqlRequests.ListeComptesAssociesCarte(1234567890123456)[i].TypeDuCompte;
                exportcompte.solde = $"{SqlRequests.ListeComptesAssociesCarte(1234567890123456)[i].Solde: 00.00} €";


                exportcompte.Transactions = new List<ExportTransaction>();
                for (int j = 0; j < SqlRequests.ListeTransactionsAssociesCompte(exportcompte.ID).Count; j++)
                {
                    ExportTransaction exporttransaction = new ExportTransaction();
                    exporttransaction.IdTransaction = SqlRequests.ListeTransactionsAssociesCompte(exportcompte.ID)[j].IdTransaction;
                    exporttransaction.Horodatage = SqlRequests.ListeTransactionsAssociesCompte(exportcompte.ID)[j].Horodatage.ToString("dd/MM/ yyyy HH:mm:ss tt");
                    exporttransaction.Montant = $"{SqlRequests.ListeTransactionsAssociesCompte(exportcompte.ID)[j].Montant: 00.00} €";
                    if (SqlRequests.ListeTransactionsAssociesCompte(exportcompte.ID)[j].Expediteur != 0)
                    {
                        exporttransaction.Expediteur = SqlRequests.ListeTransactionsAssociesCompte(exportcompte.ID)[j].Expediteur.ToString();
                    }
                    if (SqlRequests.ListeTransactionsAssociesCompte(exportcompte.ID)[j].Destinataire != 0)
                    {
                        exporttransaction.Destinataire = SqlRequests.ListeTransactionsAssociesCompte(exportcompte.ID)[j].Destinataire.ToString();
                    }

                    //récuperer le type de transaction
                    Operation operation = Tools.TypeTransaction(SqlRequests.ListeTransactionsAssociesCompte(exportcompte.ID)[j].Expediteur, SqlRequests.ListeTransactionsAssociesCompte(exportcompte.ID)[j].Destinataire);
                    exporttransaction.Operation = Tools.TypeTransacConverter(operation);

                    exportcompte.Transactions.Add(exporttransaction);
                }
                comptes.Comptes.Add(exportcompte);
            }
            return comptes;
        }

        // Import des transactions des differents comptes d'une carte
        private void DeSerialiserComptesTransaction(ExportComptes ImportComptes)
        {
            List<Compte> comptes = new List<Compte>();
            comptes = SqlRequests.ListeComptesAssociesCarte(long.Parse(Numero.Text));

            foreach (var compte in ImportComptes.Comptes)
            {
                foreach (var transaction in compte.Transactions)
                {
                    if (transaction.Operation == "Retrait")
                    {

                    }
                    else if (transaction.Operation == "Virement")
                    {

                    }
                    else if (transaction.Operation == "Dépot")
                    {

                    }
                }
            }
        }

        public void ExportTransactions(object sender, RoutedEventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ExportComptes));

            using (TextWriter stream = new StreamWriter("ComptesTransactions.xml"))
            {
                serializer.Serialize(stream, SerialiserComptesTransaction());
            }
        }

        public void ImportTransactions(object sender, RoutedEventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ExportComptes));
            // Declarer un objet a être désérialiser
            ExportComptes ImportComptes;

            using (Stream Reader = new FileStream(FichierImporter.Text, FileMode.Open))
            {
                ImportComptes = (ExportComptes)serializer.Deserialize(Reader);
                DeSerialiserComptesTransaction(ImportComptes);
            }
        }

        // Regarder la liste des bénéficiaires externes d'une carte
        private void GoBénéficiaire(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new bénéficiaire(long.Parse(Numero.Text)));
        }

        public void PageFunctionNavigate(PageFunction<long> page)
        {
            page.Return += new ReturnEventHandler<long>(PageFunction_Return);
            NavigationService.Navigate(page);
        }

        public void PageFunction_Return(object sender, ReturnEventArgs<long> e)
        {
            listView.ItemsSource = SqlRequests.ListeComptesAssociesCarte(long.Parse(Numero.Text));
        }

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
        private void Import_Fichier(object sender, EventArgs e)
        {

        }
    }
}

using Or.Models;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Or
{
    [XmlRoot("Comptes")]
    public class ExportComptes
    {
        [XmlElement("Compte", typeof(ExportCompte))]
        public List<ExportCompte> Comptes { get; set; }

    }

    [XmlType("Compte")]
    public class ExportCompte
    {
        [XmlElement("Identifiant")]
        public int ID { get; set; }

        [XmlElement("Type")]
        public TypeCompte TypeDuCompte { get; set; }

        [XmlElement("Solde")]
        public string solde { get; set; }

        [XmlArray("Transactions")]
        [XmlArrayItem("Transaction", typeof(ExportTransaction))]
        public List<ExportTransaction> Transactions { get; set; }
    }
}




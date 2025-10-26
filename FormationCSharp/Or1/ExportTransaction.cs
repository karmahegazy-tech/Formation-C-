using System.Xml.Serialization;

namespace Or
{
    [XmlType("Transaction")]
    public class ExportTransaction
    {
        [XmlElement("Identifiant")]
        public int IdTransaction { get; set; }

        [XmlElement("Date")]
        public string Horodatage { get; set; }

        [XmlElement("Type")]
        public string TypeTransaction { get; set; }

        [XmlElement("Montant")]
        public string Montant { get; set; }
        
        [XmlElement("Expéditeur")]
        public string Expediteur { get; set; }

        [XmlElement("Destinataire")]
        public string Destinataire { get; set; }

        [XmlElement("Opération")]
        public string Operation { get; set; }
    }

}

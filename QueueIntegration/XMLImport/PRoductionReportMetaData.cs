using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLImport.Models
{
    [Serializable]
    [XmlRoot("Car")]
    public class ProductionReportMetaData
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Vin")]
        public string Vin { get; set; }

        [XmlElement("ProductionYear")]
        public string ProductionYear { get; set; }

        [XmlElement("Model")]
        public string Model { get; set; }
    }

    [MetadataType(typeof(ProductionReportMetaData))]
    public partial class ProductionReport
    {

    }
}

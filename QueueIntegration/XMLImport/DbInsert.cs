using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace XMLImport
{
    public class DbInsert
    {
        XmlDbEntities2 xmlDbEntities = new XmlDbEntities2();
        public List<ProductionReport> DbInserts(string fileName)
        {
            XDocument xDocument = XDocument.Load(fileName);

            List<ProductionReport> productList = xDocument.Descendants("Car").Select
                (product =>
                new ProductionReport
                {
                    Id = Convert.ToInt32(product.Element("Id").Value),
                    Vin = product.Element("Vin").Value,
                    ProductionYear = product.Element("ProductionYear").Value,
                    Model = product.Element("Model").Value,
                }).ToList();

            using (xmlDbEntities)
            {
                foreach (var item in productList)
                {
                    var v = xmlDbEntities.ProductionReports.Where(a => a.Id.Equals
                    (item.Id)).FirstOrDefault();

                    if (v != null)
                    {
                        v.Id = item.Id;
                        v.Vin = item.Vin;
                        v.ProductionYear = item.ProductionYear;
                        v.Model = item.Model;
                    }
                    else
                    {
                        xmlDbEntities.ProductionReports.Add(item);
                    }
                }

                xmlDbEntities.SaveChanges();

                return xmlDbEntities.ProductionReports.ToList();
            }
        }

        public List<ProductionReport> ListReport()
        {
            return xmlDbEntities.ProductionReports.ToList();
        }
    }
}

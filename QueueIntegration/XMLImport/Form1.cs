using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace XMLImport
{
    public partial class Form1 : Form
    {
        private string fileName = "";
        public Form1()
        {
            InitializeComponent();
        }

        public void label1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            buttonImport.Enabled = false;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select xml";
            openFileDialog.Filter = "Xml File |*.xml";
            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                buttonImport.Enabled = true;
                fileName = openFileDialog.FileName;
                MessageBox.Show(fileName);
            }


        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            try
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

                using (XmlDbEntities2 xmlDbEntities = new XmlDbEntities2())
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
                    dataGridViewProduct.DataSource = xmlDbEntities.ProductionReports.ToList();
                    MessageBox.Show("Done");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cant import" + ex);
            }
        }
    }
}

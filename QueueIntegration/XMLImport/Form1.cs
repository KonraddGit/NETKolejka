using System;
using System.Windows.Forms;

namespace XMLImport
{
    public partial class Form1 : Form
    {
        private readonly Receive _receive = new Receive();
        private readonly Send _sendMessage = new Send();
        private readonly DbInsert _dbInsert = new DbInsert();

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
            BrowseFilesAndSendMessage();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            ReceiveMessage();

            try
            {
                dataGridViewProduct.DataSource = _dbInsert.DbInserts(fileName);

                MessageBox.Show("Successful insert");
            }
            catch (Exception)
            {
                MessageBox.Show("Can not insert Xml to database");
            }
        }

        private void BrowseFilesAndSendMessage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select xml";
            openFileDialog.Filter = "Xml File |*.xml";
            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                buttonImport.Enabled = true;
                fileName = openFileDialog.FileName;

                xmlString.Text = _sendMessage.Message(fileName);
            }
        }

        private void ReceiveMessage()
        {
            if (_receive.ReceiveMessage() == true)
            {
                MessageBox.Show("Successfuly received message");
            }
            else MessageBox.Show("Something went wrong");
        }
    }
}

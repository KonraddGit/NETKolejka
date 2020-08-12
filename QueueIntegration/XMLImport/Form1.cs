using System;
using System.Windows.Forms;

namespace XMLImport
{
    public partial class Form1 : Form
    {
        Receive _receive = new Receive();
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
            if (_receive.ReceiveMessages() == true)
            {
                MessageBox.Show("Successfuly received message and inserted to db");

                dataGridViewProduct.DataSource = _dbInsert.ListReport();
            }
            else MessageBox.Show("Something went wrong");
        }
    }
}

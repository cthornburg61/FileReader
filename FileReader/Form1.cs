using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
        }
        private void loadTextFile()
        {
            string dataFileName;
            //string dirs = Directory.GetFiles(@"c:\", "c*");
            OpenFileDialog filePicker = new OpenFileDialog();
            filePicker.Multiselect = false;
            var clickedOK = filePicker.ShowDialog();

            if (clickedOK == DialogResult.OK)
            {
                dataGridView1.Dock = DockStyle.Fill;

                dataFileName = filePicker.FileName;

                //Read the data from text file
                string[] textData = System.IO.File.ReadAllLines(dataFileName);
                string[] headers = textData[0].Split(',');

                //Create and populate DataTable
                DataTable dataTable1 = new DataTable();
                //create the columns
                int iCtr = 1;
                string columnName = "";

                foreach (string header in headers)
                {
                    if (header.ToUpper() == "NULL" )
                    {
                        columnName = columnName.Trim() + "NULLField_" + iCtr.ToString().Trim();
                        iCtr++;
                    }
                    dataTable1.Columns.Add(columnName.ToString() , typeof(string), null);
                    //iCtr++;
                    columnName = "";
                }

                //add all the rows 
                for (int i = 0; i < textData.Length; i++)
                    dataTable1.Rows.Add(textData[i].Replace('"',' ').Split(','));

                //Set the DataSource of DataGridView to the DataTable
                dataGridView1.DataSource = dataTable1;
                //set the form caption
                this.Text = dataFileName;
                //show the grid and refresh it
                dataGridView1.Visible = true;
                dataGridView1.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadTextFile();
        }
    }
}

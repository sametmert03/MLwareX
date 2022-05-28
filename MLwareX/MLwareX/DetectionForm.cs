using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLwareX
{
    public partial class DetectionForm : Form
    {
        public DetectionForm()
        {
            InitializeComponent();
        }

        private void DetectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm frmMain = new MainForm();
            frmMain.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm frmMain = new MainForm();
            frmMain.Enabled = true;
            this.Close();
        }

        private void DetectionForm_Load(object sender, EventArgs e)
        {
            List<Label> labels = new List<Label>();
            string dbinfo = @"C:\\Users\\FikriSametMert\Desktop\MLwareX\MLwareX\bin\Debug\selectedInfo.csv";

            using (var reader = new StreamReader(dbinfo))
            {
                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    listA.Add(values[0]);
                    listB.Add(values[1]);

                }

                for (int i = 0; i < listA.Count; i++)
                {
                    listBox1.Items.Add(listA[i].ToString());
                    listBox2.Items.Add(listB[i].ToString());
                }

            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            listBox2.SelectedIndex = listBox1.SelectedIndex;
        }

        private void listBox2_Click(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = listBox2.SelectedIndex;
        }
    }
    
}

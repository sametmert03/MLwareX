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
                List<string> a = listA.Distinct().ToList();
                List<string> b = listB.ToList();
                for (int i = 0; i < a.Count; i++)
                {
                    listBox1.Items.Add(a[i].ToString());
                    listBox2.Items.Add(b[i].ToString());
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string yourFilepath = @"C:\\Users\\FikriSametMert\Desktop\MLwareX\MLwareX\bin\Debug\selectedInfo.csv";
            string selectedText = listBox1.SelectedItem.ToString();
            string selectedHm = listBox2.SelectedItem.ToString();

            string[] Lines = File.ReadAllLines(yourFilepath);
            File.Delete(yourFilepath);// Deleting the file
            using (StreamWriter sw = File.AppendText(yourFilepath))

            {
                foreach (string line in Lines)
                {
                    if (line.IndexOf(selectedText) >= 0)
                    {
                        //Skip the line
                        continue;
                    }
                    else
                    {
                        sw.WriteLine(line);
                    }
                }
            }

            File.Delete(selectedText);

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            DetectionForm_Load(sender, e);
        }
    }
    
}

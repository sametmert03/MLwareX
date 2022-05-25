using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    }
}

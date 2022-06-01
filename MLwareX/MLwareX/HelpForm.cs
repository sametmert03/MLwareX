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
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        /*private void HelpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm frmMain = new MainForm();
            frmMain.Show();
        }*/

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm frmMain = new MainForm();
            frmMain.Show();
            frmMain.Enabled = true;
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm frmMain = new MainForm();
            frmMain.Show();
            frmMain.Enabled = true;
            this.Close();
        }
    }
}

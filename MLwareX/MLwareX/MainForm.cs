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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        static MainForm frmMain = new MainForm();
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "MLwareX         " + DateTime.Now.ToShortDateString();
        }
        
        private void btnDetection_Click(object sender, EventArgs e)
        {
            DetectionForm frmDetection = new DetectionForm();
            frmDetection.Show();
            this.Hide();
            this.Enabled = false;
        }

        private void btnScanner_Click(object sender, EventArgs e)
        {
            ScannerForm frmScanner = new ScannerForm();
            frmScanner.Show();
            this.Hide();
            this.Enabled = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm frmSettings = new SettingsForm();
            frmSettings.Show();
            this.Hide();
            this.Enabled = false;
        }

        private void btnNotifications_Click(object sender, EventArgs e)
        {
            NotificationsForm frmNotification = new NotificationsForm();
            frmNotification.Show();
            this.Hide();
            this.Enabled = false;
        }

        
    }
}

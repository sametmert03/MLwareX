using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        public void OnCreated(object source, FileSystemEventArgs e)
        {
            try
            {
                Console.WriteLine(e.FullPath);
                string dbfile = @"C:\\Users\\FikriSametMert\Desktop\MLwareX\MLwareX\bin\Debug\activeProt.csv";
                File.WriteAllText(dbfile, e.FullPath);
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                cmd.StandardInput.WriteLine(@"C:\\Users\FikriSametMert\AppData\Local\Programs\Python\Python39\python.exe C:/Users/FikriSametMert/Desktop/MalConv-keras-master/MalConv-keras-master/MalConv-keras-master/src/predict.py C:\\Users\\FikriSametMert\\Desktop\\MLwareX\\MLwareX\\bin\\Debug\\activeProt.csv");
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                Console.WriteLine(cmd.StandardOutput.ReadToEnd());


                using (var reader = new StreamReader(@"C:\Users\FikriSametMert\Desktop\MalConv-keras-master\MalConv-keras-master\MalConv-keras-master\src\samp\result.csv"))
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

                        float f1 = float.Parse(listB[i].Replace(".", ","));
                        Console.WriteLine("listB " + f1);
                        if (f1 < 0.85)
                        {
                            Console.WriteLine(listA[i] + " is harmless brooo");
                        }
                        else
                        {
                            //MessageBox.Show("A threat found.. " + listA[i] + " is malware.");
                            //Console.WriteLine(listA[i] + " is malware");
                            string dbinfo = @"C:\\Users\\FikriSametMert\Desktop\MLwareX\MLwareX\bin\Debug\selectedInfo.csv";
                            var src = DateTime.Now;
                            var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, src.Second);
                            using (StreamWriter writer = new StreamWriter(dbinfo, true))
                            {
                                writer.WriteLine(listA[i] + "," + hm.ToString());
                            }
                            Console.WriteLine(listA[i] + " is malware");
                            using (var readerx = new StreamReader(dbinfo))
                            {
                                List<string> lines = new List<string>();
                                while (!reader.EndOfStream)
                                {
                                    var line = reader.ReadLine();
                                    var values = line.Split(',');
                                    lines.Add(values[0]);

                                }
                                foreach (var line in lines.Distinct()) // might want to supply an equality comparer
                                {
                                    Console.WriteLine(line);
                                    // write line to output file
                                }
                            }
                        }
                    }
                }
            }




            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        FileSystemWatcher watcher = new FileSystemWatcher(@"C:\Users\FikriSametMert\Desktop\test");
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "MLwareX         " + DateTime.Now.ToShortDateString();
            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;
            watcher.Filter = "*.exe";
            watcher.IncludeSubdirectories = true;
            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.EnableRaisingEvents = true;

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

        private void rjToggleButton1_CheckedChanged(object sender, EventArgs e)
        {

            if (rjToggleButton1.Checked == true)
            {
                watcher.EnableRaisingEvents = true;
            }
            else
                watcher.EnableRaisingEvents = false;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            HelpForm frmHelp = new HelpForm();
            frmHelp.Show();
            this.Hide();
            this.Enabled = false;
        }
    }
}

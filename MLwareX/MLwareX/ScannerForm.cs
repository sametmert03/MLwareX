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
using WinFormAnimation;


namespace MLwareX
{
    public partial class ScannerForm : Form
    {
        public ScannerForm()
        {
            InitializeComponent();
        }
        
        private void ScannerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm frmMain = new MainForm();
            frmMain.Show();
        }

        private void ScannerForm_Load(object sender, EventArgs e)
        {
            circularProgressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm frmMain = new MainForm();
            frmMain.Enabled = true;
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            circularProgressBar1.Value++;
            if (circularProgressBar1.Value >= 100)
            {
                circularProgressBar1.Value = 0;
            }

        }

        private void btnScanFile_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Executable Files |*.exe;*.dll";
            file.RestoreDirectory = true;
            file.CheckFileExists = true;
            file.Multiselect = true;
            file.Title = "Select File...";

            if (file.ShowDialog() == DialogResult.OK)
            {
                timer1.Enabled = true;
                string[] filePaths = file.FileNames;
                for (int i = 0; i < file.FileNames.Length; i++)
                {
                    ListViewItem li = new ListViewItem(filePaths);
                }
                string dbfile = @"C:\\Users\\FikriSametMert\Desktop\MLwareX\MLwareX\bin\Debug\selectedFiles.csv";
                File.WriteAllLines(dbfile, file.FileNames);
                for (int x = 0; x < filePaths.Length; x += 1)
                {
                    var src = DateTime.Now;
                    var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0);
                    Console.WriteLine();
                    Console.WriteLine(filePaths[x] + "," + hm);
                }
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                cmd.StandardInput.WriteLine(@"C:\\Users\FikriSametMert\AppData\Local\Programs\Python\Python39\python.exe C:/Users/FikriSametMert/Desktop/MalConv-keras-master/MalConv-keras-master/MalConv-keras-master/src/predict.py C:\\Users\\FikriSametMert\\Desktop\\MLwareX\\MLwareX\\bin\\Debug\\selectedFiles.csv");
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

                        decimal f1 = decimal.Parse(listB[i]);
                        Console.WriteLine("listB " + f1);
                        Console.WriteLine(listB[i]);
                        if (f1 < Convert.ToDecimal(0.70) )
                        {
                            //MessageBox.Show(listA[i].ToString() + " is harmless","Result",0,MessageBoxIcon.Hand);
                            listBox1.Items.Add(listA[i] + " is harmless");
                        }
                        else
                        {
                            //MessageBox.Show(listA[i].ToString() + " is malware","Result",0,MessageBoxIcon.Warning);
                            listBox1.Items.Add(listA[i] + " is malware");
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
                        listBox1.Visible = true;
                    }
                }

            }
            else
            {
                MessageBox.Show("Nothing selected.");
            }



        }

        private void btnScanDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Tag = "Select File...";

            if (folder.ShowDialog() == DialogResult.OK)
            {
                timer1.Enabled = true;
                string folderPath = folder.SelectedPath;
                MessageBox.Show(folderPath);
                string dbfile = @"C:\\Users\\FikriSametMert\Desktop\MLwareX\MLwareX\bin\Debug\selectedFiles.csv";
                string dbinfo = @"C:\\Users\\FikriSametMert\Desktop\MLwareX\MLwareX\bin\Debug\selectedInfo.csv";

                string[] files = Directory.GetFiles(folderPath, "*.exe", SearchOption.AllDirectories);
                try
                {

                    File.WriteAllLines(dbfile, files);
                    for (int x = 0; x < files.Length; x += 1)
                    {
                        var src = DateTime.Now;
                        var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0);
                        Console.WriteLine();
                        Console.WriteLine(files[x] + "," + hm);
                    }

                    Process cmd = new Process();
                    cmd.StartInfo.FileName = "cmd.exe";
                    cmd.StartInfo.RedirectStandardInput = true;
                    cmd.StartInfo.RedirectStandardOutput = true;
                    cmd.StartInfo.CreateNoWindow = true;
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.Start();

                    cmd.StandardInput.WriteLine(@"C:\\Users\FikriSametMert\AppData\Local\Programs\Python\Python39\python.exe C:/Users/FikriSametMert/Desktop/MalConv-keras-master/MalConv-keras-master/MalConv-keras-master/src/predict.py C:\\Users\\FikriSametMert\\Desktop\\MLwareX\\MLwareX\\bin\\Debug\\selectedFiles.csv");
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

                            decimal f1 = decimal.Parse(listB[i]);
                            Console.WriteLine("listB " + f1);
                            Console.WriteLine(listB[i]);
                            if (f1 < Convert.ToDecimal(0.70))
                            {
                                Console.WriteLine(listA[i] + " is harmless");
                            }
                            else
                            {
                                //MessageBox.Show("A threat found.. " + listA[i] + " is malware.");
                                //Console.WriteLine(listA[i] + " is malware");
                               // string dbinfo = @"C:\\Users\\FikriSametMert\Desktop\MLwareX\MLwareX\bin\Debug\selectedInfo.csv";
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
            else
            {
                MessageBox.Show("Nothing selected.");
            }

            MessageBox.Show("Scan Completed. Please check 'Detection History.'","Completed");
        }
    }
}

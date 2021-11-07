using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public string sourceDir = "";
        public string destinationDir = "";
        public string fileName = "";
        public string newdestdir = "";
        public string backupname = "";

        public static string UserName { get; }

        string configPath = "C:/Users/" + Environment.UserName.ToString() + "/saved_backups.txt";
        public Form1()
        {
            InitializeComponent();

            if (File.Exists(configPath))

            {
                foreach (string line in File.ReadLines(configPath))
                {
                    if (line.Contains("BackupName:"))
                    {
                        var lineset = line.Split('*');
                        var lineset2 = lineset[1].ToString();
                        comboBox1.Items.Add(lineset2);
                    }
                }

            }
            else
            {
                File.Create("C:/Users/" + Environment.UserName.ToString() + "/saved_backups.txt");

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        private void folderBrowserDialog2_HelpRequest(object sender, EventArgs e)
        {
        }
        private void button3_Click(object sender, EventArgs e)
        {
            sourceDir = textBox1.Text;
            destinationDir = textBox2.Text;
            if (sourceDir.Length >= 1 && destinationDir.Length >= 1)
            {
                panel1.Enabled = true;
                panel1.Visible = true;
                progressBar1.Value = 0;

            }
            else
            {
                MessageBox.Show("Please Enter Source and Destination Data");
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            backupname = comboBox1.Text.ToString();

            if (File.Exists(configPath))

            {
                foreach (string line in File.ReadLines(configPath))
                {
                    if (line.Contains(backupname) && line.Contains("BackupName:"))
                    {
                        var lineset = line.Split('*');
                        var lineset2 = lineset[1].ToString();
                        richTextBox1.Text = lineset2;
                    }
                    if (line.Contains(backupname) && line.Contains("SourceName:"))
                    {
                        var lineset = line.Split('*');
                        var lineset2 = lineset[1].ToString();
                        textBox1.Text = lineset2;
                    }

                    if (line.Contains(backupname) && line.Contains("DestinationName:"))
                    {
                        var lineset = line.Split('*');
                        var lineset2 = lineset[1].ToString();
                        textBox2.Text = lineset2;
                    }

                }

            }
            sourceDir = textBox1.Text;
            destinationDir = textBox2.Text;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (richTextBox1.Text.Length <= 0)
            {
                button4.Enabled = false;
                button5.Enabled = false;
            }
            if (richTextBox1.Text.Length >= 1)
            {
                button4.Enabled = true;
                button5.Enabled = true;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length <= 0)
            {
                button4.Enabled = false;
                button5.Enabled = false;
            }
            if (richTextBox1.Text.Length >= 1)
            {
                button4.Enabled = true;
                button5.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sourceDir = textBox1.Text;
            destinationDir = textBox2.Text;
            backupname = richTextBox1.Text;


            if (File.Exists(configPath))

            {
                foreach (string line in File.ReadLines(configPath))
                {
                    if (line.Contains(backupname) && line.Contains("BackupName: "))
                    {
                        MessageBox.Show("Plese Enter a Distinct Backup Name");
                        return;
                    }
                }
            }
            using (StreamWriter sw = File.AppendText(configPath))
                sw.WriteLine("BackupName: " + "*" + backupname);
            using (StreamWriter sw = File.AppendText(configPath))
                sw.WriteLine(backupname + "SourceName:" + "*" + sourceDir);
            using (StreamWriter sw = File.AppendText(configPath))
                sw.WriteLine(backupname + "DestinationName:" + "*" + destinationDir);
            button4.Enabled = false;

            if (File.Exists(configPath))
            {
                comboBox1.Items.Clear();
                foreach (string line in File.ReadLines(configPath))
                {
                    if (line.Contains("BackupName:"))
                    {
                        var lineset = line.Split('*');
                        var lineset2 = lineset[1].ToString();
                        
                        comboBox1.Items.Add(lineset2);
                    }
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            panel1.Enabled = false;
            panel1.Visible = false;
            newdestdir = destinationDir + "/" + "temp" + "/";
            Directory.CreateDirectory(newdestdir);
            {
                {
                    foreach (string d in Directory.GetDirectories(sourceDir, "*",
                        SearchOption.AllDirectories))
                        Directory.CreateDirectory(d.Replace(sourceDir, newdestdir));
                    foreach (string f in Directory.GetFiles(sourceDir, "*.*",
                        SearchOption.AllDirectories))
                        File.Copy(f, f.Replace(sourceDir, newdestdir), true);

                    string startPath = destinationDir + "/" + "temp";
                    string zipPath = destinationDir + "/" + backupname + ".zip";
                    if (File.Exists(zipPath))
                    {
                        File.Delete(zipPath);
                    }
                    ZipFile.CreateFromDirectory(startPath, zipPath);
                    Directory.Delete(newdestdir, true);
                }
                progressBar1.Value = 100;
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void button6_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            panel1.Visible = false;
            panel1.Enabled = false;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (File.Exists(configPath))
            {
                File.Delete(configPath);
                File.Create("C:/Users/" + Environment.UserName.ToString() + "/saved_backups.txt");
                button7.Enabled = false;
                comboBox1.Enabled = false;
                comboBox1.Visible = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                MessageBox.Show("Please Restart the Program");
            }
        }
    }
}



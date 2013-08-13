﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace BatchCompiler
{
    public partial class Form1 : Form
    {
        string studiomdlexe;
        string qcfolder;
        string gamefolder;
        string[] config = new string[] {"", "", "","#", "" } ;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            studiomdlexe = "";
            qcfolder = "";
            gamefolder = "";
            config.Initialize();
            config[0] = studiomdlexe;
            config[1] = qcfolder;
            config[2] = gamefolder;
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "config")) ;
            {
                StringBuilder sb = new StringBuilder();
                using (System.IO.StreamReader sr = new System.IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory + "config"))
                {

                    String line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.AppendLine(line);
                    }
                }
                string config1 = sb.ToString();
                if (config1 != "")
                {
                    config = config1.Split('#');
                    studiomdlexe = config[0];
                    qcfolder = config[1] + @"\";
                    gamefolder = config[2] + @"\";
                    Console.WriteLine("reading...");
                    Console.WriteLine("Studiomdl: " + studiomdlexe);
                    Console.WriteLine("Game: " + gamefolder);
                    Console.WriteLine("QC: " + qcfolder);
                    label5.Text = "OK";
                    label6.Text = "OK";
                    StudioMdlOkay.Text = "OK";
                    label5.ForeColor = Color.Green;
                    label6.ForeColor = Color.Green;
                    StudioMdlOkay.ForeColor = Color.Green;
                }
                else
                {
                    Console.WriteLine("No config, will be created.");
                }
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            // Set filter options and filter index.

            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.ShowDialog();
            qcfolder = folderBrowserDialog.SelectedPath;
            if (qcfolder == "")
            {
                MessageBox.Show("Douchebag, you didn't select a shit.");

            }
            else
            {
                Console.WriteLine("Qc folder selected: " + folderBrowserDialog.SelectedPath);
                config[1] = qcfolder;
                label5.ForeColor = Color.Green;
                label5.Text = "OK";
                
                if (gamefolder != "" & qcfolder != "" & studiomdlexe != "")
                {
                    string configtowrite = String.Join("#", config);
                    System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "config", configtowrite);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "Windows Portable Executable (*.exe)|*.exe";
            openfiledialog.ShowDialog();
            studiomdlexe = openfiledialog.FileName;
            if (studiomdlexe == "")
            {
                MessageBox.Show("Douchebag, you didn't select a shit.");

            }
            else
            {
                Console.WriteLine("Studiomdl.exe selected: " + openfiledialog.FileName);
                StudioMdlOkay.ForeColor = Color.Green;
                StudioMdlOkay.Text = "OK";
                config[0] = studiomdlexe;
                if (gamefolder != "" & qcfolder != "" & studiomdlexe != "")
                {
                    string configtowrite = String.Join("#", config);
                    System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "config", configtowrite);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            // Set filter options and filter index.

            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.ShowDialog();
            gamefolder = folderBrowserDialog.SelectedPath;
            if (gamefolder == "")
            {
                MessageBox.Show("Douchebag, you didn't select a shit.");

            }
            else
            {
                Console.WriteLine("Game folder selected: " + folderBrowserDialog.SelectedPath);
                label6.Text = "OK";
                label6.ForeColor = Color.Green;
                config[2] = gamefolder;
                if (gamefolder != "" & qcfolder != "" & studiomdlexe != "")
                {
                    string configtowrite = String.Join("#", config);
                    System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "config", configtowrite);
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

                string[] filePaths = System.IO.Directory.GetFiles(qcfolder, "*.qc");
                int fileindex;
                string output;
                fileindex = filePaths.Length;
                while (fileindex >= 1)
                {
                    MessageBox.Show("starting");
                    fileindex = fileindex - 1;
                    Console.WriteLine(fileindex);
                    // Start the child process.
                    Process p = new Process();
                    // Redirect the output stream of the child process.
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.FileName = studiomdlexe;
                    p.StartInfo.Arguments = "-game " + (char)34 + gamefolder + (char)34 + (char)34 + filePaths[fileindex] + (char)34;
                    p.Start();
                    output = p.StandardOutput.ReadToEnd();
                    // Do not wait for the child process to exit before
                    // reading to the end of its redirected stream.
                    // p.WaitForExit();
                    // Read the output stream first and then wait.
                    p.WaitForExit();
                    MessageBox.Show(output);
                }
                
           }
        }

    }

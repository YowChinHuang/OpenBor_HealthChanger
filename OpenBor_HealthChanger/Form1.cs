using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace OpenBor_HealthChanger {
    public partial class frmMain : Form {

        private Boolean ProcessFinish;

        public frmMain() {
            InitializeComponent();
            ProcessFinish = false;
        }

        private void lblPanel_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.All;
        }

        private void lblPanel_DragDrop(object sender, DragEventArgs e) {

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            lblPanel.Visible = false;
            ProcessFinish = false;
            lstFile.Items.Clear();

            foreach (string file in files) {

                if ((File.GetAttributes(file) & FileAttributes.Directory) == FileAttributes.Directory) {
                    ProcessDir(file);
                } else {

                    if (Path.GetExtension(file).ToLower().Equals(".pak") == true) {
                        Boolean Createborpak = false;

                        if (File.Exists("borpak.exe") == false) {
                            Stream borpak = Assembly.GetExecutingAssembly().GetManifestResourceStream("OpenBor_HealthChanger.borpak.exe");
                            byte[] res = new byte[borpak.Length];
                            borpak.Read(res, 0, res.Length);

                            File.WriteAllBytes("borpak.exe", res);
                            Createborpak = true;
                            res = null;
                            borpak = null;
                        }

                        //Extract PAK
                        Process BORPAK = new Process();
                        BORPAK.StartInfo.FileName = "borpak.exe";
                        BORPAK.StartInfo.UseShellExecute = false;
                        BORPAK.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        BORPAK.StartInfo.RedirectStandardOutput = true;
                        BORPAK.StartInfo.CreateNoWindow = true;
                        BORPAK.StartInfo.Arguments = "\"" + file + "\"";
                        BORPAK.Start();
                        while (!BORPAK.HasExited) {
                            lstFile.Items.Add(BORPAK.StandardOutput.ReadLine());
                            lstFile.SelectedIndex = lstFile.Items.Count - 1;
                            Application.DoEvents();
                        }

                        //Process
                        lstFile.Items.Add("========================================");
                        lstFile.Items.Add("Processing Files!");
                        String newPath = Path.GetDirectoryName(file);
                        ProcessDir(newPath + "\\data\\");
                        File.Move(file, file + String.Format("{0:.yyyyMMddHHmmssffff}", DateTime.Now));

                        //Create PAK
                        lstFile.Items.Add("========================================");
                        lstFile.Items.Add("Packing Files!");
                        BORPAK.StartInfo.Arguments = "-b -d data \"" + file + "\"";
                        BORPAK.Start();
                        while (!BORPAK.HasExited) {
                            lstFile.Items.Add(BORPAK.StandardOutput.ReadLine());
                            lstFile.SelectedIndex = lstFile.Items.Count - 1;
                            Application.DoEvents();
                        }
                        Directory.Delete(newPath + "\\data\\", true);
                        if (Createborpak == true) {
                            File.Delete("borpak.exe");
                        }
                    } else if (Path.GetExtension(file).ToLower().Equals(".txt") == true) {
                        if (ProcessFile(file) == true) {
                            lstFile.Items.Add(file);
                            lstFile.SelectedIndex = lstFile.Items.Count - 1;
                        }
                        Application.DoEvents();
                    }
                }
            }
            lstFile.Items.Add("========================================");
            lstFile.Items.Add("Double Click to Finish!");
            lstFile.SelectedIndex = lstFile.Items.Count - 1;
            ProcessFinish = true;
        }

        private void lstFile_DoubleClick(object sender, EventArgs e) {

            if (ProcessFinish == true) {
                lblPanel.Visible = true;
            }
        }

        void ProcessDir(string Path) {
            DirectoryInfo DI = new DirectoryInfo(Path);

            foreach (FileInfo fileindirs in DI.GetFiles("*.txt", SearchOption.AllDirectories)) {
                if (ProcessFile(fileindirs.FullName) == true) {
                    lstFile.Items.Add(fileindirs.FullName);
                    lstFile.SelectedIndex = lstFile.Items.Count - 1;
                }
                Application.DoEvents();
            }
            DI = null;
        }

        Boolean ProcessFile(String filename) {
            string[] lines = File.ReadAllLines(filename);
            Boolean DataChanged = false;

            if (filename.IndexOf("\\chars\\", StringComparison.CurrentCultureIgnoreCase) != -1) {
                Boolean KeyWord = false;

                for (int i = 0;i < lines.Length;i++) {

                    if ((lines[i].IndexOf("type", StringComparison.CurrentCultureIgnoreCase) != -1) && (lines[i].IndexOf("enemy", StringComparison.CurrentCultureIgnoreCase) != -1)) {
                        KeyWord = true;
                    }
                    if (lines[i].IndexOf("health", StringComparison.CurrentCultureIgnoreCase) != -1) {
                        string OldVal = lines[i].ToLower().Replace("health", "").Trim();

                        lines[i] = lines[i].Replace(OldVal, "1");
                        DataChanged = true;
                    }
                }
                if (KeyWord == false) {
                    DataChanged = false;
                }

            } else {

                for (int i = 0; i < lines.Length;i++) {
                    if ((lines[i].IndexOf("health", StringComparison.CurrentCultureIgnoreCase) != -1) && (lines[i].StartsWith("#") == false)) {
                        string OldVal = lines[i].ToLower().Replace("health", "").Trim();

                        lines[i] = lines[i].Replace(OldVal, "1");
                        DataChanged = true;
                    }
                }
            }
            if (DataChanged == true) {
                File.WriteAllLines(filename, lines);
                return true;
            }
            return false;
        }

    }
}
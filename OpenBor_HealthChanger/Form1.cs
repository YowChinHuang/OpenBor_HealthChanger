using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

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
                    DirectoryInfo DI = new DirectoryInfo(file);

                    foreach (FileInfo fileindirs in DI.GetFiles("*.txt", SearchOption.AllDirectories)) {
                        if (ProcessFile(fileindirs.FullName) == true) {
                            lstFile.Items.Add(fileindirs.FullName);
                            lstFile.SelectedIndex = lstFile.Items.Count - 1;
                        }
                        Application.DoEvents();
                    }
                    DI = null;
                } else {

                    if (Path.GetExtension(file).ToLower().Equals(".txt") == true) {
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
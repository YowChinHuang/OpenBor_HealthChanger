namespace OpenBor_HealthChanger {
    partial class frmMain {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該公開 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblPanel = new System.Windows.Forms.Label();
            this.lstFile = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lblPanel
            // 
            this.lblPanel.AllowDrop = true;
            this.lblPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPanel.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPanel.Location = new System.Drawing.Point(0, 0);
            this.lblPanel.Name = "lblPanel";
            this.lblPanel.Size = new System.Drawing.Size(389, 273);
            this.lblPanel.TabIndex = 0;
            this.lblPanel.Text = "Drop OpenBor Game Files (PAK or TXT) or data Directory";
            this.lblPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblPanel_DragDrop);
            this.lblPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.lblPanel_DragEnter);
            // 
            // lstFile
            // 
            this.lstFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFile.FormattingEnabled = true;
            this.lstFile.ItemHeight = 12;
            this.lstFile.Location = new System.Drawing.Point(0, 0);
            this.lstFile.Name = "lstFile";
            this.lstFile.Size = new System.Drawing.Size(389, 268);
            this.lstFile.TabIndex = 1;
            this.lstFile.DoubleClick += new System.EventHandler(this.lstFile_DoubleClick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 273);
            this.Controls.Add(this.lblPanel);
            this.Controls.Add(this.lstFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpenBor Enemy Health Changer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPanel;
        private System.Windows.Forms.ListBox lstFile;
    }
}


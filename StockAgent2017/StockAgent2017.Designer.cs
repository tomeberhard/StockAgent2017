namespace StockAgent2017
{
    partial class StockAgent2017
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ResultsTextBox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ScanTabPage = new System.Windows.Forms.TabPage();
            this.RunScan200DMA = new System.Windows.Forms.Button();
            this.RawDataTabPage = new System.Windows.Forms.TabPage();
            this.DataFolderLabel = new System.Windows.Forms.Label();
            this.DataFolderTextBox = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.ScanTabPage.SuspendLayout();
            this.RawDataTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // ResultsTextBox
            // 
            this.ResultsTextBox.Location = new System.Drawing.Point(12, 211);
            this.ResultsTextBox.Multiline = true;
            this.ResultsTextBox.Name = "ResultsTextBox";
            this.ResultsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ResultsTextBox.Size = new System.Drawing.Size(871, 189);
            this.ResultsTextBox.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.ScanTabPage);
            this.tabControl1.Controls.Add(this.RawDataTabPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(871, 193);
            this.tabControl1.TabIndex = 1;
            // 
            // ScanTabPage
            // 
            this.ScanTabPage.Controls.Add(this.RunScan200DMA);
            this.ScanTabPage.Location = new System.Drawing.Point(4, 22);
            this.ScanTabPage.Name = "ScanTabPage";
            this.ScanTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ScanTabPage.Size = new System.Drawing.Size(863, 167);
            this.ScanTabPage.TabIndex = 0;
            this.ScanTabPage.Text = "Scan";
            this.ScanTabPage.UseVisualStyleBackColor = true;
            // 
            // RunScan200DMA
            // 
            this.RunScan200DMA.Location = new System.Drawing.Point(6, 6);
            this.RunScan200DMA.Name = "RunScan200DMA";
            this.RunScan200DMA.Size = new System.Drawing.Size(75, 23);
            this.RunScan200DMA.TabIndex = 0;
            this.RunScan200DMA.Text = "200 DMA Crossing";
            this.RunScan200DMA.UseVisualStyleBackColor = true;
            this.RunScan200DMA.Click += new System.EventHandler(this.RunScan200DMA_Click);
            // 
            // RawDataTabPage
            // 
            this.RawDataTabPage.Controls.Add(this.DataFolderTextBox);
            this.RawDataTabPage.Controls.Add(this.DataFolderLabel);
            this.RawDataTabPage.Location = new System.Drawing.Point(4, 22);
            this.RawDataTabPage.Name = "RawDataTabPage";
            this.RawDataTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.RawDataTabPage.Size = new System.Drawing.Size(863, 167);
            this.RawDataTabPage.TabIndex = 1;
            this.RawDataTabPage.Text = "Raw Data";
            this.RawDataTabPage.UseVisualStyleBackColor = true;
            // 
            // DataFolderLabel
            // 
            this.DataFolderLabel.AutoSize = true;
            this.DataFolderLabel.Location = new System.Drawing.Point(6, 12);
            this.DataFolderLabel.Name = "DataFolderLabel";
            this.DataFolderLabel.Size = new System.Drawing.Size(62, 13);
            this.DataFolderLabel.TabIndex = 0;
            this.DataFolderLabel.Text = "Data Folder";
            // 
            // DataFolderTextBox
            // 
            this.DataFolderTextBox.Location = new System.Drawing.Point(74, 9);
            this.DataFolderTextBox.Name = "DataFolderTextBox";
            this.DataFolderTextBox.Size = new System.Drawing.Size(307, 20);
            this.DataFolderTextBox.TabIndex = 1;
            this.DataFolderTextBox.Text = "C:\\Users\\Tom\\Downloads\\StockData\\AMEX_all";
            // 
            // StockAgent2017
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 412);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.ResultsTextBox);
            this.Name = "StockAgent2017";
            this.Text = "StockAgent2017";
            this.tabControl1.ResumeLayout(false);
            this.ScanTabPage.ResumeLayout(false);
            this.RawDataTabPage.ResumeLayout(false);
            this.RawDataTabPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ResultsTextBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ScanTabPage;
        private System.Windows.Forms.TabPage RawDataTabPage;
        private System.Windows.Forms.Button RunScan200DMA;
        private System.Windows.Forms.TextBox DataFolderTextBox;
        private System.Windows.Forms.Label DataFolderLabel;
    }
}


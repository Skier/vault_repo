namespace Dalworth.Server.Servman.Win32.MainForm
{
    partial class MainFormView
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
            this.m_btnExportTransactions = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_btnOrderListenerStop = new System.Windows.Forms.Button();
            this.m_btnOrderListenerStart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_btnCopy = new System.Windows.Forms.Button();
            this.m_txtOrderNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_txtXmlNumber = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtFolder = new System.Windows.Forms.TextBox();
            this.m_btnImport = new System.Windows.Forms.Button();
            this.m_btnExport = new System.Windows.Forms.Button();
            this.m_btnFirstTimeSync = new System.Windows.Forms.Button();
            this.m_btnCustomerImport = new System.Windows.Forms.Button();
            this.m_btnImportTechnicians = new System.Windows.Forms.Button();
            this.m_btnBackgroundJobs = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtXmlNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnExportTransactions
            // 
            this.m_btnExportTransactions.Location = new System.Drawing.Point(712, 132);
            this.m_btnExportTransactions.Name = "m_btnExportTransactions";
            this.m_btnExportTransactions.Size = new System.Drawing.Size(85, 23);
            this.m_btnExportTransactions.TabIndex = 3;
            this.m_btnExportTransactions.Text = "Export";
            this.m_btnExportTransactions.UseVisualStyleBackColor = true;
            this.m_btnExportTransactions.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_btnOrderListenerStop);
            this.groupBox1.Controls.Add(this.m_btnOrderListenerStart);
            this.groupBox1.Location = new System.Drawing.Point(515, 224);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 60);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Order and Customer listener";
            this.groupBox1.Visible = false;
            // 
            // m_btnOrderListenerStop
            // 
            this.m_btnOrderListenerStop.Enabled = false;
            this.m_btnOrderListenerStop.Location = new System.Drawing.Point(192, 23);
            this.m_btnOrderListenerStop.Name = "m_btnOrderListenerStop";
            this.m_btnOrderListenerStop.Size = new System.Drawing.Size(90, 23);
            this.m_btnOrderListenerStop.TabIndex = 1;
            this.m_btnOrderListenerStop.Text = "Stop";
            this.m_btnOrderListenerStop.UseVisualStyleBackColor = true;
            // 
            // m_btnOrderListenerStart
            // 
            this.m_btnOrderListenerStart.Location = new System.Drawing.Point(9, 23);
            this.m_btnOrderListenerStart.Name = "m_btnOrderListenerStart";
            this.m_btnOrderListenerStart.Size = new System.Drawing.Size(90, 23);
            this.m_btnOrderListenerStart.TabIndex = 0;
            this.m_btnOrderListenerStart.Text = "Start";
            this.m_btnOrderListenerStart.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_btnCopy);
            this.groupBox2.Controls.Add(this.m_txtOrderNumber);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(515, 370);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 55);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Order Copy";
            this.groupBox2.Visible = false;
            // 
            // m_btnCopy
            // 
            this.m_btnCopy.Location = new System.Drawing.Point(192, 21);
            this.m_btnCopy.Name = "m_btnCopy";
            this.m_btnCopy.Size = new System.Drawing.Size(90, 23);
            this.m_btnCopy.TabIndex = 2;
            this.m_btnCopy.Text = "Copy";
            this.m_btnCopy.UseVisualStyleBackColor = true;
            // 
            // m_txtOrderNumber
            // 
            this.m_txtOrderNumber.Location = new System.Drawing.Point(86, 22);
            this.m_txtOrderNumber.Name = "m_txtOrderNumber";
            this.m_txtOrderNumber.Size = new System.Drawing.Size(100, 20);
            this.m_txtOrderNumber.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Order Number";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.m_txtXmlNumber);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.m_txtFolder);
            this.groupBox3.Location = new System.Drawing.Point(510, 29);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(287, 83);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Export Properties";
            this.groupBox3.Visible = false;
            // 
            // m_txtXmlNumber
            // 
            this.m_txtXmlNumber.Location = new System.Drawing.Point(110, 52);
            this.m_txtXmlNumber.Name = "m_txtXmlNumber";
            this.m_txtXmlNumber.Size = new System.Drawing.Size(62, 20);
            this.m_txtXmlNumber.TabIndex = 2;
            this.m_txtXmlNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "XML file number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "XML files folder";
            // 
            // m_txtFolder
            // 
            this.m_txtFolder.Location = new System.Drawing.Point(110, 23);
            this.m_txtFolder.Name = "m_txtFolder";
            this.m_txtFolder.Size = new System.Drawing.Size(164, 20);
            this.m_txtFolder.TabIndex = 1;
            this.m_txtFolder.Text = "C:\\temp\\";
            // 
            // m_btnImport
            // 
            this.m_btnImport.Location = new System.Drawing.Point(37, 64);
            this.m_btnImport.Name = "m_btnImport";
            this.m_btnImport.Size = new System.Drawing.Size(90, 23);
            this.m_btnImport.TabIndex = 5;
            this.m_btnImport.Text = "Import";
            this.m_btnImport.UseVisualStyleBackColor = true;
            // 
            // m_btnExport
            // 
            this.m_btnExport.Location = new System.Drawing.Point(133, 64);
            this.m_btnExport.Name = "m_btnExport";
            this.m_btnExport.Size = new System.Drawing.Size(94, 23);
            this.m_btnExport.TabIndex = 6;
            this.m_btnExport.Text = "Export";
            this.m_btnExport.UseVisualStyleBackColor = true;
            // 
            // m_btnFirstTimeSync
            // 
            this.m_btnFirstTimeSync.Location = new System.Drawing.Point(37, 35);
            this.m_btnFirstTimeSync.Name = "m_btnFirstTimeSync";
            this.m_btnFirstTimeSync.Size = new System.Drawing.Size(90, 23);
            this.m_btnFirstTimeSync.TabIndex = 7;
            this.m_btnFirstTimeSync.Text = "First Import";
            this.m_btnFirstTimeSync.UseVisualStyleBackColor = true;
            // 
            // m_btnCustomerImport
            // 
            this.m_btnCustomerImport.Location = new System.Drawing.Point(133, 35);
            this.m_btnCustomerImport.Name = "m_btnCustomerImport";
            this.m_btnCustomerImport.Size = new System.Drawing.Size(94, 23);
            this.m_btnCustomerImport.TabIndex = 8;
            this.m_btnCustomerImport.Text = "Customer Import";
            this.m_btnCustomerImport.UseVisualStyleBackColor = true;
            // 
            // m_btnImportTechnicians
            // 
            this.m_btnImportTechnicians.Location = new System.Drawing.Point(37, 93);
            this.m_btnImportTechnicians.Name = "m_btnImportTechnicians";
            this.m_btnImportTechnicians.Size = new System.Drawing.Size(190, 23);
            this.m_btnImportTechnicians.TabIndex = 9;
            this.m_btnImportTechnicians.Text = "Import Technicians";
            this.m_btnImportTechnicians.UseVisualStyleBackColor = true;
            // 
            // m_btnBackgroundJobs
            // 
            this.m_btnBackgroundJobs.Location = new System.Drawing.Point(37, 122);
            this.m_btnBackgroundJobs.Name = "m_btnBackgroundJobs";
            this.m_btnBackgroundJobs.Size = new System.Drawing.Size(190, 23);
            this.m_btnBackgroundJobs.TabIndex = 10;
            this.m_btnBackgroundJobs.Text = "Process Background Jobs";
            this.m_btnBackgroundJobs.UseVisualStyleBackColor = true;
            // 
            // MainFormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 177);
            this.Controls.Add(this.m_btnBackgroundJobs);
            this.Controls.Add(this.m_btnImportTechnicians);
            this.Controls.Add(this.m_btnCustomerImport);
            this.Controls.Add(this.m_btnFirstTimeSync);
            this.Controls.Add(this.m_btnExport);
            this.Controls.Add(this.m_btnImport);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_btnExportTransactions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainFormView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainFormView";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtXmlNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button m_btnExportTransactions;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.Button m_btnOrderListenerStop;
        internal System.Windows.Forms.Button m_btnOrderListenerStart;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.Button m_btnCopy;
        internal System.Windows.Forms.TextBox m_txtOrderNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox m_txtFolder;
        internal System.Windows.Forms.NumericUpDown m_txtXmlNumber;
        internal System.Windows.Forms.Button m_btnImport;
        internal System.Windows.Forms.Button m_btnExport;
        internal System.Windows.Forms.Button m_btnFirstTimeSync;
        internal System.Windows.Forms.Button m_btnCustomerImport;
        internal System.Windows.Forms.Button m_btnImportTechnicians;
        internal System.Windows.Forms.Button m_btnBackgroundJobs;

    }
}
namespace Dalworth.Server.MainForm.Components
{
    partial class Messages
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_lblMessage1 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtMessage3 = new DevExpress.XtraEditors.MemoEdit();
            this.m_txtMessage1 = new DevExpress.XtraEditors.MemoEdit();
            this.m_lblMessage3 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtMessage2 = new DevExpress.XtraEditors.MemoEdit();
            this.m_lblMessage2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtMessage3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtMessage1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtMessage2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl5
            // 
            this.groupControl5.Controls.Add(this.tableLayoutPanel1);
            this.groupControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl5.Location = new System.Drawing.Point(0, 0);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(590, 173);
            this.groupControl5.TabIndex = 11;
            this.groupControl5.Text = "Messages";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.m_lblMessage1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_txtMessage3, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_txtMessage1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_lblMessage3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_txtMessage2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_lblMessage2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(586, 151);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // m_lblMessage1
            // 
            this.m_lblMessage1.Location = new System.Drawing.Point(3, 3);
            this.m_lblMessage1.Name = "m_lblMessage1";
            this.m_lblMessage1.Size = new System.Drawing.Size(53, 13);
            this.m_lblMessage1.TabIndex = 10;
            this.m_lblMessage1.Text = "Description";
            // 
            // m_txtMessage3
            // 
            this.m_txtMessage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtMessage3.Location = new System.Drawing.Point(393, 20);
            this.m_txtMessage3.Name = "m_txtMessage3";
            this.m_txtMessage3.Size = new System.Drawing.Size(190, 128);
            this.m_txtMessage3.TabIndex = 5;
            // 
            // m_txtMessage1
            // 
            this.m_txtMessage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtMessage1.Location = new System.Drawing.Point(3, 20);
            this.m_txtMessage1.Name = "m_txtMessage1";
            this.m_txtMessage1.Size = new System.Drawing.Size(189, 128);
            this.m_txtMessage1.TabIndex = 3;
            // 
            // m_lblMessage3
            // 
            this.m_lblMessage3.Location = new System.Drawing.Point(393, 3);
            this.m_lblMessage3.Name = "m_lblMessage3";
            this.m_lblMessage3.Size = new System.Drawing.Size(42, 13);
            this.m_lblMessage3.TabIndex = 12;
            this.m_lblMessage3.Text = "Message";
            // 
            // m_txtMessage2
            // 
            this.m_txtMessage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtMessage2.Location = new System.Drawing.Point(198, 20);
            this.m_txtMessage2.Name = "m_txtMessage2";
            this.m_txtMessage2.Size = new System.Drawing.Size(189, 128);
            this.m_txtMessage2.TabIndex = 4;
            // 
            // m_lblMessage2
            // 
            this.m_lblMessage2.Location = new System.Drawing.Point(198, 3);
            this.m_lblMessage2.Name = "m_lblMessage2";
            this.m_lblMessage2.Size = new System.Drawing.Size(28, 13);
            this.m_lblMessage2.TabIndex = 11;
            this.m_lblMessage2.Text = "Notes";
            // 
            // Messages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl5);
            this.Name = "Messages";
            this.Size = new System.Drawing.Size(590, 173);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtMessage3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtMessage1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtMessage2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraEditors.LabelControl m_lblMessage3;
        private DevExpress.XtraEditors.LabelControl m_lblMessage2;
        private DevExpress.XtraEditors.LabelControl m_lblMessage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.MemoEdit m_txtMessage3;
        private DevExpress.XtraEditors.MemoEdit m_txtMessage2;
        private DevExpress.XtraEditors.MemoEdit m_txtMessage1;

    }
}

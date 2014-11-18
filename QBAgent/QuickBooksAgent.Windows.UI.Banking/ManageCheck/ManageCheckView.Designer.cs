namespace QuickBooksAgent.Windows.UI.Banking.ManageCheck
{
    partial class ManageCheckView
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
            this.m_lblAccountLabel = new System.Windows.Forms.Label();
            this.m_lblAccount = new System.Windows.Forms.Label();
            this.m_pnlTop = new System.Windows.Forms.Panel();
            this.m_table = new QuickBooksAgent.Windows.UI.Controls.Table();
            this.m_pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lblAccountLabel
            // 
            this.m_lblAccountLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblAccountLabel.Location = new System.Drawing.Point(3, 3);
            this.m_lblAccountLabel.Name = "m_lblAccountLabel";
            this.m_lblAccountLabel.Size = new System.Drawing.Size(63, 20);
            this.m_lblAccountLabel.Text = "Account:";
            // 
            // m_lblAccount
            // 
            this.m_lblAccount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblAccount.Location = new System.Drawing.Point(64, 3);
            this.m_lblAccount.Name = "m_lblAccount";
            this.m_lblAccount.Size = new System.Drawing.Size(173, 20);
            // 
            // m_pnlTop
            // 
            this.m_pnlTop.Controls.Add(this.m_lblAccount);
            this.m_pnlTop.Controls.Add(this.m_lblAccountLabel);
            this.m_pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlTop.Location = new System.Drawing.Point(0, 0);
            this.m_pnlTop.Name = "m_pnlTop";
            this.m_pnlTop.Size = new System.Drawing.Size(240, 24);
            // 
            // m_table
            // 
            this.m_table.AllowColumnResize = false;
            this.m_table.AltBackColor = System.Drawing.Color.Linen;
            this.m_table.AltForeColor = System.Drawing.Color.Black;
            this.m_table.AutoColumnSize = true;
            this.m_table.AutoMoveRow = true;
            this.m_table.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.m_table.ColumnBackColor = System.Drawing.Color.LightGray;
            this.m_table.ColumnFont = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.m_table.ColumnForeColor = System.Drawing.Color.Black;
            this.m_table.DefaultLineAligment = System.Drawing.StringAlignment.Center;
            this.m_table.DefaultRowHeight = 20;
            this.m_table.DefaultTextAligment = System.Drawing.StringAlignment.Center;
            this.m_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_table.DrawGridBorder = true;
            this.m_table.FocusCellBackColor = System.Drawing.Color.Black;
            this.m_table.FocusCellForeColor = System.Drawing.Color.White;
            this.m_table.GreyOut = false;
            this.m_table.LeftHeader = false;
            this.m_table.Location = new System.Drawing.Point(0, 24);
            this.m_table.MultipleSelection = false;
            this.m_table.Name = "m_table";
            this.m_table.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_table.SelectionForeColor = System.Drawing.Color.Black;
            this.m_table.ShowSplitterValue = true;
            this.m_table.ShowStartSplitter = true;
            this.m_table.Size = new System.Drawing.Size(240, 270);
            this.m_table.SplitterColor = System.Drawing.Color.Red;
            this.m_table.SplitterMode = QuickBooksAgent.Windows.UI.Controls.SplitterMode.Default;
            this.m_table.SplitterStartColor = System.Drawing.Color.Brown;
            this.m_table.SplitterWidth = 1;
            this.m_table.TabIndex = 6;
            this.m_table.Text = "table1";
            // 
            // ManageCheckView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_table);
            this.Controls.Add(this.m_pnlTop);
            this.Name = "ManageCheckView";
            this.Size = new System.Drawing.Size(240, 294);
            this.m_pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label m_lblAccountLabel;
        internal System.Windows.Forms.Label m_lblAccount;
        private System.Windows.Forms.Panel m_pnlTop;
        internal QuickBooksAgent.Windows.UI.Controls.Table m_table;

    }
}

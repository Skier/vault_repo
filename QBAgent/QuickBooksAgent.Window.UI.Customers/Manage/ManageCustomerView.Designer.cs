namespace QuickBooksAgent.Windows.UI.Customers.Manage
{
    partial class ManageCustomerView
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
            this.m_pnlTop = new System.Windows.Forms.Panel();
            this.m_txtSearch = new System.Windows.Forms.TextBox();
            this.m_pnlBottom = new System.Windows.Forms.Panel();
            this.m_table = new QuickBooksAgent.Windows.UI.Controls.Table();
            this.m_pnlTop.SuspendLayout();
            this.m_pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnlTop
            // 
            this.m_pnlTop.Controls.Add(this.m_txtSearch);
            this.m_pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlTop.Location = new System.Drawing.Point(0, 0);
            this.m_pnlTop.Name = "m_pnlTop";
            this.m_pnlTop.Size = new System.Drawing.Size(240, 28);
            // 
            // m_txtSearch
            // 
            this.m_txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtSearch.Location = new System.Drawing.Point(3, 3);
            this.m_txtSearch.Name = "m_txtSearch";
            this.m_txtSearch.Size = new System.Drawing.Size(234, 21);
            this.m_txtSearch.TabIndex = 0;
            // 
            // m_pnlBottom
            // 
            this.m_pnlBottom.AutoScroll = true;
            this.m_pnlBottom.AutoScrollMargin = new System.Drawing.Size(0, 10);
            this.m_pnlBottom.Controls.Add(this.m_table);
            this.m_pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlBottom.Location = new System.Drawing.Point(0, 28);
            this.m_pnlBottom.Name = "m_pnlBottom";
            this.m_pnlBottom.Size = new System.Drawing.Size(240, 240);
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
            this.m_table.DefaultTextAligment = System.Drawing.StringAlignment.Near;
            this.m_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_table.DrawGridBorder = true;
            this.m_table.FocusCellBackColor = System.Drawing.Color.Black;
            this.m_table.FocusCellForeColor = System.Drawing.Color.White;
            this.m_table.GreyOut = false;
            this.m_table.LeftHeader = false;
            this.m_table.Location = new System.Drawing.Point(0, 0);
            this.m_table.MultipleSelection = false;
            this.m_table.Name = "m_table";
            this.m_table.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_table.SelectionForeColor = System.Drawing.Color.Black;
            this.m_table.ShowSplitterValue = true;
            this.m_table.ShowStartSplitter = true;
            this.m_table.Size = new System.Drawing.Size(240, 240);
            this.m_table.SplitterColor = System.Drawing.Color.Red;
            this.m_table.SplitterMode = QuickBooksAgent.Windows.UI.Controls.SplitterMode.Default;
            this.m_table.SplitterStartColor = System.Drawing.Color.Brown;
            this.m_table.SplitterWidth = 1;
            this.m_table.TabIndex = 0;
            // 
            // ManageCustomerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.m_pnlBottom);
            this.Controls.Add(this.m_pnlTop);
            this.Name = "ManageCustomerView";
            this.Size = new System.Drawing.Size(240, 268);
            this.m_pnlTop.ResumeLayout(false);
            this.m_pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_pnlTop;
        private System.Windows.Forms.Panel m_pnlBottom;
        internal System.Windows.Forms.TextBox m_txtSearch;
        internal QuickBooksAgent.Windows.UI.Controls.Table m_table;
    }
}
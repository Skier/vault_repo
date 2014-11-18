namespace QuickBooksAgent.Windows.UI.Synchronize.Details
{
    partial class SynchronizeDetailsView
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
            this.m_table = new QuickBooksAgent.Windows.UI.Controls.Table();
            this.SuspendLayout();
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
            this.m_table.Location = new System.Drawing.Point(0, 0);
            this.m_table.MultipleSelection = false;
            this.m_table.Name = "m_table";
            this.m_table.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_table.SelectionForeColor = System.Drawing.Color.Black;
            this.m_table.ShowSplitterValue = true;
            this.m_table.ShowStartSplitter = true;
            this.m_table.Size = new System.Drawing.Size(216, 249);
            this.m_table.SplitterColor = System.Drawing.Color.Red;
            this.m_table.SplitterMode = QuickBooksAgent.Windows.UI.Controls.SplitterMode.Default;
            this.m_table.SplitterStartColor = System.Drawing.Color.Brown;
            this.m_table.SplitterWidth = 1;
            this.m_table.TabIndex = 0;
            this.m_table.Text = "table1";
            // 
            // SynchronizeDetailsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.m_table);
            this.Name = "SynchronizeDetailsView";
            this.Size = new System.Drawing.Size(216, 249);
            this.ResumeLayout(false);

        }

        #endregion

        internal QuickBooksAgent.Windows.UI.Controls.Table m_table;

    }
}

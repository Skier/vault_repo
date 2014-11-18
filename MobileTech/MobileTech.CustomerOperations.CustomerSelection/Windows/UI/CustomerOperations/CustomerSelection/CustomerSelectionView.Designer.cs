namespace MobileTech.Windows.UI.CustomerOperations.CustomerSelection
{
    partial class CustomerSelectionView
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
            this.m_mbSelect = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_table = new MobileTech.Windows.UI.Controls.Table();
            this.SuspendLayout();
            // 
            // m_mbSelect
            // 
            this.m_mbSelect.BackColor = System.Drawing.Color.White;
            this.m_mbSelect.BackDownColor = System.Drawing.Color.Black;
            this.m_mbSelect.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbSelect.ForeDownColor = System.Drawing.Color.White;
            this.m_mbSelect.IconMargin = 3;
            this.m_mbSelect.IconShift = false;
            this.m_mbSelect.IconTextSpace = 3;
            this.m_mbSelect.Location = new System.Drawing.Point(192, 230);
            this.m_mbSelect.Name = "m_mbSelect";
            this.m_mbSelect.Picture = MobileTech.Windows.UI.ImageKeys.Select_Small;
            this.m_mbSelect.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Select_SmallDisabled;
            this.m_mbSelect.PictureFocus = MobileTech.Windows.UI.ImageKeys.Select_SmallFocus;
            this.m_mbSelect.ShowBorder = true;
            this.m_mbSelect.ShowFocusBorder = true;
            this.m_mbSelect.Size = new System.Drawing.Size(48, 64);
            this.m_mbSelect.TabIndex = 2;
            this.m_mbSelect.Text = "Select";
            this.m_mbSelect.TextShift = false;
            this.m_mbSelect.TransparentIcon = true;
            this.m_mbSelect.TransparentImage = true;
            this.m_mbSelect.Click += new System.EventHandler(this.OnSelectClick);
            // 
            // m_table
            // 
            this.m_table.AllowColumnResize = false;
            this.m_table.AltBackColor = System.Drawing.Color.Linen;
            this.m_table.AltForeColor = System.Drawing.Color.Black;
            this.m_table.AutoColumnSize = true;
            this.m_table.BackColor = System.Drawing.Color.White;
            this.m_table.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.m_table.ColumnBackColor = System.Drawing.Color.LightGray;
            this.m_table.ColumnFont = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.m_table.ColumnForeColor = System.Drawing.Color.Black;
            this.m_table.DefaultLineAligment = System.Drawing.StringAlignment.Center;
            this.m_table.DefaultRowHeight = 20;
            this.m_table.DefaultTextAligment = System.Drawing.StringAlignment.Center;
            this.m_table.FocusCellBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_table.FocusCellForeColor = System.Drawing.Color.Black;
            this.m_table.Location = new System.Drawing.Point(2, 3);
            this.m_table.Name = "m_table";
            this.m_table.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_table.SelectionForeColor = System.Drawing.Color.Black;
            this.m_table.ShowSplitterValue = true;
            this.m_table.ShowStartSplitter = true;
            this.m_table.Size = new System.Drawing.Size(237, 221);
            this.m_table.SplitterColor = System.Drawing.Color.Red;
            this.m_table.SplitterMode = MobileTech.Windows.UI.Controls.SplitterMode.Default;
            this.m_table.SplitterStartColor = System.Drawing.Color.Brown;
            this.m_table.SplitterWidth = 1;
            this.m_table.TabIndex = 1;
            this.m_table.Text = "table1";
            this.m_table.Enter += new MobileTech.Windows.UI.Controls.CellValueHandler(this.OnTableEnter);
            // 
            // CustomerSelectionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_table);
            this.Controls.Add(this.m_mbSelect);
            this.Name = "CustomerSelectionView";
            this.Text = "3010 - Customer Selection";
            this.ResumeLayout(false);

        }

        #endregion

        private MobileTech.Windows.UI.Controls.MenuButton m_mbSelect;
        private MobileTech.Windows.UI.Controls.Table m_table;
    }
}
namespace MobileTech.Windows.UI.FlagEditor
{
    partial class FlagEditorRouteValuesView
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
            this.m_pnButtons = new System.Windows.Forms.Panel();
            this.m_mbDelete = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbAdd = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbDone = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_lbSearch = new System.Windows.Forms.Label();
            this.m_txtSearch = new System.Windows.Forms.TextBox();
            this.m_table = new MobileTech.Windows.UI.Controls.Table();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.m_pnButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnButtons
            // 
            this.m_pnButtons.Controls.Add(this.m_mbDelete);
            this.m_pnButtons.Controls.Add(this.m_mbAdd);
            this.m_pnButtons.Controls.Add(this.m_mbDone);
            this.m_pnButtons.Location = new System.Drawing.Point(2, 235);
            this.m_pnButtons.Name = "m_pnButtons";
            this.m_pnButtons.Size = new System.Drawing.Size(237, 57);
            // 
            // m_mbDelete
            // 
            this.m_mbDelete.BackDownColor = System.Drawing.Color.Black;
            this.m_mbDelete.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbDelete.ForeDownColor = System.Drawing.Color.White;
            this.m_mbDelete.IconMargin = 3;
            this.m_mbDelete.IconShift = false;
            this.m_mbDelete.IconTextSpace = 3;
            this.m_mbDelete.ImageDown = null;
            this.m_mbDelete.ImageUp = null;
            this.m_mbDelete.Location = new System.Drawing.Point(58, 2);
            this.m_mbDelete.Name = "m_mbDelete";
            this.m_mbDelete.Picture = MobileTech.Windows.UI.ImageKeys.None;
            this.m_mbDelete.PictureDisabled = MobileTech.Windows.UI.ImageKeys.None;
            this.m_mbDelete.PictureFocus = MobileTech.Windows.UI.ImageKeys.None;
            this.m_mbDelete.ShowBorder = true;
            this.m_mbDelete.ShowFocusBorder = true;
            this.m_mbDelete.Size = new System.Drawing.Size(49, 51);
            this.m_mbDelete.TabIndex = 2;
            this.m_mbDelete.Text = "Delete";
            this.m_mbDelete.TextShift = false;
            this.m_mbDelete.TransparentIcon = true;
            this.m_mbDelete.TransparentImage = true;
            // 
            // m_mbAdd
            // 
            this.m_mbAdd.BackDownColor = System.Drawing.Color.Black;
            this.m_mbAdd.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbAdd.ForeDownColor = System.Drawing.Color.White;
            this.m_mbAdd.IconMargin = 3;
            this.m_mbAdd.IconShift = false;
            this.m_mbAdd.IconTextSpace = 3;
            this.m_mbAdd.ImageDown = null;
            this.m_mbAdd.ImageUp = null;
            this.m_mbAdd.Location = new System.Drawing.Point(3, 2);
            this.m_mbAdd.Name = "m_mbAdd";
            this.m_mbAdd.Picture = MobileTech.Windows.UI.ImageKeys.None;
            this.m_mbAdd.PictureDisabled = MobileTech.Windows.UI.ImageKeys.None;
            this.m_mbAdd.PictureFocus = MobileTech.Windows.UI.ImageKeys.None;
            this.m_mbAdd.ShowBorder = true;
            this.m_mbAdd.ShowFocusBorder = true;
            this.m_mbAdd.Size = new System.Drawing.Size(49, 51);
            this.m_mbAdd.TabIndex = 1;
            this.m_mbAdd.Text = "Add";
            this.m_mbAdd.TextShift = false;
            this.m_mbAdd.TransparentIcon = true;
            this.m_mbAdd.TransparentImage = true;
            // 
            // m_mbDone
            // 
            this.m_mbDone.BackDownColor = System.Drawing.Color.Black;
            this.m_mbDone.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbDone.ForeDownColor = System.Drawing.Color.White;
            this.m_mbDone.IconMargin = 3;
            this.m_mbDone.IconShift = false;
            this.m_mbDone.IconTextSpace = 3;
            this.m_mbDone.ImageDown = null;
            this.m_mbDone.ImageUp = null;
            this.m_mbDone.Location = new System.Drawing.Point(184, 3);
            this.m_mbDone.Name = "m_mbDone";
            this.m_mbDone.Picture = MobileTech.Windows.UI.ImageKeys.None;
            this.m_mbDone.PictureDisabled = MobileTech.Windows.UI.ImageKeys.None;
            this.m_mbDone.PictureFocus = MobileTech.Windows.UI.ImageKeys.None;
            this.m_mbDone.ShowBorder = true;
            this.m_mbDone.ShowFocusBorder = true;
            this.m_mbDone.Size = new System.Drawing.Size(49, 51);
            this.m_mbDone.TabIndex = 0;
            this.m_mbDone.Text = "Done";
            this.m_mbDone.TextShift = false;
            this.m_mbDone.TransparentIcon = true;
            this.m_mbDone.TransparentImage = true;
            // 
            // m_lbSearch
            // 
            this.m_lbSearch.Location = new System.Drawing.Point(5, 65);
            this.m_lbSearch.Name = "m_lbSearch";
            this.m_lbSearch.Size = new System.Drawing.Size(52, 20);
            this.m_lbSearch.Text = "Search:";
            // 
            // m_txtSearch
            // 
            this.m_txtSearch.Location = new System.Drawing.Point(60, 64);
            this.m_txtSearch.Name = "m_txtSearch";
            this.m_txtSearch.Size = new System.Drawing.Size(177, 21);
            this.m_txtSearch.TabIndex = 6;
            // 
            // m_table
            // 
            this.m_table.AllowColumnResize = false;
            this.m_table.AltBackColor = System.Drawing.Color.Linen;
            this.m_table.AltForeColor = System.Drawing.Color.Black;
            this.m_table.AutoColumnSize = true;
            this.m_table.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.m_table.ColumnBackColor = System.Drawing.Color.LightGray;
            this.m_table.ColumnFont = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.m_table.ColumnForeColor = System.Drawing.Color.Black;
            this.m_table.DefaultLineAligment = System.Drawing.StringAlignment.Center;
            this.m_table.DefaultRowHeight = 20;
            this.m_table.DefaultTextAligment = System.Drawing.StringAlignment.Center;
            this.m_table.FocusCellBackColor = System.Drawing.Color.Black;
            this.m_table.FocusCellForeColor = System.Drawing.Color.White;
            this.m_table.Location = new System.Drawing.Point(3, 93);
            this.m_table.Name = "m_table";
            this.m_table.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_table.SelectionForeColor = System.Drawing.Color.Black;
            this.m_table.ShowSplitterValue = true;
            this.m_table.ShowStartSplitter = true;
            this.m_table.Size = new System.Drawing.Size(234, 138);
            this.m_table.SplitterColor = System.Drawing.Color.Red;
            this.m_table.SplitterMode = MobileTech.Windows.UI.Controls.SplitterMode.Default;
            this.m_table.SplitterStartColor = System.Drawing.Color.Brown;
            this.m_table.SplitterWidth = 1;
            this.m_table.TabIndex = 3;
            this.m_table.Text = "table1";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 20);
            this.label1.Text = "Location:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 20);
            this.label2.Text = "Route:";
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(60, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(177, 22);
            this.comboBox1.TabIndex = 10;
            // 
            // comboBox2
            // 
            this.comboBox2.Location = new System.Drawing.Point(60, 34);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(177, 22);
            this.comboBox2.TabIndex = 11;
            // 
            // FlagEditorRouteValuesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_pnButtons);
            this.Controls.Add(this.m_lbSearch);
            this.Controls.Add(this.m_txtSearch);
            this.Controls.Add(this.m_table);
            this.Name = "FlagEditorRouteValuesView";
            this.Text = "FlagEditorRouteValuesView";
            this.m_pnButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_pnButtons;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbDelete;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbAdd;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbDone;
        private System.Windows.Forms.Label m_lbSearch;
        private System.Windows.Forms.TextBox m_txtSearch;
        private MobileTech.Windows.UI.Controls.Table m_table;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}
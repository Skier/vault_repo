namespace MobileTech.Windows.UI.ItemSearch
{
    partial class ItemSearchView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            this.m_table = new MobileTech.Windows.UI.Controls.Table();
            this.m_txtSearch = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_rbSearchByName = new System.Windows.Forms.RadioButton();
            this.m_rbSearchByNumber = new System.Windows.Forms.RadioButton();
            this.m_mbSelect = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_linkCategory = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.m_table.ColumnFont = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.m_table.ColumnForeColor = System.Drawing.Color.Black;
            this.m_table.DefaultLineAligment = System.Drawing.StringAlignment.Center;
            this.m_table.DefaultRowHeight = 20;
            this.m_table.DefaultTextAligment = System.Drawing.StringAlignment.Center;
            this.m_table.FocusCellBackColor = System.Drawing.Color.Black;
            this.m_table.FocusCellForeColor = System.Drawing.Color.White;
            this.m_table.Location = new System.Drawing.Point(3, 70);
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
            this.m_table.TabIndex = 3;
            this.m_table.Text = "table1";
            this.m_table.Enter += new MobileTech.Windows.UI.Controls.CellValueHandler(this.OnTableEnter);
            this.m_table.RowChanged += new MobileTech.Windows.UI.Controls.RowHandler(this.OnRowChanged);
            // 
            // m_txtSearch
            // 
            this.m_txtSearch.BackColor = System.Drawing.Color.White;
            this.m_txtSearch.Location = new System.Drawing.Point(3, 5);
            this.m_txtSearch.Name = "m_txtSearch";
            this.m_txtSearch.Size = new System.Drawing.Size(183, 21);
            this.m_txtSearch.TabIndex = 1;
            this.m_txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnEnterPress);
            this.m_txtSearch.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.m_txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnSearchKeyDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.m_rbSearchByName);
            this.panel1.Controls.Add(this.m_rbSearchByNumber);
            this.panel1.Location = new System.Drawing.Point(3, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(172, 24);
            // 
            // m_rbSearchByName
            // 
            this.m_rbSearchByName.BackColor = System.Drawing.Color.White;
            this.m_rbSearchByName.Location = new System.Drawing.Point(3, 1);
            this.m_rbSearchByName.Name = "m_rbSearchByName";
            this.m_rbSearchByName.Size = new System.Drawing.Size(76, 20);
            this.m_rbSearchByName.TabIndex = 3;
            this.m_rbSearchByName.Text = "Name";
            this.m_rbSearchByName.CheckedChanged += new System.EventHandler(this.OnModeChange);
            // 
            // m_rbSearchByNumber
            // 
            this.m_rbSearchByNumber.BackColor = System.Drawing.Color.White;
            this.m_rbSearchByNumber.Location = new System.Drawing.Point(90, 1);
            this.m_rbSearchByNumber.Name = "m_rbSearchByNumber";
            this.m_rbSearchByNumber.Size = new System.Drawing.Size(79, 20);
            this.m_rbSearchByNumber.TabIndex = 2;
            this.m_rbSearchByNumber.TabStop = false;
            this.m_rbSearchByNumber.Text = "Number";
            this.m_rbSearchByNumber.CheckedChanged += new System.EventHandler(this.OnModeChange);
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
            this.m_mbSelect.Location = new System.Drawing.Point(192, 5);
            this.m_mbSelect.Name = "m_mbSelect";
            this.m_mbSelect.Picture = MobileTech.Windows.UI.ImageKeys.Done_Small;
            this.m_mbSelect.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Done_SmallDisabled;
            this.m_mbSelect.PictureFocus = MobileTech.Windows.UI.ImageKeys.Done_SmallFocus;
            this.m_mbSelect.ShowBorder = true;
            this.m_mbSelect.ShowFocusBorder = true;
            this.m_mbSelect.Size = new System.Drawing.Size(48, 64);
            this.m_mbSelect.TabIndex = 4;
            this.m_mbSelect.Text = "Select";
            this.m_mbSelect.TextShift = false;
            this.m_mbSelect.TransparentIcon = true;
            this.m_mbSelect.TransparentImage = true;
            this.m_mbSelect.Click += new System.EventHandler(this.OnSelectClick);
            // 
            // m_linkCategory
            // 
            this.m_linkCategory.Location = new System.Drawing.Point(3, 54);
            this.m_linkCategory.Name = "m_linkCategory";
            this.m_linkCategory.Size = new System.Drawing.Size(186, 13);
            this.m_linkCategory.TabIndex = 5;
            this.m_linkCategory.TabStop = false;
            this.m_linkCategory.Click += new System.EventHandler(this.OnCategoryClick);
            // 
            // ItemSearchView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_linkCategory);
            this.Controls.Add(this.m_mbSelect);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_txtSearch);
            this.Controls.Add(this.m_table);
            this.Name = "ItemSearchView";
            this.Text = "9000 - Find";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MobileTech.Windows.UI.Controls.Table m_table;
        private System.Windows.Forms.TextBox m_txtSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton m_rbSearchByName;
        private System.Windows.Forms.RadioButton m_rbSearchByNumber;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbSelect;
        private System.Windows.Forms.LinkLabel m_linkCategory;

    }
}
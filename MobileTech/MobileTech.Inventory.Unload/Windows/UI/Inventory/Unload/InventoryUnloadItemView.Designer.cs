namespace MobileTech.Windows.UI.Inventory.Unload
{
    partial class InventoryUnloadItemView
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
            this.m_table = new MobileTech.Windows.UI.Controls.Table();
            this.m_cbMode = new System.Windows.Forms.ComboBox();
            this.m_lbItemNumber = new System.Windows.Forms.Label();
            this.m_mbSave = new MobileTech.Windows.UI.Controls.MenuButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.m_cbStorage = new System.Windows.Forms.ComboBox();
            this.m_mbAddItem = new MobileTech.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
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
            this.m_table.Location = new System.Drawing.Point(3, 26);
            this.m_table.Name = "m_table";
            this.m_table.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_table.SelectionForeColor = System.Drawing.Color.Black;
            this.m_table.ShowSplitterValue = true;
            this.m_table.ShowStartSplitter = true;
            this.m_table.Size = new System.Drawing.Size(234, 199);
            this.m_table.SplitterColor = System.Drawing.Color.Red;
            this.m_table.SplitterMode = MobileTech.Windows.UI.Controls.SplitterMode.Default;
            this.m_table.SplitterStartColor = System.Drawing.Color.Brown;
            this.m_table.SplitterWidth = 1;
            this.m_table.TabIndex = 0;
            this.m_table.RowChanged += new MobileTech.Windows.UI.Controls.RowHandler(this.OnTableRowChanged);
            // 
            // m_cbMode
            // 
            this.m_cbMode.Location = new System.Drawing.Point(3, 2);
            this.m_cbMode.Name = "m_cbMode";
            this.m_cbMode.Size = new System.Drawing.Size(157, 22);
            this.m_cbMode.TabIndex = 1;
            this.m_cbMode.SelectedIndexChanged += new System.EventHandler(this.OnModeSelectedIndexChanged);
            // 
            // m_lbItemNumber
            // 
            this.m_lbItemNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lbItemNumber.Location = new System.Drawing.Point(3, 228);
            this.m_lbItemNumber.Name = "m_lbItemNumber";
            this.m_lbItemNumber.Size = new System.Drawing.Size(142, 22);
            // 
            // m_mbSave
            // 
            this.m_mbSave.BackColor = System.Drawing.Color.White;
            this.m_mbSave.BackDownColor = System.Drawing.Color.Black;
            this.m_mbSave.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbSave.ForeDownColor = System.Drawing.Color.White;
            this.m_mbSave.IconMargin = 3;
            this.m_mbSave.IconShift = false;
            this.m_mbSave.IconTextSpace = 3;
            this.m_mbSave.Location = new System.Drawing.Point(189, 227);
            this.m_mbSave.Name = "m_mbSave";
            this.m_mbSave.Picture = MobileTech.Windows.UI.ImageKeys.Done_Small;
            this.m_mbSave.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Done_SmallDisabled;
            this.m_mbSave.PictureFocus = MobileTech.Windows.UI.ImageKeys.Done_SmallFocus;
            this.m_mbSave.ShowBorder = true;
            this.m_mbSave.ShowFocusBorder = true;
            this.m_mbSave.Size = new System.Drawing.Size(48, 64);
            this.m_mbSave.TabIndex = 3;
            this.m_mbSave.Text = "Done";
            this.m_mbSave.TextShift = false;
            this.m_mbSave.TransparentIcon = true;
            this.m_mbSave.TransparentImage = true;
            this.m_mbSave.Click += new System.EventHandler(this.OnSaveClick);
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(234, 22);
            this.comboBox1.TabIndex = 1;
            // 
            // m_cbStorage
            // 
            this.m_cbStorage.Location = new System.Drawing.Point(163, 2);
            this.m_cbStorage.Name = "m_cbStorage";
            this.m_cbStorage.Size = new System.Drawing.Size(74, 22);
            this.m_cbStorage.TabIndex = 4;
            this.m_cbStorage.SelectedIndexChanged += new System.EventHandler(this.OnStorageSelectedIndexChanged);
            // 
            // m_mbAddItem
            // 
            this.m_mbAddItem.BackColor = System.Drawing.Color.White;
            this.m_mbAddItem.BackDownColor = System.Drawing.Color.Black;
            this.m_mbAddItem.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbAddItem.ForeDownColor = System.Drawing.Color.White;
            this.m_mbAddItem.IconMargin = 3;
            this.m_mbAddItem.IconShift = false;
            this.m_mbAddItem.IconTextSpace = 3;
            this.m_mbAddItem.Location = new System.Drawing.Point(3, 227);
            this.m_mbAddItem.Name = "m_mbAddItem";
            this.m_mbAddItem.Picture = MobileTech.Windows.UI.ImageKeys.Add_Small;
            this.m_mbAddItem.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Add_Small;
            this.m_mbAddItem.PictureFocus = MobileTech.Windows.UI.ImageKeys.Add_Small;
            this.m_mbAddItem.ShowBorder = true;
            this.m_mbAddItem.ShowFocusBorder = true;
            this.m_mbAddItem.Size = new System.Drawing.Size(48, 64);
            this.m_mbAddItem.TabIndex = 6;
            this.m_mbAddItem.Text = "Add";
            this.m_mbAddItem.TextShift = false;
            this.m_mbAddItem.TransparentIcon = true;
            this.m_mbAddItem.TransparentImage = true;
            this.m_mbAddItem.Click += new System.EventHandler(this.OnAddItemClick);
            // 
            // InventoryUnloadGoodView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_mbAddItem);
            this.Controls.Add(this.m_cbStorage);
            this.Controls.Add(this.m_lbItemNumber);
            this.Controls.Add(this.m_mbSave);
            this.Controls.Add(this.m_cbMode);
            this.Controls.Add(this.m_table);
            this.Name = "InventoryUnloadGoodView";
            this.Text = "Unload - Good";
            this.ResumeLayout(false);

        }

        #endregion

        private MobileTech.Windows.UI.Controls.Table m_table;
        private System.Windows.Forms.ComboBox m_cbMode;
        private System.Windows.Forms.Label m_lbItemNumber;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbSave;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox m_cbStorage;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbAddItem;
    }
}
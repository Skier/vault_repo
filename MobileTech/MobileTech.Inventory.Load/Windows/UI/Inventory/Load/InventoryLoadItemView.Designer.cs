namespace MobileTech.Windows.UI.Inventory.Load
{
    partial class InventoryLoadItemView
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
            this.m_cbModes = new System.Windows.Forms.ComboBox();
            this.m_lbItemNumber = new System.Windows.Forms.Label();
            this.m_lbTruckQty = new System.Windows.Forms.Label();
            this.m_mbAddItem = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbSave = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_table = new MobileTech.Windows.UI.Controls.Table();
            this.m_cbStorage = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // m_cbModes
            // 
            this.m_cbModes.BackColor = System.Drawing.Color.White;
            this.m_cbModes.Location = new System.Drawing.Point(3, 3);
            this.m_cbModes.Name = "m_cbModes";
            this.m_cbModes.Size = new System.Drawing.Size(157, 22);
            this.m_cbModes.TabIndex = 0;
            this.m_cbModes.SelectedIndexChanged += new System.EventHandler(this.OnModeSelectedIndexChanged);
            // 
            // m_lbItemNumber
            // 
            this.m_lbItemNumber.BackColor = System.Drawing.Color.White;
            this.m_lbItemNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lbItemNumber.Location = new System.Drawing.Point(3, 183);
            this.m_lbItemNumber.Name = "m_lbItemNumber";
            this.m_lbItemNumber.Size = new System.Drawing.Size(234, 22);
            // 
            // m_lbTruckQty
            // 
            this.m_lbTruckQty.BackColor = System.Drawing.Color.White;
            this.m_lbTruckQty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lbTruckQty.Location = new System.Drawing.Point(3, 205);
            this.m_lbTruckQty.Name = "m_lbTruckQty";
            this.m_lbTruckQty.Size = new System.Drawing.Size(234, 22);
            this.m_lbTruckQty.Visible = false;
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
            this.m_mbAddItem.TabIndex = 3;
            this.m_mbAddItem.Text = "Add";
            this.m_mbAddItem.TextShift = false;
            this.m_mbAddItem.TransparentIcon = true;
            this.m_mbAddItem.TransparentImage = true;
            this.m_mbAddItem.Click += new System.EventHandler(this.OnAddItemClick);
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
            this.m_mbSave.TabIndex = 2;
            this.m_mbSave.Text = "Done";
            this.m_mbSave.TextShift = false;
            this.m_mbSave.TransparentIcon = true;
            this.m_mbSave.TransparentImage = true;
            this.m_mbSave.Click += new System.EventHandler(this.OnSaveClick);
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
            this.m_table.DefaultTextAligment = System.Drawing.StringAlignment.Near;
            this.m_table.FocusCellBackColor = System.Drawing.Color.Black;
            this.m_table.FocusCellForeColor = System.Drawing.Color.White;
            this.m_table.Location = new System.Drawing.Point(3, 27);
            this.m_table.Name = "m_table";
            this.m_table.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_table.SelectionForeColor = System.Drawing.Color.Black;
            this.m_table.ShowSplitterValue = true;
            this.m_table.ShowStartSplitter = true;
            this.m_table.Size = new System.Drawing.Size(234, 153);
            this.m_table.SplitterColor = System.Drawing.Color.Red;
            this.m_table.SplitterMode = MobileTech.Windows.UI.Controls.SplitterMode.Default;
            this.m_table.SplitterStartColor = System.Drawing.Color.Brown;
            this.m_table.SplitterWidth = 1;
            this.m_table.TabIndex = 1;
            // 
            // m_cbStorage
            // 
            this.m_cbStorage.Location = new System.Drawing.Point(163, 3);
            this.m_cbStorage.Name = "m_cbStorage";
            this.m_cbStorage.Size = new System.Drawing.Size(74, 22);
            this.m_cbStorage.TabIndex = 5;
            this.m_cbStorage.SelectedIndexChanged += new System.EventHandler(this.OnStorageSelectedIndexChanged);
            // 
            // InventoryLoadItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_cbStorage);
            this.Controls.Add(this.m_lbTruckQty);
            this.Controls.Add(this.m_lbItemNumber);
            this.Controls.Add(this.m_mbAddItem);
            this.Controls.Add(this.m_mbSave);
            this.Controls.Add(this.m_cbModes);
            this.Controls.Add(this.m_table);
            this.Name = "InventoryLoadItemView";
            this.Text = "2100 - Load";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private MobileTech.Windows.UI.Controls.Table m_table;
        private System.Windows.Forms.ComboBox m_cbModes;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbSave;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbAddItem;
        private System.Windows.Forms.Label m_lbItemNumber;
        private System.Windows.Forms.Label m_lbTruckQty;
        private System.Windows.Forms.ComboBox m_cbStorage;
    }
}
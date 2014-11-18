namespace MobileTech.Windows.UI.CustomerOperations.Invoice
{
    partial class InvoiceView
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
            this.m_lbTruck = new System.Windows.Forms.Label();
            this.m_lbID = new System.Windows.Forms.Label();
            this.m_table = new MobileTech.Windows.UI.Controls.Table();
            this.m_mbDone = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbAdd = new MobileTech.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_lbTruck
            // 
            this.m_lbTruck.BackColor = System.Drawing.Color.White;
            this.m_lbTruck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lbTruck.Location = new System.Drawing.Point(0, 205);
            this.m_lbTruck.Name = "m_lbTruck";
            this.m_lbTruck.Size = new System.Drawing.Size(237, 22);
            this.m_lbTruck.Text = "Truck: ";
            this.m_lbTruck.Visible = false;
            // 
            // m_lbID
            // 
            this.m_lbID.BackColor = System.Drawing.Color.White;
            this.m_lbID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lbID.Location = new System.Drawing.Point(0, 183);
            this.m_lbID.Name = "m_lbID";
            this.m_lbID.Size = new System.Drawing.Size(237, 22);
            this.m_lbID.Text = "ID:";
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
            this.m_table.Location = new System.Drawing.Point(3, 3);
            this.m_table.Name = "m_table";
            this.m_table.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_table.SelectionForeColor = System.Drawing.Color.Black;
            this.m_table.ShowSplitterValue = true;
            this.m_table.ShowStartSplitter = true;
            this.m_table.Size = new System.Drawing.Size(234, 177);
            this.m_table.SplitterColor = System.Drawing.Color.Red;
            this.m_table.SplitterMode = MobileTech.Windows.UI.Controls.SplitterMode.Default;
            this.m_table.SplitterStartColor = System.Drawing.Color.Brown;
            this.m_table.SplitterWidth = 1;
            this.m_table.TabIndex = 1;
            this.m_table.Text = "table1";
            this.m_table.RowChanged += new MobileTech.Windows.UI.Controls.RowHandler(this.OnRowChange);
            // 
            // m_mbDone
            // 
            this.m_mbDone.BackColor = System.Drawing.Color.White;
            this.m_mbDone.BackDownColor = System.Drawing.Color.Black;
            this.m_mbDone.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbDone.ForeDownColor = System.Drawing.Color.White;
            this.m_mbDone.IconMargin = 3;
            this.m_mbDone.IconShift = false;
            this.m_mbDone.IconTextSpace = 3;
            this.m_mbDone.Location = new System.Drawing.Point(192, 230);
            this.m_mbDone.Name = "m_mbDone";
            this.m_mbDone.Picture = MobileTech.Windows.UI.ImageKeys.Done_Small;
            this.m_mbDone.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Done_SmallDisabled;
            this.m_mbDone.PictureFocus = MobileTech.Windows.UI.ImageKeys.Done_SmallFocus;
            this.m_mbDone.ShowBorder = true;
            this.m_mbDone.ShowFocusBorder = true;
            this.m_mbDone.Size = new System.Drawing.Size(48, 64);
            this.m_mbDone.TabIndex = 3;
            this.m_mbDone.Text = "Done";
            this.m_mbDone.TextShift = false;
            this.m_mbDone.TransparentIcon = true;
            this.m_mbDone.TransparentImage = true;
            this.m_mbDone.Click += new System.EventHandler(this.OnDoneClick);
            // 
            // m_mbAdd
            // 
            this.m_mbAdd.BackColor = System.Drawing.Color.White;
            this.m_mbAdd.BackDownColor = System.Drawing.Color.Black;
            this.m_mbAdd.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbAdd.ForeDownColor = System.Drawing.Color.White;
            this.m_mbAdd.IconMargin = 3;
            this.m_mbAdd.IconShift = false;
            this.m_mbAdd.IconTextSpace = 3;
            this.m_mbAdd.Location = new System.Drawing.Point(0, 230);
            this.m_mbAdd.Name = "m_mbAdd";
            this.m_mbAdd.Picture = MobileTech.Windows.UI.ImageKeys.Add_Small;
            this.m_mbAdd.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Add_SmallDisabled;
            this.m_mbAdd.PictureFocus = MobileTech.Windows.UI.ImageKeys.Add_SmallFocus;
            this.m_mbAdd.ShowBorder = true;
            this.m_mbAdd.ShowFocusBorder = true;
            this.m_mbAdd.Size = new System.Drawing.Size(48, 64);
            this.m_mbAdd.TabIndex = 2;
            this.m_mbAdd.Text = "Add";
            this.m_mbAdd.TextShift = false;
            this.m_mbAdd.TransparentIcon = true;
            this.m_mbAdd.TransparentImage = true;
            this.m_mbAdd.Click += new System.EventHandler(this.OnAddClick);
            // 
            // InvoiceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_mbAdd);
            this.Controls.Add(this.m_lbID);
            this.Controls.Add(this.m_lbTruck);
            this.Controls.Add(this.m_table);
            this.Controls.Add(this.m_mbDone);
            this.Name = "InvoiceView";
            this.Text = "3315 - Invoice-Item Entry";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lbTruck;
        private System.Windows.Forms.Label m_lbID;
        private MobileTech.Windows.UI.Controls.Table m_table;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbDone;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbAdd;
    }
}
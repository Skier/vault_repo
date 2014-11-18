namespace MobileTech.Windows.UI.CustomerOperations.Invoice
{
    partial class OversellWarningView
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
            this.m_lbItemNumber = new System.Windows.Forms.Label();
            this.m_txtItemNumber = new System.Windows.Forms.TextBox();
            this.m_txtItemName = new System.Windows.Forms.TextBox();
            this.m_lbQtyInStock = new System.Windows.Forms.Label();
            this.m_txtQtyInStock = new System.Windows.Forms.TextBox();
            this.m_lbTrxnQty = new System.Windows.Forms.Label();
            this.m_txtTrxnQty = new System.Windows.Forms.TextBox();
            this.m_mbOk = new MobileTech.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_lbItemNumber
            // 
            this.m_lbItemNumber.BackColor = System.Drawing.Color.White;
            this.m_lbItemNumber.Location = new System.Drawing.Point(16, 19);
            this.m_lbItemNumber.Name = "m_lbItemNumber";
            this.m_lbItemNumber.Size = new System.Drawing.Size(85, 21);
            this.m_lbItemNumber.Text = "Item number:";
            // 
            // m_txtItemNumber
            // 
            this.m_txtItemNumber.BackColor = System.Drawing.Color.White;
            this.m_txtItemNumber.Location = new System.Drawing.Point(121, 19);
            this.m_txtItemNumber.Name = "m_txtItemNumber";
            this.m_txtItemNumber.ReadOnly = true;
            this.m_txtItemNumber.Size = new System.Drawing.Size(101, 21);
            this.m_txtItemNumber.TabIndex = 1;
            this.m_txtItemNumber.TabStop = false;
            // 
            // m_txtItemName
            // 
            this.m_txtItemName.BackColor = System.Drawing.Color.White;
            this.m_txtItemName.Location = new System.Drawing.Point(16, 54);
            this.m_txtItemName.Name = "m_txtItemName";
            this.m_txtItemName.ReadOnly = true;
            this.m_txtItemName.Size = new System.Drawing.Size(206, 21);
            this.m_txtItemName.TabIndex = 0;
            this.m_txtItemName.TabStop = false;
            // 
            // m_lbQtyInStock
            // 
            this.m_lbQtyInStock.BackColor = System.Drawing.Color.White;
            this.m_lbQtyInStock.Location = new System.Drawing.Point(16, 92);
            this.m_lbQtyInStock.Name = "m_lbQtyInStock";
            this.m_lbQtyInStock.Size = new System.Drawing.Size(100, 20);
            this.m_lbQtyInStock.Text = "Qty in Stock:";
            // 
            // m_txtQtyInStock
            // 
            this.m_txtQtyInStock.BackColor = System.Drawing.Color.White;
            this.m_txtQtyInStock.Enabled = false;
            this.m_txtQtyInStock.Location = new System.Drawing.Point(122, 92);
            this.m_txtQtyInStock.Name = "m_txtQtyInStock";
            this.m_txtQtyInStock.ReadOnly = true;
            this.m_txtQtyInStock.Size = new System.Drawing.Size(100, 21);
            this.m_txtQtyInStock.TabIndex = 0;
            this.m_txtQtyInStock.TabStop = false;
            // 
            // m_lbTrxnQty
            // 
            this.m_lbTrxnQty.BackColor = System.Drawing.Color.White;
            this.m_lbTrxnQty.Location = new System.Drawing.Point(16, 131);
            this.m_lbTrxnQty.Name = "m_lbTrxnQty";
            this.m_lbTrxnQty.Size = new System.Drawing.Size(85, 21);
            this.m_lbTrxnQty.Text = "Trxn Qty:";
            // 
            // m_txtTrxnQty
            // 
            this.m_txtTrxnQty.BackColor = System.Drawing.Color.White;
            this.m_txtTrxnQty.Location = new System.Drawing.Point(122, 131);
            this.m_txtTrxnQty.Name = "m_txtTrxnQty";
            this.m_txtTrxnQty.Size = new System.Drawing.Size(100, 21);
            this.m_txtTrxnQty.TabIndex = 1;
            this.m_txtTrxnQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnQtyKeyPress);
            this.m_txtTrxnQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnQtyKeyDown);
            // 
            // m_mbOk
            // 
            this.m_mbOk.BackColor = System.Drawing.Color.White;
            this.m_mbOk.BackDownColor = System.Drawing.Color.Black;
            this.m_mbOk.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbOk.ForeDownColor = System.Drawing.Color.White;
            this.m_mbOk.IconMargin = 3;
            this.m_mbOk.IconShift = false;
            this.m_mbOk.IconTextSpace = 3;
            this.m_mbOk.Location = new System.Drawing.Point(192, 230);
            this.m_mbOk.Name = "m_mbOk";
            this.m_mbOk.Picture = MobileTech.Windows.UI.ImageKeys.Done_Small;
            this.m_mbOk.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Done_SmallDisabled;
            this.m_mbOk.PictureFocus = MobileTech.Windows.UI.ImageKeys.Done_SmallFocus;
            this.m_mbOk.ShowBorder = true;
            this.m_mbOk.ShowFocusBorder = true;
            this.m_mbOk.Size = new System.Drawing.Size(48, 64);
            this.m_mbOk.TabIndex = 2;
            this.m_mbOk.Text = "Done";
            this.m_mbOk.TextShift = false;
            this.m_mbOk.TransparentIcon = true;
            this.m_mbOk.TransparentImage = true;
            this.m_mbOk.Click += new System.EventHandler(this.OnOkClick);
            // 
            // OversellWarningView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_mbOk);
            this.Controls.Add(this.m_txtTrxnQty);
            this.Controls.Add(this.m_lbTrxnQty);
            this.Controls.Add(this.m_txtQtyInStock);
            this.Controls.Add(this.m_lbQtyInStock);
            this.Controls.Add(this.m_txtItemName);
            this.Controls.Add(this.m_txtItemNumber);
            this.Controls.Add(this.m_lbItemNumber);
            this.Name = "OversellWarningView";
            this.Text = "9320 - Oversell Warning";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OversellWarningView_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lbItemNumber;
        private System.Windows.Forms.TextBox m_txtItemNumber;
        private System.Windows.Forms.TextBox m_txtItemName;
        private System.Windows.Forms.Label m_lbQtyInStock;
        private System.Windows.Forms.TextBox m_txtQtyInStock;
        private System.Windows.Forms.Label m_lbTrxnQty;
        private System.Windows.Forms.TextBox m_txtTrxnQty;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbOk;
    }
}
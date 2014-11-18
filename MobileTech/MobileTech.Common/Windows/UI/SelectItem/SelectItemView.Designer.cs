namespace MobileTech.Windows.UI.SelectItem
{
    partial class SelectItemView
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
            this.m_txItemNumber = new System.Windows.Forms.TextBox();
            this.m_lbItemNumber = new System.Windows.Forms.Label();
            this.m_txItemName = new System.Windows.Forms.TextBox();
            this.m_lbQuantity = new System.Windows.Forms.Label();
            this.m_txQuantity = new System.Windows.Forms.TextBox();
            this.m_lbTruckStock = new System.Windows.Forms.Label();
            this.m_txTruckStock = new System.Windows.Forms.TextBox();
            this.m_lbAllowance = new System.Windows.Forms.Label();
            this.m_txtAllowance = new System.Windows.Forms.TextBox();
            this.m_mbFind = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_mbDone = new MobileTech.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_txItemNumber
            // 
            this.m_txItemNumber.BackColor = System.Drawing.Color.White;
            this.m_txItemNumber.Location = new System.Drawing.Point(126, 21);
            this.m_txItemNumber.Name = "m_txItemNumber";
            this.m_txItemNumber.Size = new System.Drawing.Size(100, 21);
            this.m_txItemNumber.TabIndex = 1;
            this.m_txItemNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnEnterPress);
            this.m_txItemNumber.TextChanged += new System.EventHandler(this.OnItemTextChanged);
            this.m_txItemNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnItemNumberKeyDown);
            // 
            // m_lbItemNumber
            // 
            this.m_lbItemNumber.BackColor = System.Drawing.Color.White;
            this.m_lbItemNumber.Location = new System.Drawing.Point(13, 21);
            this.m_lbItemNumber.Name = "m_lbItemNumber";
            this.m_lbItemNumber.Size = new System.Drawing.Size(89, 20);
            this.m_lbItemNumber.Text = "Item Number";
            // 
            // m_txItemName
            // 
            this.m_txItemName.BackColor = System.Drawing.Color.White;
            this.m_txItemName.Enabled = false;
            this.m_txItemName.Location = new System.Drawing.Point(13, 47);
            this.m_txItemName.Name = "m_txItemName";
            this.m_txItemName.Size = new System.Drawing.Size(213, 21);
            this.m_txItemName.TabIndex = 2;
            // 
            // m_lbQuantity
            // 
            this.m_lbQuantity.BackColor = System.Drawing.Color.White;
            this.m_lbQuantity.Location = new System.Drawing.Point(13, 76);
            this.m_lbQuantity.Name = "m_lbQuantity";
            this.m_lbQuantity.Size = new System.Drawing.Size(100, 20);
            this.m_lbQuantity.Text = "Quantity";
            // 
            // m_txQuantity
            // 
            this.m_txQuantity.BackColor = System.Drawing.Color.White;
            this.m_txQuantity.Enabled = false;
            this.m_txQuantity.Location = new System.Drawing.Point(126, 75);
            this.m_txQuantity.Name = "m_txQuantity";
            this.m_txQuantity.Size = new System.Drawing.Size(100, 21);
            this.m_txQuantity.TabIndex = 3;
            this.m_txQuantity.GotFocus += new System.EventHandler(this.OnQuantityFocus);
            this.m_txQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnEnterPress);
            this.m_txQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnQuantityKeyDown);
            // 
            // m_lbTruckStock
            // 
            this.m_lbTruckStock.BackColor = System.Drawing.Color.White;
            this.m_lbTruckStock.Location = new System.Drawing.Point(13, 103);
            this.m_lbTruckStock.Name = "m_lbTruckStock";
            this.m_lbTruckStock.Size = new System.Drawing.Size(100, 20);
            this.m_lbTruckStock.Text = "Truck Stock";
            this.m_lbTruckStock.Visible = false;
            // 
            // m_txTruckStock
            // 
            this.m_txTruckStock.BackColor = System.Drawing.Color.White;
            this.m_txTruckStock.Enabled = false;
            this.m_txTruckStock.Location = new System.Drawing.Point(126, 102);
            this.m_txTruckStock.Name = "m_txTruckStock";
            this.m_txTruckStock.Size = new System.Drawing.Size(100, 21);
            this.m_txTruckStock.TabIndex = 4;
            this.m_txTruckStock.Visible = false;
            // 
            // m_lbAllowance
            // 
            this.m_lbAllowance.BackColor = System.Drawing.Color.White;
            this.m_lbAllowance.Location = new System.Drawing.Point(13, 130);
            this.m_lbAllowance.Name = "m_lbAllowance";
            this.m_lbAllowance.Size = new System.Drawing.Size(100, 20);
            this.m_lbAllowance.Text = "Allowance";
            this.m_lbAllowance.Visible = false;
            // 
            // m_txtAllowance
            // 
            this.m_txtAllowance.BackColor = System.Drawing.Color.White;
            this.m_txtAllowance.Enabled = false;
            this.m_txtAllowance.Location = new System.Drawing.Point(126, 129);
            this.m_txtAllowance.Name = "m_txtAllowance";
            this.m_txtAllowance.Size = new System.Drawing.Size(100, 21);
            this.m_txtAllowance.TabIndex = 5;
            this.m_txtAllowance.Visible = false;
            // 
            // m_mbFind
            // 
            this.m_mbFind.BackColor = System.Drawing.Color.White;
            this.m_mbFind.BackDownColor = System.Drawing.Color.Black;
            this.m_mbFind.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.m_mbFind.ForeDownColor = System.Drawing.Color.White;
            this.m_mbFind.IconMargin = 3;
            this.m_mbFind.IconShift = false;
            this.m_mbFind.IconTextSpace = 3;
            this.m_mbFind.Location = new System.Drawing.Point(0, 230);
            this.m_mbFind.Name = "m_mbFind";
            this.m_mbFind.Picture = MobileTech.Windows.UI.ImageKeys.Detail_Small;
            this.m_mbFind.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Detail_SmallDisabled;
            this.m_mbFind.PictureFocus = MobileTech.Windows.UI.ImageKeys.Detail_SmallFocus;
            this.m_mbFind.ShowBorder = true;
            this.m_mbFind.ShowFocusBorder = true;
            this.m_mbFind.Size = new System.Drawing.Size(48, 64);
            this.m_mbFind.TabIndex = 8;
            this.m_mbFind.Text = "Find";
            this.m_mbFind.TextShift = false;
            this.m_mbFind.TransparentIcon = true;
            this.m_mbFind.TransparentImage = true;
            this.m_mbFind.Click += new System.EventHandler(this.OnFindClick);
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
            this.m_mbDone.TabIndex = 12;
            this.m_mbDone.Text = "Done";
            this.m_mbDone.TextShift = false;
            this.m_mbDone.TransparentIcon = true;
            this.m_mbDone.TransparentImage = true;
            this.m_mbDone.Click += new System.EventHandler(this.OnExitClick);
            // 
            // SelectItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_mbDone);
            this.Controls.Add(this.m_mbFind);
            this.Controls.Add(this.m_txtAllowance);
            this.Controls.Add(this.m_lbAllowance);
            this.Controls.Add(this.m_txTruckStock);
            this.Controls.Add(this.m_lbTruckStock);
            this.Controls.Add(this.m_txQuantity);
            this.Controls.Add(this.m_lbQuantity);
            this.Controls.Add(this.m_txItemName);
            this.Controls.Add(this.m_lbItemNumber);
            this.Controls.Add(this.m_txItemNumber);
            this.Name = "SelectItemView";
            this.Text = "9030 - Select Item";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox m_txItemNumber;
        private System.Windows.Forms.Label m_lbItemNumber;
        private System.Windows.Forms.TextBox m_txItemName;
        private System.Windows.Forms.Label m_lbQuantity;
        private System.Windows.Forms.TextBox m_txQuantity;
        private System.Windows.Forms.Label m_lbTruckStock;
        private System.Windows.Forms.TextBox m_txTruckStock;
        private System.Windows.Forms.Label m_lbAllowance;
        private System.Windows.Forms.TextBox m_txtAllowance;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbFind;
        private MobileTech.Windows.UI.Controls.MenuButton m_mbDone;
    }
}
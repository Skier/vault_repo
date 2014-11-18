namespace Dalworth.Windows.ServiceVisit.VisitReceipt
{
    partial class VisitReceiptView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_lblTaxAmount = new System.Windows.Forms.Label();
            this.m_lblTaskType = new System.Windows.Forms.Label();
            this.m_lblCustomerName = new System.Windows.Forms.Label();
            this.m_lblTaskNumber = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_lblTotal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_lblService = new System.Windows.Forms.Label();
            this.m_curService = new Dalworth.Controls.CurrencyEdit();
            this.m_btnPayByCC = new Dalworth.Controls.Menu.MenuButton();
            this.m_btnPayByCheck = new Dalworth.Controls.Menu.MenuButton();
            this.m_btnPayByCash = new Dalworth.Controls.Menu.MenuButton();
            this.SuspendLayout();
            // 
            // m_lblTaxAmount
            // 
            this.m_lblTaxAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTaxAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTaxAmount.Location = new System.Drawing.Point(67, 78);
            this.m_lblTaxAmount.Name = "m_lblTaxAmount";
            this.m_lblTaxAmount.Size = new System.Drawing.Size(170, 20);
            this.m_lblTaxAmount.Text = "$0.00";
            this.m_lblTaxAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblTaskType
            // 
            this.m_lblTaskType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTaskType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTaskType.Location = new System.Drawing.Point(71, 37);
            this.m_lblTaskType.Name = "m_lblTaskType";
            this.m_lblTaskType.Size = new System.Drawing.Size(166, 20);
            this.m_lblTaskType.Text = "Rug Pickup";
            this.m_lblTaskType.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblCustomerName
            // 
            this.m_lblCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblCustomerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblCustomerName.Location = new System.Drawing.Point(71, 20);
            this.m_lblCustomerName.Name = "m_lblCustomerName";
            this.m_lblCustomerName.Size = new System.Drawing.Size(166, 20);
            this.m_lblCustomerName.Text = "Love, Rob";
            this.m_lblCustomerName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblTaskNumber
            // 
            this.m_lblTaskNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTaskNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTaskNumber.Location = new System.Drawing.Point(71, 3);
            this.m_lblTaskNumber.Name = "m_lblTaskNumber";
            this.m_lblTaskNumber.Size = new System.Drawing.Size(166, 20);
            this.m_lblTaskNumber.Text = "1001";
            this.m_lblTaskNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 20);
            this.label5.Text = "Task Type";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 20);
            this.label4.Text = "Customer";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 20);
            this.label6.Text = "TKT";
            // 
            // m_lblTotal
            // 
            this.m_lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTotal.Location = new System.Drawing.Point(67, 95);
            this.m_lblTotal.Name = "m_lblTotal";
            this.m_lblTotal.Size = new System.Drawing.Size(170, 20);
            this.m_lblTotal.Text = "$50.11";
            this.m_lblTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 20);
            this.label3.Text = "Total";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.Text = "Tax, $";
            // 
            // m_lblService
            // 
            this.m_lblService.ForeColor = System.Drawing.Color.Red;
            this.m_lblService.Location = new System.Drawing.Point(3, 57);
            this.m_lblService.Name = "m_lblService";
            this.m_lblService.Size = new System.Drawing.Size(84, 20);
            this.m_lblService.Text = "Service, $";
            // 
            // m_curService
            // 
            this.m_curService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_curService.Location = new System.Drawing.Point(93, 55);
            this.m_curService.Name = "m_curService";
            this.m_curService.Size = new System.Drawing.Size(144, 20);
            this.m_curService.TabIndex = 22;
            // 
            // m_btnPayByCC
            // 
            this.m_btnPayByCC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPayByCC.Location = new System.Drawing.Point(87, 201);
            this.m_btnPayByCC.Name = "m_btnPayByCC";
            this.m_btnPayByCC.Size = new System.Drawing.Size(48, 64);
            this.m_btnPayByCC.TabIndex = 23;
            this.m_btnPayByCC.Text = "CC";
            // 
            // m_btnPayByCheck
            // 
            this.m_btnPayByCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPayByCheck.Location = new System.Drawing.Point(138, 201);
            this.m_btnPayByCheck.Name = "m_btnPayByCheck";
            this.m_btnPayByCheck.Size = new System.Drawing.Size(48, 64);
            this.m_btnPayByCheck.TabIndex = 24;
            this.m_btnPayByCheck.Text = "Check";
            // 
            // m_btnPayByCash
            // 
            this.m_btnPayByCash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPayByCash.Location = new System.Drawing.Point(189, 201);
            this.m_btnPayByCash.Name = "m_btnPayByCash";
            this.m_btnPayByCash.Size = new System.Drawing.Size(48, 64);
            this.m_btnPayByCash.TabIndex = 25;
            this.m_btnPayByCash.Text = "Cash";
            // 
            // VisitReceiptView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_btnPayByCash);
            this.Controls.Add(this.m_btnPayByCheck);
            this.Controls.Add(this.m_btnPayByCC);
            this.Controls.Add(this.m_curService);
            this.Controls.Add(this.m_lblTaxAmount);
            this.Controls.Add(this.m_lblTaskType);
            this.Controls.Add(this.m_lblCustomerName);
            this.Controls.Add(this.m_lblTaskNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_lblTotal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_lblService);
            this.Name = "VisitReceiptView";
            this.Size = new System.Drawing.Size(240, 268);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label m_lblTaxAmount;
        internal System.Windows.Forms.Label m_lblTaskType;
        internal System.Windows.Forms.Label m_lblCustomerName;
        internal System.Windows.Forms.Label m_lblTaskNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label m_lblTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        internal Dalworth.Controls.CurrencyEdit m_curService;
        internal Dalworth.Controls.Menu.MenuButton m_btnPayByCC;
        internal Dalworth.Controls.Menu.MenuButton m_btnPayByCheck;
        internal Dalworth.Controls.Menu.MenuButton m_btnPayByCash;
        internal System.Windows.Forms.Label m_lblService;
    }
}

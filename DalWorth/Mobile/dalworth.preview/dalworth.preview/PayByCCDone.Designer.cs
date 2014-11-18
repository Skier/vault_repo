namespace dalworth.preview
{
    partial class PayByCCDone
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.m_menuDone = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_lblCCNumber = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_lblAmount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            this.mainMenu1.MenuItems.Add(this.m_menuDone);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Menu";
            // 
            // m_menuDone
            // 
            this.m_menuDone.Text = "Done";
            this.m_menuDone.Click += new System.EventHandler(this.OnDoneClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 35);
            this.label1.Text = "CC payment has been successfully processed";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.Text = "CC Number";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 20);
            this.label3.Text = "Confirmation Number";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.Text = "Amount";
            // 
            // m_lblCCNumber
            // 
            this.m_lblCCNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblCCNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblCCNumber.Location = new System.Drawing.Point(82, 39);
            this.m_lblCCNumber.Name = "m_lblCCNumber";
            this.m_lblCCNumber.Size = new System.Drawing.Size(155, 20);
            this.m_lblCCNumber.Text = "2323344";
            this.m_lblCCNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(132, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 20);
            this.label6.Text = "782366018659";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblAmount
            // 
            this.m_lblAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblAmount.Location = new System.Drawing.Point(70, 79);
            this.m_lblAmount.Name = "m_lblAmount";
            this.m_lblAmount.Size = new System.Drawing.Size(167, 20);
            this.m_lblAmount.Text = "$560.12";
            this.m_lblAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // PayByCCDone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.m_lblAmount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_lblCCNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "PayByCCDone";
            this.Text = "0263 Pay By CC Done";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem m_menuDone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label m_lblCCNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label m_lblAmount;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}
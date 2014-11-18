namespace QuickBooksAgent.Windows.UI.Banking.Menu
{
    partial class BankingMenuView
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
            this.m_mbWriteCheck = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbCreditCard = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_mbWriteCheck
            // 
            this.m_mbWriteCheck.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.m_mbWriteCheck.Location = new System.Drawing.Point(5, 5);
            this.m_mbWriteCheck.Name = "m_mbWriteCheck";
            this.m_mbWriteCheck.Picture = QuickBooksAgent.Windows.UI.ImageKeys.WriteCheck;
            this.m_mbWriteCheck.ShowBorder = true;
            this.m_mbWriteCheck.Size = new System.Drawing.Size(115, 82);
            this.m_mbWriteCheck.TabIndex = 2;
            this.m_mbWriteCheck.Tag = "";
            this.m_mbWriteCheck.Text = "Write Check";
            // 
            // m_mbCreditCard
            // 
            this.m_mbCreditCard.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.m_mbCreditCard.Location = new System.Drawing.Point(122, 5);
            this.m_mbCreditCard.Name = "m_mbCreditCard";
            this.m_mbCreditCard.Picture = QuickBooksAgent.Windows.UI.ImageKeys.CreditCards;
            this.m_mbCreditCard.ShowBorder = true;
            this.m_mbCreditCard.Size = new System.Drawing.Size(115, 82);
            this.m_mbCreditCard.TabIndex = 3;
            this.m_mbCreditCard.Tag = "";
            this.m_mbCreditCard.Text = "Enter CC Charges";
            // 
            // BankingMenuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_mbCreditCard);
            this.Controls.Add(this.m_mbWriteCheck);
            this.Name = "BankingMenuView";
            this.Size = new System.Drawing.Size(240, 294);
            this.ResumeLayout(false);

        }

        #endregion

        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbWriteCheck;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbCreditCard;
    }
}

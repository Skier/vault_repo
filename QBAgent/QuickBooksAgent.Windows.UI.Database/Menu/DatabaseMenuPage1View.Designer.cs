using System.Drawing;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Menu
{
    partial class DatabaseMenuPage1View
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
            this.m_mbBanking = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbCustomers = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbEmployees = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbNext = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_mbBanking
            // 
            this.m_mbBanking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_mbBanking.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbBanking.Location = new System.Drawing.Point(122, 5);
            this.m_mbBanking.Name = "m_mbBanking";
            this.m_mbBanking.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Banking;
            this.m_mbBanking.ShowBorder = true;
            this.m_mbBanking.Size = new System.Drawing.Size(115, 82);
            this.m_mbBanking.TabIndex = 4;
            this.m_mbBanking.Tag = "";
            this.m_mbBanking.Text = "Banking";
            // 
            // m_mbCustomers
            // 
            this.m_mbCustomers.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbCustomers.Location = new System.Drawing.Point(5, 5);
            this.m_mbCustomers.Name = "m_mbCustomers";
            this.m_mbCustomers.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Contacts;
            this.m_mbCustomers.ShowBorder = true;
            this.m_mbCustomers.Size = new System.Drawing.Size(115, 82);
            this.m_mbCustomers.TabIndex = 0;
            this.m_mbCustomers.Tag = "";
            this.m_mbCustomers.Text = "Customers";
            // 
            // m_mbEmployees
            // 
            this.m_mbEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_mbEmployees.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbEmployees.Location = new System.Drawing.Point(5, 210);
            this.m_mbEmployees.Name = "m_mbEmployees";
            this.m_mbEmployees.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Employee;
            this.m_mbEmployees.ShowBorder = true;
            this.m_mbEmployees.Size = new System.Drawing.Size(115, 82);
            this.m_mbEmployees.TabIndex = 2;
            this.m_mbEmployees.Tag = "";
            this.m_mbEmployees.Text = "Employees";
            // 
            // m_mbNext
            // 
            this.m_mbNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_mbNext.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbNext.Location = new System.Drawing.Point(122, 210);
            this.m_mbNext.Name = "m_mbNext";
            this.m_mbNext.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Forward;
            this.m_mbNext.ShowBorder = true;
            this.m_mbNext.Size = new System.Drawing.Size(115, 82);
            this.m_mbNext.TabIndex = 3;
            this.m_mbNext.Tag = "";
            this.m_mbNext.Text = "Next";
            // 
            // DatabaseMenuPage1View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_mbNext);
            this.Controls.Add(this.m_mbEmployees);
            this.Controls.Add(this.m_mbBanking);
            this.Controls.Add(this.m_mbCustomers);
            this.Name = "DatabaseMenuPage1View";
            this.Size = new System.Drawing.Size(240, 294);
            this.ResumeLayout(false);

        }

        #endregion

        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbCustomers;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbEmployees;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbNext;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbBanking;

    }
}
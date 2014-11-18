using System.Drawing;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Menu
{
    partial class DatabaseMenuPage2View
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
            this.m_mbItems = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbNext = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbAccounts = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbVendors = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_mbItems
            // 
            this.m_mbItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_mbItems.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbItems.Location = new System.Drawing.Point(122, 5);
            this.m_mbItems.Name = "m_mbItems";
            this.m_mbItems.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Items;
            this.m_mbItems.ShowBorder = true;
            this.m_mbItems.Size = new System.Drawing.Size(115, 82);
            this.m_mbItems.TabIndex = 1;
            this.m_mbItems.Tag = "";
            this.m_mbItems.Text = "Items";
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
            // m_mbAccounts
            // 
            this.m_mbAccounts.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.m_mbAccounts.Location = new System.Drawing.Point(5, 5);
            this.m_mbAccounts.Name = "m_mbAccounts";
            this.m_mbAccounts.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Accounts;
            this.m_mbAccounts.ShowBorder = true;
            this.m_mbAccounts.Size = new System.Drawing.Size(115, 82);
            this.m_mbAccounts.TabIndex = 5;
            this.m_mbAccounts.Tag = "";
            this.m_mbAccounts.Text = "Accounts";
            // 
            // m_mbVendors
            // 
            this.m_mbVendors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_mbVendors.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbVendors.Location = new System.Drawing.Point(5, 210);
            this.m_mbVendors.Name = "m_mbVendors";
            this.m_mbVendors.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Vendors;
            this.m_mbVendors.ShowBorder = true;
            this.m_mbVendors.Size = new System.Drawing.Size(115, 82);
            this.m_mbVendors.TabIndex = 6;
            this.m_mbVendors.Tag = "";
            this.m_mbVendors.Text = "Vendors";
            // 
            // DatabaseMenuPage2View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_mbVendors);
            this.Controls.Add(this.m_mbNext);
            this.Controls.Add(this.m_mbAccounts);
            this.Controls.Add(this.m_mbItems);
            this.Name = "DatabaseMenuPage2View";
            this.Size = new System.Drawing.Size(240, 294);
            this.ResumeLayout(false);

        }

        #endregion

        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbItems;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbNext;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbAccounts;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbVendors;

    }
}
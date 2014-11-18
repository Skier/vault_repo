using System.Drawing;

namespace QuickBooksAgent.Windows.UI.Menu.MainMenu
{
    partial class MainMenuView
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
            this.m_mbManageTime = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbSetup = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbSynchronize = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbCustomer = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_menuManager = new QuickBooksAgent.Windows.UI.Controls.Menu.MenuManager();
            this.m_mbBanking = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbAccounts = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbVendors = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbEmployees = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbItems = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_menuManager.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_mbManageTime
            // 
            this.m_mbManageTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbManageTime.Location = new System.Drawing.Point(3, 3);
            this.m_mbManageTime.Name = "m_mbManageTime";
            this.m_mbManageTime.Picture = QuickBooksAgent.Windows.UI.ImageKeys.ManageTime;
            this.m_mbManageTime.ShowBorder = true;
            this.m_mbManageTime.Size = new System.Drawing.Size(107, 82);
            this.m_mbManageTime.TabIndex = 0;
            this.m_mbManageTime.Tag = "";
            this.m_mbManageTime.Text = "Manage Time";
            // 
            // m_mbSetup
            // 
            this.m_mbSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_mbSetup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbSetup.Location = new System.Drawing.Point(116, 71);
            this.m_mbSetup.Name = "m_mbSetup";
            this.m_mbSetup.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Setup;
            this.m_mbSetup.ShowBorder = true;
            this.m_mbSetup.Size = new System.Drawing.Size(115, 82);
            this.m_mbSetup.TabIndex = 7;
            this.m_mbSetup.Tag = "";
            this.m_mbSetup.Text = "Setup";
            // 
            // m_mbSynchronize
            // 
            this.m_mbSynchronize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_mbSynchronize.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbSynchronize.Location = new System.Drawing.Point(3, 91);
            this.m_mbSynchronize.Name = "m_mbSynchronize";
            this.m_mbSynchronize.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Synchronize;
            this.m_mbSynchronize.ShowBorder = true;
            this.m_mbSynchronize.Size = new System.Drawing.Size(115, 82);
            this.m_mbSynchronize.TabIndex = 8;
            this.m_mbSynchronize.Tag = "";
            this.m_mbSynchronize.Text = "Synchronize";
            // 
            // m_mbCustomer
            // 
            this.m_mbCustomer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbCustomer.Location = new System.Drawing.Point(116, 3);
            this.m_mbCustomer.Name = "m_mbCustomer";
            this.m_mbCustomer.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Contacts;
            this.m_mbCustomer.ShowBorder = true;
            this.m_mbCustomer.Size = new System.Drawing.Size(115, 82);
            this.m_mbCustomer.TabIndex = 1;
            this.m_mbCustomer.Tag = "";
            this.m_mbCustomer.Text = "Customers";
            // 
            // m_menuManager
            // 
            this.m_menuManager.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_menuManager.Controls.Add(this.m_mbCustomer);
            this.m_menuManager.Controls.Add(this.m_mbManageTime);
            this.m_menuManager.Controls.Add(this.m_mbBanking);
            this.m_menuManager.Controls.Add(this.m_mbAccounts);
            this.m_menuManager.Controls.Add(this.m_mbVendors);
            this.m_menuManager.Controls.Add(this.m_mbEmployees);
            this.m_menuManager.Controls.Add(this.m_mbItems);
            this.m_menuManager.Controls.Add(this.m_mbSetup);
            this.m_menuManager.Controls.Add(this.m_mbSynchronize);
            this.m_menuManager.HorisontalInterval = 2;
            this.m_menuManager.Location = new System.Drawing.Point(3, 3);
            this.m_menuManager.Name = "m_menuManager";
            this.m_menuManager.Size = new System.Drawing.Size(234, 288);
            this.m_menuManager.VerticalInterval = 2;
            // 
            // m_mbBanking
            // 
            this.m_mbBanking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_mbBanking.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbBanking.Location = new System.Drawing.Point(3, 24);
            this.m_mbBanking.Name = "m_mbBanking";
            this.m_mbBanking.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Banking;
            this.m_mbBanking.ShowBorder = true;
            this.m_mbBanking.Size = new System.Drawing.Size(115, 82);
            this.m_mbBanking.TabIndex = 2;
            this.m_mbBanking.Tag = "";
            this.m_mbBanking.Text = "Banking";
            // 
            // m_mbAccounts
            // 
            this.m_mbAccounts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbAccounts.Location = new System.Drawing.Point(116, 24);
            this.m_mbAccounts.Name = "m_mbAccounts";
            this.m_mbAccounts.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Accounts;
            this.m_mbAccounts.ShowBorder = true;
            this.m_mbAccounts.Size = new System.Drawing.Size(115, 82);
            this.m_mbAccounts.TabIndex = 3;
            this.m_mbAccounts.Tag = "";
            this.m_mbAccounts.Text = "Accounts";
            // 
            // m_mbVendors
            // 
            this.m_mbVendors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_mbVendors.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbVendors.Location = new System.Drawing.Point(116, 50);
            this.m_mbVendors.Name = "m_mbVendors";
            this.m_mbVendors.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Vendors;
            this.m_mbVendors.ShowBorder = true;
            this.m_mbVendors.Size = new System.Drawing.Size(115, 82);
            this.m_mbVendors.TabIndex = 5;
            this.m_mbVendors.Tag = "";
            this.m_mbVendors.Text = "Vendors";
            // 
            // m_mbEmployees
            // 
            this.m_mbEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_mbEmployees.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbEmployees.Location = new System.Drawing.Point(3, 50);
            this.m_mbEmployees.Name = "m_mbEmployees";
            this.m_mbEmployees.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Employee;
            this.m_mbEmployees.ShowBorder = true;
            this.m_mbEmployees.Size = new System.Drawing.Size(115, 82);
            this.m_mbEmployees.TabIndex = 4;
            this.m_mbEmployees.Tag = "";
            this.m_mbEmployees.Text = "Employees";
            // 
            // m_mbItems
            // 
            this.m_mbItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_mbItems.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_mbItems.Location = new System.Drawing.Point(3, 71);
            this.m_mbItems.Name = "m_mbItems";
            this.m_mbItems.Picture = QuickBooksAgent.Windows.UI.ImageKeys.Items;
            this.m_mbItems.ShowBorder = true;
            this.m_mbItems.Size = new System.Drawing.Size(115, 82);
            this.m_mbItems.TabIndex = 6;
            this.m_mbItems.Tag = "";
            this.m_mbItems.Text = "Items";
            // 
            // MainMenuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_menuManager);
            this.Name = "MainMenuView";
            this.Size = new System.Drawing.Size(240, 294);
            this.m_menuManager.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbCustomer;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbSynchronize;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbSetup;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbManageTime;
        internal QuickBooksAgent.Windows.UI.Controls.Menu.MenuManager m_menuManager;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbBanking;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbAccounts;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbEmployees;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbVendors;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbItems;

    }
}
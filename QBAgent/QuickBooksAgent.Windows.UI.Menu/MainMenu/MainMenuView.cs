using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.Menu.MainMenu
{
    public partial class MainMenuView : BaseControl
    {

        internal MenuItem m_menuSynchronize = new MenuItem(); 

        //Database
        internal MenuItem m_menuCustomers = new MenuItem();
        internal MenuItem m_menuEmployee = new MenuItem();
        internal MenuItem m_menuVendors = new MenuItem();
        internal MenuItem m_menuItems = new MenuItem();
        internal MenuItem m_menuAccounts = new MenuItem();

        //Banking
        internal MenuItem m_menuBanking = new MenuItem();
        internal MenuItem m_menuBankingAddCheck = new MenuItem();
        internal MenuItem m_menuBankingCreditCard = new MenuItem();
        
        //Manage Time
        internal MenuItem m_menuManageTime = new MenuItem();
        internal MenuItem m_menuWeeklyTimeSheet = new MenuItem();
        internal MenuItem m_menuSingleTimeEntry = new MenuItem();

        //Setup
        internal MenuItem m_menuSetup = new MenuItem();
        internal MenuItem m_menuSetupConnection = new MenuItem();
        internal MenuItem m_menuRegister = new MenuItem();
        internal MenuItem m_menuSetupApplication = new MenuItem();
        internal MenuItem m_menuAbout = new MenuItem();

        public MainMenuView()
        {
            InitializeComponent();

            m_menuSetupConnection.Enabled = false;
            
            m_menuSynchronize.Text = "Synchronize";
            m_menuManageTime.Text = "Manage Time";
            m_menuSetup.Text = "Setup";
            m_menuCustomers.Text = "Customers";

            m_menuVendors.Text = "Vendors";
            m_menuEmployee.Text = "Employee";
            m_menuBanking.Text = "Banking";
            m_menuItems.Text = "Items";
            m_menuAccounts.Text = "Accounts";
            m_menuBankingCreditCard.Text = "Enter CC Charges";

            m_menuBanking.MenuItems.Add(m_menuBankingAddCheck);
            m_menuBanking.MenuItems.Add(m_menuBankingCreditCard);

            m_menuBankingAddCheck.Text = "Write Check";

            m_menuSingleTimeEntry.Text = "Single Time Entry";
            m_menuWeeklyTimeSheet.Text = "Weekly Time Sheet";

            m_menuManageTime.MenuItems.Add(m_menuSingleTimeEntry);
            m_menuManageTime.MenuItems.Add(m_menuWeeklyTimeSheet);


            m_menuSetupConnection.Text = "Connection";
            m_menuRegister.Text = "Register";
            m_menuSetupApplication.Text = "Application";
            m_menuAbout.Text = "About";

            //m_menuSetup.MenuItems.Add(m_menuSetupConnection);
            m_menuSetup.MenuItems.Add(m_menuRegister);
            m_menuSetup.MenuItems.Add(m_menuSetupApplication);
            m_menuSetup.MenuItems.Add(m_menuAbout);

            MenuItems.Add(m_menuManageTime);
            MenuItems.Add(m_menuCustomers);            
            MenuItems.Add(m_menuBanking);
            MenuItems.Add(m_menuAccounts);            
            MenuItems.Add(m_menuEmployee);
            MenuItems.Add(m_menuVendors);            
            MenuItems.Add(m_menuItems);                        
            MenuItems.Add(m_menuSetup);
            MenuItems.Add(m_menuSynchronize);                        
        }
        
        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            this.Text = "Main Menu - Q-Agent";
        }
    }
}
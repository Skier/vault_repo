using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Menu
{
    public partial class DatabaseMenuPage2View : BaseControl
    {
        internal MenuItem m_menuCustomers = new MenuItem();
        internal MenuItem m_menuBanking = new MenuItem();
        internal MenuItem m_menuEmployee = new MenuItem();
        internal MenuItem m_menuVendors = new MenuItem();
        internal MenuItem m_menuItems = new MenuItem();
        internal MenuItem m_menuAccounts = new MenuItem();

        internal MenuItem m_menuWriteCheck = new MenuItem();


        public DatabaseMenuPage2View()
        {
            InitializeComponent();

            m_menuVendors.Text = "Vendors";
            m_menuEmployee.Text = "Employee";
            m_menuBanking.Text = "Banking";
            m_menuCustomers.Text = "Customers";
            m_menuItems.Text = "Items";
            m_menuAccounts.Text = "Accounts";
            m_menuWriteCheck.Text = "Write Check";

            m_menuBanking.MenuItems.Add(m_menuWriteCheck);

            MenuItems.Add(m_menuCustomers);
            MenuItems.Add(m_menuBanking);
            MenuItems.Add(m_menuEmployee);
            MenuItems.Add(m_menuAccounts);
            MenuItems.Add(m_menuItems);
            MenuItems.Add(m_menuVendors);
        }

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "2/2 Database - Q-Agent";
        }

        protected override void OnInit()
        {
            base.OnInit();


            Joystick.Add(m_mbAccounts, m_mbItems, m_mbItems, m_mbVendors, m_mbVendors);
            Joystick.Add(m_mbItems, m_mbAccounts, m_mbAccounts, m_mbNext, m_mbNext);
            Joystick.Add(m_mbVendors, m_mbNext, m_mbNext, m_mbAccounts, m_mbAccounts);
            Joystick.Add(m_mbNext, m_mbVendors, m_mbVendors, m_mbItems, m_mbItems);
        }
    }
}
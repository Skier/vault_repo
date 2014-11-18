using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Accounts
{
    public partial class AccountsView : BaseControl
    {
        internal MenuItem m_menuManageChecks = new MenuItem();
        internal MenuItem m_menuManageCCCharges = new MenuItem();
        
        public AccountsView()
        {
            InitializeComponent();

            MenuItems.Add(m_menuManageChecks);
            MenuItems.Add(m_menuManageCCCharges);
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Accounts - Q-Agent";

            m_menuManageChecks.Text = "Manage Checks";
            m_menuManageCCCharges.Text = "Manage CC Charges";            
        }
    }
}

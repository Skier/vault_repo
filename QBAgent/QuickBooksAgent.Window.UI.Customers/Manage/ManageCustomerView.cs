using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.Customers.Manage
{
    public partial class ManageCustomerView : BaseControl
    {
        internal MenuItem m_menuAdd = new MenuItem();
        internal MenuItem m_menuDelete = new MenuItem();
        internal MenuItem m_menuEdit = new MenuItem();
        internal MenuItem m_menuUndo = new MenuItem();

        internal MenuItem m_menuSeparator = new MenuItem();
        internal MenuItem m_menuCall = new MenuItem();
        internal MenuItem m_menuSeparator2 = new MenuItem();

        internal MenuItem m_menuNewInvoice = new MenuItem();
        internal MenuItem m_menuShowInvoices = new MenuItem();

        internal Dictionary<MenuItem, string> m_phoneMenus = new Dictionary<MenuItem, string>();

        public ManageCustomerView()
        {
            InitializeComponent();

            MenuItems.Add(m_menuAdd);
            MenuItems.Add(m_menuDelete);
            MenuItems.Add(m_menuEdit);                        

            m_menuUndo.Enabled = false;
            m_menuDelete.Enabled = false;
            m_menuShowInvoices.Enabled = false;

            MenuItems.Add(m_menuUndo);
            MenuItems.Add(m_menuSeparator);
            MenuItems.Add(m_menuCall);
            MenuItems.Add(m_menuSeparator2);

            MenuItems.Add(m_menuNewInvoice);
            MenuItems.Add(m_menuShowInvoices);
        }

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            m_menuAdd.Text = "Add";
            m_menuDelete.Text = "Delete";
            m_menuEdit.Text = "Edit";
            m_menuUndo.Text = "Undo";

            m_menuSeparator.Text = "-";
            m_menuCall.Text = "Call";
            m_menuSeparator2.Text = "-";

            m_menuNewInvoice.Text = "Add new invoice";
            m_menuShowInvoices.Text = "Show invoices";

            this.Text = "Manage Customers - Q-Agent";
        }

        protected override void OnInit()
        {
            base.OnInit();

            Joystick.Add(m_txtSearch, m_table, m_table, m_table, m_table);
            Joystick.Add(m_table, m_txtSearch, m_txtSearch, m_txtSearch, m_txtSearch);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.CustomerOperations.InvoiceSelection
{
    public partial class InvoiceSelectionView : BaseControl
    {
        internal MenuItem m_menuDelete = new MenuItem();
        internal MenuItem m_menuSelect = new MenuItem();
        internal MenuItem m_menuCreate = new MenuItem();
        internal MenuItem m_menuSeparator = new MenuItem();
        internal MenuItem m_menuSend = new MenuItem();
        

        public InvoiceSelectionView()
        {
            InitializeComponent();

            MenuItems.Add(m_menuCreate);            
            MenuItems.Add(m_menuDelete);
            MenuItems.Add(m_menuSelect);
            MenuItems.Add(m_menuSeparator);
            MenuItems.Add(m_menuSend);

            m_menuDelete.Enabled = false;
            m_menuSelect.Enabled = false;
            m_menuSend.Enabled = false;
        }

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            m_menuCreate.Text = "Add";
            m_menuDelete.Text = "Delete";
            m_menuSelect.Text = "Edit";
            m_menuSeparator.Text = "-";
            m_menuSend.Text = "Send";
            
            Text = "Invoices - Q-Agent";
        }
    }
}
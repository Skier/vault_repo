using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Vendors
{
    public partial class VendorsView : BaseControl
    {
        internal MenuItem m_menuCall = new MenuItem();
        internal Dictionary<MenuItem, string> m_phoneMenus = new Dictionary<MenuItem, string>();
        
        public VendorsView()
        {
            InitializeComponent();

            MenuItems.Add(m_menuCall);
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Vendors - Q-Agent";
            m_menuCall.Text = "Call";
        }
    }
}

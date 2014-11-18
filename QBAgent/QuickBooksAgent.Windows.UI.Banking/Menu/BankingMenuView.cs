using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.Banking.Menu
{
    public partial class BankingMenuView : BaseControl
    {
        internal MenuItem m_menuWriteCheck = new MenuItem();
        internal MenuItem m_menuCreditCard = new MenuItem();
        
        public BankingMenuView()
        {
            InitializeComponent();

            MenuItems.Add(m_menuWriteCheck);
            MenuItems.Add(m_menuCreditCard);            
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);
            Text = "Banking - Q-Agent";

            m_menuWriteCheck.Text = m_mbWriteCheck.Text;
            m_menuCreditCard.Text = m_mbCreditCard.Text;
        }

        protected override void OnInit()
        {
            base.OnInit();

            Joystick.Add(m_mbWriteCheck, m_mbCreditCard, m_mbCreditCard, m_mbWriteCheck, m_mbWriteCheck);
            Joystick.Add(m_mbCreditCard, m_mbWriteCheck, m_mbWriteCheck, m_mbCreditCard, m_mbCreditCard);

        }
    }
}

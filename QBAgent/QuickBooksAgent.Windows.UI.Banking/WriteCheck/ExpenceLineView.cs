using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.Banking.WriteCheck
{
    public partial class ExpenceLineView : BaseControl
    {
        public ExpenceLineView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);
            Text = "Expence Line - Q-Agent";
        }

        protected override void OnInit()
        {
            base.OnInit();

            Joystick.Add(m_cmbAccount, m_txtMemo, m_cmbCustomer, m_txtMemo, m_cmbCustomer);
            Joystick.Add(m_cmbCustomer, m_cmbAccount, m_curAmount, m_cmbAccount, m_curAmount);
            Joystick.Add(m_curAmount, m_cmbCustomer, m_txtMemo, m_cmbCustomer, m_txtMemo);
            Joystick.Add(m_txtMemo, m_curAmount, m_cmbAccount, m_curAmount, m_cmbAccount);
        }
    }
}

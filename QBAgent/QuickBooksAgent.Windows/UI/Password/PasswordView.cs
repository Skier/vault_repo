using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.Password
{        
    public partial class PasswordView : BaseControl
    {
        public PasswordView()
        {
            InitializeComponent();
        }
        
        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            this.Text = "Authorization - Q-Agent";            
        }

        protected override void OnInit()
        {
            base.OnInit();

            //Joystick.Add(m_txtLogin, m_txtPassword, m_txtPassword, m_txtPassword, m_txtPassword);
            Joystick.Add(m_txtPassword, m_cmbTransactionHistory, m_cmbTransactionHistory, m_cmbTransactionHistory, m_cmbTransactionHistory);
            Joystick.Add(m_cmbTransactionHistory, m_txtPassword, m_txtPassword, m_txtPassword, m_txtPassword);

        }
    }
}


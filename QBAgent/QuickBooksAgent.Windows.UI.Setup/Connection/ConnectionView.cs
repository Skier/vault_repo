using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.Setup.Connection
{
    public partial class ConnectionView : QuickBooksAgent.Windows.UI.BaseControl
    {
        public ConnectionView()
        {
            InitializeComponent();
        }

        protected override void OnInit()
        {
            base.OnInit();

            Joystick.Add(m_chbSecure, m_chbSecure, m_chbSecure, m_defaultLogin, m_ticket);
            Joystick.Add(m_ticket, m_ticket, m_ticket, m_chbSecure, m_defaultLogin);
            Joystick.Add(m_defaultLogin, m_defaultLogin, m_defaultLogin, m_ticket, m_chbSecure);
        }

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Connection Setup - Q-Agent";
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.Setup.Application
{
    public partial class ApplicationView : BaseControl
    {
        public ApplicationView()
        {
            InitializeComponent();
        }

        protected override void OnInit()
        {
            base.OnInit();

            Joystick.Add(m_cmbUserType, m_tabs, m_cmbUser, m_tabs, m_chkUseUserIdentification);
            Joystick.Add(m_cmbUser, m_cmbUserType, m_chkUseUserIdentification, m_tabs, m_chkUseUserIdentification);
            Joystick.Add(m_chkUseUserIdentification, m_cmbUser, m_cmbTransactionHistory, m_cmbUserType, m_cmbTransactionHistory);
            Joystick.Add(m_cmbTransactionHistory, m_chkUseUserIdentification, m_chbTrace, m_chkUseUserIdentification, m_chbTrace);
            Joystick.Add(m_chbTrace, m_cmbTransactionHistory, m_tabs, m_cmbTransactionHistory, m_tabs);

            Joystick.Add(m_cmbSettingsType, m_tabs, m_cmbOutlookAccount, m_tabs, m_cmbOutlookAccount);
            Joystick.Add(m_cmbOutlookAccount, m_cmbSettingsType, m_txtSmtpServer, m_cmbSettingsType, m_txtSmtpServer);
            Joystick.Add(m_txtSmtpServer, m_cmbOutlookAccount, m_txtSmtpPort, m_cmbOutlookAccount, m_txtSmtpPort);
            Joystick.Add(m_txtSmtpPort, m_txtSmtpServer, m_txtEmailFrom, m_txtSmtpServer, m_txtEmailFrom);
            Joystick.Add(m_txtEmailFrom, m_txtSmtpPort, m_tabs, m_txtSmtpPort, m_tabs);
            
            Joystick.Add(m_tabs, 0, m_chbTrace, m_cmbUser);
            Joystick.Add(m_tabs, 1, m_txtEmailFrom, m_cmbSettingsType);
            
        }

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Application Setup - Q-Agent";
        }
    }
}

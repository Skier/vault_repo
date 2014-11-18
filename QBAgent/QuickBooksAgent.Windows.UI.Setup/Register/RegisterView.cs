using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.Setup.Register
{
    public partial class RegisterView : BaseControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        protected override void OnInit()
        {
            base.OnInit();
            Joystick.Add(m_txtLicense1, m_txtCompany, m_txtLicense2, m_txtCompany, m_txtFirstName);
            Joystick.Add(m_txtLicense2, m_txtLicense1, m_txtLicense3, m_txtCompany, m_txtFirstName);
            Joystick.Add(m_txtLicense3, m_txtLicense2, m_txtLicense4, m_txtCompany, m_txtFirstName);
            Joystick.Add(m_txtLicense4, m_txtLicense3, m_txtFirstName, m_txtCompany, m_txtFirstName);
            Joystick.Add(m_txtFirstName, m_txtLicense4, m_txtLastName, m_txtLicense1, m_txtLastName);
            Joystick.Add(m_txtLastName, m_txtFirstName, m_txtCompany, m_txtFirstName, m_txtCompany);
            Joystick.Add(m_txtCompany, m_txtLastName, m_txtLicense1, m_txtLastName, m_txtLicense1);
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);
            Text = "Register - Q-Agent";
        }
    }
}

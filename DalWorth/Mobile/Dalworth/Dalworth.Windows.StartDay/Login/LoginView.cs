using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Windows.StartDay.Login
{
    public partial class LoginView : BaseControl
    {
        public LoginView()
        {
            InitializeComponent();
        }


        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Start Day Login";
        }


        protected override void OnInit()
        {
            Joystick.Add(m_cmbTechnician, m_txtPassword, m_txtPassword, m_txtPassword, m_txtPassword);
            Joystick.Add(m_txtPassword, m_cmbTechnician, m_cmbTechnician, m_cmbTechnician, m_cmbTechnician);
        }
    }
}

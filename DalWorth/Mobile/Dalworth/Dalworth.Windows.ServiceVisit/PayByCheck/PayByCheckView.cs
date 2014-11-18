using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Windows.ServiceVisit.PayByCheck
{
    public partial class PayByCheckView : BaseControl
    {
        public PayByCheckView()
        {
            InitializeComponent();
        }


        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Pay By Check";
        }


        protected override void OnInit()
        {
            Joystick.Add(m_txtFirstName, m_tabs, m_txtLastName, m_tabs, m_txtLastName);
            Joystick.Add(m_txtLastName, m_txtFirstName, m_txtAddress, m_txtFirstName, m_txtAddress);
            Joystick.Add(m_txtAddress, m_txtLastName, m_txtCity, m_txtLastName, m_txtCity);
            Joystick.Add(m_txtCity, m_txtAddress, m_cmbState, m_txtAddress, m_cmbState);
            Joystick.Add(m_cmbState, m_txtCity, m_txtZip, m_txtCity, m_tabs);
            Joystick.Add(m_txtZip, m_cmbState, m_tabs, m_txtCity, m_tabs);


            Joystick.Add(m_cmbAccountType, m_tabs, m_txtCompany, m_tabs, m_txtCompany);
            Joystick.Add(m_txtCompany, m_cmbAccountType, m_txtBankName, m_cmbAccountType, m_txtBankName);
            Joystick.Add(m_txtBankName, m_txtCompany, m_txtAccountNumber, m_txtCompany, m_txtAccountNumber);
            Joystick.Add(m_txtAccountNumber, m_txtBankName, m_txtCheckNumber, m_txtBankName, m_txtCheckNumber);
            Joystick.Add(m_txtCheckNumber, m_txtAccountNumber, m_txtBankRouteNumber, m_txtAccountNumber, m_txtBankRouteNumber);
            Joystick.Add(m_txtBankRouteNumber, m_txtCheckNumber, m_tabs, m_txtCheckNumber, m_tabs);

            Joystick.Add(m_tabs, 0, m_cmbState, m_txtFirstName);
            Joystick.Add(m_tabs, 1, m_txtBankRouteNumber, m_txtCheckNumber);            
        }
    }
}

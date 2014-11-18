using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Windows.ServiceVisit.PayByCC
{
    public partial class PayByCCView : BaseControl
    {
        public PayByCCView()
        {
            InitializeComponent();
        }


        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Pay By CC";
        }


        protected override void OnInit()
        {
            Joystick.Add(m_txtFirstName, m_tabs, m_txtLastName, m_tabs, m_txtLastName);
            Joystick.Add(m_txtLastName, m_txtFirstName, m_txtAddress, m_txtFirstName, m_txtAddress);
            Joystick.Add(m_txtAddress, m_txtLastName, m_txtCity, m_txtLastName, m_txtCity);
            Joystick.Add(m_txtCity, m_txtAddress, m_cmbState, m_txtAddress, m_cmbState);
            Joystick.Add(m_cmbState, m_txtCity, m_txtZip, m_txtCity, m_tabs);
            Joystick.Add(m_txtZip, m_cmbState, m_tabs, m_txtCity, m_tabs);

            Joystick.Add(m_txtCCNumber, m_tabs, m_cmbExpMonth, m_tabs, m_cmbExpMonth);
            Joystick.Add(m_cmbExpMonth, m_txtCCNumber, m_cmbExpYear, m_txtCCNumber, m_cmbCVV2Type);
            Joystick.Add(m_cmbExpYear, m_cmbExpMonth, m_cmbCVV2Type, m_txtCCNumber, m_cmbCVV2Type);
            Joystick.Add(m_cmbCVV2Type, m_cmbExpYear, m_txtCVV2, m_cmbExpMonth, m_txtCVV2);
            Joystick.Add(m_txtCVV2, m_cmbCVV2Type, m_tabs, m_cmbCVV2Type, m_tabs);

            Joystick.Add(m_tabs, 0, m_cmbState, m_txtFirstName);
            Joystick.Add(m_tabs, 1, m_txtCVV2, m_txtCCNumber);            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.Customers.Edit
{
    public partial class EditCustomerView : BaseControl
    {
        public EditCustomerView()
        {
            InitializeComponent();
        }

        protected override void OnInit()
        {
            base.OnInit();            

            // Info Page
            Joystick.Add(m_cmbSalutation, m_tabs, m_cmbSuffix, m_tabs, m_txtFirstName);
            Joystick.Add(m_txtFirstName, m_cmbSuffix, m_txtMiddleName, m_cmbSalutation, m_txtMiddleName);
            Joystick.Add(m_txtMiddleName, m_txtFirstName, m_txtLastName, m_txtFirstName, m_txtLastName);
            Joystick.Add(m_txtLastName, m_txtMiddleName, m_txtDisplayAs, m_txtMiddleName, m_txtDisplayAs);
            Joystick.Add(m_cmbSuffix, m_cmbSalutation, m_txtFirstName, m_tabs, m_txtFirstName);
            Joystick.Add(m_txtDisplayAs, m_txtLastName, m_txtPrintAs, m_txtLastName, m_txtPrintAs);
            Joystick.Add(m_txtPrintAs, m_txtDisplayAs, m_txtCompanyName, m_txtDisplayAs, m_txtCompanyName);
            Joystick.Add(m_txtCompanyName, m_txtPrintAs, m_tabs, m_txtPrintAs, m_tabs);

            // Contact Page
            Joystick.Add(m_txtPhone, m_tabs, m_txtMobile, m_tabs, m_txtMobile);
            Joystick.Add(m_txtMobile, m_txtPhone, m_txtFax, m_txtPhone, m_txtFax);
            Joystick.Add(m_txtFax, m_txtMobile, m_txtPager, m_txtMobile, m_txtPager);
            Joystick.Add(m_txtPager, m_txtFax, m_txtEmail, m_txtFax, m_txtEmail);
            Joystick.Add(m_txtEmail, m_txtPager, m_txtOther, m_txtPager, m_txtOther);
            Joystick.Add(m_txtOther, m_txtEmail, m_tabs, m_txtEmail, m_tabs);
            // Billing Address Page
            Joystick.Add(m_txtAddress, m_tabs, m_txtCity, m_tabs, m_txtCity);
            Joystick.Add(m_txtCity, m_txtAddress, m_txtPostalCode, m_txtAddress, m_txtPostalCode);
            Joystick.Add(m_txtPostalCode, m_txtCity, m_txtState, m_txtCity, m_txtCountry);
            Joystick.Add(m_txtState, m_txtPostalCode, m_txtCountry, m_txtCity, m_txtCountry);
            Joystick.Add(m_txtCountry, m_txtState, m_tabs, m_txtPostalCode, m_tabs);
            // Shipping Address Page
            Joystick.Add(m_txtShipAddress, m_tabs, m_txtShipCity, m_tabs, m_txtShipCity);
            Joystick.Add(m_txtShipCity, m_txtShipAddress, m_txtShipPostalCode, m_txtShipAddress, m_txtShipPostalCode);
            Joystick.Add(m_txtShipPostalCode, m_txtShipCity, m_txtShipState, m_txtShipCity, m_txtShipCountry);
            Joystick.Add(m_txtShipState, m_txtShipPostalCode, m_txtShipCountry, m_txtShipCity, m_txtShipCountry);
            Joystick.Add(m_txtShipCountry, m_txtShipState, m_chkSameAsBill, m_txtShipPostalCode, m_chkSameAsBill);
            Joystick.Add(m_chkSameAsBill, m_txtShipCountry, m_tabs, m_txtShipCountry, m_tabs);
            // Tax Page
            Joystick.Add(m_txtResale, m_tabs, m_tabs, m_tabs, m_tabs);
            // Bill Page
            
            Joystick.Add(m_cmbTerms, m_tabs, m_cmbDelivery, m_tabs, m_cmbDelivery);
            Joystick.Add(m_cmbDelivery, m_cmbTerms, m_curBalance, m_cmbTerms, m_curBalance);
            Joystick.Add(m_curBalance, m_cmbDelivery, m_dtpBalanceDate, m_cmbDelivery, m_dtpBalanceDate);
            Joystick.Add(m_dtpBalanceDate, m_curBalance, m_tabs, m_curBalance, m_tabs);
            
            Joystick.Add(m_tabs, 0, m_txtCompanyName, m_cmbSalutation);
            Joystick.Add(m_tabs, 1, m_txtOther, m_txtPhone);
            Joystick.Add(m_tabs, 2, m_txtCountry, m_txtAddress);
            Joystick.Add(m_tabs, 3, m_chkSameAsBill, m_txtShipAddress);
            Joystick.Add(m_tabs, 4, m_txtResale, m_txtResale);
            Joystick.Add(m_tabs, 5, m_dtpBalanceDate, m_cmbTerms);


        }

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Edit Customer - Q-Agent";
        }
    }
}
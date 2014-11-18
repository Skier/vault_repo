using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.CustomerOperations.Invoice
{
    public partial class InvoiceLineView : BaseControl
    {
        public InvoiceLineView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Edit Charge - Q-Agent";
        }        
        
        protected override void OnInit()
        {
            base.OnInit();

            Joystick.Add(m_dtpServiceDate, m_curAmount, m_cmbProductService, m_curAmount, m_cmbProductService);
            Joystick.Add(m_cmbProductService, m_dtpServiceDate, m_txtDescription, m_dtpServiceDate, m_txtDescription);
            Joystick.Add(m_txtDescription, m_cmbProductService, m_txtQty, m_cmbProductService, m_txtRate);
            Joystick.Add(m_txtQty, m_txtDescription, m_txtRate, m_txtDescription, m_chkTax);
            Joystick.Add(m_txtRate, m_txtQty, m_chkTax, m_txtDescription, m_chkRateAsPercent);
            Joystick.Add(m_chkTax, m_txtRate, m_chkRateAsPercent, m_txtQty, m_curAmount);
            Joystick.Add(m_chkRateAsPercent, m_chkTax, m_curAmount, m_txtRate, m_curAmount);
            Joystick.Add(m_curAmount, m_chkRateAsPercent, m_dtpServiceDate, m_chkRateAsPercent, m_dtpServiceDate);
        }

    }
        
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dalworth.domain;
using hobson.controls;
using MobileTech.Windows.UI;

namespace dalworth.preview
{
    public partial class TicketReceipt : BaseForm
    {
        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        private decimal m_tax;
        
        public TicketReceipt()
        {
            InitializeComponent();
            m_btnPayByCash.Click += new EventHandler(OnPayByCashClick);
            m_btnPayByCC.Click += new EventHandler(OnPayByCCClick);
            m_btnPayByCheck.Click += new EventHandler(OnPayByCheckClick);
        }

        protected override void OnJoystickInit()
        {
            Joystick.Add(m_curService, m_btnPayByCheck, m_btnPayByCash, m_btnPayByCash, m_btnPayByCash);
            Joystick.Add(m_btnPayByCash, m_curService, m_btnPayByCC, m_curService, m_curService);
            Joystick.Add(m_btnPayByCC, m_btnPayByCash, m_btnPayByCheck, m_curService, m_curService);
            Joystick.Add(m_btnPayByCheck, m_btnPayByCC, m_curService, m_curService, m_curService);
        }

        protected override void OnFormLoad(EventArgs e)
        {
            m_btnPayByCash.Picture = ImageKeys.Cash;
            m_btnPayByCC.Picture = ImageKeys.CreditCard;
            m_btnPayByCheck.Picture = ImageKeys.Check;

            m_lblTicketNumber.Text = Model.CurrentTicket.Number.ToString();
            m_lblCustomerName.Text = Model.CurrentTicket.CustomerName;
            m_lblJobType.Text = Model.CurrentTicket.JobType;
            
            m_curService.Value = Model.CurrentServiceSum;
            
            UpdateTotal();
            UpdateMenuStatus();            
        }

        private void UpdateTotal()
        {
            Model.CurrentTotalSum = (m_curService.Value ?? decimal.Zero) + m_tax;
            m_lblTotal.Text = Model.CurrentTotalSum.ToString("C");
        }
        
        private void UpdateMenuStatus()
        {
            if (!m_curService.Value.HasValue
                || m_curService.Value.Value == decimal.Zero)
            {
                m_menuPayByCash.Enabled = m_menuPayByCC.Enabled = m_menuPayByCheck.Enabled 
                    = m_btnPayByCash.Enabled = m_btnPayByCC.Enabled = m_btnPayByCheck.Enabled = false;
                m_lblService.ForeColor = Color.Red;
            } else
            {
                m_menuPayByCash.Enabled = m_menuPayByCC.Enabled = m_menuPayByCheck.Enabled
                    = m_btnPayByCash.Enabled = m_btnPayByCC.Enabled = m_btnPayByCheck.Enabled = true;
                m_lblService.ForeColor = Color.Black;
            }
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            Close();
        }

        private void OnPayByCCClick(object sender, EventArgs e)
        {
            if (!IsFromValid())
                return;
            
            PayByCC payByCC = new PayByCC();
            ShowForm(payByCC);
            if (!payByCC.IsCancelled)
                Close();
        }

        private void OnPayByCheckClick(object sender, EventArgs e)
        {
            if (!IsFromValid())
                return;
            
            PayByCheck payByCheck = new PayByCheck();
            ShowForm(payByCheck);
            if (!payByCheck.IsCancelled)
                Close();
        }

        private void OnPayByCashClick(object sender, EventArgs e)
        {
            if (!IsFromValid())
                return;
            
            PayByCashDone payByCashDone = new PayByCashDone();
            ShowForm(payByCashDone);
            if (Model.CurrentTicket.Status == TicketStatus.Processed)
                Close();
        }

        private void OnServiceValueChanged(object sender, EventArgs e)
        {
            UpdateTotal();
            UpdateMenuStatus();
            if (m_curService.Value.HasValue)
                m_tax = m_curService.Value.Value * (decimal)0.05;
            else
                m_tax = decimal.Zero;
            m_lblTaxAmount.Text = m_tax.ToString("C");
        }
        
        private bool IsFromValid()
        {
            if (!m_curService.Value.HasValue || m_curService.Value.Value == decimal.Zero)
            {
                MessageBox.Show("Please enter Service amount");
                m_curService.Focus();
                return false;
            }

            return true;
        }
    }
}
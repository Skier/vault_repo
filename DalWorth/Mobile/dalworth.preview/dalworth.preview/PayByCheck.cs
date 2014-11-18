using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dalworth.domain;
using hobson.controls;

namespace dalworth.preview
{
    public partial class PayByCheck : BaseForm
    {
        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        public PayByCheck()
        {
            InitializeComponent();
            m_txtFirstName.TextChanged += new EventHandler(OnRequiredFieldChanged);
            m_txtLastName.TextChanged += new EventHandler(OnRequiredFieldChanged);
            m_txtCity.TextChanged += new EventHandler(OnRequiredFieldChanged);
            m_cmbState.SelectedIndexChanged += new EventHandler(OnRequiredFieldChanged);
            m_txtZip.TextChanged += new EventHandler(OnRequiredFieldChanged);
            m_txtCheckNumber.TextChanged += new EventHandler(OnRequiredFieldChanged);
            m_txtBankRouteNumber.TextChanged += new EventHandler(OnRequiredFieldChanged);              
        }

        protected override void OnJoystickInit()
        {
            Joystick.Add(m_txtFirstName, m_tabs, m_txtLastName, m_tabs, m_txtLastName);
            Joystick.Add(m_txtLastName, m_txtFirstName, m_txtCity, m_txtFirstName, m_txtCity);
            Joystick.Add(m_txtCity, m_txtLastName, m_cmbState, m_txtLastName, m_cmbState);
            Joystick.Add(m_cmbState, m_txtCity, m_txtZip, m_txtCity, m_tabs);
            Joystick.Add(m_txtZip, m_cmbState, m_tabs, m_txtCity, m_tabs);

            Joystick.Add(m_txtCheckNumber, m_tabs, m_txtBankRouteNumber, m_tabs, m_txtBankRouteNumber);
            Joystick.Add(m_txtBankRouteNumber, m_txtCheckNumber, m_tabs, m_txtCheckNumber, m_tabs);

            Joystick.Add(m_tabs, 0, m_cmbState, m_txtFirstName);
            Joystick.Add(m_tabs, 1, m_txtBankRouteNumber, m_txtCheckNumber);

        }

        private void OnRequiredFieldChanged(object sender, EventArgs e)
        {
            UpdateMenuStatus();
        }


        protected override void OnFormLoad(EventArgs e)
        {
            m_lblAmountDue.Text = Model.CurrentTotalSum.ToString("C");

            m_lblTicketNumber.Text = Model.CurrentTicket.Number.ToString();
            m_lblCustomerName.Text = Model.CurrentTicket.CustomerName;
            m_lblJobType.Text = Model.CurrentTicket.JobType;

            m_txtFirstName.Text = Model.CurrentTicket.FirstName;
            m_txtLastName.Text = Model.CurrentTicket.LastName;
            m_txtCity.Text = Model.CurrentTicket.City;
            m_cmbState.SelectedItem = Model.CurrentTicket.State;
            m_txtZip.Text = Model.CurrentTicket.Zip;            
            
            UpdateMenuStatus();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (Model.CurrentTicket.Status != TicketStatus.Processed)
                m_isCancelled = true;

            e.Cancel = false;
        }        
        
        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            Close();
        }

        private void UpdateMenuStatus()
        {
            if (m_txtFirstName.Text == String.Empty)
                m_lblFirstName.ForeColor = Color.Red;
            else
                m_lblFirstName.ForeColor = Color.Black;

            if (m_txtLastName.Text == String.Empty)
                m_lblLastName.ForeColor = Color.Red;
            else
                m_lblLastName.ForeColor = Color.Black;

            if (m_txtCity.Text == String.Empty)
                m_lblCity.ForeColor = Color.Red;
            else
                m_lblCity.ForeColor = Color.Black;

            if (m_txtZip.Text == String.Empty || m_cmbState.SelectedIndex < 0)
                m_lblStateZip.ForeColor = Color.Red;
            else
                m_lblStateZip.ForeColor = Color.Black;

            if (m_txtCheckNumber.Text == String.Empty)
                m_lblCheckNumber.ForeColor = Color.Red;
            else
                m_lblCheckNumber.ForeColor = Color.Black;
            
            if (m_txtBankRouteNumber.Text == String.Empty)
                m_lblBankRouteNumber.ForeColor = Color.Red;
            else
                m_lblBankRouteNumber.ForeColor = Color.Black;                                   
            
            if (m_txtFirstName.Text == string.Empty
                || m_txtLastName.Text == string.Empty
                || m_txtCity.Text == string.Empty
                || m_cmbState.SelectedIndex < 0
                || m_txtZip.Text == string.Empty
                || m_txtCheckNumber.Text == string.Empty
                || m_txtBankRouteNumber.Text == string.Empty)
            {
                m_menuDone.Enabled = false;
            }
            else
                m_menuDone.Enabled = true;

        }         

        private void OnDoneClick(object sender, EventArgs e)
        {            
            Model.CurrentNumber = m_txtCheckNumber.Text;
            Model.CurrentTicket.Status = TicketStatus.Processed;
            Model.CurrentAmountReceived = Model.CurrentTotalSum;
            PayByCheckDone payByCheckDone = new PayByCheckDone();
            ShowForm(payByCheckDone);
            Close();
        }        
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dalworth.domain;
using hobson.controls;
using OpenNETCF.Phone;
using OpenNETCF.Phone.Sim;

namespace dalworth.preview
{
    public partial class TicketInfo : BaseForm
    {
        public TicketInfo()
        {
            InitializeComponent();
        }

        protected override void OnFormLoad(EventArgs e)
        {
            m_lblDate.Text = DateTime.Now.ToString("ddd, MMM dd yyyy");

            m_lblMap.Text = "MAP: " + Model.CurrentTicket.Map;
            m_lblCustomerName.Text = Model.CurrentTicket.CustomerName;
            m_txtAddress.Text = Model.CurrentTicket.Address;
            m_linkPhone1.Text = "HM " + Model.CurrentTicket.Phone1;
            m_linkPhone2.Text = "BS " + Model.CurrentTicket.Phone2;            
            m_lblJobType.Text = Model.CurrentTicket.JobType;
            m_lblTicketNumber.Text = "TKT: " + Model.CurrentTicket.Number;
            m_txtNotes.Text = Model.CurrentTicket.Notes;            
            
            m_txtAddress.Focus();
        }

        protected override void OnJoystickInit()
        {
            Joystick.Add(m_txtAddress, m_txtNotes, m_linkPhone1, m_txtNotes, m_linkPhone1);
            Joystick.Add(m_linkPhone1, m_txtAddress, m_linkPhone2, m_txtAddress, m_txtNotes);
            Joystick.Add(m_linkPhone2, m_linkPhone1, m_txtNotes, m_txtAddress, m_txtNotes);
            Joystick.Add(m_txtNotes, m_linkPhone2, m_txtAddress, m_linkPhone1, m_txtAddress);
        }

        private void OnArrivedClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Did you arrive to destination?", "Confirmation", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, 
                MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                MessageBox.Show("Dispatch notified");
            } else
                return;
            
            CustomerMessage customerMessage = new CustomerMessage();
            ShowForm(customerMessage);
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (Model.CurrentTicket.Status == TicketStatus.Started)
                e.Cancel = true;
            else
                e.Cancel = false;
        }

        private void OnPhone1Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Do you want to call {0} at {1}?", 
                Model.CurrentTicket.CustomerName, Model.CurrentTicket.Phone1), 
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 
                MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            try
            {
                Phone.MakeCall(Model.CurrentTicket.Phone1 + "\0", false);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot make a call, please check your phone settings", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void OnPhone2Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Do you want to call {0} at {1}?",
                Model.CurrentTicket.CustomerName, Model.CurrentTicket.Phone2),
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            try
            {
                Phone.MakeCall(Model.CurrentTicket.Phone2 + "\0", false);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot make a call, please check your phone settings", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
    }
}
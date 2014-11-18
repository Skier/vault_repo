using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using hobson.controls;
using OpenNETCF.Phone;

namespace dalworth.preview
{
    public partial class JobDetails : BaseForm
    {
        public JobDetails()
        {
            InitializeComponent();
        }

        protected override void OnFormLoad(EventArgs e)
        {
            m_lblDate.Text = Model.CurrentServicedTicket.Date.ToString("ddd, MMM dd yyyy");
            m_lblMap.Text = "MAP: " + Model.CurrentServicedTicket.Map;
            m_lblCustomerName.Text = Model.CurrentServicedTicket.CustomerName;
            m_txtAddress.Text = Model.CurrentServicedTicket.Address;
            m_linkPhone1.Text = "HM " + Model.CurrentServicedTicket.Phone1;
            m_linkPhone2.Text = "BS " + Model.CurrentServicedTicket.Phone2;
            m_lblJobType.Text = Model.CurrentServicedTicket.JobType;
            m_lblJobType.Text = Model.CurrentServicedTicket.JobType;
            m_txtNotes.Text = Model.CurrentServicedTicket.Notes;
            m_lblTicketNumber.Text = "TKT: " + Model.CurrentServicedTicket.Number;
            m_lblAmount.Text = Model.CurrentServicedTicket.AmountCollected.ToString("C");
            
            m_txtAddress.Focus();
        }

        protected override void OnJoystickInit()
        {
            Joystick.Add(m_txtAddress, m_txtNotes, m_linkPhone1, m_txtNotes, m_linkPhone1);
            Joystick.Add(m_linkPhone1, m_txtAddress, m_linkPhone2, m_txtAddress, m_txtNotes);
            Joystick.Add(m_linkPhone2, m_linkPhone1, m_txtNotes, m_txtAddress, m_txtNotes);
            Joystick.Add(m_txtNotes, m_linkPhone2, m_txtAddress, m_linkPhone1, m_txtAddress);
        }

        private void OnPhone1Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Do you want to call {0} at {1}?",
                Model.CurrentServicedTicket.CustomerName, Model.CurrentServicedTicket.Phone1),
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            try
            {
                Phone.MakeCall(Model.CurrentServicedTicket.Phone1 + "\0", false);
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
                Model.CurrentServicedTicket.CustomerName, Model.CurrentServicedTicket.Phone2),
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            try
            {
                Phone.MakeCall(Model.CurrentServicedTicket.Phone2 + "\0", false);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot make a call, please check your phone settings", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
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
    public partial class PayByCashDone : BaseForm
    {
        public PayByCashDone()
        {
            InitializeComponent();
        }

        protected override void OnFormLoad(EventArgs e)
        {
            m_lblAmountDue.Text = Model.CurrentTotalSum.ToString("C");

            m_lblTicketNumber.Text = Model.CurrentTicket.Number.ToString();
            m_lblCustomerName.Text = Model.CurrentTicket.CustomerName;
            m_lblJobType.Text = Model.CurrentTicket.JobType;            
        }

        private void OnDoneClick(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Did you collect {0}?", Model.CurrentTotalSum.ToString("C")),
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 
                                MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }
            MessageBox.Show("Dispatch notified");
            Model.CurrentTicket.Status = TicketStatus.Processed;
            Model.CurrentAmountReceived = Model.CurrentTotalSum;
            Close();
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
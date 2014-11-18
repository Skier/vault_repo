using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dalworth.domain;
using hobson.controls;
using QuickBooksAgent.Windows.UI;

namespace dalworth.preview
{
    public partial class CustomerMessage : BaseForm
    {
        public CustomerMessage()
        {
            InitializeComponent();
        }

        protected override void OnFormLoad(EventArgs e)
        {
            m_txtMessage.Text = Model.CurrentTicket.Notes2;
        }

        private void OnNextClick(object sender, EventArgs e)
        {
            TicketService ticketService = new TicketService();
            ShowForm(ticketService);
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (Model.CurrentTicket.Status == TicketStatus.Started)
                e.Cancel = true;
            else
                e.Cancel = false;
        }        
    }
}
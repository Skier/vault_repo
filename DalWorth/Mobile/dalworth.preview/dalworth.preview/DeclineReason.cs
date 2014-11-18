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
    public partial class DeclineReason : BaseForm
    {
        public DeclineReason()
        {
            InitializeComponent();
        }

        protected override void OnFormLoad(EventArgs e)
        {
            UpdateMenu();
        }

        private void UpdateMenu()
        {
            m_menuSend.Enabled = m_txtMessage.Text.Length != 0;
        }
        
        private void OnCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnSendClick(object sender, EventArgs e)
        {
            MessageBox.Show("Dispatch notified");
            Model.CurrentTicket.Status = TicketStatus.Declined;
            Close();
        }

        private void OnMessageChanged(object sender, EventArgs e)
        {
            UpdateMenu();
        }
    }
}
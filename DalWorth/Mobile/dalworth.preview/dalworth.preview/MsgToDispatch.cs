using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using hobson.controls;

namespace dalworth.preview
{
    public partial class MsgToDispatch : BaseForm
    {
        public MsgToDispatch()
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
            if (MessageBox.Show("Do you want to send this message to Dispatch?", "Confirmation", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            MessageBox.Show("Dispatch notified");
            Close();
        }

        private void OnMessageTextChanged(object sender, EventArgs e)
        {
            UpdateMenu();
        }
    }
}
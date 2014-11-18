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
    public partial class MsgToTechnician : BaseForm
    {
        public MsgToTechnician()
        {
            InitializeComponent();
        }

        protected override void OnFormLoad(EventArgs e)
        {
            UpdateMenu();
        }

        protected override void OnJoystickInit()
        {
            Joystick.Add(m_cmbTechnician, m_txtMessage, m_txtMessage, m_txtMessage, m_txtMessage);
            Joystick.Add(m_txtMessage, m_cmbTechnician, m_cmbTechnician, m_cmbTechnician, m_cmbTechnician);
        }

        private void UpdateMenu()
        {
            if (m_cmbTechnician.SelectedIndex >= 0 && m_txtMessage.Text.Length > 0)
                m_menuSend.Enabled = true;
            else
                m_menuSend.Enabled = false;
        }
        
        private void OnCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnSendClick(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Do you want to send this message to {0}?", m_cmbTechnician.SelectedItem.ToString()), "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            MessageBox.Show(string.Format("{0} notified", m_cmbTechnician.SelectedItem.ToString()));
            Close();
        }

        private void OnTechnicianChanged(object sender, EventArgs e)
        {
            UpdateMenu();
        }

        private void OnMessageChanged(object sender, EventArgs e)
        {
            UpdateMenu();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using dalworth.controls;
using dalworth.domain;
using hobson.controls;
using MobileTech;

namespace dalworth.preview
{
    public partial class TicketIncome : BaseForm
    {
        private bool m_isButtonRed;
        private Sound m_sound;
        
        public TicketIncome()
        {
            InitializeComponent();
        }

        protected override void OnFormLoad(EventArgs e)
        {
            m_lblDate.Text = DateTime.Now.ToString("ddd, MMM dd yyyy");
            
            m_btnAccept.Click += new EventHandler(OnAcceptClick);
            m_sound = new Sound(Host.GetPath("Sounds") + "\\Alarm4.wav");

            StartNotification();

            m_lblCustomerName.Text = Model.CurrentTicket.CustomerName;
            m_lblJobType.Text = Model.CurrentTicket.JobType;
            m_lblTicketNumber.Text = "TKT: " + Model.CurrentTicket.Number;
            m_txtNotes.Text = Model.CurrentTicket.Notes;
        }

        protected override void OnJoystickInit()
        {
            Joystick.Add(m_txtNotes, m_btnAccept, m_btnAccept, m_btnAccept, m_btnAccept);
            Joystick.Add(m_btnAccept, m_txtNotes, m_txtNotes, m_txtNotes, m_txtNotes);
        }

        private void OnTimerSoundTick(object sender, EventArgs e)
        {
            m_sound.Play();
        }

        private void OnTimerBlinkingTick(object sender, EventArgs e)
        {
            if (m_isButtonRed)
                m_btnAccept.BackColor = Color.White;
            else
                m_btnAccept.BackColor = Color.Red;

            m_isButtonRed = !m_isButtonRed;
        }
        
        private void StartNotification()
        {
            m_sound.Play();
            m_timerBlinking.Enabled = true;
            m_timerSound.Enabled = true;
        }
        
        private void StopNotification()
        {
            m_timerBlinking.Enabled = false;
            m_timerSound.Enabled = false;
            m_btnAccept.BackColor = Color.White;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (Model.CurrentTicket.Status == TicketStatus.Started)
                e.Cancel = true;
            else
                e.Cancel = false;
        }

        private void OnDeclineClick(object sender, EventArgs e)
        {
            StopNotification();
            
            DeclineReason declineReason = new DeclineReason();
            ShowForm(declineReason);

            if (Model.CurrentTicket.Status == TicketStatus.Declined)
                Close();
            else
                StartNotification();
        }

        private void OnAcceptClick(object sender, EventArgs e)
        {
            StopNotification();
            MessageBox.Show("Dispatch notified");
            TicketInfo ticketInfo = new TicketInfo();
            ShowForm(ticketInfo);

            Close();
        }
    }
}
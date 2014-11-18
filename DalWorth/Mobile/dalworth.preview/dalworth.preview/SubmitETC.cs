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
    public partial class SubmitETC : BaseForm
    {
        public SubmitETC()
        {
            InitializeComponent();
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnJoystickInit()
        {
            Joystick.Add(m_curSale, m_txtNotes, m_cmbEtcHH, m_txtNotes, m_cmbEtcMM);
            Joystick.Add(m_cmbEtcHH, m_curSale, m_cmbEtcMM, m_curSale, m_txtNotes);
            Joystick.Add(m_cmbEtcMM, m_cmbEtcHH, m_txtNotes, m_curSale, m_txtNotes);
            Joystick.Add(m_txtNotes, m_cmbEtcMM, m_curSale, m_cmbEtcHH, m_curSale);
        }

        protected override void OnFormLoad(EventArgs e)
        {
            if (Model.CurrentETC != DateTime.MinValue)
            {
                m_cmbEtcHH.SelectedIndex = Model.CurrentETC.Hour + 1;
                if (Model.CurrentETC.Minute == 0)
                    m_cmbEtcMM.SelectedIndex = 1;
                else if (Model.CurrentETC.Minute == 15)
                    m_cmbEtcMM.SelectedIndex = 2;
                else if (Model.CurrentETC.Minute == 30)
                    m_cmbEtcMM.SelectedIndex = 3;
                else if (Model.CurrentETC.Minute == 45)
                    m_cmbEtcMM.SelectedIndex = 4;
            }

            m_lblTicketNumber.Text = Model.CurrentTicket.Number.ToString();
            m_lblCustomerName.Text = Model.CurrentTicket.CustomerName;
            m_lblJobType.Text = Model.CurrentTicket.JobType;
            
            //m_curSale.Value = Model.CurrentServiceSum;
            m_txtNotes.Text = Model.CurrentEtcNote;                
        }

        private void OnDoneClick(object sender, EventArgs e)
        {
            if (m_cmbEtcHH.SelectedIndex <= 0 && m_cmbEtcMM.SelectedIndex > 0)
            {
                MessageBox.Show("Please enter correct time");
                m_cmbEtcHH.Focus();
                return;                
            }
            
            if (m_cmbEtcMM.SelectedIndex <= 0 && m_cmbEtcHH.SelectedIndex > 0)
            {
                MessageBox.Show("Please enter correct time");
                m_cmbEtcMM.Focus();
                return;                
            }

            if (m_cmbEtcMM.SelectedIndex > 0 && m_cmbEtcHH.SelectedIndex > 0)
            {
                DateTime etcDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                    int.Parse(m_cmbEtcHH.SelectedItem.ToString()), 
                    int.Parse(m_cmbEtcMM.SelectedItem.ToString()), 0);
                
                Model.CurrentETC = etcDate;
            }

            if (MessageBox.Show("Is Information Correct?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                MessageBox.Show("Dispatch notified");
            }
            else
                return;
            
                                                
            Model.CurrentServiceSum = m_curSale.Value ?? decimal.Zero;
            Model.CurrentEtcNote = m_txtNotes.Text;
            Close();
        }

        private void OnHourChanged(object sender, EventArgs e)
        {
            if (m_cmbEtcMM.SelectedIndex <= 0)
                m_cmbEtcMM.SelectedIndex = 1;
        }
    }
}
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
    public partial class Finish : BaseForm
    {
        public Finish()
        {
            InitializeComponent();
            
            m_txtOdometer.TextChanged += new EventHandler(OnRequiredFieldTextChanged);
            m_txtHoursMeter.TextChanged += new EventHandler(OnRequiredFieldTextChanged);
            m_txtVanNumber.TextChanged += new EventHandler(OnRequiredFieldTextChanged);
            m_txtSpecialNeeds.TextChanged += new EventHandler(OnRequiredFieldTextChanged);
        }

        protected override void OnJoystickInit()
        {
            Joystick.Add(m_txtVanNumber, m_txtSpecialNeeds, m_chkOilChecked, m_txtSpecialNeeds, m_chkOilChecked);
            Joystick.Add(m_chkOilChecked, m_txtVanNumber, m_chkUnitClean, m_txtVanNumber, m_chkVanClean);
            Joystick.Add(m_chkUnitClean, m_chkOilChecked, m_chkVanClean, m_txtVanNumber, m_chkSuppliesStocked);
            Joystick.Add(m_chkVanClean, m_chkUnitClean, m_chkSuppliesStocked, m_chkOilChecked, m_txtOdometer);
            Joystick.Add(m_chkSuppliesStocked, m_chkVanClean, m_txtOdometer, m_chkUnitClean, m_txtOdometer);
            Joystick.Add(m_txtOdometer, m_chkSuppliesStocked, m_txtHoursMeter, m_chkSuppliesStocked, m_txtHoursMeter);
            Joystick.Add(m_txtHoursMeter, m_txtOdometer, m_txtSpecialNeeds, m_txtOdometer, m_txtSpecialNeeds);
            Joystick.Add(m_txtSpecialNeeds, m_txtHoursMeter, m_txtVanNumber, m_txtHoursMeter, m_txtVanNumber);
        }

        private void OnRequiredFieldTextChanged(object sender, EventArgs e)
        {
            UpdateCompleteButton();
        }

        protected override void OnFormLoad(EventArgs e)
        {
            Text = string.Format("0150 Van Checklist - step {0}/{0}", Model.EquipmentRequests.Count + 1);            
            UpdateCompleteButton();
            m_txtVanNumber.Focus();            
        }

        private void OnBackClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnCompleteClick(object sender, EventArgs e)
        {            
            Model.AppPoint = ApplicationPoint.StartDayDone;
            MessageBox.Show("Dispatch notified");
            Close();
        }
        
        private void UpdateCompleteButton()
        {
            if (m_txtOdometer.Text == String.Empty)
                m_lblOdometer.ForeColor = Color.Red;
            else
                m_lblOdometer.ForeColor = Color.Black;
            
            if (m_txtHoursMeter.Text == String.Empty)
                m_lblPumpReading.ForeColor = Color.Red;
            else
                m_lblPumpReading.ForeColor = Color.Black;
            
            if (m_txtVanNumber.Text == String.Empty)
                m_lblVanNumber.ForeColor = Color.Red;
            else
                m_lblVanNumber.ForeColor = Color.Black;
            
            if (m_txtSpecialNeeds.Text == String.Empty)
                m_lblSpecialNeeds.ForeColor = Color.Red;
            else
                m_lblSpecialNeeds.ForeColor = Color.Black;            
            
            if (m_txtOdometer.Text == String.Empty
                || m_txtHoursMeter.Text == String.Empty
                || m_txtVanNumber.Text == String.Empty
                || m_txtSpecialNeeds.Text == String.Empty)
            {
                m_menuComplete.Enabled = false;
            } else
                m_menuComplete.Enabled = true;
        }        
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using hobson.controls;
using MobileTech.Windows.UI;

namespace dalworth.preview
{
    public partial class More : BaseForm
    {
        public More()
        {
            InitializeComponent();
        }

        protected override void OnFormLoad(EventArgs e)
        {
            m_btnTools.Picture = ImageKeys.Tools;
            m_btnJobHistory.Picture = ImageKeys.JobHistory;
            m_btnMessageToDispatch.Picture = ImageKeys.MessageToDispatch;
            m_btnMessageToTechnician.Picture = ImageKeys.MessageToTechnician;
        }

        protected override void OnJoystickInit()
        {
            Joystick.Add(m_btnTools, m_btnMessageToTechnician, m_btnJobHistory, m_btnMessageToDispatch, m_btnMessageToDispatch);
            Joystick.Add(m_btnJobHistory, m_btnTools, m_btnMessageToDispatch, m_btnMessageToTechnician, m_btnMessageToTechnician);
            Joystick.Add(m_btnMessageToDispatch, m_btnJobHistory, m_btnMessageToTechnician, m_btnTools, m_btnTools);
            Joystick.Add(m_btnMessageToTechnician, m_btnMessageToDispatch, m_btnTools, m_btnJobHistory, m_btnJobHistory);
        }

        private void OnToolsClick(object sender, EventArgs e)
        {
            Tools tools = new Tools();
            ShowForm(tools);
        }

        private void OnJobHistoryClick(object sender, EventArgs e)
        {
            JobHistory jobHistory = new JobHistory();
            ShowForm(jobHistory);
        }

        private void OnMessageToDispatchClick(object sender, EventArgs e)
        {
            MsgToDispatch msgToDispatch = new MsgToDispatch();
            ShowForm(msgToDispatch);
        }

        private void OnMessageToTechnicianClick(object sender, EventArgs e)
        {
            MsgToTechnician msgToTechnician = new MsgToTechnician();
            ShowForm(msgToTechnician);
        }
    }
}
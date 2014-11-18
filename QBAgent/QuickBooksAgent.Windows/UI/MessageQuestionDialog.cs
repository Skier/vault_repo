using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace QuickBooksAgent.Windows.UI
{
    public partial class MessageQuestionDialog:BaseForm
    {
        public MessageQuestionDialog(String message)
        {
            InitializeComponent();
            m_txMessage.Text = (String)message;

            Joystick.Add(m_mbYes, m_mbNo, m_mbNo, m_mbYes, m_mbYes);
            Joystick.Add(m_mbNo, m_mbYes, m_mbYes, m_mbNo, m_mbNo);
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            CommonUIResources.Culture = cultureInfo;

            Text = CommonResources.QuestionTitle;
            m_mbYes.Text = CommonUIResources.BtnYes;
            m_mbNo.Text = CommonUIResources.BtnNo;
        }        

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            WinAPI.MessageBeep(WinAPI.MessageBeepType.MB_ICONQUESTION);
        }

        private void OnYesClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;

            Destroy();
        }

        private void OnNoClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;

            Destroy();
        }

        protected override bool OnCancel()
        {
            this.DialogResult = DialogResult.No;

            return base.OnCancel();
        }
    }
}
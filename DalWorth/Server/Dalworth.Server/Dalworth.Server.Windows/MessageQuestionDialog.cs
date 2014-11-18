using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Dalworth.Server.Controls;

namespace Dalworth.Server.Windows
{
    public partial class MessageQuestionDialog:BaseForm
    {
        public MessageQuestionDialog(String message)
        {
            InitializeComponent();
            m_txMessage.Text = (String)message;
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            Text = "Question";
            m_btnYes.Text = "Yes";
            m_btnNo.Text = "No";
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
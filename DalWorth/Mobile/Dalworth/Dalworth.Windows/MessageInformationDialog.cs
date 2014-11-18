using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Dalworth.Controls;

namespace Dalworth.Windows
{
    public partial class MessageInformationDialog : BaseForm
    {

        public MessageInformationDialog(String message)
        {
            InitializeComponent();
            m_txMessage.Text = message;
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            CommonUIResources.Culture = cultureInfo;

            Text = CommonResources.InformationTitle;
        }  

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            WinAPI.MessageBeep(WinAPI.MessageBeepType.MB_ICONINFORMATION);

        }

        private void m_mbOk_Click(object sender, EventArgs e)
        {
            Destroy();
        }
    }
}
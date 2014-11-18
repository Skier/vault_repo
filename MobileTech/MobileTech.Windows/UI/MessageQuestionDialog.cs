using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MobileTech.Windows.UI
{
    internal class MessageQuestionDialog : MessageDialog
    {
        public MessageQuestionDialog(String message)
            : base(message)
        {

        }

        public override DialogResult Show()
        {
            return MessageBox.Show(m_message,
                CommonResources.DialogCaption,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
        }
    }
}

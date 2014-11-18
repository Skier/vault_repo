using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MobileTech.Windows.UI
{
    internal class MessageWarningDialog:MessageDialog
    {

        public MessageWarningDialog(String message)
            : base(message)
        {

        }

        public override DialogResult Show()
        {
            return MessageBox.Show(m_message,
                CommonResources.DialogCaption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
        }
    }
}

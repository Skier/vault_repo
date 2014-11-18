using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MobileTech.Windows.UI
{
    public enum MessageDialogType
    {
        Information,
        Warning,
        Error,
        Question
    }

    public abstract class MessageDialog
    {
        protected String m_message;

        protected MessageDialog(String message)
        {
            m_message = message;
        }

        public abstract DialogResult Show();

        public static DialogResult Show(MessageDialogType type, String message)
        {
            return CreateDialog(type, message).Show();
        }

        public static MessageDialog CreateDialog(MessageDialogType type, String message)
        {
            switch (type)
            {
                case MessageDialogType.Information:
                    return new MessageInformationDialog(message);
                case MessageDialogType.Warning:
                    return new MessageWarningDialog(message);
                case MessageDialogType.Error:
                    return new MessageErrorDialog(message);
                case MessageDialogType.Question:
                    return new MessageQuestionDialog(message);
                default:
                    break;
            }


            throw new Exception("Unsupported dialog type");
        }
    }
}

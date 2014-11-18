using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Dalworth.Server.Windows
{
    public enum MessageDialogType
    {
        Information,
        Warning,
        Error,
        Question
    }

    public static class MessageDialog
    {

        public static DialogResult Show(MessageDialogType type, String message)
        {
            Debug.Assert(type != MessageDialogType.Error, "For error dialog use: Show(Exception exception) function");

            return CreateDialog(type, message).ShowDialog();
        }

        public static DialogResult Show(Exception exception)
        {
            return CreateDialog(MessageDialogType.Error, exception).ShowDialog();
        }

        private static BaseForm CreateDialog(MessageDialogType type, Object message)
        {
            switch (type)
            {
                case MessageDialogType.Information:
                    return new MessageInformationDialog((String)message);
                case MessageDialogType.Warning:
                   return new MessageWarningDialog((String)message);
                case MessageDialogType.Error:
                    return new MessageErrorDialog((Exception)message);
                case MessageDialogType.Question:
                    return new MessageQuestionDialog((String)message);
                default:
                    break;
            }


            throw new Exception("Unsupported dialog type");
        }
    }
}
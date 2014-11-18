using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Dalworth.Windows
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

            switch (type)
            {
                case MessageDialogType.Information:
                    return MessageBox.Show((String)message, "Information", MessageBoxButtons.OK, 
                        MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                case MessageDialogType.Warning:
                    return MessageBox.Show((String)message, "Warning", MessageBoxButtons.OK, 
                        MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);                    
                case MessageDialogType.Question:
                    return MessageBox.Show((String)message, "Question", MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);                    
                default:
                    break;
            }

            throw new Exception("Unsupported dialog type");
        }

        public static DialogResult Show(Exception exception)
        {
            return new MessageErrorDialog(exception).ShowDialog();
        }
    }
}
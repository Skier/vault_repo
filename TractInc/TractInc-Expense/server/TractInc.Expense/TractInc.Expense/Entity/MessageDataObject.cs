using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Expense.Entity
{

    public class MessageDataObject
    {

        public int MessageId;

        public int SenderUserId;

        public int ReceiverUserId;

        public DateTime Posted;

        public string Subject;

        public string Body;

        public bool IsRead;

        public string SenderLogin;

        public string ReceiverLogin;

    }

}

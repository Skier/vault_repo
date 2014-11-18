using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class Message
    {

        private static Message c_Message = new Message();

        public static Message GetInstance()
        {
            return c_Message;
        }

        private Message()
        {
        }

        private const string SQL_SELECT_BY_SENDER = @"
            select  m.[MessageId],
                    m.[SenderUserId],
                    m.[ReceiverUserId],
                    m.[Posted],
                    m.[Subject],
                    m.[Body],
                    m.[IsRead],
                    us.[Login],
                    ur.[Login]
            from    [Message] m
                    inner join [User] us
                            on us.[UserId] = m.[SenderUserId]
                    inner join [User] ur
                            on ur.[UserId] = m.[ReceiverUserId]
            where   m.[SenderUserId] = @SenderUserId";

        public List<MessageDataObject> GetMessagesBySender(SqlTransaction tran, int senderUserId)
        {
            List<MessageDataObject> result = new List<MessageDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@SenderUserId", senderUserId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_SENDER, parms))
            {
                while (dataReader.Read())
                {
                    MessageDataObject messageInfo = new MessageDataObject();

                    messageInfo.MessageId = (int)dataReader.GetValue(0);
                    messageInfo.SenderUserId = (int)dataReader.GetValue(1);
                    messageInfo.ReceiverUserId = (int)dataReader.GetValue(2);
                    messageInfo.Posted = dataReader.GetDateTime(3);
                    messageInfo.Subject = (string)dataReader.GetValue(4);
                    messageInfo.Body = (string)dataReader.GetValue(5);
                    messageInfo.IsRead = (bool)dataReader.GetValue(6);
                    messageInfo.SenderLogin = (string)dataReader.GetValue(7);
                    messageInfo.ReceiverLogin = (string)dataReader.GetValue(8);

                    result.Add(messageInfo);
                }
            }

            return result;
        }

        private const string SQL_SELECT_BY_RECEIVER = @"
            select  m.[MessageId],
                    m.[SenderUserId],
                    m.[ReceiverUserId],
                    m.[Posted],
                    m.[Subject],
                    m.[Body],
                    m.[IsRead],
                    us.[Login],
                    ur.[Login]
            from    [Message] m
                    inner join [User] us
                            on us.[UserId] = m.[SenderUserId]
                    inner join [User] ur
                            on ur.[UserId] = m.[ReceiverUserId]
            where   [ReceiverUserId] = @ReceiverUserId";

        public List<MessageDataObject> GetMessagesByReceiver(SqlTransaction tran, int receiverUserId)
        {
            List<MessageDataObject> result = new List<MessageDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@ReceiverUserId", receiverUserId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_RECEIVER, parms))
            {
                while (dataReader.Read())
                {
                    MessageDataObject messageInfo = new MessageDataObject();

                    messageInfo.MessageId = (int)dataReader.GetValue(0);
                    messageInfo.SenderUserId = (int)dataReader.GetValue(1);
                    messageInfo.ReceiverUserId = (int)dataReader.GetValue(2);
                    messageInfo.Posted = dataReader.GetDateTime(3);
                    messageInfo.Subject = (string)dataReader.GetValue(4);
                    messageInfo.Body = (string)dataReader.GetValue(5);
                    messageInfo.IsRead = (bool)dataReader.GetValue(6);
                    messageInfo.SenderLogin = (string)dataReader.GetValue(7);
                    messageInfo.ReceiverLogin = (string)dataReader.GetValue(8);

                    result.Add(messageInfo);
                }
            }

            return result;
        }

        private const string SQL_INSERT = @"
        insert  into [Message]
              ( [SenderUserId],
                [ReceiverUserId],
                [Posted],
                [Subject],
                [Body],
                [IsRead])
        values( @SenderUserId,
                @ReceiverUserId,
                @Posted,
                @Subject,
                @Body,
                @IsRead);
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, MessageDataObject messageInfo)
        {
            DbParameter[] parms = new DbParameter[6] {
                new SqlParameter("@SenderUserId",   messageInfo.SenderUserId),
                new SqlParameter("@ReceiverUserId", messageInfo.ReceiverUserId),
                new SqlParameter("@Posted",         messageInfo.Posted),
                new SqlParameter("@Subject",        messageInfo.Subject),
                new SqlParameter("@Body",           messageInfo.Body),
                new SqlParameter("@IsRead",         messageInfo.IsRead)
            };

            messageInfo.MessageId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);

            MessageDataObject updatedInfo = GetMessage(tran, messageInfo.MessageId);
            messageInfo.SenderLogin = updatedInfo.SenderLogin;
            messageInfo.ReceiverLogin = updatedInfo.ReceiverLogin;
        }

        private const string SQL_MARK_AS_READ = @"
        update  [Message]
        set     [IsRead] = 1
        where   [MessageId] = @MessageId";

        public void MarkAsRead(SqlTransaction tran, MessageDataObject messageInfo)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@MessageId", messageInfo.MessageId) };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_MARK_AS_READ, parms);

            messageInfo.IsRead = true;
        }

        private const string SQL_SELECT_BY_ID = @"
            select  m.[MessageId],
                    m.[SenderUserId],
                    m.[ReceiverUserId],
                    m.[Posted],
                    m.[Subject],
                    m.[Body],
                    m.[IsRead],
                    us.[Login],
                    ur.[Login]
            from    [Message] m
                    inner join [User] us
                            on us.[UserId] = m.[SenderUserId]
                    inner join [User] ur
                            on ur.[UserId] = m.[ReceiverUserId]
            where   m.[MessageId] = @MessageId";

        public MessageDataObject GetMessage(SqlTransaction tran, int messageId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@MessageId", messageId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ID, parms))
            {
                if (dataReader.Read())
                {
                    MessageDataObject messageInfo = new MessageDataObject();

                    messageInfo.MessageId = (int)dataReader.GetValue(0);
                    messageInfo.SenderUserId = (int)dataReader.GetValue(1);
                    messageInfo.ReceiverUserId = (int)dataReader.GetValue(2);
                    messageInfo.Posted = dataReader.GetDateTime(3);
                    messageInfo.Subject = (string)dataReader.GetValue(4);
                    messageInfo.Body = (string)dataReader.GetValue(5);
                    messageInfo.IsRead = (bool)dataReader.GetValue(6);
                    messageInfo.SenderLogin = (string)dataReader.GetValue(7);
                    messageInfo.ReceiverLogin = (string)dataReader.GetValue(8);

                    return messageInfo;
                }
            }

            return null;
        }

        private const string SQL_REMOVE = @"
        delete  from [Message]
        where   [MessageId] = @MessageId";

        public void Remove(SqlTransaction tran, int messageId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@MessageId", messageId) };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_REMOVE, parms);
        }

    }

}

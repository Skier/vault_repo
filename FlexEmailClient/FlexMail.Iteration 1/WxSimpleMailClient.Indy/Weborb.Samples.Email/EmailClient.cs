using System;
using System.Collections;
using System.Data;
using Indy.Sockets;
using Weborb.Samples.Email.Entities;
using IndyMessage = Indy.Sockets.Message;

namespace Weborb.Samples.Email
{
    public class EmailClient
    {
        public AccountInfo Login(string email, string password)
        {
            AccountInfo account = Account.RetreiveByEmail(email);
            ServerSettingsInfo settingsInfo = ServerSettings.Retrieve(account.Pop3SettingsId);
            settingsInfo.Password = password;
            
            TestPop3Connect(settingsInfo);

            ServerSettings.Update(settingsInfo);

            settingsInfo = ServerSettings.Retrieve(account.SmtpSettingsId);
            if (settingsInfo.Password == string.Empty) {
                settingsInfo.Password = password;                
            }

            ServerSettings.Update(settingsInfo);
            return account;
        }
        
        public int CreateAccount(string email, ServerSettingsInfo pop3, ServerSettingsInfo smtp)
        {
            TestPop3Connect(pop3);

            using (IDbConnection connection = DataAccessBrigde.CreateConnection())
            {
                connection.Open();
                
                int Pop3SettingsId = ServerSettings.Create(connection, pop3);
                int SmtpSettingsId = ServerSettings.Create(connection, smtp);

                AccountInfo account = new AccountInfo(0, email, Pop3SettingsId, SmtpSettingsId);
                
                return Account.Create(connection, account);
            }
        }
        
        public ServerSettingsInfo GetSettings(int settingsId) {
            return ServerSettings.Retrieve(settingsId);                
        }
        
        public void UpdateAccount(AccountInfo info, ServerSettingsInfo pop3, ServerSettingsInfo smtp) 
        {
            TestPop3Connect(pop3);

            using (IDbConnection connection = DataAccessBrigde.CreateConnection())
            {
                connection.Open();

                Account.Update(info);
                
                ServerSettings.Update(pop3);
                ServerSettings.Update(smtp);
            }
        }
        
        public int CheckMessages(int accountId) {
            AccountInfo account = Account.RetreiveById(accountId);
            ServerSettingsInfo connectInfo = ServerSettings.Retrieve(account.Pop3SettingsId);
            
            using (POP3 pop3Protocol = ServerSettings.ConnectToPop3(connectInfo)) {
                return pop3Protocol.CheckMessages();                
            }
        }
        
        public MessageInfo Receive(int accountId, int messageNumber) {
            AccountInfo account = Account.RetreiveById(accountId);
            ServerSettingsInfo connectInfo = ServerSettings.Retrieve(account.Pop3SettingsId);
            
            using (POP3 pop3Protocol = ServerSettings.ConnectToPop3(connectInfo)) {
                return RetrieveMessage(pop3Protocol, messageNumber, accountId);
            }
        }
        
        public void Send(int accountId, MessageInfo[] messages) {
            ArrayList messageList = new ArrayList();
            
            if (messages.Length == 0)
                return;
            
            AccountInfo account = Account.RetreiveById(accountId);

            foreach (MessageInfo message in messages) {
                message.From = new EmailAddressItem("", account.Email, "");
                messageList.Add(Message.Convert(message));
            }
            
            using(SMTP smtpProtocol = ServerSettings.ConnectToSmtp(ServerSettings.Retrieve(account.SmtpSettingsId))){
                foreach (IndyMessage message in messageList) {
                    smtpProtocol.Send(message);
                    
                    foreach (EMailAddressItem item in message.Recipients) {
                        Address.Create(new AddressInfo(0, accountId, item.Text));
                    }
                }
            }
        }

        public void DeleteMessages(int accountId, string[] messageUids) {
            AccountInfo account = Account.RetreiveById(accountId);
            ServerSettingsInfo connectInfo = ServerSettings.Retrieve(account.Pop3SettingsId);

            using (POP3 pop3Protocol = ServerSettings.ConnectToPop3(connectInfo)) {
                
                int count = pop3Protocol.CheckMessages();
                
                for (int i = 1; i <= count; i++) {
                    MessageInfo msg = RetrieveMessage(pop3Protocol, i, accountId);

                    foreach (string messageUid in messageUids) {
                        if (msg.Uid == messageUid) pop3Protocol.Delete(i);
                    }
                }
            }
        }

        public string GetFileUploaderURL() {
            string result = HttpUrlHelper.AbsoluteRoot;
            if (!result.EndsWith("/")) {
                result += "/";
            }
            
            return result + "upload.ashx";
        }
        
        public string[] GetContacts(int accountId) {
            AddressInfo[] addressList = Address.RetreiveAllByAccountId(accountId);
            string[] result = new string[addressList.Length];
            for (int i = 0; i < result.Length; i++ )
                result[i] = addressList[i].Email;
            return result;
        }

        private MessageInfo RetrieveMessage(POP3 pop3, int messageNumber, int accountId) {
            IndyMessage indyMessage = new IndyMessage();

            pop3.Retrieve(messageNumber, indyMessage);
            int size = pop3.RetrieveMsgSize(messageNumber);

            MessageInfo message = Message.Convert(indyMessage, size, accountId);

            Address.Create(new AddressInfo(0, accountId, indyMessage.From.Text));

            return message;
        }
        
        private void TestPop3Connect(ServerSettingsInfo settings) {
            POP3 pop3Protocol = null;

            try {
                pop3Protocol = ServerSettings.ConnectToPop3(settings);
            } catch (Exception ex) {
                throw new Exception("POP3 connection failed. \n" + ex.Message);
            } finally {
                if (pop3Protocol != null)
                    pop3Protocol.Disconnect();
            }
        }
    }
}
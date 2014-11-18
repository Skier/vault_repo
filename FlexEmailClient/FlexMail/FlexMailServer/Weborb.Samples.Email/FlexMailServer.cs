using System;
using System.Collections;
using System.Data;
using System.Net.Mail;
using System.Web;
using Weborb.Activation;
using Weborb.Exceptions;
using Weborb.Samples.Email.Pop3;
using Weborb.Samples.Email.Entities;
using System.Collections.Generic;
using Weborb.Util.Logging;

namespace Weborb.Samples.Email
{
    [SessionActivation()]    
    public class FlexMailServer
    {
        public const string OBJECT_STATE_IS_INVALID = "Internall error. Server object state is invalid.";
        public const string SESSION_EXPIRED = "Your session has expired";
        public const int SESSION_EXPIRED_CODE = 1001;
        
        public readonly Hashtable uploadedFilesMap = new Hashtable();
        public readonly Hashtable retrievedMessages = new Hashtable();
        
        private List<EmailUid> messageUIDsList;
        private List<int> messageSizeList;
        
        private AccountInfo account;
        private Pop3MailClient pop3Client;
        
        public static FlexMailServer SessionInstance {
            get {
                return (FlexMailServer) HttpContext.Current.Session["EmailClient"];
            }
            
            set {
                HttpContext.Current.Session["EmailClient"] = value;
            }
        }
        
        public static int CreateAccount(AccountInfo accountVO) {
            
            TestPop3Connect(accountVO.Pop3Settings);

            using (IDbConnection connection = DataAccessBrigde.CreateConnection()) {
                connection.Open();

                int Pop3SettingsId = ServerSettings.Create(connection, accountVO.Pop3Settings);
                int SmtpSettingsId = ServerSettings.Create(connection, accountVO.SmtpSettings);

                AccountInfo account = new AccountInfo(0, accountVO.Email, Pop3SettingsId, SmtpSettingsId);

                return Account.Create(connection, account);
            }
        }
        
        private bool EnsurePop3ClientConnected() {
            bool isReconnectWasMade = false;
            
            if ( null == pop3Client ) {
                pop3Client = ServerSettings.ConnectToPop3(account.Pop3Settings);
            }
            
            if (pop3Client.ConnectionState != Pop3ConnectionStateEnum.Connected) {
                pop3Client.Connect();
                isReconnectWasMade = true;
            }
            
            return isReconnectWasMade;
        }
        
        public AccountInfo Login(string email, string password) {
            
            using (IDbConnection connection = DataAccessBrigde.CreateConnection()) {
                connection.Open();

                account = Account.RetreiveByEmail(connection, email);
                
                if (null == account) {
                    throw new Exception("Account not found.");
                }
                
                account.Pop3Settings = ServerSettings.Retrieve(connection, account.Pop3SettingsId);
                account.SmtpSettings = ServerSettings.Retrieve(connection, account.SmtpSettingsId);
                
                account.Pop3Settings.UserPassword = password;

                if (pop3Client != null) {
                    pop3Client.Disconnect();
                }
                
                pop3Client = ServerSettings.ConnectToPop3(account.Pop3Settings);

                ServerSettings.Update(account.Pop3Settings);
            }
            
            retrievedMessages.Clear();
            uploadedFilesMap.Clear();
            
            messageSizeList = null;
            messageUIDsList = null;
            SessionInstance = this;
            
            return account;
        }

        public void UpdateAccount(AccountInfo accountVO) {
            CheckIsSessionValid();
            
            if (pop3Client != null) {
                pop3Client.Disconnect();
            }
            
            pop3Client = ServerSettings.ConnectToPop3(accountVO.Pop3Settings);

            account.Email = accountVO.Email;

            account.Pop3Settings = accountVO.Pop3Settings;
            account.Pop3Settings.Id = account.Pop3SettingsId;
            
            account.SmtpSettings = accountVO.SmtpSettings;
            account.SmtpSettings.Id = account.SmtpSettingsId;
            
            using (IDbConnection connection = DataAccessBrigde.CreateConnection()) {
                connection.Open();

                Account.Update(connection, account);

                ServerSettings.Update(connection, account.Pop3Settings);
                ServerSettings.Update(connection, account.SmtpSettings);
            }
        }

        public MailBoxStatus GetMailBoxStatus() {
            CheckIsSessionValid();

            EnsurePop3ClientConnected();
            
            MailBoxStatus status = new MailBoxStatus();
            
            List<EmailUid> newMessageUIDsList = pop3Client.GetUniqueEmailIdList();

            status.TotalMessages = newMessageUIDsList.Count;
            
            foreach (EmailUid messageUid in newMessageUIDsList) {
                if (IsMessageNew(messageUid.Uid)) {
                    status.NewMessages++;
                }
            }

            //Some POP3 servers (e.g. GMail) doesn't return actually count of messages on server.
            //They returns only new messages count. So we need to handle this situation and 
            //calculate count of messages based on "retrieved count" + "new count"
            //P.S. GMail has option "Enable POP for all mail" in order to return POP3 clients all messages.
            if (status.TotalMessages == status.NewMessages && retrievedMessages.Count > 0) {
                status.TotalMessages += retrievedMessages.Count;
            }
            
            return status;
        }

        private void UpdateMessageUIDsList() {
            messageUIDsList = pop3Client.GetUniqueEmailIdList();
            messageSizeList = pop3Client.GetEmailSizeList();
            messageUIDsList.Reverse();                
            messageSizeList.Reverse();
        }
        
        public MessageInfo[] RetrieveMoreMessages(int maxPaketSize) {

            CheckIsSessionValid();
            
            bool isReconnected = EnsurePop3ClientConnected();

            if (isReconnected) {
                UpdateMessageUIDsList();
            }
            
            ArrayList result = new ArrayList();
            ArrayList contacts = new ArrayList();

            foreach (EmailUid messageUID in messageUIDsList) {
                
                if (!retrievedMessages.ContainsKey(messageUID.Uid)) {
                    
                    RxMailMessage mm = pop3Client.GetEmailHeader(messageUID.EmailId);

                    int messageSize = messageSizeList[messageUID.EmailId - 1];
                    
                    MessageInfo message = new MessageInfo(mm, messageUID.Uid, messageSize);
                    
                    result.Add(message);

                    retrievedMessages.Add(messageUID.Uid, message);
                    
                    contacts.Add(message.From);
                    
                }
                
                if (result.Count == maxPaketSize) {
                    break;
                }
            }
            
            Address.Create(account.Id, (EmailAddressInfo[]) contacts.ToArray(typeof(EmailAddressInfo)));

            return (MessageInfo[])result.ToArray(typeof(MessageInfo));
        }

        private bool IsMessageNew(string messageUID) {
            if (null == messageUIDsList) {
                return true;
            }
            
            foreach (EmailUid emailUid in messageUIDsList) {
                if (emailUid.Uid == messageUID) {
                    return false;
                }
            }
            
            return true;
        }
        
        public MessageInfo[] RetrieveNewMessages(int maxPaketSize) {

       // Log.log(LoggingConstants.DEBUG, "BF: Started RetrieveMessages");
       // DateTime startTime = DateTime.Now;


            CheckIsSessionValid();
            
            ArrayList result = new ArrayList();
            ArrayList contacts = new ArrayList();
            
            EnsurePop3ClientConnected();

          //  DateTime startTimeGetUniqueEmailIdList = DateTime.Now;

            List<EmailUid> newUIDsList = pop3Client.GetUniqueEmailIdList();
            newUIDsList.Reverse();
            
          //  DateTime stopTimeGetUniqueEmailIdList = DateTime.Now;

          //  DateTime startTimeGetEmailSizeList = DateTime.Now;
            messageSizeList = pop3Client.GetEmailSizeList();
            messageSizeList.Reverse();
          //  DateTime stopTimeGetEmailSizeList = DateTime.Now;
            
          //  DateTime startTimeGetHeader = DateTime.Now;

            foreach (EmailUid messageUID in newUIDsList) {
                
                if (retrievedMessages.ContainsKey(messageUID.Uid) && ! IsMessageNew(messageUID.Uid)) {
                    continue;
                }
                
                RxMailMessage mm = pop3Client.GetEmailHeader(messageUID.EmailId);

                int messageSize = messageSizeList[messageUID.EmailId - 1];
                
                MessageInfo message = new MessageInfo(mm, messageUID.Uid, messageSize);
                
                result.Add(message);

                retrievedMessages.Add(messageUID.Uid, message);
                
                contacts.Add(message.From);

                if (result.Count == maxPaketSize) {
                    break;
                }
            }
            
            messageUIDsList = newUIDsList;

            //DateTime stopTimeGetHeader = DateTime.Now;

            Address.Create(account.Id, (EmailAddressInfo[]) contacts.ToArray(typeof(EmailAddressInfo)));

            //DateTime stopTime = DateTime.Now;
            //Log.log(LoggingConstants.DEBUG, "---BF:GetUniqueEmailIdList took" + (stopTimeGetUniqueEmailIdList - startTimeGetUniqueEmailIdList).TotalMilliseconds.ToString());
            //Log.log(LoggingConstants.DEBUG, "---BF:GetEmailSizeList took" + (stopTimeGetEmailSizeList - startTimeGetEmailSizeList).TotalMilliseconds.ToString());
            //Log.log(LoggingConstants.DEBUG, "---BF:Received Headers took" + (stopTimeGetHeader - startTimeGetHeader).TotalMilliseconds.ToString());
            //Log.log(LoggingConstants.DEBUG, "BF: Ended RetrieveMessages took" + (stopTime - startTime).TotalMilliseconds.ToString());
            
            return (MessageInfo[])result.ToArray(typeof(MessageInfo));
        }
        
        //retrieve full message content (with attachments) from POP3
        public MessageBodyInfo RetrieveMessageBody(string messageUID) {
            CheckIsSessionValid();
            
            if (!retrievedMessages.ContainsKey(messageUID))
                throw new InvalidOperationException(OBJECT_STATE_IS_INVALID);
            
            MessageInfo message = (MessageInfo) retrievedMessages[messageUID];
            
            if (null != message.Body ) {
                return message.Body;
            }

            bool isReconnected = EnsurePop3ClientConnected();
            
            if (isReconnected) {
                UpdateMessageUIDsList();
            }

            int messageNumber = int.MinValue;
            
            foreach (EmailUid emailUid in messageUIDsList) {
                if (emailUid.Uid == messageUID) {
                    messageNumber = emailUid.EmailId;
                    break;
                }
            }
            
            
            if (int.MinValue == messageNumber) {
                throw new Exception("Message not found.");
            }

            RxMailMessage mm = pop3Client.GetEmail(messageNumber);
            
            message.Body = new MessageBodyInfo(mm, messageUID);
            
            return message.Body;
        }
        
        public void SendMessage(MessageInfo messageVO) {
            CheckIsSessionValid();
            
            SmtpClient smtp = ServerSettings.ConnectToSmtp(account.SmtpSettings);
            
            messageVO.From = new EmailAddressInfo(account.Email);
            
            List<FileInfo> uploadedMessageFiles = (List<FileInfo>) uploadedFilesMap[messageVO.Uid];
            
            MailMessage mailMessage = messageVO.ToMailMessage(uploadedMessageFiles);
            smtp.Send(mailMessage);
            
            uploadedFilesMap.Remove(messageVO.Uid);
            
            ArrayList contacts = new ArrayList();
            contacts.AddRange(messageVO.To);
            contacts.AddRange(messageVO.Cc);
            contacts.AddRange(messageVO.Bcc);
            
            Address.Create(account.Id, (EmailAddressInfo[]) contacts.ToArray(typeof(EmailAddressInfo)));
        }
        
        public void DeleteMessages(string[] messageUids) {
            CheckIsSessionValid();
            EnsurePop3ClientConnected();
            
            Hashtable uidsNumberMap = pop3Client.GetUniqueEmailIdMap();

            foreach (string uid in messageUids) {
                if (null != uidsNumberMap[uid]) {
                    pop3Client.DeleteEmail((int)uidsNumberMap[uid]);
                    
                    retrievedMessages.Remove(uid);
                }
            }

        }

        public string GetFileUploaderURL() {
            string result = HttpUrlHelper.AbsoluteRoot;
            if (!result.EndsWith("/")) {
                result += "/";
            }

            return result + "UploadAttachment.aspx";
        }

        public string[] GetContacts() {
            AddressInfo[] addressList = Address.RetreiveAllByAccountId(account.Id);
            string[] result = new string[addressList.Length];
            
            for (int i = 0; i < result.Length; i++) {
                result[i] = addressList[i].Email;
            }
            
            return result;
        }

        private static void TestPop3Connect(ServerSettingsInfo settings) {
            Pop3MailClient pop3Client = null;
            
            try {
                pop3Client = ServerSettings.ConnectToPop3(settings);

            } finally {
                if (pop3Client.ConnectionState == Pop3ConnectionStateEnum.Connected) {
                    pop3Client.Disconnect();
                }
            }
        }

        private void CheckIsSessionValid() {
            if (null == SessionInstance) {
                throw new ServiceException(SESSION_EXPIRED, SESSION_EXPIRED_CODE);
            }
            
            if (null == account || null == account.Pop3Settings || null == account.SmtpSettings) {
                throw new InvalidOperationException(OBJECT_STATE_IS_INVALID);
            }
        }
        
    }
}
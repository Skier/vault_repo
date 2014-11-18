using System;
using System.Collections;
using System.Data;
using System.Net.Mail;
using System.Web;
using Weborb.Activation;
using Weborb.Samples.Email.Pop3;
using Weborb.Samples.Email.Entities;
using System.Collections.Generic;

namespace Weborb.Samples.Email
{
    [SessionActivation()]    
    public class EmailClient
    {

        public readonly Hashtable messageBodyMap = new Hashtable();
        public readonly Hashtable uploadedFilesMap = new Hashtable();
        
        private AccountInfo account;
        private ServerSettingsInfo pop3Settings;
        private ServerSettingsInfo smtpSettings;
        private Hashtable retrievedUIDsMap;
        
        public static EmailClient SessionInstance {
            get {
                return (EmailClient) HttpContext.Current.Session["EmailClient"];
            }
            
            set {
                HttpContext.Current.Session["EmailClient"] = value;
            }
        }
        
        public static int CreateAccount(string email, ServerSettingsInfo pop3, ServerSettingsInfo smtp) {
            TestPop3Connect(pop3);

            using (IDbConnection connection = DataAccessBrigde.CreateConnection()) {
                connection.Open();

                int Pop3SettingsId = ServerSettings.Create(connection, pop3);
                int SmtpSettingsId = ServerSettings.Create(connection, smtp);

                AccountInfo account = new AccountInfo(0, email, Pop3SettingsId, SmtpSettingsId);

                return Account.Create(connection, account);
            }
        }
        
        public AccountInfo Login(string email, string password) {
            account = Account.RetreiveByEmail(email);
            
            pop3Settings = ServerSettings.Retrieve(account.Pop3SettingsId);
            smtpSettings = ServerSettings.Retrieve(account.SmtpSettingsId);
            
            pop3Settings.UserPassword = password;

            TestPop3Connect(pop3Settings);

            ServerSettings.Update(pop3Settings);

            retrievedUIDsMap = new Hashtable();
            messageBodyMap.Clear();
            uploadedFilesMap.Clear();
            
            SessionInstance = this;
            
            return account;
        }

        public ServerSettingsInfo GetPop3Settings() {
            return pop3Settings;
        }

        public ServerSettingsInfo GetSmtpSettings() {
            return smtpSettings;
        }
        
        public void UpdateAccount(AccountInfo accountInfo, ServerSettingsInfo pop3Info, ServerSettingsInfo smtpInfo) {
            CheckIsSessionValid();
            
            TestPop3Connect(pop3Info);

            account.Email = accountInfo.Email;

            int pop3SettingsId = pop3Settings.Id;
            int smtpSettingsId = smtpSettings.Id;
            
            pop3Settings = pop3Info;
            pop3Settings.Id = pop3SettingsId;
            
            smtpSettings = smtpInfo;
            smtpSettings.Id = smtpSettingsId;
            
            using (IDbConnection connection = DataAccessBrigde.CreateConnection()) {
                connection.Open();

                Account.Update(accountInfo);

                ServerSettings.Update(pop3Settings);
                ServerSettings.Update(smtpSettings);
            }
        }

        public MailBoxStatus GetMailBoxStatus() {
            CheckIsSessionValid();
            
            MailBoxStatus status = new MailBoxStatus(0, 0);
            
            List<EmailUid> uIDsList;
            
            using (Pop3MailClient pop3Protocol = ServerSettings.ConnectToPop3(pop3Settings)) {
                pop3Protocol.GetUniqueEmailIdList(out uIDsList);
            }

            status.MessagesOnServer = uIDsList.Count;
            
            foreach (EmailUid uid in uIDsList) {
                if (retrievedUIDsMap.ContainsKey(uid.Uid)) {
                    continue;
                }

                status.NewMessages++;
            }

            //Some POP3 servers (e.g. GMail) doesn't return actually count of messages on server.
            //They returns only new messages count. So we need to handle this situation and 
            //calculate count of messages based on "retrieved Count) + "new Count"
            //P.S. GMail has option "Enable POP for all mail" in order to return POP3 clients all messages.
            if (status.MessagesOnServer == status.NewMessages && retrievedUIDsMap.Count > 0) {
                status.MessagesOnServer += retrievedUIDsMap.Count;
            }
            
            return status;
        }

        public MessageInfo[] GetMissingMessages(int maxPaketSize) {
            CheckIsSessionValid();
            
            ArrayList result = new ArrayList();
            Hashtable contactsMap = new Hashtable();
            
            using (Pop3MimeClient pop3Protocol = ServerSettings.ConnectToPop3(pop3Settings)) {
                
                List<EmailUid> uIDsList;
                pop3Protocol.GetUniqueEmailIdList(out uIDsList);
                
                ArrayList mailSizeList = pop3Protocol.GetEmailSizeList();
                
                uIDsList.Reverse(); //reverse uids list in order to retrieve new messages first
                
                foreach (EmailUid uid in uIDsList) {
                    
                    if (retrievedUIDsMap.ContainsKey(uid.Uid)) {
                        continue;
                    }

                    //retrieve message header
                    RxMailMessage mm = pop3Protocol.GetEmailHeader(uid.EmailId);

                    //retrieve message size
                    int messageSize = 0;
                    if (mailSizeList.Count >= uid.EmailId) {
                        messageSize = (int) mailSizeList[uid.EmailId - 1];
                    }
                    
                    MessageInfo message = new MessageInfo(mm, uid.Uid, messageSize);
                    
                    result.Add(message);
                    
                    contactsMap[message.From.DisplayValue] = null;
                    retrievedUIDsMap.Add(uid.Uid, null);
                    
                    maxPaketSize--;
                    if (maxPaketSize == 0 ) break;
                }
            }
            
            String[] contactAddresses = new string[contactsMap.Count];
            contactsMap.Keys.CopyTo(contactAddresses, 0);
            Address.Create(account.Id, contactAddresses);
            
            return (MessageInfo[])result.ToArray(typeof(MessageInfo));
        }
        
        //retrieve full message content (with attachments) from POP3
        public MessageBodyInfo RetrieveMessageBody(string messageUID) {
            CheckIsSessionValid();
            
            if (messageBodyMap.ContainsKey(messageUID)) {
                return (MessageBodyInfo) messageBodyMap[messageUID];
            }
            
            using (Pop3MimeClient pop3Protocol = ServerSettings.ConnectToPop3(pop3Settings)) {
                Hashtable uidlMap = pop3Protocol.GetUniqueEmailIdMap();
                
                if (!uidlMap.Contains(messageUID)) {
                    throw new Exception("Message not found.");
                }
                
                int messageNumber = (int) uidlMap[messageUID];
                
                RxMailMessage mm;
                if (pop3Protocol.GetEmail(messageNumber, out mm)) {
                    MessageBodyInfo bodyInfo = new MessageBodyInfo(mm, messageUID);
                    messageBodyMap.Add(bodyInfo.Uid, bodyInfo);
                    
                    return bodyInfo;
                }
            }
            
            throw new Exception("Message not found.");
        }
        
        public void SendMessage(MessageInfo messageVO) {
            CheckIsSessionValid();
            
            SmtpClient smtp = ServerSettings.ConnectToSmtp(smtpSettings);
            
            messageVO.From = new EmailAddressInfo(account.Email);
            
            List<FileInfo> uploadedMessageFiles = (List<FileInfo>) uploadedFilesMap[messageVO.Uid];
            
            MailMessage mailMessage = messageVO.ToMailMessage(uploadedMessageFiles);
            smtp.Send(mailMessage);
            
            uploadedFilesMap.Remove(messageVO.Uid);
            
            foreach (MailAddress address in messageVO.To) {
                Address.Create(new AddressInfo(0, account.Id, address.Address));
            }
        }
        
        public void DeleteMessages(string[] messageUids) {
            CheckIsSessionValid();
            
            using (Pop3MailClient pop3Protocol = ServerSettings.ConnectToPop3(pop3Settings)) {
                Hashtable uidsNumberMap = pop3Protocol.GetUniqueEmailIdMap();

                foreach (string uid in messageUids) {
                    if (null != uidsNumberMap[uid]) {
                        pop3Protocol.DeleteEmail((int)uidsNumberMap[uid]);
                        
                        uidsNumberMap.Remove(uid);
                        messageBodyMap.Remove(uid);
                    }
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
            Pop3MailClient pop3Protocol = new Pop3MailClient(
                settings.Host, settings.Port,
                settings.ConnectionType == ServerSettingsInfo.CONNECTION_TYPE_SECURE_TLS,
                settings.UserName, settings.UserPassword);
            
            try {
                pop3Protocol.Connect();
            } catch (Exception ex) {
                throw new Exception("POP3 connection failed. \n" + ex.Message);
            } finally {
                if (pop3Protocol.Pop3ConnectionState == Pop3ConnectionStateEnum.Connected) {
                    pop3Protocol.Disconnect();
                }
            }
        }

        private void CheckIsSessionValid() {
            if (null == SessionInstance) {
                throw new InvalidOperationException("Your session has expired.");
            }
            
            if (null == account || null == pop3Settings || null == smtpSettings) {
                throw new InvalidOperationException("Object state is invalid");
            }
        }
        
    }
}
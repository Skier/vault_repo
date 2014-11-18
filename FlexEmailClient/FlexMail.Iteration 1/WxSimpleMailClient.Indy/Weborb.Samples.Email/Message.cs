using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using Indy.Sockets;
using Weborb.Samples.Email.Entities;
using IndyMessage = Indy.Sockets.Message;
using IndyEmailAddressItem = Indy.Sockets.EMailAddressItem;

namespace Weborb.Samples.Email
{
    public class Message
    {
        const string MESSAGE_FILE_NAME = "Message.html";
        
        const string CONTENT_TYPE_PLAIN = "text/plain";
        const string CONTENT_TYPE_HTML  = "text/html";
        const string CONTENT_TYPE_MULTIPART = "multipart/mixed";
        
        public static MessageInfo Convert(IndyMessage indyMsg, int size, int accountId)
        {
            string uid;
            string plainBody;
            BodyPartInfo[] bodyParts;
            
            ProcessMessageParts(indyMsg, out uid, out plainBody, out bodyParts);

            string inReplyTo = (indyMsg.InReplyTo != null)
                ? indyMsg.InReplyTo
                : indyMsg.References;
            
            return new MessageInfo(uid,
                                   indyMsg.From,
                                   ConvertEmailAddresses(indyMsg.Recipients),
                                   ConvertEmailAddresses(indyMsg.CCList),
                                   ConvertEmailAddresses(indyMsg.BccList),                                   
                                   GetSubject(indyMsg),
                                   inReplyTo, size, 
                                   DateTime.FromOADate(indyMsg.Date),
                                   plainBody, 
                                   string.Empty, bodyParts);
        }

        public static IndyMessage Convert(MessageInfo message)
        {
            IndyMessage indyMessage = new IndyMessage();

            indyMessage.From = message.From;
            
            EMailAddressList indyToAddresses = indyMessage.Recipients;
            ConvertEmailAddresses(message.To, ref indyToAddresses);

            EMailAddressList indyCcAddresses = indyMessage.CCList;
            ConvertEmailAddresses(message.Cc, ref indyCcAddresses);

            EMailAddressList indyBccAddresses = indyMessage.BccList;
            ConvertEmailAddresses(message.Bcc, ref indyBccAddresses);
            
            indyMessage.Subject = message.Subject;
            indyMessage.Body.Text = message.BodyPlainText;
            indyMessage.MsgId = GenerateMessageUID(indyMessage.From.Domain);
            
            indyMessage.InReplyTo = message.InReplyTo;
            indyMessage.References = message.InReplyTo;
            
            if (message.BodyPartList.Length > 0) {
                
                foreach (BodyPartInfo partInfo in message.BodyPartList) {
                    
                    if (partInfo.FileName == "" && partInfo.ContentType == CONTENT_TYPE_HTML) {

                        TIdStringsFCL text = new TIdStringListFCL();
                        text.Text = partInfo.Text;
                        Text msgTextPart = new Text(indyMessage.MessageParts, text);
                        msgTextPart.ContentType = CONTENT_TYPE_HTML;
                        
                    } else if (partInfo.FileName != ""){
                        
                        string attachmentPath = GetAttachmentPath(message.AttachmentDir, partInfo.FileName);
                        
                        AttachmentFile file = new AttachmentFile(indyMessage.MessageParts, attachmentPath);

                        string fileExtension = Path.GetExtension(partInfo.FileName);
                        switch (fileExtension) {
                            case "html" :
                                file.ContentType = CONTENT_TYPE_HTML;
                                break;
                            case "txt" :
                                file.ContentType = CONTENT_TYPE_PLAIN;
                                break;
                            default :
                                break;
                        }
                        
                    }
                }

                indyMessage.ContentType = CONTENT_TYPE_MULTIPART;
                Text body = new Text(indyMessage.MessageParts, indyMessage.Body);
                body.ContentType = CONTENT_TYPE_PLAIN;
                
            } else {
                indyMessage.ContentType = "text/plain";
            }

            return indyMessage;
        }
        
        private static string GetSubject(IndyMessage indyMsg)
        {
            string subject = string.Empty;

            foreach (string token in indyMsg.Subject.Split(' '))
                if (token.StartsWith("=?") && token.EndsWith("?="))
                {
                    int charSetEndIndex = token.IndexOf("?", 2);

//                    string charSet = token.Substring(2, charSetEndIndex - 2);
//                    if (charSet != "UTF-8")
//                        throw new NotSupportedException("UTF-8 encoding is not supported yet.");

                    string encoding = token.Substring(charSetEndIndex + 1, 1);
                    if (encoding != "B")
                        throw new NotSupportedException("Q encoding is not supported yet.");

                    string encodedText = token.Substring(
                        charSetEndIndex + 3, token.Length - charSetEndIndex - 3 - 2);

                    byte[] bytes = System.Convert.FromBase64String(encodedText);
                    UTF8Encoding encoder = new UTF8Encoding();
                    subject += subject != string.Empty ? ' ' + encoder.GetString(bytes)
                                   : encoder.GetString(bytes);
                }
                else
                    subject += token + ' ';
            
            return subject;
        }

        private static void ProcessMessageParts(IndyMessage indyMsg,
                                                out string uid,
                                                out string plainBody,
                                                out BodyPartInfo[] partList)
        {
            plainBody = string.Empty;
            partList = new BodyPartInfo[0];
            
            if (indyMsg.UID != string.Empty)
                uid = indyMsg.UID;
            else if (indyMsg.MsgId != string.Empty)
                uid = indyMsg.MsgId;
            else
                uid = Guid.NewGuid().ToString(); //we need message identifier to store attachment files.
            
            Collection<BodyPartInfo> bodyPartCollection = new Collection<BodyPartInfo>();
            
            Dictionary<string, string> contentIDCollection;
            contentIDCollection = new Dictionary<string, string>();

            if (indyMsg.ContentType == CONTENT_TYPE_PLAIN) {
                plainBody = indyMsg.Body.Text;
                return;
            } 
            
            if (indyMsg.ContentType == CONTENT_TYPE_HTML) {

                string attachmentPath = GetAttachmentPath(uid, MESSAGE_FILE_NAME);
                SaveToFile(uid, indyMsg.Body.Text, attachmentPath);

                BodyPartInfo info = new BodyPartInfo();
                info.ContentType = CONTENT_TYPE_HTML;
                info.FileName = MESSAGE_FILE_NAME;
                info.FileUrl = GetAttachmentUrl(uid, MESSAGE_FILE_NAME);
                info.Size = new FileInfo(GetAttachmentPath(uid, MESSAGE_FILE_NAME)).Length;

                partList = new BodyPartInfo[1]{info};
                return;
            }

            string htmlBody = string.Empty;
            
            foreach (MessagePart part in indyMsg.MessageParts) {

                if (part is Attachment) {
                    Attachment attachment = (Attachment)part;
                    
                    if (null == attachment.FileName || attachment.FileName.Length > 0) 
                        return;
                    
                    string attachmentPath = GetAttachmentPath(uid, attachment.FileName);

                    attachment.SaveToFile(attachmentPath);

                    string attachmentUrl = GetAttachmentUrl(uid, attachment.FileName);

                    BodyPartInfo partInfo = new BodyPartInfo(attachment.FileName,
                        attachmentUrl, attachment.ContentType, "", 
                        (new FileInfo(attachmentPath)).Length);

                    if (part.ContentID.Length > 0) {
                        contentIDCollection.Add(part.ContentID, attachmentUrl);
                    }

                    bodyPartCollection.Add(partInfo);

                } else if (part.ContentType == CONTENT_TYPE_PLAIN) {
                    
                    plainBody = (part as Text).Body.Text;

                } else if (part.ContentType == CONTENT_TYPE_HTML) {
                    
                    htmlBody = (part as Text).Body.Text;
                    
                }
            }

            if (htmlBody != string.Empty) {

                ReplaceContentIDEntries(contentIDCollection, ref htmlBody);

                string attachmentPath = GetAttachmentPath(uid, MESSAGE_FILE_NAME);
                SaveToFile(uid, htmlBody, attachmentPath);

                BodyPartInfo info = new BodyPartInfo();
                info.ContentType = CONTENT_TYPE_HTML;
                info.FileName = MESSAGE_FILE_NAME;
                info.FileUrl = GetAttachmentUrl(uid, MESSAGE_FILE_NAME);
                info.Size = new FileInfo(GetAttachmentPath(uid, MESSAGE_FILE_NAME)).Length;

                bodyPartCollection.Add(info);
                
            }
            
            partList = new BodyPartInfo[bodyPartCollection.Count];
            bodyPartCollection.CopyTo(partList, 0);
        }
        
        private static void ReplaceContentIDEntries(Dictionary<string, string> contentIDCollection, 
                                                    ref string body)
        {
            foreach (KeyValuePair<string, string> pair in contentIDCollection)
            {
                string oldValue = "cid:" + pair.Key;
                oldValue = oldValue.Replace("<", string.Empty);
                oldValue = oldValue.Replace(">", string.Empty);
                body = body.Replace(oldValue, pair.Value);
            }
        }

        private static EmailAddressItem[] ConvertEmailAddresses(EMailAddressList indyAddressList){
            EmailAddressItem[] result = new EmailAddressItem[indyAddressList.Count];

            for (int i = 0; i < indyAddressList.Count; i++ ) {
                result[i] = indyAddressList[i];
            }

            return result;
        }

        private static void ConvertEmailAddresses(EmailAddressItem[] addresses, ref EMailAddressList list) {
            foreach (EmailAddressItem item in addresses) {
                list.Add().Text = item.Text;
            }
            
            return;
        }
        
        private static void SaveToFile(string uid, string text, string fileName) {
            string path = GetAttachmentPath(uid, fileName);
            using (StreamWriter sw = new StreamWriter(path))
                sw.Write(text);
        }

        private static string GetAttachmentPath(string subDir, string fileName) {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory + "attachments";
            string path = Path.Combine(Path.Combine(baseDir, NormalizePath(subDir)), fileName);
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            return path;
        }

        private static string GetAttachmentUrl(string subDir, string fileName) {
            string baseUrl = HttpUrlHelper.AbsoluteRoot + "/attachments/";
            return Path.Combine(Path.Combine(baseUrl, NormalizePath(subDir)), fileName);
        }

        private static string NormalizePath(string path) {
            foreach (char ch in Path.GetInvalidPathChars())
                path = path.Replace(ch, '_');
            return path + "/";
        }
        
        public static string GenerateMessageUID(string clientDomain) {
            byte[] buffer1 = Guid.NewGuid().ToByteArray();
            
              StringBuilder builder1 = new StringBuilder(buffer1.Length * 2);
              for (int num1 = 0; num1 < buffer1.Length; num1++){
                    builder1.AppendFormat(CultureInfo.InvariantCulture, "{0:x2}", new object[] { buffer1[num1] });
              }

              if (clientDomain == null) {
                  clientDomain = Dns.GetHostName();
              }

              if ((clientDomain == null) || (clientDomain.Length == 0)) {
                  clientDomain = "@localhost";
              } else {
                  clientDomain = "@" + clientDomain.ToLower(CultureInfo.InvariantCulture);
              }
            
              builder1.Append(clientDomain);
              return builder1.ToString();
        }
        
    }
}

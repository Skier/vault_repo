using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Weborb.Samples.Email.Pop3;

namespace Weborb.Samples.Email.Entities
{
    public class MessageInfo
    {
        #region Fields

        //The Unique Email Id for the message in email box. Never changes. (e.g "000012d")
        public string Uid;

        //The value of message header field "Message-ID". (e.g "<JHGFdjHGsfdhgsfdJHGF123123@mydomain.com>")
        //Can be empty!
        public string MessageId;

        public EmailAddressInfo From;
        public EmailAddressInfo[] To;
        public EmailAddressInfo[] Cc;
        public EmailAddressInfo[] Bcc;

        public string Subject;
        public string InReplyTo;
        public int Size;
        public DateTime Sent;
        public bool HasAttachments;
        
        public MessageBodyInfo Body;

        #endregion
        
        public MessageInfo() {
            Uid = string.Empty;
            MessageId = string.Empty;
            From = new EmailAddressInfo();
            To = new EmailAddressInfo[0];
            Subject = string.Empty;
            Body = null;
            HasAttachments = false;
            Body = null;
        }

        public MessageInfo (RxMailMessage mm, string messageUID, int messageSize) {

            Uid = messageUID;
            Size = messageSize;
            To = ConvertMailAddressList(mm.To);
            Cc = ConvertMailAddressList(mm.CC);
            Bcc = ConvertMailAddressList(mm.Bcc);
            Subject = QuotedCoding.Decode(mm.Subject);
            MessageId = mm.MessageId;
            InReplyTo = mm.InReplyTo;
            Sent = mm.DeliveryDate;

            if (null != mm.ContentType) {
                HasAttachments = mm.ContentType.MediaType.Contains("multipart");
            } else {
                HasAttachments = false;
            }

            if (null != mm.From) {
                From = new EmailAddressInfo(mm.From.DisplayName, mm.From.Address);
            } else {
                From = new EmailAddressInfo("unknown sender");
            }
            
            Body = null;
        }

        //Convert MessageInfo value object to System.Net.Mail.MailMessage object
        public MailMessage ToMailMessage(List<FileInfo> uploadedFiles) {
            MailMessage result = new MailMessage();

            foreach (EmailAddressInfo addressInfo in To) {
                result.To.Add(addressInfo);
            }

            foreach (EmailAddressInfo addressInfo in Cc) {
                result.CC.Add(addressInfo);
            }

            foreach (EmailAddressInfo addressInfo in Bcc) {
                result.Bcc.Add(addressInfo);
            }

            result.From = From;
            result.Subject = Subject;

            if (InReplyTo != null && InReplyTo.Length > 0) {
                result.Headers.Set("In-Reply-To", InReplyTo);
                result.Headers.Set("References", InReplyTo);
            }

            result.Headers.Set("Message-ID", GenerateMessageUID(result.From.Host));

            if (null == Body) {
                throw new ArgumentNullException("Body");
            }
            
            if (Body.Views.Length > 1) {
                foreach (ViewInfo viewInfo in Body.Views) {
                    AlternateView view = AlternateView.CreateAlternateViewFromString(viewInfo.Text);
                    view.ContentType.MediaType = viewInfo.ContentType;
                    result.AlternateViews.Add(view);
                }
            } else if (Body.Views.Length == 1) {
                result.Body = (Body.Views[0]).Text;
                if ((Body.Views[0]).ContentType == "text/html") {
                    result.IsBodyHtml = true;
                }
            }

            if (null != uploadedFiles && uploadedFiles.Count > 0) {

                foreach (FileInfo clientFileRef in Body.Attachments) {

                    int iPos = uploadedFiles.FindIndex(delegate(FileInfo f)
                    {
                        return f.Name == clientFileRef.Name;
                    });
                    if (iPos != -1) {
                        FileInfo file = uploadedFiles[iPos];
                        Attachment a = new Attachment(file.Content, file.Name);
                        a.ContentType.MediaType = MediaTypeNames.Application.Octet;
                        a.ContentStream.Position = 0;
                        result.Attachments.Add(a);
                    }
                }
            }

            return result;
        }
        
        private EmailAddressInfo[] ConvertMailAddressList(MailAddressCollection list) {
            EmailAddressInfo[] result = new EmailAddressInfo[list.Count];
            
            for (int i = 0; i < list.Count; i++) {
                result[i] = list[i];
            }

            return result;
        }

        private string GenerateMessageUID(string clientDomain) {
            byte[] buffer1 = Guid.NewGuid().ToByteArray();

            StringBuilder builder1 = new StringBuilder(buffer1.Length * 2);
            for (int num1 = 0; num1 < buffer1.Length; num1++) {
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
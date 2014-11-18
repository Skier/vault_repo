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

        private string m_uid;
        private string m_messageId;
        private EmailAddressInfo m_from;
        private EmailAddressInfo[] m_to;
        private EmailAddressInfo[] m_cc;
        private EmailAddressInfo[] m_bcc;
        private string m_subject;
        private string m_inReplyTo;
        private int m_size;
        private DateTime m_sent;
        private bool m_hasAttachments;
        private MessageBodyInfo m_body;

        #endregion
        
        public MessageInfo() {
            m_uid = string.Empty;
            m_messageId = string.Empty;
            m_from = new EmailAddressInfo();
            m_to = new EmailAddressInfo[0];
            m_subject = string.Empty;
            m_body = null;
            m_hasAttachments = false;
            m_body = null;
        }

        public MessageInfo (RxMailMessage mm, string messageUID, int messageSize) {

            Uid = messageUID;
            Size = messageSize;
            To = ConvertMailAddressList(mm.To);
            Cc = ConvertMailAddressList(mm.CC);
            Bcc = ConvertMailAddressList(mm.Bcc);
            
            try {
                Subject = QuotedCoding.Decode(mm.Subject);
            } catch {
                Subject = mm.Subject;
            }
            
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
                From.Name = QuotedCoding.Decode(mm.From.DisplayName);
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

            if (null != Body.HtmlBody && Body.HtmlBody.Text.Length > 0) {
                AlternateView view = AlternateView.CreateAlternateViewFromString(Body.HtmlBody.Text);
                view.ContentType.MediaType = MessageBodyInfo.CONTENT_TYPE_HTML;
                result.AlternateViews.Add(view);
            }

            if (null != Body.PlainBody && Body.PlainBody.Length > 0) {
                AlternateView view = AlternateView.CreateAlternateViewFromString(Body.PlainBody);
                view.ContentType.MediaType = MessageBodyInfo.CONTENT_TYPE_PLAIN;
                view.TransferEncoding = TransferEncoding.Base64;
                result.AlternateViews.Add(view);
            }
            
            if (null != uploadedFiles && uploadedFiles.Count > 0) {

                foreach (FileInfo clientFileRef in Body.Attachments) {

                    int iPos = uploadedFiles.FindIndex(delegate(FileInfo f) {
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
                result[i].Name = QuotedCoding.Decode(result[i].Name);
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

        public string Uid {
            get { return m_uid; }
            set { m_uid = value; }
        }

        public string MessageId {
            get { return m_messageId; }
            set { m_messageId = value; }
        }

        public EmailAddressInfo From {
            get { return m_from; }
            set { m_from = value; }
        }

        public EmailAddressInfo[] To {
            get { return m_to; }
            set { m_to = value; }
        }

        public EmailAddressInfo[] Cc {
            get { return m_cc; }
            set { m_cc = value; }
        }

        public EmailAddressInfo[] Bcc {
            get { return m_bcc; }
            set { m_bcc = value; }
        }

        public string Subject {
            get { return m_subject; }
            set { m_subject = value; }
        }

        public string InReplyTo {
            get { return m_inReplyTo; }
            set { m_inReplyTo = value; }
        }

        public int Size {
            get { return m_size; }
            set { m_size = value; }
        }

        public DateTime Sent {
            get { return m_sent; }
            set { m_sent = value; }
        }

        public bool HasAttachments {
            get { return m_hasAttachments; }
            set { m_hasAttachments = value; }
        }

        public MessageBodyInfo Body {
            get { return m_body; }
            set { m_body = value; }
        }
    }
}
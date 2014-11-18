using System;
using System.Collections;
using System.IO;
using System.Net.Mail;
using System.Text;
using Weborb.Samples.Email.Pop3;

namespace Weborb.Samples.Email.Entities
{
    public class MessageBodyInfo
    {
        #region Constants

        internal const string CONTENT_TYPE_PLAIN = "text/plain";
        internal const string CONTENT_TYPE_HTML = "text/html";

        #endregion

        #region Fields

        private static string BODY_PART_REQUEST_URL_TEMPLATE = HttpUrlHelper.AbsoluteRoot + "/bodypart.aspx" +
                                                               "?uid={0}&name={1}&type={2}";

        private string m_plainBody;
        private FileInfo m_htmlBody;
        private FileInfo[] m_attachments;

        #endregion

        #region Constructors

        public MessageBodyInfo() {
            m_plainBody = string.Empty;
            m_htmlBody = null;
            m_attachments = new FileInfo[0];
        }

        public MessageBodyInfo (RxMailMessage mm, string messageUID) : this() {

            string safeUID = ReplaceReservedURLSymbols(messageUID);

            if (mm.ContentType.MediaType == CONTENT_TYPE_PLAIN) {
                
                try {
                    Encoding encoder = Encoding.GetEncoding(mm.ContentType.CharSet);
                    m_plainBody = encoder.GetString(Encoding.Default.GetBytes(mm.Body));
                } catch {}
                
            }

            Hashtable resourcesMap = new Hashtable();

            ArrayList attachmentsList = new ArrayList();

            foreach (Attachment attachment in mm.Attachments) {
                FileInfo att = new FileInfo();
                att.Name = attachment.Name;
                att.Url = string.Format(BODY_PART_REQUEST_URL_TEMPLATE, safeUID, att.Name, "a");
                att.Content = (MemoryStream)attachment.ContentStream;

                attachmentsList.Add(att);

                if (null != attachment.ContentId && attachment.ContentId.Length > 1) {
                    resourcesMap.Add(attachment.ContentId, att.Url);
                }
            }

            m_attachments = (FileInfo[])attachmentsList.ToArray(typeof(FileInfo));

            if (null != mm.ContentStream && mm.ContentType.MediaType == CONTENT_TYPE_PLAIN || mm.ContentType.MediaType == CONTENT_TYPE_HTML) {
                mm.AlternateViews.Add(new AlternateView(mm.ContentStream, mm.ContentType));
            }

            foreach (AlternateView view in mm.AlternateViews) {

                if (view.ContentType.MediaType != "text/plain" && view.ContentType.MediaType != "text/html") {
                    continue; //we doesn't support other types
                }

                if (view.ContentType.MediaType == CONTENT_TYPE_PLAIN) {
                    
                    if (m_plainBody == null || m_plainBody.Length == 0) {
                        Encoding encoder = Encoding.Default;

                        try {
                            encoder = Encoding.GetEncoding(view.ContentType.CharSet);
                        } catch {}
                        
                        m_plainBody = encoder.GetString(((MemoryStream)view.ContentStream).ToArray());
                    }

                } else {
                    if (m_htmlBody != null)
                        return;

                    m_htmlBody = new FileInfo();

                    if (resourcesMap.Count > 0) {
                        string htmlPartString = Encoding.ASCII.GetString(((MemoryStream)view.ContentStream).ToArray());

                        foreach (DictionaryEntry entry in resourcesMap) {
                            ReplaceContentIDEntry((string)entry.Key, (string)entry.Value, ref htmlPartString);
                        }
                        
                        MemoryStream stream;
                        SetStreamContent(out stream, htmlPartString);
                        
                        m_htmlBody.Content = stream;
                    } else {
                        m_htmlBody.Content = (MemoryStream)view.ContentStream;
                    }

                    m_htmlBody.Name = "Message.html";
                    m_htmlBody.Url = string.Format(BODY_PART_REQUEST_URL_TEMPLATE, safeUID, m_htmlBody.Name, "v");
                }
            }
        }

        #endregion

        #region Methods

        public FileInfo GetAttachmentByName(string name) {
            foreach (FileInfo file in m_attachments) {
                if (file.Name == name) {
                    return file;
                }
            }
            
            throw new Exception(string.Format("Attachment {0} not found", name));
        }

        private string ReplaceReservedURLSymbols(string s) {
            return s.Replace("+", "plus");
        }
        
        private void SetStreamContent(out MemoryStream stream, string newContent) {
            byte[] bytes = Encoding.Default.GetBytes(newContent);

            stream = new MemoryStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }
        
        private void ReplaceContentIDEntry(string contentId, string contentFileUrl, ref string htmlString) {
            string oldValue = "cid:" + contentId;
            oldValue = oldValue.Replace("<", string.Empty);
            oldValue = oldValue.Replace(">", string.Empty);
            htmlString = htmlString.Replace(oldValue, contentFileUrl);
        }

        #endregion

        #region Properties

        public string PlainBody {
            get { return m_plainBody; }
            set { m_plainBody = value; }
        }

        public FileInfo HtmlBody {
            get { return m_htmlBody; }
            set { m_htmlBody = value; }
        }

        public FileInfo[] Attachments {
            get { return m_attachments; }
            set { m_attachments = value; }
        }

        #endregion

    }
}
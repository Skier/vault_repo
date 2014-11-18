using System;
using System.Collections;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Weborb.Samples.Email.Pop3;

namespace Weborb.Samples.Email.Entities
{
    public class MessageBodyInfo
    {

        private const string CONTENT_TYPE_PLAIN = "text/plain";
        private const string CONTENT_TYPE_HTML = "text/html";

        private static string BODY_PART_REQUEST_URL_TEMPLATE = HttpUrlHelper.AbsoluteRoot + "/bodypart.aspx" +
            "?uid={0}&name={1}&type={2}";
        
        public string Uid;
        public string ReplyText;
        public ViewInfo[] Views;
        public FileInfo[] Attachments;
        
        public MessageBodyInfo() {
            Uid = string.Empty;
            ReplyText = string.Empty;
            Views = new ViewInfo[0];
            Attachments = new FileInfo[0];
        }


        public MessageBodyInfo (RxMailMessage mm, string messageUID) {

            Uid = messageUID;
            
            string safeUID = ReplaceReservedURLSymbols(messageUID);
            
            ReplyText = mm.Body;

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

            Attachments = (FileInfo[])attachmentsList.ToArray(typeof(FileInfo));

            if (null != mm.ContentStream && mm.ContentType.MediaType == CONTENT_TYPE_PLAIN || mm.ContentType.MediaType == CONTENT_TYPE_HTML) {
                mm.AlternateViews.Add(new AlternateView(mm.ContentStream, mm.ContentType));
            }

            ArrayList viewsList = new ArrayList();

            foreach (AlternateView view in mm.AlternateViews) {

                if (view.ContentType.MediaType != "text/plain" && view.ContentType.MediaType != "text/html") {
                    continue; //we doesn't support other types
                }

                ViewInfo part = new ViewInfo();

                if (view.ContentType.MediaType == CONTENT_TYPE_PLAIN) {
                    string plainBodyString = Encoding.ASCII.GetString(((MemoryStream)view.ContentStream).ToArray());

                    if (ReplyText.Length == 0) {
                        //This part is plain text. We can use this text when user will make a reply for this message
                        ReplyText = plainBodyString;
                    }

                    plainBodyString = HttpUtility.HtmlEncode(plainBodyString);
                    
                    Regex rgx = new Regex("\r\n|\r|\n");
                    plainBodyString = rgx.Replace(plainBodyString, "<br>");

                    SetStreamContent(out part.Content, plainBodyString);

                } else {

                    if (resourcesMap.Count > 0) {
                        string htmlPartString = Encoding.ASCII.GetString(((MemoryStream)view.ContentStream).ToArray());

                        foreach (DictionaryEntry entry in resourcesMap) {
                            ReplaceContentIDEntry((string)entry.Key, (string)entry.Value, ref htmlPartString);
                        }

                        SetStreamContent(out part.Content, htmlPartString);
                    } else {
                        part.Content = (MemoryStream)view.ContentStream;
                    }
                }

                part.Name = "View " + (viewsList.Count + 1);
                part.Url = string.Format(BODY_PART_REQUEST_URL_TEMPLATE, safeUID, part.Name, "v");
                part.ContentType = view.ContentType.MediaType;

                viewsList.Add(part);
            }

            Views = (ViewInfo[])viewsList.ToArray(typeof(ViewInfo));
        }
        
        public ViewInfo GetViewByName(string name) {
            foreach (ViewInfo view in Views) {
                if (view.Name == name) {
                    return view;
                }
            }
            
            throw new Exception(string.Format("Body part {0} not found", name));
        }

        public FileInfo GetAttachmentByName(string name) {
            foreach (FileInfo file in Attachments) {
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
        
    }
}
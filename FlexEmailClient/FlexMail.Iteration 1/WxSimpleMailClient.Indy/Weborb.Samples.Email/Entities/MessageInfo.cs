using System;

namespace Weborb.Samples.Email.Entities
{
    
    public class MessageInfo
    {
        public const string DEFAULT_CONTENT_TYPE = "text/html";
        
        private string _uid;
        private EmailAddressItem _from;
        private EmailAddressItem[] _to;
        private EmailAddressItem[] _cc;
        private EmailAddressItem[] _bcc;
        private string _subject;
        private string _inReplyTo;
        private int _size;
        private DateTime _sent;
        private string _bodyPlainText;
        private string _attachmentDir;
        private BodyPartInfo[] _bodyParts;

        public MessageInfo()
        {
            _uid = string.Empty;
            _from = new EmailAddressItem();
            _to = new EmailAddressItem[0];
            _subject = string.Empty;
            _bodyPlainText = "";
            _attachmentDir = string.Empty;
            _bodyParts = new BodyPartInfo[0];
        }

        public MessageInfo(string uid, EmailAddressItem from, EmailAddressItem[] to,
                           EmailAddressItem[] cc, EmailAddressItem[] bcc, string subject, string inReplyTo, 
                           int size, DateTime sent, string bodyPlainText, string attachmentId, 
                           BodyPartInfo[] bodyParts)
        {
            _uid = uid;
            _from = from;
            _to = to;
            _cc = cc;
            _bcc = bcc;
            _subject = subject;
            _inReplyTo = inReplyTo;
            _size = size;
            _sent = sent;
            _bodyPlainText = bodyPlainText;
            _attachmentDir = attachmentId;
            _bodyParts = bodyParts;
        }

        public string Uid {
            get { return _uid; }
            set { _uid = value; }
        }

        public EmailAddressItem From {
            get { return _from; }
            set { _from = value; }
        }

        public EmailAddressItem[] To {
            get { return _to; }
            set { _to = value; }
        }

        public EmailAddressItem[] Cc {
            get { return _cc; }
            set { _cc = value; }
        }

        public EmailAddressItem[] Bcc {
            get { return _bcc; }
            set { _bcc = value; }
        }

        public string InReplyTo
        {
            get { return _inReplyTo; }
            set { _inReplyTo = value; }
        }

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }
        
        public string BodyPlainText
        {
            get { return _bodyPlainText; }
            set { _bodyPlainText = value; }
        }

        public DateTime Sent
        {
            get { return _sent; }
            set { _sent = value; }
        }

        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public string AttachmentDir
        {
            get { return _attachmentDir; }
            set { _attachmentDir = value; }
        }

        public BodyPartInfo[] BodyPartList
        {
            get { return _bodyParts; }
            set { _bodyParts = value; }
        }
    }
    
    public class BodyPartInfo 
    {
        private string _fileName;
        private string _fileUrl;
        private string _contentType;
        private string _text;
        
        private long _size;

        public BodyPartInfo()
        {
        }

        public BodyPartInfo(string fileName, string filePath, string contentType, string text, long size) {
            _fileName = fileName;
            _fileUrl = filePath;
            _contentType = contentType;
            _text = text;
            _size = size;
        }

        public BodyPartInfo(string fileName, string filePath, long size)
        {
            _fileName = fileName;
            _fileUrl = filePath;
            _contentType = "";
            _text = "";
            _size = size;
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public long Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public string FileUrl {
            get { return _fileUrl; }
            set { _fileUrl = value; }
        }

        public string ContentType {
            get { return _contentType; }
            set { _contentType = value; }
        }

        public string Text {
            get { return _text; }
            set { _text = value; }
        }

    }
}
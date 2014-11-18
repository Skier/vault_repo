using System;

namespace Weborb.Samples.Email.Entities
{
    
    public class MessageInfo
    {
        public const string DEFAULT_CONTENT_TYPE = "text/html";
        
        private string _uid;
        private string _from;
        private string _to;
        private string _cc;
        private string _bcc;       
        private string _subject;
        private string _inReplyTo;
        private int _size;
        private DateTime _sent;
        private int _accountId;
        private string _bodyPlainText;
        private string _attachmentDir;
        private BodyPartInfo[] _bodyParts;

        public MessageInfo()
        {
            _uid = string.Empty;
            _from = string.Empty;
            _to = string.Empty;
            _subject = string.Empty;
            _accountId = -1;
            _bodyPlainText = "";
            _attachmentDir = string.Empty;
            _bodyParts = new BodyPartInfo[0];
        }

        public MessageInfo(string uid, string from, string to, string cc, string bcc, string subject, string inReplyTo, 
                           int size, DateTime sent, int accountId,
                           string bodyPlainText, string attachmentId, BodyPartInfo[] bodyParts)
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
            _accountId = accountId;
            _bodyPlainText = bodyPlainText;
            _attachmentDir = attachmentId;
            _bodyParts = bodyParts;
        }

        public string Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public string From
        {
            get { return _from; }
            set { _from = value; }
        }

        public string To
        {
            get { return _to; }
            set { _to = value; }
        }

        public string CC
        {
            get { return _cc; }
            set { _cc = value; }
        }

        public string BCC
        {
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

        public int AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
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
using System;

namespace TractInc.TrueTract.Entity
{
    public class ProjectAttachmentInfo
    {
        private const string XML_TEMPLATE = @"<attachment id=""{0}"" typeId=""{1}"">{2}</attachment>";
        
        public int ProjectAttachmentId;
        public int ProjectAttachmentTypeId;
        public int FileId;
        public int ProjectId;

        public FileInfo FileRef;

        public string toXml()
        {
            if (FileRef != null)
            {
                return String.Format(XML_TEMPLATE,
                                     ProjectAttachmentId,
                                     ProjectAttachmentTypeId, FileRef.toXml());
            } else
            {
                return String.Empty;
            }
        }

        public string toSearchString()
        {
            if (FileRef != null)
            {
                return FileRef.toSearchString();
            }
            else
            {
                return String.Empty;
            }
        }
    }
}

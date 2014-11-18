namespace TractInc.TrueTract.Entity
{
    public class ProjectAttachmentInfo
    {
        public int ProjectAttachmentId;
        public int ProjectAttachmentTypeId;
        public int FileId;
        public int ProjectId;

        public FileInfo FileRef;

        private const string XML_TEMPLATE = @"<attachment id=""{0}"" typeId=""{1}"">{2}</attachment>";
        
        public string toXml()
        {
            if (FileRef != null)
            {
                return string.Format(XML_TEMPLATE,
                                     ProjectAttachmentId,
                                     ProjectAttachmentTypeId, FileRef.toXml());
            } else
            {
                return string.Empty;
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
                return string.Empty;
            }
        }
    }
}

using System;

namespace TractInc.TrueTract.Entity
{
public class DocumentAttachmentInfo
{
    private const string XML_TEMPLATE = @"<attachment id=""{0}"" typeId=""{1}"">{2}</attachment>";

    public int DocumentAttachmentId;
    public int DocumentAttachmentTypeId;
    public int DocumentId;
    public int FileId;

    public FileInfo FileRef;

    public string toXml()
    {
        if (FileRef != null)
        {
            return String.Format(XML_TEMPLATE,
                                 DocumentAttachmentId,
                                 DocumentAttachmentTypeId, FileRef.toXml());
        }
        else
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

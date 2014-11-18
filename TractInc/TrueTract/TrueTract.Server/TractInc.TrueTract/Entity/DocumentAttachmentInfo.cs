namespace TractInc.TrueTract.Entity
{
public class DocumentAttachmentInfo
{
    public int DocumentAttachmentId;
    public int DocumentAttachmentTypeId;
    public int DocumentId;
    public int FileId;

    public FileInfo FileRef;

    private const string XML_TEMPLATE = @"<attachment id=""{0}"" typeId=""{1}"">{2}</attachment>";
    public string toXml()
    {
        if (FileRef != null)
        {
            return string.Format(XML_TEMPLATE,
                                 DocumentAttachmentId,
                                 DocumentAttachmentTypeId, FileRef.toXml());
        }
        else
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

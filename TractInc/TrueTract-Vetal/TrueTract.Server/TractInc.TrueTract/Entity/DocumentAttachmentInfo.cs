namespace TractInc.TrueTract.Entity
{
public class DocumentAttachmentInfo
{
    #region Fields

    public int DocumentAttachmentId;
    public int DocumentAttachmentTypeId;
    public int DocumentId;
    
    public string FileName;
    public string FileUrl;

    public string Description;

    #endregion

    #region Constructors

    public DocumentAttachmentInfo() 
    {
    }

    #endregion
    
}
}

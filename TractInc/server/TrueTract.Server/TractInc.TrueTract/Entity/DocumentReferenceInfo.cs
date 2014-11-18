namespace TractInc.TrueTract.Entity
{
public class DocumentReferenceInfo
{
    public int DocumentReferenceId;
    public int DocumentId;
    public int ReferenceId;
    public string Description;

    public int State;
    public int County;
    public int DocTypeId;
    public string DocumentNo;
    public string Volume;
    public string Page;

    public DocumentInfo ReferencedDoc;
}
}

namespace TractInc.TrueTract.Entity
{
public class DocumentsFilterInfo
{
    public int documentId;
    public int stateId;
    public int countyId;
    public int docTypeId;
    public string docNumber;
    public string volume;
    public string page;
    public string seller;
    public string buyer;
    public DateRange createdRange;
    public DateRange signedRange;
    public DateRange filedRange;
    
    public bool isComplete()
    {
        if (stateId == 0 || countyId == 0 || docTypeId == 0)
            return false;

        if ((docNumber != null && docNumber.Length > 0) || ((volume != null && volume.Length > 0) && (page != null && page.Length > 0)))
            return true;

        return false;
    }
}
}

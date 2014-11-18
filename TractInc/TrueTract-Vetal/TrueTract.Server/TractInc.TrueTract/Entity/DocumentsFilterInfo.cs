namespace TractInc.TrueTract.Entity
{
public class DocumentsFilterInfo
{
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
}
}

namespace TractInc.TrueTract.Entity
{
public class SearchItemInfo
{
    public int SearchItemId;
    public int ItemTypeId;
    public int ItemId;
    internal string ItemValue;
    public string ItemXmlValue;

    public SearchItemInfo()
    {
    }

    public SearchItemInfo(int itemTypeId, int itemId, string itemValue, string itemXmlValue)
    {
        ItemTypeId = itemTypeId;
        ItemId = itemId;
        ItemValue = itemValue;
        ItemXmlValue = itemXmlValue;
    }
}
}

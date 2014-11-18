package truetract.domain
{
import mx.collections.ArrayCollection;
import mx.formatters.DateFormatter;
    
[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.ProjectTabDocumentInfo")]
public class ProjectTabDocument
{
    public var ProjectTabId:int;
    public var DocumentId:int;
    public var Description:String;
    public var Remarks:String;

    public var ProjectTabRef:ProjectTab;
    public var DocumentRef:Document;

    public function get DocumentTypeName():String
    {
        return DocumentRef ? DocumentRef.DocumentTypeName : null;
    }

    public function get DateSigned():String
    {
        return DocumentRef ? getDateFormater().format(DocumentRef.DateSigned) : null;
    }

    public function get DateFiled():String
    {
        return DocumentRef ? getDateFormater().format(DocumentRef.DateFiled) : null;
    }

    public function get SellerName():String
    {
        return DocumentRef ? DocumentRef.SellerName : null;
    }

    public function get BuyerName():String
    {
        return DocumentRef ? DocumentRef.BuyerName : null;
    }

    private var _df:DateFormatter;
    private function getDateFormater():DateFormatter
    {
        if (!_df)
        {
            _df = new DateFormatter();
            _df.formatString = "MMM DD YYYY";
        }
        return _df;
    };

}
}
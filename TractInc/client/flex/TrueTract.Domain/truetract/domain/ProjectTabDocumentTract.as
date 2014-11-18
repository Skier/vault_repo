package truetract.domain
{
import mx.collections.ArrayCollection;
import mx.formatters.DateFormatter;

[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.ProjectTabDocumentTractInfo")]
public class ProjectTabDocumentTract 
{
    public var ProjectTabDocumentTractId:int;
    public var ProjectTabDocumentId:int;
    public var TractId:int;

    public var ProjectTabDocumentRef:ProjectTabDocument;
    public var TractRef:Tract;

}
}
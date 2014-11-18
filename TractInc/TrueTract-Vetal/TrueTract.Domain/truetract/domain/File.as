package truetract.domain
{
[Bindable] [RemoteClass(alias="TractInc.TrueTract.Entity.FileInfo")]
public class File
{
    public var FileId:int;
    public var FileName:String;
    public var FileUrl:String;
    public var FilePath:String;
    public var Description:String;
    public var Created:Date;
    public var CreatedBy:int;
    public var CreatedByName:String;
}
}
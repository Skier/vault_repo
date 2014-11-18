package truetract.domain
{
[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.ClientInfo")]    
public class Client
{
    public var ClientId:int;
    public var Name:String;
    
    public var Projects:Array;
    
    public function get children():Array
    {
        return Projects;
    }
}
}
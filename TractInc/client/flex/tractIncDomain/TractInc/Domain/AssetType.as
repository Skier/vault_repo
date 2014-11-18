package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.AssetType")]
    public class AssetType
    {
        public var AssetTypeId:int;      
        public var TypeName:String;
    }
}
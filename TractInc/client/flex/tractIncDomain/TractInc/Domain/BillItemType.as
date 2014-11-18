package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.BillItemType")]
    public class BillItemType
    {
        public var BillItemTypeId:int;      
        public var TypeName:String;
    }
}
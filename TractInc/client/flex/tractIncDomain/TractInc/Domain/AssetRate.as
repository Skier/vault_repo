package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.AssetRate")]
    public class AssetRate
    {
        public var AssetRateId:int;      
        public var AssetId:int;      
        public var BillItemTypeId:int;      
        public var ContractId:int;      
        public var Rate:Number;
    }
}
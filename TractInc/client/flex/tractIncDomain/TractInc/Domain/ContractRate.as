package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.ContractRate")]
    public class ContractRate
    {
        public var ContractRateId:int;      
        public var ContractId:int;      
        public var InvoiceItemTypeId:int;      
        public var Rate:Number;
    }
}
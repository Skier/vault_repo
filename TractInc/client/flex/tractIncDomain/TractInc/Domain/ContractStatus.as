package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.ContractStatus")]
    public class ContractStatus
    {
        public var ContractStatusId:int;      
        public var StatusName:String;
    }
}
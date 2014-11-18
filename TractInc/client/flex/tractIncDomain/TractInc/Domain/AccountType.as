package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.AccountType")]
    public class AccountType
    {
        public var AccountTypeId:int;      
        public var TypeName:String;
    }
}
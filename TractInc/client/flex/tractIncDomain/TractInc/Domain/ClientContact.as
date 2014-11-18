package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    import TractInc.Domain.Person;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.ClientContact")]
    public class ClientContact
    {
        public var ClientContactId:int;      
        public var ContractId:int;      
        public var PersonId:int;      
        public var StartDate:Date;
        public var EndDate:Date;
        
        public var ContactPerson:Person;
    }
}
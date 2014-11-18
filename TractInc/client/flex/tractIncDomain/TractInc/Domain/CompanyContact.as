package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.CompanyContact")]
    public class CompanyContact
    {
        public var CompanyContactId:int;      
        public var ContractId:int;      
        public var PersonId:int;      
        public var StartDate:Date;
        public var EndDate:Date;

        public var ContactPerson:Person;
    }
}
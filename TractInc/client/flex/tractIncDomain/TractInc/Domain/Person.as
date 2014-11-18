package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.Person")]
    public class Person
    {
        public var PersonId:int;      
        public var ClientId:int;      
        public var CompanyId:int;      
        public var FirstName:String;
        public var MiddleName:String;
        public var LastName:String;
        public var PhoneNumber:String;
        public var Email:String;
        public var SSN:String;
    }
}
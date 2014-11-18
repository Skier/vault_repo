package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.Project")]
    public class Project
    {
        public var ProjectId:int;      
        public var ContractId:int;      
        public var AccountId:int;      
        public var ProjectStatusId:int;      
        public var ProjectName:String;
        public var ShortName:String;
        public var Description:String;
        public var CreatedBy:String;
    }
}
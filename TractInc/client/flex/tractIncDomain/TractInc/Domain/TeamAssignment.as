package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.TeamAssignment")]
    public class TeamAssignment
    {
        public var TeamAssignmentId:int;      
        public var TeamId:int;      
        public var ProjectId:int;      
        public var LeadAssetId:int;      
        public var StartDate:Date;
        public var EndDate:Date;
    }
}
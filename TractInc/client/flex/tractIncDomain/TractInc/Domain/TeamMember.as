package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.TeamMember")]
    public class TeamMember
    {
        public var TeamMemberId:int;      
        public var TeamId:int;      
        public var AssetId:int;      
        public var StartDate:Date;
        public var EndDate:Date;

        public var MemberAsset:Asset;
    }
}
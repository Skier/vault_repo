package tractInc.domain.storage
{
    import mx.rpc.Responder;
    
    import TractInc.Domain.User;
    import TractInc.Domain.Client;
    import TractInc.Domain.Company;
    import TractInc.Domain.TeamAssignment;
    import TractInc.Domain.Contract;
    import TractInc.Domain.AssetRate;
    import TractInc.Domain.Asset;
    import TractInc.Domain.Team;
    import TractInc.Domain.TeamMember;
    import tractInc.domain.packages.StaffManagerPackage;
    
    public interface IStaffManagerStorage
    {
        function getStaffManagerPackage(userId:int, responder:Responder):void;
        
        function getAssetList(userId:int, responder:Responder):void;
        function saveAsset(asset:Asset, responder:Responder):void;
        function removeAsset(asset:Asset, responder:Responder):void;
        
        function getTeamList(userId:int, responder:Responder):void;
        function saveTeam(team:Team, responder:Responder):void;
        function removeTeam(team:Team, responder:Responder):void;
        
        function getTeamMemberList(teamId:int, responder:Responder):void;
        function saveTeamMember(teamMember:TeamMember, responder:Responder):void;
        function removeTeamMember(teamMember:TeamMember, responder:Responder):void;

        function getAssetRateList(assetId:int, responder:Responder):void;
        function saveAssetRates(rates:Array, responder:Responder):void;
        function removeAssetRates(rates:Array, responder:Responder):void;

        function getTeamAssignmentList(teamId:int, responder:Responder):void;
        function saveTeamAssignment(teamAssignment:TeamAssignment, responder:Responder):void;
        function removeTeamAssignment(teamAssignment:TeamAssignment, responder:Responder):void;
    }
}
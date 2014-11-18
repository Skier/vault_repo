package tractInc.domain.storage
{
    import mx.rpc.Responder;
    import mx.rpc.AsyncToken;
    import mx.rpc.remoting.RemoteObject;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;

    import TractInc.Domain.Client;
    import TractInc.Domain.Company;
    import TractInc.Domain.TeamAssignment;
    import TractInc.Domain.Contract;
    
    import TractInc.Domain.AssetRate;
    import TractInc.Domain.Asset;
    import TractInc.Domain.Team;
    import TractInc.Domain.TeamMember;
    import tractInc.domain.packages.StaffManagerPackage;

    public class StaffManagerStorage implements IStaffManagerStorage
    {
        private static const CONTRACT_MANAGER_SERVICE:String = "TractInc.Server.WebOrbServices.StaffManagerService";
        
        private static var _instance:IStaffManagerStorage;
        private var _services:Object = new Object();
        
        public static function get instance():IStaffManagerStorage
        {
            if (_instance == null) {
                _instance = new StaffManagerStorage();
            }
            return _instance;
        }
        
        public function StaffManagerStorage()
        {
            if (_instance != null) {
                throw new Error("Singleton!");
            }
        }
        
        public function getStaffManagerPackage(userId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getStaffManagerService().GetStaffManagerPackage(userId);
            asyncToken.addResponder(responder);
        }
        
        public function getAssetList(userId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getStaffManagerService().GetAssetList(userId);
            asyncToken.addResponder(responder);
        }

        public function saveAsset(asset:Asset, responder:Responder):void
        {
            var asyncToken:AsyncToken = getStaffManagerService().SaveAsset(asset);
            asyncToken.addResponder(responder);
        }
        
        public function removeAsset(asset:Asset, responder:Responder):void
        {
            var asyncToken:AsyncToken = getStaffManagerService().RemoveAsset(asset);
            asyncToken.addResponder(responder);
        }

        public function getTeamList(userId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getStaffManagerService().GetTeamList(userId);
            asyncToken.addResponder(responder);
        }

        public function saveTeam(team:Team, responder:Responder):void
        {
            var asyncToken:AsyncToken = getStaffManagerService().SaveTeam(team);
            asyncToken.addResponder(responder);
        }
        
        public function removeTeam(team:Team, responder:Responder):void
        {
            var asyncToken:AsyncToken = getStaffManagerService().RemoveTeam(team);
            asyncToken.addResponder(responder);
        }

        public function getTeamMemberList(teamId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getStaffManagerService().GetTeamMemberList(teamId);
            asyncToken.addResponder(responder);
        }
        
        public function saveTeamMember(teamMember:TeamMember, responder:Responder):void
        {
            var asyncToken:AsyncToken = getStaffManagerService().SaveTeamMember(teamMember);
            asyncToken.addResponder(responder);
        }
        
        public function removeTeamMember(teamMember:TeamMember, responder:Responder):void
        {
            var asyncToken:AsyncToken = getStaffManagerService().RemoveTeamMember(teamMember);
            asyncToken.addResponder(responder);
        }
        
        public function getAssetRateList(assetId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getStaffManagerService().GetAssetRateList(assetId);
            asyncToken.addResponder(responder);
        }
        
        public function saveAssetRates(rates:Array, responder:Responder):void
        {
            var asyncToken:AsyncToken = getStaffManagerService().SaveAssetRates(rates);
            asyncToken.addResponder(responder);
        }
        
        public function removeAssetRates(rates:Array, responder:Responder):void
        {
            var asyncToken:AsyncToken = getStaffManagerService().RemoveAssetRates(rates);
            asyncToken.addResponder(responder);
        }
        
        public function getTeamAssignmentList(teamId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getStaffManagerService().GetTeamAssignmentList(teamId);
            asyncToken.addResponder(responder);
        }
        
        public function saveTeamAssignment(teamAssignment:TeamAssignment, responder:Responder):void
        {
            var asyncToken:AsyncToken = getStaffManagerService().SaveTeamAssignment(teamAssignment);
            asyncToken.addResponder(responder);
        }
        
        public function removeTeamAssignment(teamAssignment:TeamAssignment, responder:Responder):void
        {
            var asyncToken:AsyncToken = getStaffManagerService().RemoveTeamAssignment(teamAssignment);
            asyncToken.addResponder(responder);
        }
        
        private function getStaffManagerService():RemoteObject {
            return getService(StaffManagerStorage.CONTRACT_MANAGER_SERVICE);
        }

        private function getService(name:String):RemoteObject {
            if (_services[name] == null) {
                var service:RemoteObject = new RemoteObject();
                service.destination = "GenericDestination";
                service.source = name;
                _services[name] = service;
            } 
            return _services[name];
        }
        
    }
}
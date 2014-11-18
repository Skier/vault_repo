package tractInc.domain.storage
{
    import mx.rpc.Responder;
    import mx.rpc.AsyncToken;
    import mx.rpc.remoting.RemoteObject;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;

    import TractInc.Domain.Role;
    import TractInc.Domain.User;
    import tractInc.domain.packages.UserManagerPackage;

    public class UserManagerStorage implements IUserManagerStorage
    {
        private static const USER_MANAGER_SERVICE:String = "TractInc.Server.WebOrbServices.UserManagerService";
        
        private static var _instance:IUserManagerStorage;
        private var _services:Object = new Object();
        
        public static function get instance():IUserManagerStorage
        {
            if (_instance == null) {
                _instance = new UserManagerStorage();
            }
            return _instance;
        }
        
        public function UserManagerStorage()
        {
            if (_instance != null) {
                throw new Error("Singleton!");
            }
        }
        
        public function getUserManagerPackage(userId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getUserManagerService().GetUserManagerPackage(userId);
            asyncToken.addResponder(responder);
        }
        
        public function saveUser(user:User, responder:Responder):void
        {
            var asyncToken:AsyncToken = getUserManagerService().SaveUser(user);
            asyncToken.addResponder(responder);
        }
        
        public function searchUser(login:String, firstName:String, lastName:String,
                roleId:int, isActive:Boolean, companyId:int, clientId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getUserManagerService().SearchUser(
                    login, firstName, lastName, roleId, isActive, companyId, clientId);
            asyncToken.addResponder(responder);
        }
        
        public function saveRole(role:Role, responder:Responder):void
        {
            var asyncToken:AsyncToken = getUserManagerService().SaveRole(role);
            asyncToken.addResponder(responder);
        }
        
        public function removeRole(role:Role, responder:Responder):void
        {
            var asyncToken:AsyncToken = getUserManagerService().RemoveRole(role);
            asyncToken.addResponder(responder);
        }
        
        private function getUserManagerService():RemoteObject {
            return getService(UserManagerStorage.USER_MANAGER_SERVICE);
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
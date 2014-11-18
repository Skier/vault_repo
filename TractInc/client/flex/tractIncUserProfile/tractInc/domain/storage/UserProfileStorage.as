package tractInc.domain.storage
{
    import mx.rpc.Responder;
    import mx.rpc.AsyncToken;
    import mx.rpc.remoting.RemoteObject;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;

    import TractInc.Domain.Person;
    import TractInc.Domain.User;
    import tractInc.domain.packages.UserProfilePackage;

    public class UserProfileStorage implements IUserProfileStorage
    {
        private static const USER_PROFILE_SERVICE:String = "TractInc.Server.WebOrbServices.UserProfileService";
        
        private static var _instance:IUserProfileStorage;
        private var _services:Object = new Object();
        
        public static function get instance():IUserProfileStorage
        {
            if (_instance == null) {
                _instance = new UserProfileStorage();
            }
            return _instance;
        }
        
        public function UserProfileStorage()
        {
            if (_instance != null) {
                throw new Error("Singleton!");
            }
        }
        
        public function getUserProfilePackage(userId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getUserProfileService().GetUserProfilePackage(userId);
            asyncToken.addResponder(responder);
        }
        
        public function changePassword(userId:int, 
                oldPassword:String, 
                newPassword:String,
                responder:Responder):void
        {
            var asyncToken:AsyncToken = getUserProfileService().ChangePassword(
                    userId, oldPassword, newPassword);
            asyncToken.addResponder(responder);
        }

        public function savePerson(person:Person, responder:Responder):void
        {
            var asyncToken:AsyncToken = getUserProfileService().SavePerson(person);
            asyncToken.addResponder(responder);
        }
        
        private function getUserProfileService():RemoteObject {
            return getService(UserProfileStorage.USER_PROFILE_SERVICE);
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
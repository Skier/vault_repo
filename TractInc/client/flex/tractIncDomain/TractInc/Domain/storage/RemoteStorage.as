package TractInc.Domain.storage
{
	import mx.rpc.Responder;
	import mx.rpc.AsyncToken;
	import mx.rpc.remoting.RemoteObject;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;

	import TractInc.Domain.Person;
	import TractInc.Domain.User;
	import TractInc.Domain.packages.DashboardPackage;

	public class RemoteStorage implements ITractStorage
	{
	    private static const DASHBOARD_SERVICE:String = "TractInc.Server.WebOrbServices.DashboardService";
	    
		private static var _instance:RemoteStorage;
		private var _services:Object = new Object();
		
		public static function get instance():RemoteStorage
		{
			if (_instance == null) {
				_instance = new RemoteStorage();
			}
			return _instance;
		}
		
		public function RemoteStorage()
		{
			if (_instance != null) {
				throw new Error("Use instance getter instead of constructor. It's singleton !");
			}
		}
		
// implementation of abstract storage	methods
	    public function ping(responder:Responder):void
	    {
            var asyncToken:AsyncToken = getDashboardService().Ping();
            asyncToken.addResponder(responder);
	    }

        public function login(login:String, password:String, responder:Responder):void
        {
            var asyncToken:AsyncToken = getDashboardService().Login(login, password);
            asyncToken.addResponder(responder);
        }
        	    
        public function signup(person:Person, login:String, password:String, responder:Responder):void
        {
            var asyncToken:AsyncToken = getDashboardService().SignUp(person, login, password);
            asyncToken.addResponder(responder);
        }
        	    
        public function restorePassword(login:String, responder:Responder):void
        {
            var asyncToken:AsyncToken = getDashboardService().RestorePassword(login);
            asyncToken.addResponder(responder);
        }
        	    
		public function getDashboardPackage(user:User, responder:Responder):void
		{
            var asyncToken:AsyncToken = getDashboardService().GetDashboardPackage(user);
            asyncToken.addResponder(responder);
		}
		
/*		
		public function saveUser(user:User, responder:Responder):void
		{
			var service:RemoteObject = getService("TractInc.Server.WebOrbServices.UserService");
				
            var asyncToken:AsyncToken = service.saveUser(user);
            asyncToken.addResponder(responder);
		}
*/		
        private function getDashboardService():RemoteObject {
			return getService(RemoteStorage.DASHBOARD_SERVICE);
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
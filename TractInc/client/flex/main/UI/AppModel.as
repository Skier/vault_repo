package UI
{
	import TractInc.Domain.User;
	import TractInc.Domain.storage.ITractStorage;
	
	public class AppModel
	{
		public static const APP_VIEW_LOGIN:int = 0;
		public static const APP_VIEW_DASHBOARD:int = 1;
		
		public static var storage:ITractStorage;

		public var currentUser:User;
		
		public function get loggedIn():Boolean 
		{
			return currentUser != null;
		}
	
		public function get loggedOut():Boolean 
		{
			return !loggedIn;
		}
	
	}
}
package AerSysCo.Admin.UI
{
	import AerSysCo.UI.Models.ASCUserUI;
	
	
	
	[Bindable]
	public class AdminModel
	{
		public static const ADMIN_VIEW_LOGIN:int = 0;
		public static const ADMIN_VIEW_DASHBOARD:int = 1;
		
		public var loginInProcess:Boolean = false;
		
		public var currentUser:ASCUserUI;
		
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
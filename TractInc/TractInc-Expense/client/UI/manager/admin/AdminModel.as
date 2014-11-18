package UI.manager.admin
{
	import App.Entity.ManagerDataObject;
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class AdminModel
	{
		
		public static const ADMIN_VIEW_BILLING_SUMMARY:int = 0;
		public static const ADMIN_VIEW_CLIENTS:int 	       = 1;
		public static const ADMIN_VIEW_ASSETS:int          = 2;
		public static const ADMIN_VIEW_MODULES_SUMMARY:int = 3;
		public static const ADMIN_VIEW_MODULES:int         = 4;
		public static const ADMIN_VIEW_PERMISSIONS:int     = 5;
		public static const ADMIN_VIEW_ROLES:int           = 6;
		public static const ADMIN_VIEW_USERS:int           = 7;
		public static const ADMIN_VIEW_RATES:int           = 8;
		public static const ADMIN_VIEW_ASSIGNMENTS:int     = 9;
		
		public var data:ManagerDataObject;
		
		public var assignments:ArrayCollection;
		
	}
	
}

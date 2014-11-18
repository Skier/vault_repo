package UI.manager
{
	import App.Entity.ManagerDataObject;
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class ManagerModel
	{
		
        public static const VIEW_STATE_DASHBOARD:int = 0;
        public static const VIEW_STATE_BILL:int      = 1;
        public static const VIEW_STATE_INVOICE:int   = 2;
        public static const VIEW_STATE_PAYMENT:int   = 3;
        public static const VIEW_STATE_ADMIN:int     = 4;
        
        public var data:ManagerDataObject;
        
        public var assetsHash:Array;
        
        public var assets:ArrayCollection;
        
        public var projects:ArrayCollection;
        
	}
	
}

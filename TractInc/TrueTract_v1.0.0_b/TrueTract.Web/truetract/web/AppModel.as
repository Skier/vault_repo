package truetract.web    
{
    import mx.collections.ArrayCollection;

    import truetract.plotter.domain.Tract;
    import truetract.plotter.domain.User;
    import flash.external.ExternalInterface;

    [Bindable]
    public class AppModel
    {
    	public static const SCOPEMAPPING_BASE_URL:String = "http://www.scopemapping.com/maps/index.cfm?username=";
    	
        public static const WORKFLOW_STATE_LOGOUT:int = 0;
        public static const WORKFLOW_STATE_LOGIN:int = 1;

        public var user:User;

 		public var userModuleList:ArrayCollection;

        public var workflowState:Number;

		public var currentTract:Tract;

        public var tractList:ArrayCollection;

        public var recentTractList:ArrayCollection;

        public var recentDrawingList:ArrayCollection;
        
        public var isBrowserIE:Boolean = ExternalInterface.call("isMSIE");
        
        public var scopemappingUrl:String;
        
        public function reset():void 
        {
	        user              = null;
	 		userModuleList    = null;
	        workflowState     = 0;
			currentTract      = null;
	        tractList         = null;
	        recentTractList   = null;
	        recentDrawingList = null;
        }
    }
}
package tractIncClientApp
{
    import flash.events.Event;
    
    import mx.collections.ArrayCollection;
    import tractIncClientApp.services.ProjectService;
    import truetract.domain.Project;
    import truetract.domain.Document;
    import truetract.domain.Tract;
    
    [Bindable]
    public class ClientModel
    {
        public static const ALL_OPEN_PROJECTS_GROUP_NAME:String = "Current";
        public static const LAST_WEEK_CLOSED_PROJECTS_GROUP_NAME:String = "Closed last week";
        public static const ALL_CLOSED_PROJECTS_GROUP_NAME:String = "Archived";

	    public static const WORKFLOW_STATE_SUMMARY:int = 0;
	    public static const WORKFLOW_STATE_PROJECT:int = 1;
	    public static const WORKFLOW_STATE_DOCUMENT:int = 2;
	    public static const WORKFLOW_STATE_SEARCH:int = 3;

        public var currentProjects:ArrayCollection;
        public var lastWeekProjects:ArrayCollection;
        public var closedProjects:ArrayCollection;
        
        public var projects:ArrayCollection;
        
        public var historyList:ArrayCollection = new ArrayCollection();
        
        public var currentProject:Project;
        public var currentDocument:Document;
        public var currentTract:Tract;
        
        public var searchResult:ArrayCollection;
        public var searchString:String;
        
        public function reset():void 
        {
        	currentProject = null;
        	currentDocument = null;
        	currentTract = null;
        	searchString = null;

        	currentProjects = new ArrayCollection();
        	lastWeekProjects = new ArrayCollection();
        	closedProjects = new ArrayCollection();
        	projects = new ArrayCollection();
        	searchResult = new ArrayCollection();
        }
   }
}
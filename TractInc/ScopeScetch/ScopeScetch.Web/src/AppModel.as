package src    
{
    import mx.collections.ArrayCollection;

    import src.deedplotter.domain.Tract;
    import src.deedplotter.domain.User;

    [Bindable]
    public class AppModel
    {
        public static const WORKFLOW_STATE_LOGOUT:int = 0;
        public static const WORKFLOW_STATE_LOGIN:int = 1;

        public var user:User;

        public var workflowState:Number;

		public var currentTract:Tract;

        public var tractList:ArrayCollection;

        public var recentTractList:ArrayCollection;

        public var recentDrawingList:ArrayCollection;
    }
}
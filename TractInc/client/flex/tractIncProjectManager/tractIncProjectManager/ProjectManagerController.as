package tractIncProjectManager
{
    import flash.events.Event;
    import mx.formatters.DateFormatter;
	import mx.events.ItemClickEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
	import mx.collections.ArrayCollection;
    
    import TractInc.Domain.User;
    import tractInc.domain.packages.ProjectManagerPackage;
    import tractInc.domain.storage.IProjectManagerStorage;
    import tractInc.domain.storage.ProjectManagerStorage;
    
    [Bindable]
    public class ProjectManagerController
    {
        private static var instance:ProjectManagerController = null;
        
        public static function getInstance():ProjectManagerController
        {
            return instance;
        }
        
        public var tabData:Array = [
            {label:"Projects", data:"Projects"}
        ];
        
        public var user:User = null;
        public var model:ProjectManagerModel = null;
        public var storage:IProjectManagerStorage = null;
        public var view:ProjectManagerView = null;

        public function ProjectManagerController():void 
        {
            instance = this;    
        }
        
        public function init(u:User):void 
        {
            user = u;
            model = new ProjectManagerModel();
            storage = ProjectManagerStorage.instance;

            reloadModel(0);
        }

        public function reloadModel(clientId:int):void
        {
            var responder:Responder = new Responder(
                    getProjectManagerPackageResultHandler, 
                    getProjectManagerPackageFaultHandler);
            model.isBusy = true;
            storage.getProjectManagerPackage(user.UserId, clientId, responder);
            
//            reloadContractList();
        }
        
        public function reloadProjectList():void
        {
            var responder:Responder = new Responder(
                    getProjectListResultHandler, 
                    getProjectListFaultHandler);
            model.isBusy = true;
            storage.getProjectList(user.UserId, responder);
        }
        
        public function logout():Boolean 
        {
            if ( model.isBusy ) {
                Alert.show("Project Manager service is running");
                return false;
            } else {
                return true;
            }
        }
        
/*        
        public function tabChanged(event:ItemClickEvent):void 
        {
            view.tabStack.selectedIndex = event.index;
        }
*/        

        private function getProjectManagerPackageResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            model.projectManagerPackage = event.result as ProjectManagerPackage;
            view.projectTabView.controller.init(this);
        }
        
        private function getProjectManagerPackageFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.message);
        }
        
        private function getProjectListResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            model.projectManagerPackage.ProjectList = event.result as Array;
            view.projectTabView.controller.init(this);
        }
        
        private function getProjectListFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.message);
        }
        
        private var _df:DateFormatter;
        public function getDateFormater():DateFormatter
        {
            if (!_df)
            {
                _df = new DateFormatter();
                _df.formatString = "MMM DD YYYY";
            }
            return _df;
        }
    }
}
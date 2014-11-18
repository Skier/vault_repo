package tractIncProjectManager.project
{
    import flash.events.Event;
	import mx.events.ItemClickEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
    import mx.controls.dataGridClasses.DataGridColumn;
	import mx.collections.ArrayCollection;
    
    import TractInc.Domain.Project;
    import tractInc.domain.packages.ProjectManagerPackage;
    import tractInc.domain.storage.IProjectManagerStorage;
    import tractInc.domain.storage.ProjectManagerStorage;
    import tractIncProjectManager.ProjectManagerController;
    import TractInc.Domain.Contract;
    import TractInc.Domain.Client;
    
    [Bindable]
    public class ProjectController
    {
        private static var instance:ProjectController = null;
        
        public static function getInstance():ProjectController
        {
            return instance;
        }

        public var parentController:ProjectManagerController = null;  
        public var view:ProjectView = null;
        private var editView:EditView = null;
        
        public function ProjectController():void 
        {
            instance = this;    
        }

        public function init(pc:ProjectManagerController):void 
        {
            parentController = pc;
            
            view.dataGrid.dataProvider = new ArrayCollection(
                    parentController.model.projectManagerPackage.ProjectList);
                    
            var clients:Array = [null];
            for each (var a:Client in ProjectManagerController.getInstance().model.projectManagerPackage.ClientList) {
                clients.push(a);
            }
            this.view.cbClient.dataProvider = clients;
            this.view.cbClient.labelField = "ClientName";
        }
        
        public function clientLabelFunction(item:Object, column:DataGridColumn):String
        {
            var project:Project = item as Project;
            if (null != project) 
            {
	            var contract:Contract = getContractById(project.ContractId);
	            if (null != contract) 
	            {
		            var client:Client = getClientById(contract.ClientId);
		            if (null != client) 
		            {
			            return client.ClientName;
		            } else 
		            {
		            	return "n/a";
		            }
	            } else 
	            {
	            	return "n/a";
	            }
            } else 
            {
            	return "n/a";
            }
        }
            
        public function getContractById(id:int):Contract
        {
            for each (var item:Contract in ProjectManagerController.getInstance().model.projectManagerPackage.ContractList) {
                if ( id == item.ContractId ) {
                    return item;
                }
            }
            return null;    
        }
            
        public function getClientById(id:int):Client
        {
            for each (var item:Client in ProjectManagerController.getInstance().model.projectManagerPackage.ClientList) {
                if ( id == item.ClientId ) {
                    return item;
                }
            }
            return null;    
        }
            
        public function openProject(project:Project):void
        {
            editView = EditView.open(this, project, true);
        }
        
        public function addButtonOnClickHandler(event:Event):void 
        {
            editView = EditView.open(this, null, true);
        }

        public function searchButtonOnClickHandler(event:Event):void 
        {
            var client:Client = this.view.cbClient.selectedItem as Client;
            if ( null != client) {
                parentController.reloadModel(client.ClientId);
            } else {
                parentController.reloadModel(0);
            }
        }

        public function saveProject(project:Project):void 
        {
            var responder:Responder = new Responder(
                    saveProjectResultHandler, 
                    saveProjectFaultHandler);

            parentController.model.isBusy = true;
            parentController.storage.saveProject(project, responder);
        }
        
        public function removeProject(project:Project):void
        {
            var responder:Responder = new Responder(
                    removeProjectResultHandler, 
                    removeProjectFaultHandler);

            parentController.model.isBusy = true;
            parentController.storage.removeProject(project, responder);
        }
        
        private function saveProjectResultHandler(event:ResultEvent):void 
        {
            parentController.model.isBusy = false;
            editView.close();
            parentController.reloadProjectList();
        }
        
        private function saveProjectFaultHandler(event:FaultEvent):void 
        {
            parentController.model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
        
        private function removeProjectResultHandler(event:ResultEvent):void 
        {
            parentController.model.isBusy = false;
            parentController.reloadProjectList();
        }
        
        private function removeProjectFaultHandler(event:FaultEvent):void 
        {
            parentController.model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
    }
}

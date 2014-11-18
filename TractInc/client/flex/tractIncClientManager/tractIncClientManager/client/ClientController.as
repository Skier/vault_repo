package tractIncClientManager.client
{
    import flash.events.Event;
	import mx.events.ItemClickEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
	import mx.collections.ArrayCollection;
    
    import TractInc.Domain.Client;
    import tractInc.domain.packages.ClientManagerPackage;
    import tractInc.domain.storage.IClientManagerStorage;
    import tractInc.domain.storage.ClientManagerStorage;
    import tractIncClientManager.ClientManagerController;
    
    [Bindable]
    public class ClientController
    {
        private static var instance:ClientController = null;
        
        public static function getInstance():ClientController
        {
            return instance;
        }

        public var parentController:ClientManagerController = null;  
        public var view:ClientView = null;
        private var editView:EditView = null;
        
        public function ClientController():void 
        {
            instance = this;    
        }

        public function init(pc:ClientManagerController):void 
        {
            parentController = pc;
            
            view.masterDataGrid.dataProvider = new ArrayCollection(
                    parentController.model.clientList);
        }

        public function openClient(client:Client):void
        {
            editView = EditView.open(this, client, true);
        }
        
        public function addButtonOnClickHandler(event:Event):void 
        {
//            if ( model.ClientManagerPackage.canManageClients ) {
                editView = EditView.open(this, null, true);
/*                
            } else {
                Alert.show("No permissions to manage Clients");
            }
*/            
        }

        public function saveClient(client:Client):void 
        {
            var responder:Responder = new Responder(
                    saveClientResultHandler, 
                    saveClientFaultHandler);

            parentController.model.isBusy = true;
            parentController.storage.saveClient(client, responder);
        }
        
        public function removeClient(client:Client):void
        {
            var responder:Responder = new Responder(
                    removeClientResultHandler, 
                    removeClientFaultHandler);

            parentController.model.isBusy = true;
            parentController.storage.removeClient(client, responder);
        }
        
        private function saveClientResultHandler(event:ResultEvent):void 
        {
            parentController.model.isBusy = false;
            editView.close();
            parentController.reloadModel();
        }
        
        private function saveClientFaultHandler(event:FaultEvent):void 
        {
            parentController.model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
        
        private function removeClientResultHandler(event:ResultEvent):void 
        {
            parentController.model.isBusy = false;
            parentController.reloadModel();
        }
        
        private function removeClientFaultHandler(event:FaultEvent):void 
        {
            parentController.model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
    }
}
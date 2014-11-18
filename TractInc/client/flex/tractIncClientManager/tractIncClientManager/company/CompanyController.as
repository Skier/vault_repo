package tractIncClientManager.company
{
    import flash.events.Event;
	import mx.events.ItemClickEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
	import mx.collections.ArrayCollection;
    
    import TractInc.Domain.Company;
    import tractInc.domain.packages.ClientManagerPackage;
    import tractInc.domain.storage.IClientManagerStorage;
    import tractInc.domain.storage.ClientManagerStorage;
    import tractIncClientManager.ClientManagerController;
    
    [Bindable]
    public class CompanyController
    {
        private static var instance:CompanyController = null;
        
        public static function getInstance():CompanyController
        {
            return instance;
        }

        public var parentController:ClientManagerController = null;  
        public var view:CompanyView = null;
        private var editView:EditView = null;
        
        public function CompanyController():void 
        {
            instance = this;    
        }

        public function init(pc:ClientManagerController):void 
        {
            parentController = pc;
            
            view.masterDataGrid.dataProvider = new ArrayCollection(
                    parentController.model.companyList);
        }

        public function openCompany(company:Company):void
        {
            editView = EditView.open(this, company, true);
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

        public function saveCompany(company:Company):void 
        {
            var responder:Responder = new Responder(
                    saveCompanyResultHandler, 
                    saveCompanyFaultHandler);

            parentController.model.isBusy = true;
            parentController.storage.saveCompany(company, responder);
        }
        
        public function removeCompany(company:Company):void
        {
            var responder:Responder = new Responder(
                    removeCompanyResultHandler, 
                    removeCompanyFaultHandler);

            parentController.model.isBusy = true;
            parentController.storage.removeCompany(company, responder);
        }
        
        private function saveCompanyResultHandler(event:ResultEvent):void 
        {
            parentController.model.isBusy = false;
            editView.close();
            parentController.reloadModel();
        }
        
        private function saveCompanyFaultHandler(event:FaultEvent):void 
        {
            parentController.model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
        
        private function removeCompanyResultHandler(event:ResultEvent):void 
        {
            parentController.model.isBusy = false;
            parentController.reloadModel();
        }
        
        private function removeCompanyFaultHandler(event:FaultEvent):void 
        {
            parentController.model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
    }
}

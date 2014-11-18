package tractIncStaffManager.asset
{
    import flash.events.Event;
    import mx.events.ItemClickEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
    import mx.controls.dataGridClasses.DataGridColumn;
    import mx.collections.ArrayCollection;
    
    import TractInc.Domain.Asset;
    import tractInc.domain.packages.StaffManagerPackage;
    import tractInc.domain.storage.IStaffManagerStorage;
    import tractInc.domain.storage.StaffManagerStorage;
    import tractIncStaffManager.StaffManagerController;
    
    [Bindable]
    public class AssetController
    {
        private static var instance:AssetController = null;
        
        public static function getInstance():AssetController
        {
            return instance;
        }

        public var parentController:StaffManagerController = null;  
        public var view:AssetView = null;
        private var editView:EditView = null;
        
        public function AssetController():void 
        {
            instance = this;    
        }

        public function init(pc:StaffManagerController):void 
        {
            parentController = pc;
            view.dataGrid.dataProvider = new ArrayCollection(
                    parentController.model.staffManagerPackage.AssetList);
        }

        public function openAsset(asset:Asset):void
        {
            editView = EditView.open(this, asset, true);
        }
        
        public function addButtonOnClickHandler(event:Event):void 
        {
                editView = EditView.open(this, null, true);
        }

        public function saveAsset(account:Asset):void 
        {
            var responder:Responder = new Responder(
                    saveAssetResultHandler, 
                    saveAssetFaultHandler);

            parentController.model.isBusy = true;
            parentController.storage.saveAsset(account, responder);
        }
        
        public function removeAsset(account:Asset):void
        {
            var responder:Responder = new Responder(
                    removeAssetResultHandler, 
                    removeAssetFaultHandler);

            parentController.model.isBusy = true;
            parentController.storage.removeAsset(account, responder);
        }
        
        private function saveAssetResultHandler(event:ResultEvent):void 
        {
            parentController.model.isBusy = false;
            editView.close();
            parentController.reloadAssetList();
        }
        
        private function saveAssetFaultHandler(event:FaultEvent):void 
        {
            parentController.model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
        
        private function removeAssetResultHandler(event:ResultEvent):void 
        {
            parentController.model.isBusy = false;
            parentController.reloadAssetList();
        }
        
        private function removeAssetFaultHandler(event:FaultEvent):void 
        {
            parentController.model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
    }
}

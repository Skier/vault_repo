package tractIncStaffManager.asset.rate
{
    import flash.events.Event;
    import mx.events.ItemClickEvent;
    import mx.events.DataGridEvent;
    import mx.events.DataGridEventReason;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
    import mx.controls.dataGridClasses.DataGridColumn;
    import mx.collections.ArrayCollection;
    
    import TractInc.Domain.Asset;
    import TractInc.Domain.AssetRate;
    import TractInc.Domain.BillItemType;
    import TractInc.Domain.Contract;
    import tractInc.domain.packages.StaffManagerPackage;
    import tractInc.domain.storage.IStaffManagerStorage;
    import tractInc.domain.storage.StaffManagerStorage;
    import tractIncStaffManager.asset.AssetController;
    import tractIncStaffManager.StaffManagerController;
    
    [Bindable]
    public class AssetRateController
    {
        private static var instance:AssetRateController = null;
        
        public static function getInstance():AssetRateController
        {
            return instance;
        }

        public var parentController:AssetController = null;  
        public var asset:Asset = null;
        public var view:AssetRateView = null;
        private var editView:EditView = null;
        
        public function AssetRateController():void 
        {
            instance = this;    
        }

        public function init(a:Asset, pc:AssetController):void 
        {
            asset = a;
            parentController = pc;
            if ( null != asset ) {
                prepareContractList();
            }
        }

        public function isContractRatesExists(contractId:int):Boolean {
            var c:ArrayCollection = this.getContractWithRates();
            for each (var co:Contract in c) {
                if ( contractId == co.ContractId ) {
                    return true;
                }
            }
            return false;
        }
        
        public function getContractWithRates():ArrayCollection {
            var dict:Object = new Object();
            var cl:ArrayCollection = new ArrayCollection();
            
            for each (var ar:AssetRate in asset.AssetRateList) {
                if ( null == dict[ar.ContractId.toString()] ) {
                    dict[ar.ContractId.toString()] = ar.ContractId.toString();
                    if ( 0 == ar.ContractId ) {
                        var noContract:Contract = new Contract();
                        noContract.ContractName = "General";
                        cl.addItem(noContract);
                    } else {
                        cl.addItem(this.getContractById(ar.ContractId));
                    }
                }
            }
            
            return cl;
        }
        
        public function prepareContractList():void {
            view.masterDataGrid.dataProvider = getContractWithRates();
        }
        
        public function getAssetRatesByContract(contract:Contract):ArrayCollection {
            var cl:ArrayCollection = new ArrayCollection();
            for each (var ar:AssetRate in asset.AssetRateList) {
                if ( contract.ContractId == ar.ContractId ) {
                    cl.addItem(ar);        
                }
            }
            return cl;    
        }
        
        public function prepareAssetRateList(contract:Contract):void {
            view.detailDataGrid.dataProvider = getAssetRatesByContract(contract);
        }
        
        public function getBillItemTypeById(id:int):BillItemType
        {
            for each (var a:BillItemType in StaffManagerController.getInstance().model.staffManagerPackage.BillItemTypeList) {
                if ( id == a.BillItemTypeId ) {
                    return a;
                }
            }
            return null;    
        }

        public function billItemTypeLabelFunction(item:Object, column:DataGridColumn):String
        {
            var assetRate:AssetRate = item as AssetRate;
            var billItemType:BillItemType = getBillItemTypeById(assetRate.BillItemTypeId);
            return billItemType.TypeName;
        }
        
        public function getContractById(id:int):Contract
        {
            for each (var item:Contract in StaffManagerController.getInstance().model.staffManagerPackage.ContractList) {
                if ( id == item.ContractId ) {
                    return item;
                }
            }
            return null;    
        }
            
        public function contractLabelFunction(item:Object, column:DataGridColumn):String
        {
            var assetRate:AssetRate = item as AssetRate;
            var contract:Contract = getContractById(assetRate.ContractId);
            return (null != contract ? contract.ContractName : "");
        }
        
        public function masterGridOnClickHandler(event:Event):void
        {
            var contract:Contract = view.masterDataGrid.selectedItem as Contract;
            if ( null != contract ) {
                prepareAssetRateList(contract);
            }
        }
            
        public function itemOnEditEndHandler(event:DataGridEvent):void
        {
            if (event.reason == DataGridEventReason.CANCELLED)
            {
                return;
            }
/*            
            var dataGrid:DataGrid = event.currentTarget as DataGrid;
            var dataProvider:ArrayCollection = dataGrid.dataProvider as ArrayCollection;
            var idx:int = event.rowIndex;
            
            if (idx > -1) {
                var cr:ContractRate = dataProvider.getItemAt(idx) as ContractRate;
                var input:TextInput = TextInput(dataGrid.itemEditorInstance);
                cr.Rate = new Number(input.text);
            }
*/            
        }
            
        public function openAssetRates(contract:Contract):void
        {
            editView = EditView.open(this, contract, true);
        }
        
        public function addButtonOnClickHandler(event:Event):void 
        {
                editView = EditView.open(this, null, true);
        }

        public function saveAssetRates(arc:ArrayCollection):void 
        {
            var rates:Array = new Array();
            for each (var ar:AssetRate in arc) {
                ar.AssetId = this.asset.AssetId;
                rates.push(ar);
            }
                
            var responder:Responder = new Responder(
                    saveAssetRateResultHandler, 
                    saveAssetRateFaultHandler);

            StaffManagerController.getInstance().model.isBusy = true;
            StaffManagerController.getInstance().storage.saveAssetRates(rates, responder);
        }
        
        public function removeAssetRates(contract:Contract):void
        {
            var rates:Array = new Array();
            for each (var ar:AssetRate in getAssetRatesByContract(contract)) {
                rates.push(ar);
            }
            
            var responder:Responder = new Responder(
                    removeAssetRateResultHandler, 
                    removeAssetRateFaultHandler);
            StaffManagerController.getInstance().model.isBusy = true;
            StaffManagerController.getInstance().storage.removeAssetRates(rates, responder);
        }
        
        public function reloadAssetRateList():void
        {
            var responder:Responder = new Responder(
                    reloadAssetRateListResultHandler, 
                    reloadAssetRateListFaultHandler);

            StaffManagerController.getInstance().model.isBusy = true;
            StaffManagerController.getInstance().storage.getAssetRateList(asset.AssetId, responder);
        }
        
        private function saveAssetRateResultHandler(event:ResultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            editView.close();
            reloadAssetRateList();
        }
        
        private function saveAssetRateFaultHandler(event:FaultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
        
        private function removeAssetRateResultHandler(event:ResultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            reloadAssetRateList();
        }
        
        private function removeAssetRateFaultHandler(event:FaultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
        
        private function reloadAssetRateListResultHandler(event:ResultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            asset.AssetRateList = event.result as Array;
            this.prepareContractList();
            this.view.detailDataGrid.dataProvider = null;
        }
        
        private function reloadAssetRateListFaultHandler(event:FaultEvent):void 
        {
            StaffManagerController.getInstance().model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
    }
}

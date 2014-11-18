package tractIncContractManager.contract.rate
{
    import flash.events.Event;
	import mx.events.ItemClickEvent;
	import mx.events.DataGridEvent;
	import mx.events.DataGridEventReason;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
	import mx.collections.ArrayCollection;
	import mx.controls.DataGrid;
	import mx.controls.dataGridClasses.DataGridColumn;
    
    import TractInc.Domain.Contract;
    import TractInc.Domain.ContractRate;
    import TractInc.Domain.InvoiceItemType;
    import tractInc.domain.packages.ContractPackage;
    import tractInc.domain.packages.ContractManagerPackage;
    import tractInc.domain.storage.IContractManagerStorage;
    import tractInc.domain.storage.ContractManagerStorage;
    import tractIncContractManager.ContractManagerController;
    import tractIncContractManager.contract.ContractController;
    import mx.controls.TextInput;
    
    [Bindable]
    public class RateController
    {
        private static var instance:RateController = null;
        
        public static function getInstance():RateController
        {
            return instance;
        }

        public var contractPackage:ContractPackage = null;  
        public var contract:Contract = null;  
        public var parentController:ContractController = null;  
        public var view:RateView = null;
        
        public function RateController():void 
        {
            instance = this;    
        }

        public function init(cp:ContractPackage, pc:ContractController):void 
        {
            contractPackage = cp;
            if ( null != cp ) {
                contract = cp.Main;
            }
            parentController = pc;
            initRateGrids();
        }

        public function initRateGrids():void
        {
            var rates:Array = new Array();
            if ( null == contractPackage || 0 == contractPackage.ContractRateList.length ) {
                for each (var i:InvoiceItemType in ContractManagerController.getInstance().model.contractManagerPackage.InvoiceItemTypeList) {
                    var c:ContractRate = new ContractRate();
                    c.InvoiceItemTypeId = i.InvoiceItemTypeId;
                    c.Rate = 0.00;
                    rates.push(c);
                }
            } else {
                for each (var c2:ContractRate in contractPackage.ContractRateList) {
                    rates.push(c2);
                }
            }
            
            var countable:ArrayCollection = new ArrayCollection();
            var uncountable:ArrayCollection = new ArrayCollection();
            for each (var cr:ContractRate in rates) {
                var iit:InvoiceItemType = this.getInvoiceItemTypeById(cr.InvoiceItemTypeId);
                if ( iit.IsCountable ) {
                    countable.addItem(cr);
                } else {
                    uncountable.addItem(cr);
                }
            }
            
            view.countableDataGrid.dataProvider = countable;
            view.uncountableDataGrid.dataProvider = uncountable;
        }
        
        public function itemTypeLabelFunction(item:Object, column:DataGridColumn):String
        {
            var rate:ContractRate = item as ContractRate;
            var iit:InvoiceItemType = getInvoiceItemTypeById(rate.InvoiceItemTypeId);
            return iit.TypeName;
        }
            
        public function itemOnEditEndHandler(event:DataGridEvent):void
        {
            if (event.reason == DataGridEventReason.CANCELLED)
            {
                return;
            }
            
            var dataGrid:DataGrid = event.currentTarget as DataGrid;
            var dataProvider:ArrayCollection = dataGrid.dataProvider as ArrayCollection;
            var idx:int = event.rowIndex;
            
            if (idx > -1) 
            {
                var cr:ContractRate = dataProvider.getItemAt(idx) as ContractRate;
                var input:TextInput = TextInput(dataGrid.itemEditorInstance);
                cr.Rate = new Number(input.text);
            }
        }
            
        public function getInvoiceItemTypeById(id:int):InvoiceItemType
        {
            for each (var i:InvoiceItemType in parentController.parentController.model.contractManagerPackage.InvoiceItemTypeList) {
                if ( id == i.InvoiceItemTypeId ) {
                    return i;
                }
            }
            return null;    
        }
        
    }
}

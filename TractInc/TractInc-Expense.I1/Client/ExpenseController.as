package
{
    import flash.net.SharedObject;
    import mx.rpc.remoting.RemoteObject;
    import mx.rpc.events.*;
    import mx.controls.Alert;
    import mx.rpc.AsyncToken;
    import mx.collections.ItemResponder;
    import mx.collections.ArrayCollection;
    import mx.managers.PopUpManager;
    import mx.core.IFlexDisplayObject;

    import mx.utils.UIDUtil;
    import mx.events.ListEvent;
    import flash.events.MouseEvent;
    import mx.managers.CursorManager
    
    import UI.BIllItemView;
    import Domain.Asset;
    import Domain.AssetDataMapper;
    import Domain.DataMapperRegistry;
    import Domain.BillItem;


            
    
    [Bindable]
    public class ExpenseController
    {
        public var view:Expense;
        public var model:ExpenseModel;
        private var remoteObject:RemoteObject;
        private var newBillItemWindow:BIllItemView;
        private var newBillItemModel:BillItem;
        
        public function ExpenseController(view:Expense):void {
            this.view = view;
            model = new ExpenseModel();
            
            remoteObject = new RemoteObject( "GenericDestination" );
            remoteObject.source = "TractInc.Expense.Service.ExpenseService";
            remoteObject.GenerateDeviceId.addEventListener(ResultEvent.RESULT, OnGenerateDeviceIdSuccess);
            remoteObject.GenerateDeviceId.addEventListener(FaultEvent.FAULT, OnGenerateDeviceIdFault);  
            remoteObject.Sync.addEventListener(ResultEvent.RESULT, onSyncSuccess);
            remoteObject.Sync.addEventListener(FaultEvent.FAULT, onSyncFalied);     
        }
        
        public function OnCreationComplete():void {
            model.restoreFromSharedObject();
        }
        
        public function onSyncSuccess(event:ResultEvent):void
        {
/*        	
            var arr:Array = event.result as Array;
            
            for (var i:int = 0; i < arr.length; ++i) {
                
                var billItem:Billitem = Billitem(arr[i]);
                
                if (billItem.Status == 'SUBMITTED'){
            
                    //TODO:  Will need to optimize it.  This will be slow
                    //   for now just quick and dirty
                    var j:int;
                    
                    for(j= 0; j < model.newBillItems.length; j++){
                        var billItemSrc:Billitem = Billitem(model.newBillItems[j]);
                        if (billItemSrc.ClientUID == billItem.ClientUID){
                            billItemSrc.Status = 'SUBMITTED';
                            billItemSrc.idbillItem = billItem.idbillItem;
                            break;
                        }
                    }
                    
                    if ( j == model.newBillItems.length){  // Did not find
                        // Now lets look may be it is in corrected
                        var k:int;
                        for(k= 0; k < model.billItems.length; k++){
                            var billItemSrc:Billitem = Billitem(model.billItems[k]);
                            if (billItemSrc.idbillItem == billItem.idbillItem){
                                billItemSrc.Status = 'SUBMITTED';
                                break;
                            }
                        }
                        
                        if (k == model.billItems.length){  // DID not find
                            this.model.billItems.addItem(arr[i]);
                        }   
                    }
                    
                } 
                else{ 
                    if (billItem.Status == 'APPROVED' || billItem.Status == 'REJECTED'){
                        var j:int;
                        
                        for(j= 0; j < model.billItems.length; j++){
                            var billItemSrc:Billitem = Billitem(model.billItems[j]);
                            if (billItemSrc.idbillItem == billItem.idbillItem){
                                billItemSrc.Status = billItem.Status;
                                break;
                            }   
                        }
                        
                        if (j == model.billItems.length){
                            this.model.billItems.addItem(arr[i]);
                        }
                    }
                    else
                    {
                        this.model.billItems.addItem(arr[i]);
                    }
                }   
            }
            
            model.newBillItems.removeAll();
            model.correctedBillItems.removeAll();
            model.saveToSharedObject();
*/         
            CursorManager.removeBusyCursor();
        }
        
        public function onSyncFalied(event:FaultEvent):void
        {
            CursorManager.removeBusyCursor();
            Alert.show(event.fault.message);
        }
        
        public function onSyncSubmit():void {
            CursorManager.setBusyCursor();
            remoteObject.Sync(model.clientId, model.asset.AssetId, model.newBillItems.toArray(), model.correctedBillItems.toArray());
        }
        
        public function OnLoginSubmit():void {
            this.model.isLoggedIn = true;
            model.asset.AssetId = 1;
            this.model.currentState = "";
            
            if (model.clientId == null){
                CursorManager.setBusyCursor();
                var tocken:mx.rpc.AsyncToken = remoteObject.GenerateDeviceId();  
            }
        }
        
        public function onAddNewBillItemClick():void{
            this.newBillItemModel = new BillItem();
//            newBillItemModel..AssetId = this.model.asset.AssetId;
            
            newBillItemWindow = UI.BIllItemView(PopUpManager.createPopUp(this.view, UI.BIllItemView,true));
            
            newBillItemWindow.controller = this;
            newBillItemWindow.billItem = newBillItemModel;
            newBillItemWindow.btnSubmit.addEventListener(MouseEvent.CLICK, onCloseNewBillItemClick)
            PopUpManager.centerPopUp(newBillItemWindow);
        }
        
        public function onCloseNewBillItemClick(event:MouseEvent):void{
//            newBillItemModel.AssetId = int(newBillItemWindow.txtAssetId.text);
            newBillItemModel.BillingDate = new Date(Date.parse(newBillItemWindow.txtBillingDate.text));
//            newBillItemModel.DayQty = newBillItemWindow.txtDayQty.text;
//            newBillItemModel.DateQty = int(newBillItemWindow.txtDateQty.text);
            newBillItemModel.Status = "NEW";
            
            PopUpManager.removePopUp(newBillItemWindow);
            
            model.billItems.addItem(newBillItemModel);
            model.newBillItems.addItem(newBillItemModel);
            model.saveToSharedObject();
        }
        
        public function OnLogoutSubmit():void{
            this.model.isLoggedIn = false;
            this.model.currentState = "Login";
        }
        
        public function onItemClickEvent(event:ListEvent):void {
            this.newBillItemModel = this.model.billItems[event.rowIndex -1];
            if (newBillItemModel.Status == "SUBMITTED" || newBillItemModel.Status == "APPROVED"){
                return;
            }
            newBillItemWindow = UI.BIllItemView(PopUpManager.createPopUp(this.view, UI.BIllItemView,true));
            newBillItemWindow.controller = this;
            this.newBillItemModel = this.model.billItems[event.rowIndex -1];
            newBillItemWindow.billItem = this.newBillItemModel 
            newBillItemWindow.btnSubmit.addEventListener(MouseEvent.CLICK, onCloseModifyBillItem);
            PopUpManager.centerPopUp(newBillItemWindow);
            
        }
        
        public function onCloseModifyBillItem(event:MouseEvent):void{
            
//            newBillItemModel.AssetId = int(newBillItemWindow.txtAssetId.text);
            newBillItemModel.BillingDate = new Date(Date.parse(newBillItemWindow.txtBillingDate.text));
//            newBillItemModel.DayQty = newBillItemWindow.txtDayQty.text;
//            newBillItemModel.DateQty = int(newBillItemWindow.txtDateQty.text);
            this.model.correctedBillItems.addItem(newBillItemModel);
            if (newBillItemModel.Status == "REJECTED"){
                newBillItemModel.Status = "CORRECTED";
            }
            
            PopUpManager.removePopUp(newBillItemWindow);
            model.saveToSharedObject();
        }
        
        public function onClickSharedObjectClear():void {
            model.clearSharedObject()
        }

		// Remote method call backs
        private function OnGenerateDeviceIdSuccess(event:ResultEvent):void
        {
            CursorManager.removeBusyCursor();
            model.clientId = String(event.result);
            model.saveToSharedObject();
        }
        
        private function OnGenerateDeviceIdFault(event:FaultEvent):void 
        {
            CursorManager.removeBusyCursor();
            Alert.show(event.fault.message);
        }
    }
}
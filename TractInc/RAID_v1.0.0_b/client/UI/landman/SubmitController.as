package UI.landman
{
	
    import App.Domain.*;
    import mx.collections.ArrayCollection;
    import weborb.data.DynamicLoadEvent;
    import weborb.data.ActiveCollection;
    import common.StatusesRegistry;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
    import mx.managers.PopUpManager;

    [Bindable]
    public class SubmitController
    {
        
        public var view: SubmitView;
        public var Model: SubmitModel = new SubmitModel();
        public var mainApp: DiaryController;
        public var mainModel: DiaryModel;
        
        public function SubmitController(view: SubmitView, parent: DiaryController): void {
            this.view = view;
            mainApp = parent;
            mainModel = mainApp.Model;
        }
        
        public function submitBills():void {
        	for each (var bill:Bill in Model.bills) {
        		if (bill.toSubmit) {
        			view.enabled = false;
					var processor:BillSubmitProcessor = new BillSubmitProcessor(bill, new Responder(onBillSaved, onFault));
        		}
        	}
        }
        
        private function onBillSaved(b:Bill):void {
         	b.isSaved = true;

        	for each (var bill:Bill in Model.bills) {
        		if (bill.toSubmit && !bill.isSaved) {
        			return;
        		}
        	}

			mainApp.LoadBillItems();
    		PopUpManager.removePopUp(view);
        }
        
        public function selectAll(flag:Boolean):void {
        	for each (var bill:Bill in Model.bills) {
        		bill.toSubmit = flag;
        	}
        }
        
        private function onFault(event:FaultEvent):void {
        	Alert.show(event.fault.message);
        	view.enabled = true;
        }
        
    }

}

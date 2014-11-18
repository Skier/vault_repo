package UI.manager.bill
{

    import mx.managers.PopUpManager;
    import mx.collections.ArrayCollection;
    import util.ArrayUtil;
    import mx.managers.PopUpManager;
    import mx.events.ListEvent;
    import flash.events.EventDispatcher;
    import flash.events.Event;
    import common.StatusesRegistry;
    import mx.controls.Alert;
    import App.Entity.BillDataObject;

    [Bindable]
    public class DetailController extends EventDispatcher
    {

        public var view:DetailView;
        public var model:SummaryItem;
        public var parent:SummaryController;
        
        public var sr:StatusesRegistry = StatusesRegistry.instance;

        public function DetailController(detailView:DetailView, bill:BillDataObject, summaryController:SummaryController): void {
            view = detailView;
            parent = summaryController;
            
            model = new SummaryItem(
            	bill,
            	parent.parentController.appController.Model.afesHash,
            	parent.parentController.appController.Model.projectsHash,
            	parent.parentController.model.data.Assignments);
            
            model.load();
            model.addEventListener("bill_is_broken", onBillLoadFault);
        }
        
        public function cancel():void 
        {
			parent.resetBill(model.bill);
        	PopUpManager.removePopUp(view);
        }

        public function submit():void 
        {
        	parent.updateBill(model.bill);
        	PopUpManager.removePopUp(view);
        }
        
        private function onBillLoadFault(e:*):void {
            model.removeEventListener("bill_is_broken", onBillLoadFault);
        	Alert.show("current bill is incorrect. please contact admin.");
        }

    }

}

package UI.crew
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
            	parent.mainApp.mainApp.Model.afesHash,
            	parent.mainApp.mainApp.Model.projectsHash,
            	parent.model.data.Assignments);
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
        	Alert.show("Current bill is incorrect. Please contact admin.", "System Error");
        }

    }

}

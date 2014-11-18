package UI.manager.bill
{

    import App.Domain.*;
    import mx.managers.PopUpManager;
    import weborb.data.DynamicLoadEvent;
    import mx.collections.ArrayCollection;
    import util.ArrayUtil;
    import weborb.data.ActiveCollection;
    import mx.managers.PopUpManager;
    import mx.events.ListEvent;
    import flash.events.EventDispatcher;
    import flash.events.Event;
    import common.StatusesRegistry;
    import mx.controls.Alert;

    [Bindable]
    public class DetailController extends EventDispatcher
    {

        public var view:DetailView;
        public var model:SummaryItem;
        public var parent:SummaryController;
        
        public var sr:StatusesRegistry = StatusesRegistry.getInstance();

        public function DetailController(detailView:DetailView, bill:Bill, summaryController:SummaryController): void {
            view = detailView;
            parent = summaryController;
            model = new SummaryItem(bill);
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

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
    public class DetailControllerRO extends EventDispatcher
    {

        public var view:DetailViewRO;
        public var model:SummaryItem;
        public var parent:SummaryController;
        
        public var sr:StatusesRegistry = StatusesRegistry.instance;

        public function DetailControllerRO(detailView:DetailViewRO, bill:BillDataObject, summaryController:SummaryController): void {
            view = detailView;
            parent = summaryController;
            model = new SummaryItem(
            	bill,
            	parent.mainApp.mainApp.Model.afesHash,
            	parent.mainApp.mainApp.Model.projectsHash,
            	parent.model.data.Assignments);
            model.addEventListener("bill_is_broken", onBillLoadFault);
        }
        
        public function close():void 
        {
        	PopUpManager.removePopUp(view);
        }

        private function onBillLoadFault(e:*):void {
            model.removeEventListener("bill_is_broken", onBillLoadFault);
        	Alert.show("current bill is incorrect. please contact admin.");
        }

    }

}

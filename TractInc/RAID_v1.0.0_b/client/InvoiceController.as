package 
{

    import mx.binding.utils.BindingUtils;
    import mx.formatters.DateFormatter;
    import flash.display.DisplayObject;
    import App.Domain.*;
    import UI.landman.*;
    import weborb.data.DynamicLoadEvent;
    import mx.controls.Alert;
    import weborb.data.ActiveCollection;
    import UI.crew.SummaryItem;
    import UI.crew.SummaryModel;
    import mx.collections.ArrayCollection;
    import UI.crew.SummaryView;

    public class InvoiceController
    {

        [Bindable]
        public var model: InvoiceModel = new InvoiceModel();
        
        [Bindable]
        public var mainApp: AppController;
        private var view: InvoiceView;

        public function InvoiceController(view: InvoiceView, parent: AppController): void {
            this.view = view;
            mainApp = parent;
        }

        public function open(): void {
        	view.enabled = false;
        	model = new InvoiceModel();
        	var asset: Asset = mainApp.Model.currentAsset;
        	var date: Date = new Date();
        	var formatter: DateFormatter = new DateFormatter();
        	formatter.formatString="MM/DD/YYYY";
        	model.caption = asset.FirstName + " " + asset.LastName + ", " + formatter.format(date);
            model.bills = asset.RelatedBill;
            model.bills.refresh();
            if (model.bills.IsLoaded) {
            	view.enabled = true;
	            openSummary();
            } else {
                model.bills.addEventListener("loaded", OnBillsLoaded);
            }
        }
        
        private function OnBillsLoaded(evt:DynamicLoadEvent):void {
            model.bills.removeEventListener("loaded", OnBillsLoaded);
            view.enabled = true;
			openSummary();
        }

        public function Logout():void {
//        	view.summaryView.Controller.model = null;
            mainApp.SetAppWorkflowState(AppModel.WORKFLOW_STATE_LOGOUT);
        }

		public function toCrewScreen():void {
			view.viewStack.selectedIndex = 0;
            view.summaryView.Controller.open();
		}

        public function OpenBillEditor():void {
            // var summaryView:SummaryView = BillView.Open(DisplayObject(view), true);
        }

        public function openDiary():void {
        	view.diaryView.enabled = false;
       		view.diaryView.Controller.open(mainApp.Model.currentAsset, model.bills);
        	view.viewStack.selectedChild = view.diaryView;
        }
            
        public function openSummary():void {
        	view.summaryView.enabled = false;
        	view.summaryView.Controller.open();
        	view.viewStack.selectedChild = view.summaryView;
        }
        
    }

}

package 
{

    import mx.binding.utils.BindingUtils;
    import App.Domain.*;
    import mx.formatters.DateFormatter;
    import weborb.data.DynamicLoadEvent;

    public class ExpenseController
    {

        [Bindable]
        public var model: ExpenseModel = new ExpenseModel();

        [Bindable]
        public var mainApp: AppController;
        private var view: ExpenseView;

        public function ExpenseController(view: ExpenseView, parent: AppController): void {
            this.view = view;
            mainApp = parent;
        }

        public function open(): void {
        	view.enabled = false;
        	model = new ExpenseModel();
        	var asset: Asset = mainApp.Model.currentAsset;
        	var date: Date = new Date();
        	var formatter: DateFormatter = new DateFormatter();
        	formatter.formatString="MM/DD/YYYY";
        	model.caption_text = asset.FirstName + " " + asset.LastName + ", " + formatter.format(date);
            model.bills = asset.RelatedBill;
            model.bills.refresh();
            if (model.bills.IsLoaded) {
            	view.enabled = true;
	            openLandmanHome();
            } else {
                model.bills.addEventListener("loaded", OnBillsLoaded);
            }
        }
        
        private function OnBillsLoaded(evt:DynamicLoadEvent):void {
            model.bills.removeEventListener("loaded", OnBillsLoaded);
            view.enabled = true;
			openLandmanHome();
        }

        public function onLogoutClick():void {
        	view.enabled = false;
        	mainApp.SetAppWorkflowState(AppModel.WORKFLOW_STATE_LOGOUT);
        }
        
        public function openDiary(bill:Bill = null):void {
        	view.diaryView.enabled = false;
        	
        	if (null == bill) {
        		view.diaryView.Controller.open(mainApp.Model.currentAsset, model.bills);
        	} else {
        		view.diaryView.Controller.open(mainApp.Model.currentAsset, model.bills, new Date(Date.parse(bill.StartDate)));
        	}
        	view.expenseViewStack.selectedChild = view.diaryView;
        }
            
        public function openLandmanHome():void 
        {
        	for each (var bill:Bill in model.bills) {
        		bill.loadNotes();
        	}
        	
        	view.landmanHomeView.enabled = false;
        	view.landmanHomeView.Controller.open(model.bills);
        	view.expenseViewStack.selectedChild = view.landmanHomeView;
        }
        
    }

}

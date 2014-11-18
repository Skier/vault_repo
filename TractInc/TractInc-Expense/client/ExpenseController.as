package 
{

    import mx.binding.utils.BindingUtils;
    import mx.formatters.DateFormatter;
    import App.Service.LandmanService;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.collections.ArrayCollection;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
    import App.Entity.BillDataObject;
    import App.Entity.LandmanDataObject;
    import App.Entity.AssetDataObject;

    public class ExpenseController
    {

        [Bindable]
        public var model: ExpenseModel = new ExpenseModel();

        [Bindable]
        public var mainApp: AppController;
        private var view: ExpenseView;
        
        public function getView():ExpenseView {
        	return view;
        }

        public function ExpenseController(view: ExpenseView, parent: AppController): void {
            this.view = view;
            this.view.diaryView.Parent = this;
            this.view.landmanHomeView.Parent = this;
            mainApp = parent;
        }

        public function open(): void {
        	view.enabled = false;
        	model = new ExpenseModel();
        	var asset:AssetDataObject = mainApp.Model.currentAsset;
        	var date: Date = new Date();
        	var formatter: DateFormatter = new DateFormatter();
        	formatter.formatString = "MM/DD/YYYY";
        	model.caption_text = asset.FirstName + " " + asset.LastName + ", " + formatter.format(date);
            LandmanService.getInstance().getLandmanData(asset.AssetId, new Responder(
            	OnBillsLoaded,
            	OnBillsLoadFailed
            ));
        }
        
        private function OnBillsLoaded(evt:ResultEvent):void {
            model.landmanData = LandmanDataObject(evt.result);
            
            view.enabled = true;
			openLandmanHome();
        }

        private function OnBillsLoadFailed(fault:FaultEvent):void {
            view.enabled = true;
            Alert.show("Cannot load bills", "System error");
        }

        public function onLogoutClick():void {
        	view.enabled = false;
        	mainApp.SetAppWorkflowState(AppModel.WORKFLOW_STATE_LOGOUT);
        }
        
        public function openDiary(bill:BillDataObject = null):void {
        	view.diaryView.enabled = false;
        	
        	if (null == bill) {
        		view.diaryView.Controller.open(mainApp.Model.currentAsset, model.landmanData, mainApp.Model);
        	} else {
        		view.diaryView.Controller.open(mainApp.Model.currentAsset, model.landmanData, mainApp.Model, new Date(Date.parse(bill.StartDate)));
        	}
        	view.expenseViewStack.selectedChild = view.diaryView;
        }
            
        public function openLandmanHome():void 
        {
        	view.landmanHomeView.enabled = false;
        	view.landmanHomeView.Controller.open();
        	view.expenseViewStack.selectedChild = view.landmanHomeView;
        }
        
    }

}

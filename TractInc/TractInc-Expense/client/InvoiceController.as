package 
{

    import mx.binding.utils.BindingUtils;
    import mx.formatters.DateFormatter;
    import flash.display.DisplayObject;
    import UI.landman.*;
    import mx.controls.Alert;
    import UI.crew.SummaryItem;
    import UI.crew.SummaryModel;
    import mx.collections.ArrayCollection;
    import UI.crew.SummaryView;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import App.Service.LandmanService;
    import mx.rpc.Responder;
    import App.Entity.LandmanDataObject;
    import App.Entity.AssetDataObject;
    import mx.rpc.remoting.RemoteObject;
    import App.Entity.CrewChiefDataObject;

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
        	var asset:AssetDataObject = mainApp.Model.currentAsset;
        	var date: Date = new Date();
        	var formatter: DateFormatter = new DateFormatter();
        	formatter.formatString="MM/DD/YYYY";
        	model.caption = asset.FirstName + " " + asset.LastName + ", " + formatter.format(date);
        	
          	var userService:RemoteObject = new RemoteObject("GenericDestination");
	    	userService.source = "TractInc.Expense.UserService";
       		userService.GetCrewChiefData.addEventListener(ResultEvent.RESULT,
       			function(result:ResultEvent):void {
       				model.data = CrewChiefDataObject(result.result);
       				
        			switchToSummary();
		        	
            		view.enabled = true;
       			}
       		);
       		userService.GetCrewChiefData.addEventListener(FaultEvent.FAULT,
       			function(fault:FaultEvent):void {
       				view.enabled = true;
       				Alert.show("Please contact administrator", "System Error");
       			}
       		);
       		userService.GetCrewChiefData(mainApp.Model.currentAsset.AssetId);
        }
        
        public function Logout():void {
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
            LandmanService.getInstance().getLandmanData(mainApp.Model.currentAsset.AssetId, new Responder(
            	OnDiaryBillsLoaded,
            	OnBillsLoadFailed
            ));
        }
        
        private function OnDiaryBillsLoaded(evt:ResultEvent):void {
        	view.viewStack.selectedChild = view.diaryView;
            view.enabled = true;
        	view.diaryView.Controller.open(mainApp.Model.currentAsset, LandmanDataObject(evt.result), mainApp.Model);
        }

        private function OnBillsLoadFailed(fault:FaultEvent):void {
            view.enabled = true;
            Alert.show("Cannot load bills", "System error");
        }
        
        public function openSummary():void {
        	open();
        }
        
        public function switchToSummary():void {
        	view.viewStack.selectedChild = view.summaryView;
        	view.summaryView.enabled = false;
        	view.summaryView.Controller.open();
        }
        
        public function openManagement():void {
        	view.viewStack.selectedChild = view.assetsView;
        	view.assetsView.controller.open(model.data);
        }
        
    }

}

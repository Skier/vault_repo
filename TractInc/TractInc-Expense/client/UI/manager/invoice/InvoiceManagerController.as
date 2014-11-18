package UI.manager.invoice
{
	import UI.manager.ManagerController;
	import mx.collections.ArrayCollection;
	import mx.events.CollectionEvent;
	import mx.events.ListEvent;
	import mx.managers.PopUpManager;
	import flash.events.MouseEvent;
	import mx.rpc.AsyncToken;
	import mx.managers.CursorManager;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	import mx.rpc.Fault;
	import mx.rpc.Responder;
	import mx.rpc.remoting.RemoteObject;
	import App.Entity.InvoiceDataObject;
	import App.Entity.ClientDataObject;
	import App.Entity.PeriodDataObject;
	import App.Entity.AssetDataObject;
	import weborb.data.DatabaseAsyncToken;
	import App.Service.ManagerService;
	import App.Entity.AssetAssignmentDataObject;
	import mx.collections.ListCollectionView;
	import App.Entity.InvoiceStatusDataObject;
	import util.DateUtil;
	
	[Bindable]
	public class InvoiceManagerController
	{
		public var model:InvoiceManagerModel;
		public var parentController:ManagerController;
		
		public var view:InvoiceManagerView;

		private var popup:SelectClientPeriodWindow;

		public function InvoiceManagerController(view:InvoiceManagerView, parentController:ManagerController) 
		{
			this.view = view;
			this.parentController = parentController;

			model = new InvoiceManagerModel();
		}
		
		public function init():void 
		{
			CursorManager.setBusyCursor();
			
			for each (var assignmentInfo:AssetAssignmentDataObject in parentController.model.data.Assignments) {
				model.assignmentsHash[assignmentInfo.AssetAssignmentId] = assignmentInfo;
			}
			
			model.clients = new ArrayCollection(parentController.model.data.Clients);
			
          	var userService:RemoteObject = new RemoteObject("GenericDestination");
	    	userService.source = "TractInc.Expense.UserService";
       		userService.GetInvoicesData.addEventListener(ResultEvent.RESULT,
       			function(result:ResultEvent):void {
       				model.invoices = new ListCollectionView(new ArrayCollection(result.result as Array));
       				model.invoices.filterFunction = function(obj:Object):Boolean {
       					var invoice:InvoiceDataObject = InvoiceDataObject(obj);
       					return (view.btnShowCurrent.selected && ((invoice.Status == InvoiceStatusDataObject.INVOICE_STATUS_NEW)
	       							|| (invoice.Status == InvoiceStatusDataObject.INVOICE_STATUS_SUBMITTED))
    	   						|| view.btnShowPaid.selected && (invoice.Status == InvoiceStatusDataObject.INVOICE_STATUS_PAID)
       							|| view.btnShowVoid.selected && (invoice.Status == InvoiceStatusDataObject.INVOICE_STATUS_VOID))
       						&& ((null == view.cbClientFilter.selectedItem)
       							|| (ClientDataObject(view.cbClientFilter.selectedItem).ClientId == invoice.ClientId))
       						&& ((null == view.cbPeriod.selectedItem)
       							|| (DateUtil.formatFromPeriod(PeriodDataObject(view.cbPeriod.selectedItem)) == invoice.StartDate));
       				}
       				model.invoices.refresh();
       				
					CursorManager.removeBusyCursor();
					
					model.isLoaded = true;
       			}
       		);
       		userService.GetInvoicesData.addEventListener(FaultEvent.FAULT,
       			function(fault:FaultEvent):void {
       				Alert.show("Please contact administrator", "System Error");
       			}
       		);
       		userService.GetInvoicesData();
		}
		
        public function onOpenInvoice():void 
        {
        	if (view.dgInvoices.selectedItem == null) {
        		return;
        	}
        	var invoice:InvoiceDataObject = view.dgInvoices.selectedItem as InvoiceDataObject;
        	openInvoice(invoice);
        }
        
        public function openInvoice(invoice:InvoiceDataObject):void 
        {
        	view.invoiceDetail.init(invoice, this);
        }
        
        public function createInvoice():void 
        {
        	popup = PopUpManager.createPopUp(view, SelectClientPeriodWindow, true) as SelectClientPeriodWindow;
        	popup.assets = new ArrayCollection(parentController.model.data.Assets);
        	popup.clients = new ArrayCollection(parentController.model.data.Clients);
        	
        	PopUpManager.centerPopUp(popup);
        	popup.Submit.addEventListener(MouseEvent.CLICK, 
        		function(e:*):void {
        			var client:ClientDataObject = popup.cbClient.selectedItem as ClientDataObject;
        			var period:PeriodDataObject = popup.cbPeriod.selectedItem as PeriodDataObject;
        			var asset:AssetDataObject = popup.cbAsset.selectedItem as AssetDataObject;
        			var useAsset:Boolean = popup.cbByAsset.selected;
        			popup.enabled = false;
        			if (useAsset) {
        				ManagerService.getInstance().createInvoice(
        					period.year, period.month, period.isFirstPart, client.ClientId, asset.AssetId,
        					new Responder(onGetInvoice, onFault));
        			} else {
        				ManagerService.getInstance().createInvoice(
        					period.year, period.month, period.isFirstPart, client.ClientId, 0,
        					new Responder(onGetInvoice, onFault));
        			}
        		});
        	popup.Cancel.addEventListener(MouseEvent.CLICK, 
        		function(e:*):void {
        			PopUpManager.removePopUp(popup);
        		});
        }
        
        private function onGetInvoice(result:ResultEvent):void {
        	var asset:AssetDataObject = popup.cbAsset.selectedItem as AssetDataObject;
   			PopUpManager.removePopUp(popup);
   			
   			var invoice:InvoiceDataObject = InvoiceDataObject(result.result);
   			invoice.isNew = true;
   			if (null != asset) {
   				invoice.Landman = asset.BusinessName;
   			}
   			openInvoice(invoice);
        }

		private function onFault(event:FaultEvent):void {
			if (popup) {
	   			PopUpManager.removePopUp(popup);
			}
   			Alert.show(event.fault.message);
		}
        
        public function setInvoicesState(state:int): void {

            switch (state) {
                case InvoiceManagerModel.VIEW_STATE_INVOICE_LIST:
                	view.vsInvoices.selectedChild = view.invoiceList;
                    break;

                case InvoiceManagerModel.VIEW_STATE_INVOICE_DETAIL:
                	view.vsInvoices.selectedChild = view.invoiceDetail;
                    break;

                default:
                    throw new Error("State is invalid");
            }
        }
	}
}
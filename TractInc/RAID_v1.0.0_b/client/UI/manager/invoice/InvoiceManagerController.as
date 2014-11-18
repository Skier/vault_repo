package UI.manager.invoice
{
	import UI.manager.ManagerController;
	import mx.collections.ArrayCollection;
	import App.Domain.ActiveRecords;
	import App.Domain.Client;
	import mx.events.CollectionEvent;
	import weborb.data.DynamicLoadEvent;
	import App.Domain.Invoice;
	import mx.events.ListEvent;
	import mx.managers.PopUpManager;
	import flash.events.MouseEvent;
	import mx.rpc.AsyncToken;
	import mx.managers.CursorManager;
	import App.Domain.Period;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	import mx.rpc.Fault;
	import mx.rpc.Responder;
	import App.Domain.Asset;
	import weborb.data.DatabaseAsyncToken;
	
	[Bindable]
	public class InvoiceManagerController
	{
		public var model:InvoiceManagerModel;
		public var parentController:ManagerController;
		
		public var view:InvoiceManagerView;

		private var popup:SelectClientPeriodWindow;
		private var _clients:ArrayCollection = ActiveRecords.Client.findAll();

		public function InvoiceManagerController(view:InvoiceManagerView, parentController:ManagerController) 
		{
			this.view = view;
			this.parentController = parentController;

			model = new InvoiceManagerModel();
		}
		
		public function init():void 
		{
			CursorManager.setBusyCursor();
			model.invoices = ActiveRecords.Invoice.findAll();
			model.invoices.addEventListener("loaded", onInvoicesLoaded);
			model.invoices.addEventListener(FaultEvent.FAULT, onFault);
		}
		
        public function onOpenInvoice():void 
        {
        	if (view.dgInvoices.selectedItem == null) {
        		return;
        	}
        	var invoice:Invoice = view.dgInvoices.selectedItem as Invoice;
        	openInvoice(invoice);
        }
        
        public function openInvoice(invoice:Invoice):void 
        {
        	view.invoiceDetail.init(invoice, this);
        }
        
        public function createInvoice():void 
        {
        	popup = PopUpManager.createPopUp(view, SelectClientPeriodWindow, true) as SelectClientPeriodWindow;
        	PopUpManager.centerPopUp(popup);
        	popup.Submit.addEventListener(MouseEvent.CLICK, 
        		function(e:*):void {
        			var client:Client = popup.cbClient.selectedItem as Client;
        			var period:Period = popup.cbPeriod.selectedItem as Period;
        			var asset:Asset = popup.cbAsset.selectedItem as Asset;
        			var useAsset:Boolean = popup.cbByAsset.selected;
        			popup.enabled = false;
        			var a:DatabaseAsyncToken;
        			if (useAsset) {
        				a = ActiveRecords.Invoice.createInvoice(period.year, period.month, period.isFirstPart, client.ClientId, asset.AssetId);
        			} else {
        				a = ActiveRecords.Invoice.createInvoice(period.year, period.month, period.isFirstPart, client.ClientId);
        			}
        			a.addResponder(new Responder(onGetInvoice, onFault));
        		});
        	popup.Cancel.addEventListener(MouseEvent.CLICK, 
        		function(e:*):void {
        			PopUpManager.removePopUp(popup);
        		});
        }
        
        private function onGetInvoice(invoice:Invoice):void {
   			PopUpManager.removePopUp(popup);
   			invoice.isNew = true;
   			invoice.loadNotes();
   			openInvoice(invoice);
        }

		private function onFault(event:FaultEvent):void {
			if (popup) {
	   			PopUpManager.removePopUp(popup);
			}
   			Alert.show(event.fault.message);
		}
        
		private function onInvoicesLoaded(event:DynamicLoadEvent):void 
		{
			CursorManager.removeBusyCursor();
			model.invoices.removeEventListener("loaded", onInvoicesLoaded);
			model.isLoaded = true;
			for each (var item:Invoice in event.data as ArrayCollection) {
				item.loadNotes();
			}
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
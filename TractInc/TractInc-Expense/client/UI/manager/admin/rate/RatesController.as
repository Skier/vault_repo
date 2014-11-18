package UI.manager.admin.rate
{
	
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import mx.managers.PopUpManager;
	import mx.rpc.Responder;
	import mx.core.IFlexDisplayObject;
	import mx.rpc.events.FaultEvent;
	import mx.events.CollectionEvent;
	import mx.events.CloseEvent;
	import mx.events.CollectionEventKind;
	import flash.events.MouseEvent;
	import flash.display.DisplayObject;
	import UI.manager.admin.AdminController;
	import App.Entity.DefaultBillRateDataObject;
	import App.Entity.AssetDataObject;
	import App.Entity.BillItemTypeDataObject;
	import App.Entity.ClientDataObject;
	import App.Entity.DefaultInvoiceRateDataObject;
	import App.Entity.InvoiceItemTypeDataObject;
	import common.TypesRegistry;
	import App.Service.ManagerService;
	
	public class RatesController
	{
		public var view:RatesView;
		
		[Bindable]
		public var model:RatesModel;

		[Bindable]
		public var parentController:AdminController;
		
		public function RatesController(view:RatesView, parentController:AdminController)
		{
			this.view = view;
			this.parentController = parentController;
			
			model = new RatesModel();
		}
		
		public function open():void {
			model.assets = new ArrayCollection(parentController.model.data.Assets);
			model.clients = new ArrayCollection(parentController.model.data.Clients);
		}
		
		public function onClickEditDefaultBillRate():void
		{
			var billRate:DefaultBillRateDataObject = view.dgDefaultBillRates.selectedItem as DefaultBillRateDataObject;
			if (billRate == null) {
				return;
			} else {
				openDefaultBillRate(billRate);
			}
		}
		
		private function openDefaultBillRate(billRate:DefaultBillRateDataObject):void {
			var popup:DefaultBillRateDetail = DefaultBillRateDetail.Open(billRate, view.parentDocument as DisplayObject, true);
			popup.btnSubmit.addEventListener(MouseEvent.CLICK, onDefaultBillRateSubmit);
			popup.btnCancel.addEventListener(MouseEvent.CLICK, onClickCancel);
		}
		
		private function onDefaultBillRateSubmit(event:MouseEvent):void {
			var popup:DefaultBillRateDetail = event.currentTarget.parentDocument as DefaultBillRateDetail;
			
			var billRate:DefaultBillRateDataObject = popup.defaultRate;
			
			var asset:AssetDataObject = view.dgAssets.selectedItem as AssetDataObject;
			var itemType:BillItemTypeDataObject = popup.cbBillItemTypes.selectedItem as BillItemTypeDataObject;
			
			var dr:DefaultBillRateDataObject = getDefaultBillRate(asset.AssetId, itemType.BillItemTypeId);
			
			if (dr != null && dr.DefaultBillRateId != billRate.DefaultBillRateId) {
				Alert.show("Default bill rate for this type already exists");
				return;
			}
			
			billRate.AssetId = asset.AssetId;
			billRate.BillItemTypeId = itemType.BillItemTypeId;
			
			if (1 == billRate.BillItemTypeId) {
				billRate.BillRate = Number(popup.txtBillRate.text) / 8;
			} else {
				billRate.BillRate = Number(popup.txtBillRate.text);
			}
			
			ManagerService.getInstance().storeDefaultBillRate(billRate);
			
			PopUpManager.removePopUp(popup);
		}
		
		private function onDefaultBillRatesCollectionChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var lastGridIndex:int = ArrayCollection(view.dgDefaultBillRates.dataProvider).getItemIndex(event.items[event.items.length - 1]);
				view.dgDefaultBillRates.selectedIndex = lastGridIndex;
				view.dgDefaultBillRates.scrollToIndex(lastGridIndex);
			}
		}
		
		private function getDefaultBillRate(assetId:int, typeId:int):DefaultBillRateDataObject {
			var collection:ArrayCollection = view.dgDefaultBillRates.dataProvider as ArrayCollection;

			for each (var billRate:DefaultBillRateDataObject in collection) {
				if ((billRate.AssetId == assetId)
						&& (billRate.BillItemTypeId == typeId)) {
					return billRate;
				}
			}

			return null;
		}
		
		public function onClickEditDefaultInvoiceRate():void
		{
			var invoiceRate:DefaultInvoiceRateDataObject = view.dgDefaultInvoiceRates.selectedItem as DefaultInvoiceRateDataObject;
			if (invoiceRate == null) {
				return;
			} else {
				openDefaultInvoiceRate(invoiceRate);
			}
		}
		
		private function openDefaultInvoiceRate(invoiceRate:DefaultInvoiceRateDataObject):void {
			var popup:DefaultInvoiceRateDetail = DefaultInvoiceRateDetail.Open(invoiceRate, view.parentDocument as DisplayObject, true);
			popup.btnSubmit.addEventListener(MouseEvent.CLICK, onDefaultInvoiceRateSubmit);
			popup.btnCancel.addEventListener(MouseEvent.CLICK, onClickCancel);
		}
		
		private function onDefaultInvoiceRateSubmit(event:MouseEvent):void {
			var popup:DefaultInvoiceRateDetail = event.currentTarget.parentDocument as DefaultInvoiceRateDetail;
			
			var invoiceRate:DefaultInvoiceRateDataObject = popup.defaultRate;
			
			var client:ClientDataObject = view.dgClients.selectedItem as ClientDataObject;
			var itemType:InvoiceItemTypeDataObject = popup.cbInvoiceItemTypes.selectedItem as InvoiceItemTypeDataObject;
			
			var dr:DefaultInvoiceRateDataObject = getDefaultInvoiceRate(client.ClientId, itemType.InvoiceItemTypeId);
			
			if (dr != null && dr.DefaultInvoiceRateId != invoiceRate.DefaultInvoiceRateId) {
				Alert.show("Default invoice rate for this type already exists");
				return;
			}
			
			invoiceRate.ClientId = client.ClientId;
			invoiceRate.InvoiceItemTypeId = itemType.InvoiceItemTypeId;
			
			if (1 == invoiceRate.InvoiceItemTypeId) {
				invoiceRate.InvoiceRate = Number(popup.txtInvoiceRate.text) / 8;
			} else {
				invoiceRate.InvoiceRate = Number(popup.txtInvoiceRate.text);
			}
			
			ManagerService.getInstance().storeDefaultInvoiceRate(invoiceRate);
			
			PopUpManager.removePopUp(popup);
		}
		
		private function onDefaultInvoiceRatesCollectionChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var lastGridIndex:int = ArrayCollection(view.dgDefaultInvoiceRates.dataProvider).getItemIndex(event.items[event.items.length - 1]);
				view.dgDefaultInvoiceRates.selectedIndex = lastGridIndex;
				view.dgDefaultInvoiceRates.scrollToIndex(lastGridIndex);
			}
		}
		
		private function getDefaultInvoiceRate(clientId:int, typeId:int):DefaultInvoiceRateDataObject {
			var collection:ArrayCollection = view.dgDefaultInvoiceRates.dataProvider as ArrayCollection;

			for each (var invoiceRate:DefaultInvoiceRateDataObject in collection) {
				if ((invoiceRate.ClientId == clientId)
						&& (invoiceRate.InvoiceItemTypeId == typeId)) {
					return invoiceRate;
				}
			}

			return null;
		}
		
		private function onClickCancel(event:MouseEvent):void 
		{
			var popup:IFlexDisplayObject = event.currentTarget.parentDocument as IFlexDisplayObject;
			PopUpManager.removePopUp(popup);
		}

		private function onFault(event:FaultEvent):void 
		{
			Alert.show(event.fault.message);
		}

	}
}

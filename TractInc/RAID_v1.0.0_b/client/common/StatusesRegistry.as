package common
{
	import mx.collections.ArrayCollection;
	import App.Domain.ActiveRecords;
	import App.Domain.BillItemStatus;
	import App.Domain.AfeStatus;
	import weborb.data.DynamicLoadEvent;
	import App.Domain.BillStatus;
	import flash.events.EventDispatcher;
	import flash.events.Event;
	import weborb.data.ActiveCollection;
	import mx.rpc.Responder;
	import App.Domain.InvoiceStatus;
	import App.Domain.InvoiceItemStatus;
	
	public class StatusesRegistry extends EventDispatcher
	{
		public static const STATUSES_LOADED_EVENT:String = "statuses_loaded";
		
		private static var _instance:StatusesRegistry;
		
		private static var _billItemStatuses:ArrayCollection = new ArrayCollection();
		private static var _landmanBillItemStatuses:ArrayCollection = new ArrayCollection();
		private static var _crewBillItemStatuses:ArrayCollection = new ArrayCollection();
		private static var _managerBillItemStatuses:ArrayCollection = new ArrayCollection();

		private static var _billStatuses:ArrayCollection = new ArrayCollection();
		private static var _landmanBillStatuses:ArrayCollection = new ArrayCollection();
		private static var _crewBillStatuses:ArrayCollection = new ArrayCollection();
		private static var _managerBillStatuses:ArrayCollection = new ArrayCollection();

		private static var _invoiceItemStatuses:ArrayCollection = new ArrayCollection();
		private static var _invoiceStatuses:ArrayCollection = new ArrayCollection();

		public static function getInstance():StatusesRegistry {
			if (_instance == null) {
				_instance =  new StatusesRegistry();
			} 
			return _instance;
		}
		
		public function StatusesRegistry() {
            if (StatusesRegistry._instance != null)
            {
                throw new Error( "Only one StatusesRegistry instance should be instantiated" ); 
            }
		}
		
		public function Load(responder:Responder = null):void {
            var billItemStatuses:ActiveCollection = ActiveRecords.BillItemStatus.findAll();
            billItemStatuses.addEventListener("loaded", onLoadBillItemStatuses);

            var billStatuses:ActiveCollection = ActiveRecords.BillStatus.findAll();
            billStatuses.addEventListener("loaded", onLoadBillStatuses);

            var invoiceStatuses:ActiveCollection = ActiveRecords.InvoiceStatus.findAll();
            invoiceStatuses.addEventListener("loaded", onLoadInvoiceStatuses);

            var invoiceItemStatuses:ActiveCollection = ActiveRecords.InvoiceItemStatus.findAll();
            invoiceItemStatuses.addEventListener("loaded", onLoadInvoiceItemStatuses);
		}
		
		public function get landmanBillItemStatuses():ArrayCollection {
			return _landmanBillItemStatuses;
		}
		
		public function get crewBillItemStatuses():ArrayCollection {
			return _crewBillItemStatuses;
		}
		
		public function get managerBillItemStatuses():ArrayCollection {
			return _managerBillItemStatuses;
		}
		
		public function get landmanBillStatuses():ArrayCollection {
			return _landmanBillStatuses;
		}
		
		public function get crewBillStatuses():ArrayCollection {
			return _crewBillStatuses;
		}
		
		public function get managerBillStatuses():ArrayCollection {
			return _managerBillStatuses;
		}
		
		public function getBillItemStatusByName(name:String):BillItemStatus {
			for each (var status:BillItemStatus in _billItemStatuses) {
				if (status.Status == name) {
					return status;
				}
			}
			
			return null;
		}
		
		public function getBillStatusByName(name:String):BillStatus {
			for each (var status:BillStatus in _billStatuses) {
				if (status.Status == name) {
					return status;
				}
			}
			
			return null;
		}

		public function getInvoiceItemStatusByName(name:String):InvoiceItemStatus {
			for each (var status:InvoiceItemStatus in _invoiceItemStatuses) {
				if (status.Status == name) {
					return status;
				}
			}
			
			return null;
		}
		
		public function getInvoiceStatusByName(name:String):InvoiceStatus {
			for each (var status:InvoiceStatus in _invoiceStatuses) {
				if (status.Status == name) {
					return status;
				}
			}
			
			return null;
		}

		public function getColorByName(name:String):uint {
		
			var result:uint = 0xFFFFFF;
		
			switch (name) {
				
				case BillItemStatus.BILL_ITEM_STATUS_APPROVED:
				result = 0xCCFFCC;
				break;
				
				case BillItemStatus.BILL_ITEM_STATUS_REJECTED:
				result = 0xFFCCCC;
				break;
				
				case BillItemStatus.BILL_ITEM_STATUS_CORRECTED:
				result = 0xFFFFCC;
				break;
				
				case BillItemStatus.BILL_ITEM_STATUS_CONFIRMED:
				result = 0x99FF99;
				break;
				
				case BillItemStatus.BILL_ITEM_STATUS_DECLINED:
				result = 0xFF9999;
				break;
				
				case InvoiceItemStatus.INVOICE_ITEM_STATUS_PAID:
				result = 0xeeFFee;
				break;
				
				case InvoiceItemStatus.INVOICE_ITEM_STATUS_VOID:
				result = 0xFFeeee;
				break;
				
			}
			
			return result;
		
		}

		private function onLoadBillItemStatuses(event:DynamicLoadEvent):void {
			_landmanBillItemStatuses.removeAll();
			_crewBillItemStatuses.removeAll();
			_managerBillItemStatuses.removeAll();
			_billItemStatuses.removeAll();
			
			for each (var status:BillItemStatus in ActiveCollection(event.data)) {
				_billItemStatuses.addItem(status);
				switch(status.Status) {
					case BillItemStatus.BILL_ITEM_STATUS_APPROVED:
						_crewBillItemStatuses.addItem(status);
						break;
						
					case BillItemStatus.BILL_ITEM_STATUS_CORRECTED:
						_landmanBillItemStatuses.addItem(status);
						break;
						
					case BillItemStatus.BILL_ITEM_STATUS_CHANGED:
						_landmanBillItemStatuses.addItem(status);
						break;
						
					case BillItemStatus.BILL_ITEM_STATUS_NEW:
						_landmanBillItemStatuses.addItem(status);
						break;
						
					case BillItemStatus.BILL_ITEM_STATUS_REJECTED:
						_crewBillItemStatuses.addItem(status);
						break;
						
					case BillItemStatus.BILL_ITEM_STATUS_SUBMITTED:
						_landmanBillItemStatuses.addItem(status);
						break;
						
					case BillItemStatus.BILL_ITEM_STATUS_CONFIRMED:
						_managerBillItemStatuses.addItem(status);
						break;

					case BillItemStatus.BILL_ITEM_STATUS_DECLINED:
						_managerBillItemStatuses.addItem(status);
						break;
				}
			}
			
			tryDispatchEvent();
		}
		
		private function onLoadBillStatuses(event:DynamicLoadEvent):void {
			_landmanBillStatuses.removeAll();
			_crewBillStatuses.removeAll();
			_managerBillStatuses.removeAll();
			_billStatuses.removeAll();

			for each (var status:BillStatus in ActiveCollection(event.data)) {
				_billStatuses.addItem(status);
				switch(status.Status) {
					case BillStatus.BILL_STATUS_APPROVED:
						_crewBillStatuses.addItem(status);
						break;
						
					case BillStatus.BILL_STATUS_SUBMITTED:
						_landmanBillStatuses.addItem(status);
						break;
						
					case BillStatus.BILL_STATUS_NEW:
						_landmanBillStatuses.addItem(status);
						break;
						
					case BillStatus.BILL_STATUS_REJECTED:
						_crewBillStatuses.addItem(status);
						break;
						
					case BillStatus.BILL_STATUS_CONFIRMED:
						_managerBillStatuses.addItem(status);
						break;
						
					case BillStatus.BILL_STATUS_DECLINED:
						_managerBillStatuses.addItem(status);
						break;
				}
			}
			
			tryDispatchEvent();
		}
		
		private function onLoadInvoiceItemStatuses(event:DynamicLoadEvent):void {
			_invoiceItemStatuses.removeAll();
			
			for each (var status:InvoiceItemStatus in ActiveCollection(event.data)) {
				_invoiceItemStatuses.addItem(status);
			}
			
			tryDispatchEvent();
		}
		
		private function onLoadInvoiceStatuses(event:DynamicLoadEvent):void {
			_invoiceStatuses.removeAll();
			
			for each (var status:InvoiceStatus in ActiveCollection(event.data)) {
				_invoiceStatuses.addItem(status);
			}
			
			tryDispatchEvent();
		}
		
		private function tryDispatchEvent():void {
			if ((0 < _billItemStatuses.length) && (0 < _billStatuses.length) && (0 < _invoiceStatuses.length) && (0 < _invoiceItemStatuses.length)) {
				dispatchEvent(new Event(STATUSES_LOADED_EVENT));
			}
		}
		
	}
	
}

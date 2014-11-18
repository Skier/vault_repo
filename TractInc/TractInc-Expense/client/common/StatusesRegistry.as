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
	import App.Entity.DictionariesDataObject;
	import App.Entity.BillItemStatusDataObject;
	import App.Entity.BillStatusDataObject;
	import App.Entity.InvoiceItemStatusDataObject;
	import App.Entity.InvoiceStatusDataObject;
	import App.Entity.AFEStatusDataObject;
	import App.Entity.ProjectStatusDataObject;
	
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
		
		private static var _afeStatuses:ArrayCollection = new ArrayCollection();
		private static var _projectStatuses:ArrayCollection = new ArrayCollection();

		public static function get instance():StatusesRegistry {
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
		
		public function Load(dictionariesInfo:DictionariesDataObject):void {
            loadBillItemStatuses(dictionariesInfo.BillItemStatuses);

            loadBillStatuses(dictionariesInfo.BillStatuses);

            loadInvoiceStatuses(dictionariesInfo.InvoiceStatuses);

            loadInvoiceItemStatuses(dictionariesInfo.InvoiceItemStatuses);
            
            loadAFEStatuses(dictionariesInfo.AFEStatuses);
            
            loadProjectStatuses(dictionariesInfo.ProjectStatuses);
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
		
		public function get afeStatuses():ArrayCollection {
			return _afeStatuses;
		}
		
		public function get projectStatuses():ArrayCollection {
			return _projectStatuses;
		}
		
		public function getAFEStatusByName(name:String):AFEStatusDataObject {
			for each (var status:AFEStatusDataObject in _afeStatuses) {
				if (status.Status == name) {
					return status;
				}
			}
			
			return null;
		}
		
		public function getProjectStatusByName(name:String):ProjectStatusDataObject {
			for each (var status:ProjectStatusDataObject in _projectStatuses) {
				if (status.Status == name) {
					return status;
				}
			}
			
			return null;
		}
		
		public function getBillItemStatusByName(name:String):BillItemStatusDataObject {
			for each (var status:BillItemStatusDataObject in _billItemStatuses) {
				if (status.Status == name) {
					return status;
				}
			}
			
			return null;
		}
		
		public function getBillStatusByName(name:String):BillStatusDataObject {
			for each (var status:BillStatusDataObject in _billStatuses) {
				if (status.Status == name) {
					return status;
				}
			}
			
			return null;
		}

		public function getInvoiceItemStatusByName(name:String):InvoiceItemStatusDataObject {
			for each (var status:InvoiceItemStatusDataObject in _invoiceItemStatuses) {
				if (status.Status == name) {
					return status;
				}
			}
			
			return null;
		}
		
		public function getInvoiceStatusByName(name:String):InvoiceStatusDataObject {
			for each (var status:InvoiceStatusDataObject in _invoiceStatuses) {
				if (status.Status == name) {
					return status;
				}
			}
			
			return null;
		}

		public function getColorByName(name:String):uint {
		
			var result:uint = 0xFFFFFF;
		
			switch (name) {
				
				case BillItemStatusDataObject.BILL_ITEM_STATUS_APPROVED:
				result = 0xCCFFCC;
				break;
				
				case BillItemStatusDataObject.BILL_ITEM_STATUS_REJECTED:
				result = 0xFFCCCC;
				break;
				
				case BillItemStatusDataObject.BILL_ITEM_STATUS_CORRECTED:
				result = 0xFFFFCC;
				break;
				
				case BillItemStatusDataObject.BILL_ITEM_STATUS_CONFIRMED:
				result = 0x99FF99;
				break;
				
				case BillItemStatusDataObject.BILL_ITEM_STATUS_DECLINED:
				result = 0xFF9999;
				break;
				
				case InvoiceItemStatusDataObject.INVOICE_ITEM_STATUS_PAID:
				result = 0xeeFFee;
				break;
				
				case InvoiceItemStatusDataObject.INVOICE_ITEM_STATUS_VOID:
				result = 0xFFeeee;
				break;
				
			}
			
			return result;
		
		}

		private function loadBillItemStatuses(statuses:Array):void {
			_landmanBillItemStatuses.removeAll();
			_crewBillItemStatuses.removeAll();
			_managerBillItemStatuses.removeAll();
			_billItemStatuses.removeAll();
			
			for each (var status:BillItemStatusDataObject in statuses) {
				_billItemStatuses.addItem(status);
				switch(status.Status) {
					case BillItemStatusDataObject.BILL_ITEM_STATUS_APPROVED:
						_crewBillItemStatuses.addItem(status);
						break;
						
					case BillItemStatusDataObject.BILL_ITEM_STATUS_CORRECTED:
						_landmanBillItemStatuses.addItem(status);
						break;
						
					case BillItemStatusDataObject.BILL_ITEM_STATUS_CHANGED:
						_landmanBillItemStatuses.addItem(status);
						break;
						
					case BillItemStatusDataObject.BILL_ITEM_STATUS_NEW:
						_landmanBillItemStatuses.addItem(status);
						break;
						
					case BillItemStatusDataObject.BILL_ITEM_STATUS_REJECTED:
						_crewBillItemStatuses.addItem(status);
						break;
						
					case BillItemStatusDataObject.BILL_ITEM_STATUS_SUBMITTED:
						_landmanBillItemStatuses.addItem(status);
						break;
						
					case BillItemStatusDataObject.BILL_ITEM_STATUS_CONFIRMED:
						_managerBillItemStatuses.addItem(status);
						break;

					case BillItemStatusDataObject.BILL_ITEM_STATUS_DECLINED:
						_managerBillItemStatuses.addItem(status);
						break;
				}
			}
		}
		
		private function loadBillStatuses(statuses:Array):void {
			_landmanBillStatuses.removeAll();
			_crewBillStatuses.removeAll();
			_managerBillStatuses.removeAll();
			_billStatuses.removeAll();

			for each (var status:BillStatusDataObject in statuses) {
				_billStatuses.addItem(status);
				switch(status.Status) {
					case BillStatusDataObject.BILL_STATUS_APPROVED:
						_crewBillStatuses.addItem(status);
						break;
						
					case BillStatusDataObject.BILL_STATUS_SUBMITTED:
						_landmanBillStatuses.addItem(status);
						break;
						
					case BillStatusDataObject.BILL_STATUS_NEW:
						_landmanBillStatuses.addItem(status);
						break;
						
					case BillStatusDataObject.BILL_STATUS_REJECTED:
						_crewBillStatuses.addItem(status);
						break;
						
					case BillStatusDataObject.BILL_STATUS_CONFIRMED:
						_managerBillStatuses.addItem(status);
						break;
						
					case BillStatusDataObject.BILL_STATUS_DECLINED:
						_managerBillStatuses.addItem(status);
						break;
				}
			}
		}
		
		private function loadInvoiceItemStatuses(statuses:Array):void {
			_invoiceItemStatuses.removeAll();
			
			for each (var status:InvoiceItemStatusDataObject in statuses) {
				_invoiceItemStatuses.addItem(status);
			}
		}
		
		private function loadInvoiceStatuses(statuses:Array):void {
			_invoiceStatuses.removeAll();
			
			for each (var status:InvoiceStatusDataObject in statuses) {
				_invoiceStatuses.addItem(status);
			}
		}
		
		private function loadAFEStatuses(statuses:Array):void {
			_afeStatuses.removeAll();
			
			for each (var status:AFEStatusDataObject in statuses) {
				_afeStatuses.addItem(status);
			}
		}
		
		private function loadProjectStatuses(statuses:Array):void {
			_projectStatuses.removeAll();
			
			for each (var status:ProjectStatusDataObject in statuses) {
				_projectStatuses.addItem(status);
			}
		}
		
	}
	
}

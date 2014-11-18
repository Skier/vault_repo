package common
{
	
	import mx.collections.ArrayCollection;
	import flash.events.EventDispatcher;
	import flash.events.Event;
	import weborb.data.ActiveCollection;
	import App.Entity.DictionariesDataObject;
	import App.Entity.BillItemTypeDataObject;
	import App.Entity.InvoiceItemTypeDataObject;
	import App.Entity.AssetTypeDataObject;
	
	public class TypesRegistry extends EventDispatcher
	{

		public static const TYPES_LOADED_EVENT:String = "types_loaded";
		
		private static var _instance:TypesRegistry;
		
		private var _billItemTypes:ArrayCollection;
		
		private var _invoiceItemTypes:ArrayCollection;
		
		private var _assetTypes:ArrayCollection;
		
		private var _otherBillItemTypes:ArrayCollection;
		private var _dailyBillItemType:BillItemTypeDataObject;
		
		private var _countableBillItemTypes:ArrayCollection;
		private var _countableInvoiceItemTypes:ArrayCollection;

		public static function get instance():TypesRegistry {
			if (_instance == null) {
				_instance =  new TypesRegistry();
			} 
			return _instance;
		}
		
		public function TypesRegistry() {
            if (TypesRegistry._instance != null)
            {
                throw new Error( "Only one TypesRegistry instance should be instantiated" ); 
            }
		}
		
		public function Load(dictionariesInfo:DictionariesDataObject):void {
            loadBillItemTypes(dictionariesInfo.BillItemTypes);
            loadInvoiceItemTypes(dictionariesInfo.InvoiceItemTypes);
            loadAssetTypes(dictionariesInfo.AssetTypes);
		}
		
		public function get billItemTypes():ArrayCollection {
			return _otherBillItemTypes;
		}
		
		public function get dailyBillItemType():BillItemTypeDataObject {
			return _dailyBillItemType;
		}
		
		public function get getAllBillItemTypes():ArrayCollection {
			return _billItemTypes;
		}
		
		public function get getAllInvoiceItemTypes():ArrayCollection {
			return _invoiceItemTypes;
		}
		
		public function get countableBillItemTypes():ArrayCollection {
			return _countableBillItemTypes;
		}
		
		public function get countableInvoiceItemTypes():ArrayCollection {
			return _countableInvoiceItemTypes;
		}
		
		public function get assetTypes():ArrayCollection {
			return _assetTypes;
		}
		
		public function getBillItemTypeByName(name:String):BillItemTypeDataObject {
			for each (var type:BillItemTypeDataObject in _otherBillItemTypes) {
				if (type.Name == name) {
					return type;
				}
			}
			return null;
		}
		
		public function getBillItemTypeById(id:int):BillItemTypeDataObject {
			for each (var type:BillItemTypeDataObject in _billItemTypes) {
				if (type.BillItemTypeId == id) {
					return type;
				}
			}
			return null;
		}
		
		public function getInvoiceItemTypeById(id:int):InvoiceItemTypeDataObject {
			for each (var type:InvoiceItemTypeDataObject in _invoiceItemTypes) {
				if (type.InvoiceItemTypeId == id) {
					return type;
				}
			}
			return null;
		}
		
		public function getAssetTypeByName(name:String):AssetTypeDataObject {
			for each (var type:AssetTypeDataObject in _assetTypes) {
				if (type.Type == name) {
					return type;
				}
			}
			return null;
		}
		
		private function loadBillItemTypes(billItemTypes:Array):void {
			_billItemTypes = new ArrayCollection(billItemTypes);
			_otherBillItemTypes = new ArrayCollection();
			_countableBillItemTypes = new ArrayCollection();
			for each (var itemType:BillItemTypeDataObject in billItemTypes) {
				if (itemType.IsCountable) {
					_countableBillItemTypes.addItem(itemType);
				}
				if (BillItemTypeDataObject.BILL_ITEM_TYPE_DAILY_BILLING == itemType.BillItemTypeId) {
					_dailyBillItemType = itemType;
				} else {
					_otherBillItemTypes.addItem(itemType);
				}
			}
		}
		
		private function loadInvoiceItemTypes(invoiceItemTypes:Array):void {
			_invoiceItemTypes = new ArrayCollection(invoiceItemTypes);
			_countableInvoiceItemTypes = new ArrayCollection();
			for each (var itemType:InvoiceItemTypeDataObject in _invoiceItemTypes) {
				if (itemType.IsCountable) {
					_countableInvoiceItemTypes.addItem(itemType);
				}
			}
		}
		
		private function loadAssetTypes(assetTypes:Array):void {
			_assetTypes = new ArrayCollection(assetTypes);
		}
		
	}
	
}

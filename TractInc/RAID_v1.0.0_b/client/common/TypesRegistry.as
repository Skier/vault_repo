package common
{
	
	import mx.collections.ArrayCollection;
	import App.Domain.ActiveRecords;
	import App.Domain.BillItemType;
	import weborb.data.DynamicLoadEvent;
	import flash.events.EventDispatcher;
	import flash.events.Event;
	import weborb.data.ActiveCollection;
	import App.Domain.AssetType;
	
	public class TypesRegistry extends EventDispatcher
	{

		public static const TYPES_LOADED_EVENT:String = "types_loaded";
		
		private static var _instance:TypesRegistry;
		
		private var _billItemTypes:ActiveCollection;
		private var _assetTypes:ActiveCollection;
		
		private var _otherBillItemTypes:ArrayCollection;
		private var _dailyBillItemType:BillItemType;
		
		private var _countableBillItemTypes:ArrayCollection;

		public static function getInstance():TypesRegistry {
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
		
		public function Load():void {
            _billItemTypes = ActiveRecords.BillItemType.findAll();
            _billItemTypes.addEventListener("loaded", onLoadBillItemTypes);

			_assetTypes = ActiveRecords.AssetType.findAll();
			_assetTypes.addEventListener("loaded", onAssetTypesLoaded);
		}
		
		public function get billItemTypes():ArrayCollection {
			return _otherBillItemTypes;
		}
		
		public function get dailyBillItemType():BillItemType {
			return _dailyBillItemType;
		}
		
		public function get getAllBillItemTypes():ArrayCollection {
			return _billItemTypes;
		}
		
		public function get countableBillItemTypes():ArrayCollection {
			return _countableBillItemTypes;
		}
		
		public function getBillItemTypeByName(name:String):BillItemType {
			for each (var type:BillItemType in _otherBillItemTypes) {
				if (type.Name == name) {
					return type;
				}
			}
			return null;
		}
		
		public function getAssetTypeByName(name:String):AssetType {
			for each (var type:AssetType in _assetTypes) {
				if (type._Type == name) {
					return type;
				}
			}
			return null;
		}
		
		private function onLoadBillItemTypes(event:DynamicLoadEvent):void {
            _billItemTypes.removeEventListener("loaded", onLoadBillItemTypes);
			_otherBillItemTypes = new ArrayCollection();
			_countableBillItemTypes = new ArrayCollection();
			for each (var itemType:BillItemType in _billItemTypes) {
				if (itemType.IsCountable) {
					_countableBillItemTypes.addItem(itemType);
				}
				if (BillItemType.BILL_ITEM_TYPE_DAILY_BILLING == itemType.BillItemTypeId) {
					_dailyBillItemType = itemType;
				} else {
					_otherBillItemTypes.addItem(itemType);
				}
			}
			tryDispatchEvent();
		}
		
		private function onAssetTypesLoaded(event:DynamicLoadEvent):void {
			_assetTypes.removeEventListener("loaded", onAssetTypesLoaded);
			tryDispatchEvent();
		}
		
		private function tryDispatchEvent():void {
			if (_billItemTypes.IsLoaded && _assetTypes.IsLoaded) {
				dispatchEvent(new Event(TYPES_LOADED_EVENT));
			}
		}
		
	}
	
}

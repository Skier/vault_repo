package AerSysCo.UI.Catalog
{
	import AerSysCo.UI.Models.CategoryUI;
	import mx.collections.ArrayCollection;
	import AerSysCo.UI.Models.CustomerUI;
	import AerSysCo.UI.Models.WarehouseUI;
	import mx.managers.CursorManager;
	
	[Bindable]
	public class CatalogModel
	{
		public var currentCustomer:CustomerUI;
		
		public var rootCategory:CategoryUI;
		public var productList:ArrayCollection = new ArrayCollection();
		public var warehouseList:ArrayCollection = new ArrayCollection();
		public var currentWarehouse:WarehouseUI;

		public var searchString:String;
		public var defaultCategory:String;
		
		public var _isBusy:Boolean = false;
		public function get isBusy():Boolean {return _isBusy}
		public function set isBusy(value:Boolean):void 
		{
			_isBusy = value;

			if (value) 
				CursorManager.setBusyCursor();
			else 
				CursorManager.removeBusyCursor();
		}
		
		public function getDefaultCategory(): CategoryUI {
            for each (var category:CategoryUI in rootCategory.children.toArray() ) {
                if ( category.name == defaultCategory ) {
                	return category;
                }
            }
            return null;
		}
	}
}
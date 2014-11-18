package AerSysCo.UI.Models
{
	import mx.collections.ArrayCollection;
	import AerSysCo.Server.CatalogItem;
	
	[Bindable]
	public class CatalogItemUI
	{
	    public var modelItem:ModelItemUI;
	    public var inventories:ArrayCollection = new ArrayCollection();
	    
	    public var modelName:String;
	    public var customerId:int;
	    
	    public var qtyAvailable:int;
	    public var qtyNeeded:int;
	    
	    public var isAvailable:Boolean;
	    public var multiWarehouseAvailable:Boolean;
	    
		public function populateFromCatalogItem(value:CatalogItem, cascade:Boolean = true):void 
		{
			this.modelName = value.modelName;
			this.customerId = value.customerId;
			
			if (cascade && value.modelItem) 
			{
				var mi:ModelItemUI = new ModelItemUI();
				mi.populateFromModelItem(value.modelItem, true);
				this.modelItem = mi;
			}
			
			if (cascade && value.item) 
			{
				var item:ItemUI = new ItemUI();
				item.populateFromItem(value.item, true);
				this.modelItem.item = item;
			}	

			if (cascade && value.inventories) 
			{
				this.inventories.removeAll();
				
				for (var i:int = 0; i < value.inventories.count; i++) 
				{
					var inv:InventoryUI = new InventoryUI();
					inv.populateFromInventory(value.inventories.getItemAt(i), true);
					this.inventories.addItem(inv);
				}
			}
		}
		
		public function toCatalogItem():CatalogItem 
		{
			var result:CatalogItem = new CatalogItem();
			
			result.modelName = this.modelName;
			result.customerId = this.customerId;
			
			if (modelItem) 
			{
				result.modelItem = modelItem.toModelItem();
			}
			
			if (modelItem && modelItem.item) 
			{
				result.item = modelItem.item.toItem();
			}
			
			result.inventories.removeAll();
			for each (var inv:InventoryUI in this.inventories) 
			{
				result.inventories.addItem(inv.toInventory());
			}

			return result;
		}
		
		private var _currentWarehouse:WarehouseUI;
		public function get currentWarehouse():WarehouseUI { return _currentWarehouse; }
		public function set currentWarehouse(value:WarehouseUI):void 
		{
			_currentWarehouse = value;
			
			for each (var inv:InventoryUI in this.inventories) 
			{
				if (inv.warehouseId == value.warehouseId) 
				{
					qtyAvailable = inv.qty - inv.qtyAllocated;
					return;
				}
			}
		}
		
		public function updateQtyAvailable(warehouse:WarehouseUI, customer:CustomerUI):void 
		{
			qtyAvailable = 0;
			multiWarehouseAvailable = false;
			
			if (!warehouse || !customer)
				return;
			
			for each (var inv:InventoryUI in inventories) 
			{
				if (inv.warehouseId == warehouse.warehouseId && customer.shoppingCart) 
				{
					var qtySC:int = customer.shoppingCart.getQty(modelItem.modelItemId, inv.warehouseId);
					qtyAvailable += (inv.qty - inv.qtyAllocated - qtySC);
				} else if (inv.warehouseId != warehouse.warehouseId) 
				{
					if (inv.qty > 0) 
					{
						multiWarehouseAvailable = true;
					}
				}
			}
		}
		
		public function getQtyAvailable(inventory:InventoryUI, customer:CustomerUI):int 
		{
			for each (var inv:InventoryUI in inventories) 
			{
				if (inv.warehouseId == inventory.warehouseId && customer.shoppingCart) 
				{
					var qtySC:int = customer.shoppingCart.getQty(modelItem.modelItemId, inv.warehouseId);
					return (inv.qty - inv.qtyAllocated - qtySC);
				}
			}
			
			return 0;
		}

	    public function get sku():String 
	    {
	    	if (modelItem && modelItem.item && modelItem.item.sku) 
	    	{
	    		return modelItem.item.sku;
	    	} else 
	    	{
	    		return "n/a";
	    	}
	    }

	    public function get model():String 
	    {
	    	if (modelItem) 
	    	{
	    		return (modelItem.modelId.toString() + "_tmp");
	    	} else 
	    	{
	    		return "n/a";
	    	}
	    }

	    public function get description():String 
	    {
	    	if (modelItem && modelItem.item && modelItem.item.description) 
	    	{
	    		return modelItem.item.description;
	    	} else 
	    	{
	    		return "n/a";
	    	}
	    }

	    public function get price(): Number 
	    {
	    	if (modelItem && modelItem.price) 
	    	{
	    		return modelItem.price;
	    	} else 
	    	{
	    		return 0;
	    	}
	    }

	    public function get priceString():String 
	    {
	    	if (modelItem && modelItem.price) 
	    	{
	    		return modelItem.price.toFixed(2);
	    	} else 
	    	{
	    		return "0";
	    	}
	    }

	    public function get configuration():String 
	    {
	    	if (modelItem && modelItem.configuration) 
	    	{
	    		return modelItem.configuration;
	    	} else 
	    	{
	    		return "n/a";
	    	}
	    }

	    public function get xmlDescription():String 
	    {
	    	if (modelItem && modelItem.xmlBullerDescription) 
	    	{
	    		return modelItem.xmlBullerDescription;
	    	} else 
	    	{
	    		return "n/a";
	    	}
	    }

	    public function get amountString():String 
	    {
	    	if (modelItem && modelItem.price) 
	    	{
	    		return Number(modelItem.price * qtyNeeded).toFixed(2);
	    	} else 
	    	{
	    		return "n/a";
	    	}
	    }

	    public function get imageUrl():String 
	    {
    		return modelItem.imageURL;
	    }

	}
}
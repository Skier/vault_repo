package AerSysCo.UI.Models
{
	import AerSysCo.Server.Inventory;
	
	[Bindable]
	public class InventoryUI
	{
	    public var inventoryId:int;
	    public var warehouseId:int;
	    public var itemId:int;
	    public var qty:int;
	    public var qtyAllocated:int;
	
		public function populateFromInventory(value:Inventory, cascade:Boolean = false):void 
		{
			this.inventoryId = value.inventoryId;
			this.warehouseId = value.warehouseId;
			this.itemId = value.itemId;
			this.qty = value.qty;
			this.qtyAllocated = value.qtyAllocated;
		}
		
		public function toInventory():Inventory 
		{
			var result:Inventory = new Inventory();
		 
		 	result.inventoryId = this.inventoryId;
		 	result.warehouseId = this.warehouseId;
		 	result.itemId = this.itemId;
		 	result.qty = this.qty;
		 	result.qtyAllocated = this.qtyAllocated;
	    	result.dateCreated = new Date();
	    	result.lastUpdateDate = new Date();
		 	
		 	return result;
		}
	
	}
}
package AerSysCo.UI.Models
{
	import mx.collections.ArrayCollection;
	import AerSysCo.Server.ShoppingCartShipment;
	import AerSysCo.Server.ShoppingCartDetail;
	
	[Bindable]
	public class ShoppingCartShipmentUI
	{
	    public var shoppingCartShipmentId:int;
	    public var shoppingCartId:int;
	    public var warehouseId:int;
	    public var shippingTotal:Number;
	    public var shippingTypeId:int;
    	public var needLiftGate:Boolean;
    	public var liftGatePrice:Number;
	    public var poNumber:String;
	    
	    public var isPONumberValid:Boolean = false;
	    
	    public var liftGateCost:Number;

	    public var details:ArrayCollection = new ArrayCollection();
	    public var warehouse:WarehouseUI;
	    
	    public var total:Number;
	    public var totalItems:int;
	    public var grandTotal:Number;
	    
	    public var shipmentOptions:ArrayCollection = new ArrayCollection();
	    
	    public var isDirty:Boolean = false;
	    	    
	    public function populateFromShoppingcartShipment(value:ShoppingCartShipment, cascade:Boolean = false):void 
	    {
	    	this.shoppingCartShipmentId = value.shoppingCartShipmentId;
	    	this.shoppingCartId = value.shoppingCartId;
	    	this.warehouseId = value.warehouseId;
	    	this.shippingTotal = value.shippingTotal;
	    	this.shippingTypeId = value.shippingTypeId;
	    	this.poNumber = value.PoNumber;
	    	this.needLiftGate = value.needLiftGate;
	    	this.liftGatePrice = value.liftGatePrice;
	    	
	    	if (cascade) 
	    	{
	    		if (value.warehouse) 
	    		{
	    			var w:WarehouseUI = new WarehouseUI();
	    			w.populateFromWarehouse(value.warehouse, cascade);
	    			this.warehouse = (w);
	    		}
	    		if (value.details) 
	    		{
	    			this.details.removeAll();
	    			for (var i:int = 0; i < value.details.count; i++) 
	    			{
	    				var newItem:ShoppingCartDetailUI = new ShoppingCartDetailUI();
	    				newItem.populateFromShoppingCartDetail(value.details.getItemAt(i), cascade);
		    			this.details.addItem(newItem);
	    			}
	    		}
	    	}
	    }
	    
	    public function toShoppingCartShipment():ShoppingCartShipment 
	    {
	    	var result:ShoppingCartShipment = new ShoppingCartShipment();
	    	
	    	result.shoppingCartShipmentId = this.shoppingCartShipmentId;
	    	result.shoppingCartId = this.shoppingCartId;
	    	result.warehouseId = this.warehouseId;
	    	result.shippingTotal = this.shippingTotal;
	    	result.shippingTypeId = this.shippingTypeId;
	    	result.PoNumber = this.poNumber;
	    	result.dateCreated = new Date();
	    	result.lastUpdateDate = new Date();
	    	result.needLiftGate = this.needLiftGate;
	    	result.liftGatePrice = this.liftGatePrice;
	    	
	    	if (this.warehouse) 
	    	{
	    		result.warehouse = this.warehouse.toWarehouse();
	    	}
	    	
	    	result.details.removeAll();
	    	
	    	for each (var scd:ShoppingCartDetailUI in this.details) 
	    	{
	    		result.details.addItem(scd.toShoppingCartDetail());
	    	}
	    	
	    	//workaround
	    	result.details = null;
	    	
	    	return result;
	    }
	    
	    public function addShoppingCartDetail(detail:ShoppingCartDetailUI):void 
	    {
	    	if (detail.shoppingCartShipmentId != this.shoppingCartShipmentId)
	    		return;
	    	
	    	var sd:ShoppingCartDetailUI = getShoppingCartDetail(detail.modelItemId);
	    	
	    	if (sd) 
	    	{
	    		sd.qtyNeeded = detail.qtyNeeded;
	    		sd.qtyOrdered = detail.qtyOrdered;
	    	} else 
	    	{
    			this.details.addItem(detail);
	    	}
	    }
	    
	    public function getShoppingCartDetail(modelItemId:int):ShoppingCartDetailUI 
	    {
	    	for each (var d:ShoppingCartDetailUI in details) 
	    	{
	    		if (d.modelItemId == modelItemId) 
	    		{
	    			return d; 
	    		}
	    	}
	    	
	    	return null;
	    }
	}
}
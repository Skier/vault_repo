package AerSysCo.UI.Models
{
	import AerSysCo.Server.ShoppingCartDetail;
	import AerSysCo.UI.Models.Memento.ShoppingCartDetailUIMemento;
    import com.affilia.components.NumberUtilities;	
	
	[Bindable]
	public class ShoppingCartDetailUI
	{
	    public var shoppingCartDetailId:int;
	    public var shoppingCartId:int;
	    public var shoppingCartShipmentId:int;
	    public var modelItemId:int;
	    public var lineItemNumber:int;
	    public var qtyOrdered:int;
	    public var qtyNeeded:int;
//	    public var price1:Number;
	    public var sku:String;
	    public var modelName:String;

	    public var modelItem:ModelItemUI;
	    public var inventory:InventoryUI;
	    
	    public function get price():Number 
	    {
	    	if (modelItem) 
	    	{
	    		return modelItem.price;
	    	} else 
	    	{
	    		return 0;
	    	}
	    }
	    
	    public function get qtyAvailable():int 
	    {
	    	if (inventory) 
	    	{
	    		return (inventory.qty - qtyNeeded);
	    	} else 
	    	{
	    		return 0;
	    	}
	    }
	    
	    public function get qtyAvailableStr():String 
	    {
	    	if (inventory) 
	    	{
	    		return (inventory.qty - qtyNeeded).toString();
	    	} else 
	    	{
	    		return "0";
	    	}
	    }
	    
	    public function get configuration():String 
	    {
	    	if (modelItem) 
	    	{
	    		return modelItem.configuration;
	    	} else 
	    	{
	    		return "n/a";
	    	}
	    }
	    
	    public function get description():String 
	    {
	    	if (modelItem && modelItem.item) 
	    	{
	    		return modelItem.item.description;
	    	} else 
	    	{
	    		return "n/a";
	    	}
	    }
	    
	    public function get priceString():String 
	    {
	    	return price.toFixed(2);
	    }
	    
	    public function get amountString():String 
	    {
	    	return Number(NumberUtilities.round(price, .001) * qtyNeeded).toFixed(2);
	    }
	    
	    private var memento:ShoppingCartDetailUIMemento;
	    public function setMemento():void 
	    {
	    	if (!memento)
	    		memento = new ShoppingCartDetailUIMemento();
	    	
	    	memento.qtyNeeded = qtyNeeded;
	    }
	    public function getMemento():void 
	    {
	    	if (memento) 
	    	{
	    		qtyNeeded = memento.qtyNeeded;
	    	}
	    }
	    
	    public function populateFromShoppingCartDetail(value:ShoppingCartDetail, cascade:Boolean = false):void 
	    {
	    	this.shoppingCartDetailId = value.shoppingCartDetailId;
	    	this.shoppingCartId = value.shoppingCartId;
	    	this.shoppingCartShipmentId = value.shoppingCartShipmentId;
	    	this.modelItemId = value.modelItemId;
	    	this.lineItemNumber = value.lineItemNumber;
	    	this.qtyOrdered = value.qtyOrdered;
	    	this.qtyNeeded = value.qtyNeeded;
//	    	this.price = value.price;
	    	this.sku = value.sku;
	    	this.modelName = value.modelName;
	    	
	    	if (cascade && value.modelItem) 
	    	{
	    		var m:ModelItemUI = new ModelItemUI();
	    		m.populateFromModelItem(value.modelItem, cascade);
	    		this.modelItem = (m);
	    	}

	    	if (cascade && value.inventory) 
	    	{
	    		var i:InventoryUI = new InventoryUI();
	    		i.populateFromInventory(value.inventory, cascade);
	    		this.inventory = (i);
	    	}
	    }
	    
	    public function toShoppingCartDetail():ShoppingCartDetail 
	    {
	    	var result:ShoppingCartDetail = new ShoppingCartDetail();
	    	
	    	result.shoppingCartDetailId = this.shoppingCartDetailId;
	    	result.shoppingCartId = this.shoppingCartId;
	    	result.shoppingCartShipmentId = this.shoppingCartShipmentId;
	    	result.modelItemId = this.modelItemId;
	    	result.lineItemNumber = this.lineItemNumber;

//-- to do - kostil !!!
	    	result.qtyOrdered = this.qtyNeeded;
	    	result.qtyNeeded = this.qtyNeeded;
	    	result.price = this.price;
	    	result.sku = this.sku;
	    	result.modelName = this.modelName;
	    	result.dateCreated = new Date();
	    	result.lastUpdateDate = new Date();
	    	
    		result.modelItem = this.modelItem.toModelItem();
    		result.inventory = this.inventory.toInventory();

	    	return result;
	    }
	    
	}
}
package AerSysCo.UI.Models
{
	import AerSysCo.Server.OrderDetail;
	import AerSysCo.Server.ShoppingCartDetail;
	
	[Bindable]
	public class OrderDetailUI
	{
	    public var orderDetailId:int;
	    public var orderId:int;
	    public var itemId:int;
	    public var qty:int;
	    public var price:Number;
	    public var sku:String;
	    public var multiplier:Number;
	    public var cost:Number;
	    public var lineNumber:int;
	    public var shopingCartDetailId:int;
	    public var item:ItemUI;
	    public var shoppingCartDetail:ShoppingCartDetailUI;
	    
	    public function get description():String
	    {
	    	if (item)
	    	{
	    		return item.description;
	    	} else 
	    	{
	    		return "n/a";
	    	}
	    }
	    
	    public function get modelName():String 
	    {
	    	if (shoppingCartDetail) 
	    	{
	    		return shoppingCartDetail.modelName;
	    	} else 
	    	{
	    		return "n/a";
	    	}
	    }
	    
	    public function populateFromOrderDetail(value:OrderDetail):void 
	    {
	    	this.orderDetailId = value.orderDetailId;
	    	this.orderId = value.orderId;
	    	this.itemId = value.itemId;
	    	this.qty = value.qty;
	    	this.price = value.price;
	    	this.sku = value.sku;
	    	this.multiplier = value.multiplier;
	    	this.cost = value.cost;
	    	this.lineNumber = value.lineNumber;
	    	this.shopingCartDetailId = value.shopingCartDetailId;
 	    	
	    	if (value.item) 
	    	{
	    		this.item = new ItemUI();
	    		this.item.populateFromItem(value.item);
	    	}
	    	
	    	if (value.shoppingCartDetail) 
	    	{
	    		this.shoppingCartDetail = new ShoppingCartDetailUI();
	    		this.shoppingCartDetail.populateFromShoppingCartDetail(value.shoppingCartDetail);
	    	}
	    
	    }
	    
	    public function toOrderDetail():OrderDetail 
	    {
	    	var result:OrderDetail = new OrderDetail();
	    	
	    	result.orderDetailId = this.orderDetailId;
	    	result.orderId = this.orderId;
	    	result.itemId = this.itemId;
	    	result.qty = this.qty;
	    	result.price = this.price;
	    	result.sku = this.sku;
	    	result.multiplier = this.multiplier;
	    	result.cost = this.cost;
	    	result.lineNumber = this.lineNumber;
	    	result.shopingCartDetailId = this.shopingCartDetailId;
	    	
	    	if (this.item)
	    		result.item = this.item.toItem();
	    	
	    	if (this.shoppingCartDetail)
	    		result.shoppingCartDetail = this.shoppingCartDetail.toShoppingCartDetail();

	    	return result;
	    }
	}
}
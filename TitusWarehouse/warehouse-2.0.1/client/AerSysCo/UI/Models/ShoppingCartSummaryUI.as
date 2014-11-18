package AerSysCo.UI.Models
{
	[Bindable]
	public class ShoppingCartSummaryUI
	{
		public var modelName:String;
	    public var qtyOrdered:int;
	    public var qtyNeeded:int;

	    public var modelItem:ModelItemUI;
	    
	    public static function create(value:ShoppingCartDetailUI):ShoppingCartSummaryUI 
	    {
	    	var result:ShoppingCartSummaryUI = new ShoppingCartSummaryUI();

	    	result.modelName = value.modelName;
	    	result.qtyOrdered = value.qtyOrdered;
	    	result.qtyNeeded = value.qtyNeeded;
	    	result.modelItem = value.modelItem;
	    	
	    	return result;
	    }
	    
	    public function addShoppingCartDetail(value:ShoppingCartDetailUI):void 
	    {
	    	if (value.modelItemId == modelItem.modelItemId) 
	    	{
	    		qtyNeeded += value.qtyNeeded;
	    		qtyOrdered += value.qtyOrdered;
	    	}
	    }
	    
	    public function get sku():String 
	    {
	    	if (modelItem && modelItem.item) 
	    	{
		    	return modelItem.item.sku;
	    	} else 
	    	{
	    		return "n/a";
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
	    
	    public function get price():String 
	    {
	    	if (modelItem && modelItem.price) 
	    	{
	    		return modelItem.price.toFixed(2);
	    	} else 
	    	{
	    		return "0";
	    	}
	    }
	    
	}
}
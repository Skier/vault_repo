package AerSysCo.UI.Models
{
	import AerSysCo.Server.Item;
	
	[Bindable]
	public class ItemUI
	{
	    public var itemId:int;
	    public var sku:String;
	    public var description:String;
	    public var width:Number ;
	    public var length:Number;
	    public var height:Number;
	    public var weight:Number;
	    public var qtyIncrement:int;
	    public var isActive:Boolean;

		public function populateFromItem(value:Item, cascade:Boolean = false):void 
		{
			this.itemId = value.itemId;
			this.sku = value.sku;
			this.description = value.description;
			this.width = value.width;
			this.length = value.length;
			this.height = value.height;
			this.weight = value.weight;
			this.qtyIncrement = value.qtyIncrement;
			this.isActive = value.isActive;
		}
		
		public function toItem():Item 
		{
			var result:Item = new Item();
			
			result.itemId = this.itemId;
			result.sku = this.sku;
			result.description = this.description;
			result.width = this.width;
			result.length = this.length;
			result.height = this.height;
			result.weight = this.weight;
			result.qtyIncrement = this.qtyIncrement;
			result.isActive = this.isActive;
	    	result.dateCreated = new Date();
	    	result.lastUpdateDate = new Date();

			return result;
		}
		
	}
}
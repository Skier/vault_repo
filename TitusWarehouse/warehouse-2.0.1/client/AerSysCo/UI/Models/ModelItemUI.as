package AerSysCo.UI.Models
{
	import AerSysCo.Server.ModelItem;
	
	[Bindable]
	public class ModelItemUI
	{
	    public var modelItemId:int;
	    public var modelId:int;
	    public var itemId:int;
	    public var configuration:String;
	    public var price:Number;
	    public var isActive:Boolean;
	    public var imageURL:String;
	    public var xmlBullerDescription:String;
	    public var item:ItemUI;

		public function populateFromModelItem(value:ModelItem, cascade:Boolean = false):void 
		{
			this.modelItemId = value.modelItemId;
			this.modelId = value.modelId;
			this.itemId = value.itemId;
			this.configuration = value.configuration;
			this.price = value.price;
			this.isActive = value.isActive;
			this.imageURL = value.imageURL;
			this.xmlBullerDescription = value.xmlBullerDescription;
			
			if (cascade && value.item) 
			{
				var i:ItemUI = new ItemUI();
				i.populateFromItem(value.item, cascade);
				this.item = (i);
			}
		}
		
		public function toModelItem():ModelItem 
		{
			var result:ModelItem = new ModelItem();
			
			result.modelItemId = this.modelItemId;
			result.modelId = this.modelId;
			result.itemId = this.itemId;
			result.configuration = this.configuration;
			result.price = this.price;
			result.isActive = this.isActive;
			result.imageURL = this.imageURL;
			result.xmlBullerDescription = this.xmlBullerDescription;
	    	result.dateCreated = new Date();
	    	result.lastUpdateDate = new Date();
			
			if (this.item != null) 
			{
				result.item = this.item.toItem();
			}

			return result;
		}
		
	}
}
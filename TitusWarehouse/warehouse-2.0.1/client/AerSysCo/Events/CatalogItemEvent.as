package AerSysCo.Events
{
	import flash.events.Event;
	import AerSysCo.Server.CatalogItem;
	import AerSysCo.Server.Warehouse;
	import AerSysCo.UI.Models.CatalogItemUI;

	public class CatalogItemEvent extends Event
	{
	    public static const OPEN_CATALOG_ITEM_WAREHOUSES:String = "openCatalogItemWarehouses";
	    public static const OPEN_CATALOG_ITEM_DETAIL:String = "openCatalogItemDetail";

	    public var catalogItem:CatalogItemUI;
	
	    public function CatalogItemEvent(type:String, catalogItem:CatalogItemUI,
	        bubbles:Boolean = true, cancelable:Boolean = false)
	    {
	        super(type, bubbles, cancelable);
	
	        this.catalogItem = catalogItem;
	    }
	    
	    override public function clone():Event
	    {
	        return new CatalogItemEvent(type, catalogItem, bubbles, cancelable);
	    }
	    
	}
}
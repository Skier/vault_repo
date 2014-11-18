package AerSysCo.Events
{
	import flash.events.Event;
	import AerSysCo.Server.CatalogItem;
	import AerSysCo.Server.Warehouse;
	import AerSysCo.UI.Models.CatalogItemUI;
	import mx.collections.ArrayCollection;

	public class CatalogItemsEvent extends Event
	{
	    public static const ADD_CATALOG_ITEMS:String = "addCatalogItems";

	    public var catalogItems:ArrayCollection;
	
	    public function CatalogItemsEvent(type:String, catalogItems:ArrayCollection,
	        bubbles:Boolean = false, cancelable:Boolean = false)
	    {
	        super(type, bubbles, cancelable);
	
	        this.catalogItems = catalogItems;
	    }
	    
	    override public function clone():Event
	    {
	        return new CatalogItemsEvent(type, catalogItems, bubbles, cancelable);
	    }
	    
	}
}
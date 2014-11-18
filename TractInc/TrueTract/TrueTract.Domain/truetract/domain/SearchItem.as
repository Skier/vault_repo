package truetract.domain
{
	import flash.xml.XMLNode;
	
	[Bindable]
	[RemoteClass(alias="TractInc.TrueTract.Entity.SearchItemInfo")]
	public class SearchItem
	{
		public static const ITEM_TYPE_PROJECT:String = "project";
		public static const ITEM_TYPE_DOCUMENT:String = "document";
		public static const ITEM_TYPE_UNDEFINED:String = "__undefined__";
		
	    public var SearchItemId:int;
	    public var ItemTypeId:int;
	    public var ItemId:int;
//	    public var ItemXmlValue:String;

		private var _ItemXmlValue:String;
		public function set ItemXmlValue(value:String):void 
		{
			_ItemXmlValue = value;
			xmlItem = new XML(ItemXmlValue);
		}
		public function get ItemXmlValue():String 
		{
			return _ItemXmlValue;
		}

	    private var _xmlItem:XML;
	    public function get xmlItem():XML
	    {
	    	return _xmlItem;
	    }
	    private function set xmlItem(value:XML):void 
	    {
	    	_xmlItem = value;
	    }
	    
	    public function get itemType():String 
	    {
	    	if (xmlItem.attribute("itemtype") == "project") 
	    	{
	    		return ITEM_TYPE_PROJECT;
	    	} else if (xmlItem.attribute("itemtype") == "document") 
	    	{
	    		return ITEM_TYPE_DOCUMENT;
	    	} else 
	    	{
	    		return ITEM_TYPE_UNDEFINED;
	    	}
	    }
	    
	    public function get docInstrument():String 
	    {
	    	if (itemType != ITEM_TYPE_DOCUMENT)
	    		return "";
	    	
	    	var result:String = "";
	    	
	    	result += xmlItem.@type;	
   	        result += " - ";
   	        result += xmlItem.@docNo;
   	        result += " ";
   	        result += xmlItem.@volume;
   	        result += " / ";
   	        result += xmlItem.@page;

			return result;
	    }
	}
}
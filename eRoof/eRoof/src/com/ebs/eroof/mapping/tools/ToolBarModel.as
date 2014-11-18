package com.ebs.eroof.mapping.tools
{
	import com.ebs.eroof.mapping.IMapContainer;
	
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class ToolBarModel
	{
		public var tools:ArrayCollection;
		public var activeTool:IMapTool;
		public var mapContainer:IMapContainer;
		
		private static var _instance:ToolBarModel;
        public static function getInstance():ToolBarModel
        {
			if (_instance == null)
            	_instance = new ToolBarModel(new Private());
			
			return _instance;
		}
         
		public function ToolBarModel(accessPrivate:Private) 
		{
			tools = new ArrayCollection();
		}
	}
}

class Private {}
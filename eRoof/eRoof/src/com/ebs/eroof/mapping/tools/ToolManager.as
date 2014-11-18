package com.ebs.eroof.mapping.tools
{
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.events.IEventDispatcher;
	
	import mx.collections.ArrayCollection;

	[Bindable]
	public class ToolManager extends EventDispatcher
	{
		public var tools:ArrayCollection;
		
		private var activeTool:IMapTool;
		
		private static var instance:ToolManager;
		
		public static function getInstance():ToolManager 
		{
			if (!instance)
				instance = new ToolManager(new Private());
			
			return instance;
		}
		
		public function ToolManager(privClass:Private, target:IEventDispatcher=null)
		{
			super(target);
			
			tools = new ArrayCollection();
		}
		
		public function addTool(tool:IMapTool):void 
		{
			tools.addItem(tool);
			tool.addEventListener("activateRequest", activateToolHandler);
		}
		
		private function activateToolHandler(event:Event):void 
		{
			var newTool:IMapTool = event.currentTarget as IMapTool;
			
			if (activeTool)
				activeTool.deactivate();
			
			activeTool = newTool;
			activeTool.activate();
		}
	}
}

class Private {}

package com.ebs.eroof.mapping.tools
{
	import com.ebs.eroof.mapping.IMapContainer;
	
	import flash.events.IEventDispatcher;
	
	public interface IMapTool extends IEventDispatcher
	{
		function init(mapContainer:IMapContainer):void;
		function activate():Boolean;
		function deactivate():Boolean;
		
		function get icon():Class;
		function get name():String;
		function get description():String;
	}
}
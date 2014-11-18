package com.ebs.eroof.mapping.tools
{
	import com.ebs.eroof.view.maps.MapContainer;
	import flash.events.EventDispatcher;
	import flash.events.IEventDispatcher;

	public class MapToolCreateMarker extends EventDispatcher implements IMapTool
	{
		public function MapToolCreateMarker(target:IEventDispatcher=null)
		{
			super(target);
		}
		
		public function init(mapContainer:MapContainer):void
		{
		}
		
		public function activate():Boolean
		{
		}
		
		public function deactivate():Boolean
		{
		}
		
		public function get icon():Class
		{
			return null;
		}
		
		public function get name():String
		{
			return null;
		}
		
		public function get description():String
		{
			return null;
		}
		
	}
}
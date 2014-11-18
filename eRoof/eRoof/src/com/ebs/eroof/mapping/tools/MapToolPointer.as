package com.ebs.eroof.mapping.tools
{
	import com.afcomponents.umap.core.MapObject;
	import com.afcomponents.umap.core.UMap;
	import com.afcomponents.umap.events.MapEvent;
	import com.afcomponents.umap.types.LatLng;
	import com.ebs.eroof.mapping.IMapContainer;
	import com.ebs.eroof.mapping.mapClasses.SectionPolygon;
	
	import flash.events.EventDispatcher;
	import flash.events.IEventDispatcher;
	import flash.events.MouseEvent;

	public class MapToolPointer extends EventDispatcher implements IMapTool
	{
        [Embed(source="/assets/icons16/cursor.png")]
        [Bindable]
        private var iconCursor:Class;
        
        private var _activated:Boolean;
        [Bindable]
        public function get activated():Boolean { return _activated; }
        public function set activated(value:Boolean):void { _activated = value; }
                
		private var uMap:UMap;
		private var mapContainer:IMapContainer;
		
		private var currentPolygon:SectionPolygon;
		
		public function MapToolPointer(target:IEventDispatcher=null)
		{
			super(target);
		}
		
		public function init(mapContainer:IMapContainer):void
		{
			this.mapContainer = mapContainer;
			this.uMap = mapContainer.getUMap();
		}
		
		public function activate():Boolean
		{
			if (!mapContainer || !uMap)
				return false;
				
            mapContainer.addEventListener(MouseEvent.MOUSE_MOVE, containerMouseMoveHandler);
            uMap.display.overlayManager.addEventListener(MouseEvent.CLICK, mapClickHandler);
            uMap.display.gridManager.addEventListener(MouseEvent.CLICK, mapGridClickHandler);
            uMap.addEventListener(MapEvent.DRAG_START, mapDragStartHandler);
            uMap.addEventListener(MapEvent.DRAG_STOP, mapDragStopHandler);
            
            uMap.enableDragging = true;
            activated = true;
            return true;
		}
		
		public function deactivate():Boolean
		{
			if (!mapContainer || !uMap)
				return false;
				
            mapContainer.addEventListener(MouseEvent.MOUSE_MOVE, containerMouseMoveHandler);
            uMap.display.overlayManager.removeEventListener(MouseEvent.CLICK, mapClickHandler);
            uMap.display.gridManager.removeEventListener(MouseEvent.CLICK, mapGridClickHandler);
            uMap.removeEventListener(MapEvent.DRAG_START, mapDragStartHandler);
            uMap.removeEventListener(MapEvent.DRAG_STOP, mapDragStopHandler);
            activated = false;
            return true;
		}
		
		public function get icon():Class
		{
			return iconCursor;
		}
		
		public function get name():String
		{
			return "Edit tool";
		}
		
		public function get description():String
		{
			return "Edit tool";
		}
		
        private function deleteSelectedObject():void 
        {
            if (currentPolygon) 
            {
                selectMapObject(null);
            	currentPolygon.remove();
            }
        }
                
        private function containerMouseMoveHandler(event:MouseEvent):void 
        {
        	if (!uMap)
        		return;
        	
        	return;  // test option !!
        		
            var currentLatLng:LatLng = uMap.getMouseLatLng();
            if (currentLatLng == null)
                    return;
            
            updateStartMarker(currentLatLng);
            updateTempLine(currentLatLng);
            
            if (currentPolyline != null) 
                    currentPolyline.setLastPoint(currentLatLng);

            if (currentPolygon != null) 
                    currentPolygon.setLastPoint(currentLatLng);
        }
                
        private function mapClickHandler(event:MouseEvent):void 
        {
        	if (isMapDragging)
        		return;
        	
        	if (event.target is SectionPolygon)
                selectMapObject(event.target as SectionPolygon);
        }
        
        private function mapGridClickHandler(event:MouseEvent):void 
        {
        	if (isMapDragging)
        		return;
        	
            selectMapObject(null);
        }
        
        private function selectMapObject(object:MapObject):void 
        {
        	if (currentPolygon && currentPolygon != object)
        		currentPolygon.editable = false;
        		
        	if (object is SectionPolygon)
        	{
        		var polygon:SectionPolygon = object as SectionPolygon;
        		polygon.editable = true;
        		currentPolygon = polygon;
        	} else 
        	{
        		currentPolygon = null;
        	}
        }
        
        private var isMapDragging:Boolean = false;
        private function mapDragStartHandler(event:MapEvent):void 
        {
            isMapDragging = true;
        }
        
        private function mapDragStopHandler(event:MapEvent):void 
        {
            isMapDragging = false;
        }
                
	}
}
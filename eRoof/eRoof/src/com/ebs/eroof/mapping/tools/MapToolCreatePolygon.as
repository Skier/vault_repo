package com.ebs.eroof.mapping.tools
{
	import com.afcomponents.umap.core.UMap;
	import com.afcomponents.umap.events.MapEvent;
	import com.afcomponents.umap.overlays.Layer;
	import com.afcomponents.umap.overlays.Polyline;
	import com.afcomponents.umap.types.LatLng;
	import com.ebs.eroof.mapping.IMapContainer;
	import com.ebs.eroof.mapping.mapClasses.EditablePolygon;
	import com.ebs.eroof.mapping.mapClasses.SectionPolygon;
	
	import flash.events.EventDispatcher;
	import flash.events.IEventDispatcher;
	import flash.events.MouseEvent;
	import flash.geom.Point;

	public class MapToolCreatePolygon extends EventDispatcher implements IMapTool
	{
        [Embed(source="/assets/icons16/layer_shape_polygon.png")]
        [Bindable]
        private var iconPolygon:Class;
        
		private var uMap:UMap;
		private var mapContainer:IMapContainer;
		
		private var currentPolygon:EditablePolygon;
		private var tempPolyline:Polyline;
		
		private var points:Array;
		
		private var prevPoint:Point = new Point();
		
		public function MapToolCreatePolygon(target:IEventDispatcher=null)
		{
			super(target);
		}
		
		public function init(mapContainer:IMapContainer):void
		{
			this.mapContainer = mapContainer;
			this.uMap = mapContainer.getUMap();
		}
		
		private function get polygonLayer():Layer 
		{
			return mapContainer.getPolygonLayer();
		}
		
		public function activate():Boolean
		{
			if (!mapContainer || !uMap)
				return false;
				
            mapContainer.addEventListener(MouseEvent.MOUSE_MOVE, containerMouseMoveHandler);
            uMap.addEventListener(MapEvent.CLICK, mapClickHandler);
            uMap.addEventListener(MapEvent.DRAG_START, mapDragStartHandler);
            uMap.addEventListener(MapEvent.DRAG_STOP, mapDragStopHandler);
            
            uMap.enableDragging = true;
            
            initPoints();
            
            return true;
		}
		
		public function deactivate():Boolean
		{
			if (!mapContainer || !uMap)
				return false;
				
            mapContainer.removeEventListener(MouseEvent.MOUSE_MOVE, containerMouseMoveHandler);
            uMap.removeEventListener(MapEvent.CLICK, mapClickHandler);
            uMap.removeEventListener(MapEvent.DRAG_START, mapDragStartHandler);
            uMap.removeEventListener(MapEvent.DRAG_STOP, mapDragStopHandler);

            return true;
		}
		
		private function initPoints():void 
		{
			points = new Array();
		}
		
		public function get icon():Class
		{
			return iconPolygon;
		}
		
		public function get name():String
		{
			return "Create Polygon tool";
		}
		
		public function get description():String
		{
			return "Create Polygon tool";
		}
		
        private function containerMouseMoveHandler(event:MouseEvent):void 
        {
        	if (!uMap)
        		return;
        	
            var currentLatLng:LatLng = uMap.getMouseLatLng();
            if (currentLatLng == null)
            	return;
            
            if (points.length < 3)
            	updateTempPolyline(currentLatLng);
            else 
            	updateCurrentPolygon(currentLatLng);
        }
        
        private function updateTempPolyline(position:LatLng):void 
        {
        	if (points.length == 0)
        		return;
        	
        	points[1] = position;
        	tempPolyline.points = points;
        	tempPolyline.refresh(true);
        }
                
        private function updateCurrentPolygon(position:LatLng):void 
        {
        	if (points.length < 3)
        		return;
        	
        	if (tempPolyline)
        	{
        		tempPolyline.remove();
        		tempPolyline = null;
        	}
        	
        	if (!currentPolygon)
        		currentPolygon = createCurrentPolygon();

        	currentPolygon.lastPoint = position;
        	currentPolygon.refresh(true);
        }
        
        private function createTempPolyline():Polyline 
        {
        	var result:Polyline = new Polyline();
	        	result.points = points;
	        	result.autoInfo = false;
	        	result.smartPosition = false;
	        	result.setStyle(SectionPolygon.defaultStyle.strokeStyle);
			
			polygonLayer.addOverlay(result);
			
        	return result;
        }
        
        private function createCurrentPolygon():SectionPolygon 
        {
        	var result:SectionPolygon = SectionPolygon.getPolygon(points);
        	result.autoInfo = false;

        	polygonLayer.addOverlay(result);

        	return result;
        }
        
        private function closePolygon():void 
        {
        	points.splice(points.length-1, 1);
        	prepareNewDrawing();
        }
        
        private function prepareNewDrawing():void 
        {
        	currentPolygon = null;
        	tempPolyline = null;
        	points = new Array();
        }
                
        private function mapClickHandler(event:MapEvent):void 
        {
        	if (isMapDragging)
        		return;
        	
            var currentLatLng:LatLng = uMap.getMouseLatLng();
            if (currentLatLng == null)
            	return; 

			var currPoint:Point = uMap.getMouseBitmapXY();
			if (currPoint.x == prevPoint.x && currPoint.y == prevPoint.y) 
			{
				closePolygon();
				return;
			} else 
			{
				prevPoint = currPoint;
			}

            if (points.length == 0)
            {
            	points.push(currentLatLng);
            	points.push(currentLatLng.clone());

                tempPolyline = createTempPolyline();
            } else 
            {
            	points.push(currentLatLng);
				
				if (tempPolyline)
				{
	            	tempPolyline.remove();
	            	tempPolyline = null;
				}
            	
            	if (!currentPolygon)
            		currentPolygon = createCurrentPolygon();
            	
            	currentPolygon.corePoints = points;
            	currentPolygon.refresh(true);
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
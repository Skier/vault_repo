package com.loveland.mapper.tools
{
	import com.afcomponents.umap.core.UMap;
	import com.afcomponents.umap.events.MapEvent;
	import com.afcomponents.umap.overlays.Layer;
	import com.afcomponents.umap.overlays.Polyline;
	import com.afcomponents.umap.styles.GeometryStyle;
	import com.afcomponents.umap.types.LatLng;
	import com.loveland.mapper.tools.events.ToolEvent;
	import com.loveland.mapper.tracts.EditablePolygon;
	import com.loveland.mapper.tracts.TemporaryLine;
	
	import flash.events.MouseEvent;
	import flash.geom.Point;
	
	import mx.events.DynamicEvent;
	
	public class PolygonTool extends AbstractTool
	{
		private var mainMap:UMap;
		private var activeLayer:Layer;
		
		private var currentPolygonPoints:Array;
		private var tempLine:TemporaryLine;
		private var tempPolyline:Polyline;
		
		private var currentPolygon:EditablePolygon;

		public function PolygonTool(map:UMap, layer:Layer)
		{
			this.mainMap = map;
			this.activeLayer = layer;
		}
		
		override public function activate():void 
		{
			mainMap.addEventListener(MapEvent.CLICK, mapClickHandler);
			mainMap.addEventListener(MapEvent.DRAG_START, mapDragStartHandler);
			mainMap.addEventListener(MapEvent.DRAG_STOP, mapDragStopHandler);
			
			dispatchEvent(new ToolEvent(ToolEvent.TOOL_ACTIVATE, this));
		}
		
		override public function deactivate():void 
		{
			mainMap.removeEventListener(MapEvent.CLICK, mapClickHandler);
			mainMap.removeEventListener(MapEvent.DRAG_START, mapDragStartHandler);
			mainMap.removeEventListener(MapEvent.DRAG_STOP, mapDragStopHandler);

			dispatchEvent(new ToolEvent(ToolEvent.TOOL_DEACTIVATE, this));
		}
		
		private var _polygonStyle:GeometryStyle;
		public function get polygonStyle():GeometryStyle 
		{
			if (_polygonStyle == null)
				_polygonStyle = defaultTempLineStyle;

			return _polygonStyle;
		}
		public function set polygonStyle(value:GeometryStyle):void  
		{
			_polygonStyle = value;
		}
		private function get defaultPolygonStyle():GeometryStyle 
		{
			var result:GeometryStyle = new GeometryStyle();
				result.fill = GeometryStyle.RGB;
				result.fillRGB = 0x0000ff;
				result.fillAlpha = 0.3;
				result.stroke = GeometryStyle.RGB;
				result.strokeRGB = 0x000099;
				result.strokeThickness = 2;
				result.strokeAlpha = .8;
			return result;
		}
		
		private var _tempLineStyle:GeometryStyle;
		public function get tempLineStyle():GeometryStyle 
		{
			if (_tempLineStyle == null)
				_tempLineStyle = defaultTempLineStyle;

			return _tempLineStyle;
		}
		public function set tempLineStyle(value:GeometryStyle):void  
		{
			_tempLineStyle = value;
		}
		private function get defaultTempLineStyle():GeometryStyle 
		{
			var result:GeometryStyle = new GeometryStyle();
				result.stroke = GeometryStyle.RGB;
				result.strokeRGB = 0xff0000;
				result.strokeThickness = 1;
				result.strokeAlpha = 1;
			return result;
		}
		
		override public function mouseMoveHandler(event:MouseEvent):void 
		{
			if (tempLine != null)
				tempLine.endPoint = mainMap.getMouseLatLng();
		}
		
		private function drawCurrentPolygon():void 
		{
			if (activeLayer == null)
			{
				activeLayer = new Layer();
				mainMap.addOverlay(activeLayer);
			}
				
			var currentLatLng:LatLng = mainMap.getMouseLatLng();	

			if (tempLine == null) 
			{
				var startPoint:LatLng = mainMap.getMouseLatLng();
				var endPoint:LatLng = mainMap.getMouseLatLng();
	
				tempLine = new TemporaryLine(startPoint, endPoint, tempLineStyle);
				
				activeLayer.addOverlay(tempLine);
			} else 
			{
				tempLine.startPoint = currentLatLng;
			}

			if (currentPolygonPoints == null)
				currentPolygonPoints = new Array();
				
			currentPolygonPoints.push(currentLatLng);
			
			if (currentPolygon != null)
			{
				currentPolygon.remove();
				currentPolygon = null;
			}
			
			if (currentPolygonPoints.length == 2) 
			{
				var tempParam:Object = new Object();
					tempParam.points = currentPolygonPoints;
				
				tempPolyline = new Polyline(tempParam, polygonStyle);
				tempPolyline.autoInfo = false;
				
				activeLayer.addOverlay(tempPolyline);
			} else if (currentPolygonPoints.length > 2) 
			{
				if (tempPolyline != null)
					tempPolyline.remove();
					
				var param:Object = new Object();
					param.points = currentPolygonPoints;
				
				currentPolygon = new EditablePolygon(param, polygonStyle);
				currentPolygon.map = mainMap;
				currentPolygon.autoInfo = false;

				activeLayer.addOverlay(currentPolygon);
			}
		}
		
		private function closeCurrentPolygon():void 
		{
			var event:DynamicEvent = new DynamicEvent("closePolygon");
			event.polygon = currentPolygon;
			dispatchEvent(event);


//			polygon.editable = true;

			currentPolygon = null;
			currentPolygonPoints = null;
			
			if (tempLine != null)
			{
				tempLine.remove();
				tempLine = null;
			}
		}

		private var dragging:Boolean = false;
		private function mapDragStartHandler(event:MapEvent):void 
		{
			dragging = true;
		}
		
		private function mapDragStopHandler(event:MapEvent):void 
		{
			dragging = false;
		}
			
		private var prevMapClickPoint:Point = new Point();
		private var currentMapClickPoint:Point = new Point();
		private function mapClickHandler(event:*):void
		{

			if (dragging)
			{
				return;
			}
			
			currentMapClickPoint = mainMap.getComponentXYFromBitmapXY(mainMap.getMouseBitmapXY());

			drawPolygon();

			prevMapClickPoint = currentMapClickPoint;
		}
		
		private function drawPolygon():void 
		{
			if (currentMapClickPoint.x == prevMapClickPoint.x 
				&& currentMapClickPoint.y == prevMapClickPoint.y) 
				closeCurrentPolygon();
			else 
				drawCurrentPolygon();
		}
	}
}
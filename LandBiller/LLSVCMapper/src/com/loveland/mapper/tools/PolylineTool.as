package com.loveland.mapper.tools
{
	import com.afcomponents.umap.core.UMap;
	import com.afcomponents.umap.events.MapEvent;
	import com.afcomponents.umap.events.OverlayEvent;
	import com.afcomponents.umap.overlays.Layer;
	import com.afcomponents.umap.overlays.Polyline;
	import com.afcomponents.umap.types.LatLng;
	import com.loveland.mapper.markers.CustomMarker;
	import com.loveland.mapper.markers.TemporaryLine;
	
	import flash.events.MouseEvent;
	import flash.geom.Point;
	
	public class PolylineTool
	{
		private var map:UMap;
		private var layer:Layer;
		
		private var polyline:Polyline;
		private var tempLine:TemporaryLine;
		
		private var prevClickPosition:Point;
		
		public function PolylineTool(map:UMap, layer:Layer)
		{
			this.map = map;
			this.layer = layer;
			
			map.addEventListener(MapEvent.CLICK, onMapClick);
		}
		
		public function mouseMoveHandler(event:MouseEvent):void 
		{
			if (tempLine != null)
				tempLine.endPoint = map.getMouseLatLng();
		}
		
		private function startDrawing():void
		{
			var startPoint:LatLng = map.getMouseLatLng();
			var endPoint:LatLng = map.getMouseLatLng();

			var tempStyle:GeometryStyle = new GeometryStyle();
			tempStyle.stroke = GeometryStyle.RGB;
			tempStyle.strokeRGB = 0xff0000;
			tempStyle.strokeThickness = 1;
			tempStyle.strokeAlpha = 1;

			tempLine = new TemporaryLine(startPoint, endPoint, tempStyle);
			
			var style:GeometryStyle = new GeometryStyle();
			style.stroke = GeometryStyle.RGB;
			style.strokeRGB = 0x000099;
			style.strokeThickness = 4;
			style.strokeAlpha = .7;
			
			var param:Object = new Object();
			param.points = new Array();
			param.points.push(startPoint);
			
			polyline = new Polyline(param, style);
			polyline.autoInfo = false;
			
			trace("create new polyline");
			layer.addOverlay(currentPolyline);
			
			var marker:CustomMarker = new CustomMarker();
			marker.name = "Start Point";
			marker.position = startPoint;
			marker.autoInfo = false;
			marker.draggable = true;
			
			marker.addEventListener(OverlayEvent.OVERLAY_DRAG_STOP, onMarkerDragStop);
			
			layer.addOverlay(marker);
			
			prevClickPosition = map.getComponentXYFromLatLng(startPoint);
		}
		
		private function onMarkerDragStop(event:OverlayEvent):void 
		{
			polyline.refresh(true);
		}
		
		private function continueDrawing():void
		{
			if (polyline == null)
				return;
			
			var currentPoint:LatLng = map.getMouseLatLng();
			polyline.points.push(currentPoint);
			polyline.refresh(true);
			
			tempLine.startPoint = currentPoint;
		}
		
		private function onMapClick(event:MapEvent):void 
		{
			if (polyline != null) 
			{
				var currClickPosition:Point = map.getComponentXYFromLatLng(event.latlng);
				if (prevClickPosition.x == currClickPosition.x && prevClickPosition.y == currClickPosition.y)
					closePolyline();
				else 
					continueDrawing();
			} else 
			{
				startDrawing();
			}
		}

	}
}
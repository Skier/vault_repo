package com.loveland.mapper.tracts
{
	import com.afcomponents.umap.core.UMap;
	import com.afcomponents.umap.events.OverlayEvent;
	import com.afcomponents.umap.overlays.Layer;
	import com.afcomponents.umap.overlays.Polygon;
	import com.afcomponents.umap.styles.MarkerStyle;
	import com.afcomponents.umap.types.LatLng;
	import com.loveland.mapper.UI.events.VertexEvent;
	import com.loveland.mapper.markers.MiddleMarker;
	import com.loveland.mapper.markers.VertexMarker;
	
	import flash.events.Event;
	import flash.geom.Point;

	public class EditablePolygon extends Polygon
	{
        [Embed(source="/assets/icons16/radio_button.png")]
        [Bindable]
        private var iconVertex:Class;
		
        [Embed(source="/assets/icons16/radio_button_uncheck.png")]
        [Bindable]
        private var iconMidpoint:Class;
		
		private var _editable:Boolean;
		[Bindable]
		public function get editable():Boolean { return _editable; }
		public function set editable(value:Boolean):void 
		{
			_editable = value;
			
			if (_editable)
				setEditable();
			else 
				setNonEditable();
		}
		
		private var _vertexStyle:MarkerStyle = new MarkerStyle();
		[Bindable]
		public function get vertexStyle():MarkerStyle { return _vertexStyle; }
		public function set vertexStyle(value:MarkerStyle):void 
		{
			_vertexStyle = value;
			
			//setVertexStyle();
		}
		
		private var _midpointStyle:MarkerStyle = new MarkerStyle();
		[Bindable]
		public function get midpointStyle():MarkerStyle { return _midpointStyle; }
		public function set midpointStyle(value:MarkerStyle):void 
		{
			_midpointStyle = value;
			
			//setMidpointStyle();
		}
		
		public var map:UMap;
		
		private var vertexMarkers:Array;
		private var midpointMarkers:Array;
		
		private var vertexLayer:Layer;
		private var midpointLayer:Layer;
		
		private var _isDirty:Boolean;
		public function get isDirty():Boolean { return _isDirty; }
		public function set isDirty(value:Boolean):void 
		{
			_isDirty = value;

			if (value) 
				dispatchEvent(new Event("polygonChange"));
		}
		
		public var gid:int;
		public var taxpin:String;
		public var acres:String;
		public var calculated:Number = 0;
		public var parceltype:int;
		public var area:Number = 0;
		public var len:Number = 0;
		public var pidn:String;
		
		public function get pointsString():String
		{
			var result:String = "";

			var corePoints:Array = points[0] as Array;
			for (var i:int = 0; i < corePoints.length; i++) 
			{
				var pnt:LatLng = corePoints[i] as LatLng;
				if (result != "")
					result += ",";
				result += (pnt.lng.toString() + " " + pnt.lat.toString());
			}
			result += ("," + startPoint.lng.toString() + " " + startPoint.lat.toString());

			return result;
		}
		
		public static function parse(tractObj:Object):EditablePolygon 
		{
			if (tractObj == null)
				return null;
				
			var param:Object = new Object();
				param.points = new Array();
				param.name = tractObj["taxpin"];

			var pointsStr:String = tractObj["geom"] as String;
			var points:Array = pointsStr.substring(pointsStr.indexOf("(((")+3, pointsStr.indexOf(")))")).split(",");
			for each (var latlngStr:String in points)
			{
				var latlons:Array = latlngStr.split(" ");
				param.points.push(new LatLng(Number(latlons[1]), Number(latlons[0])));
			}

			var result:EditablePolygon = new EditablePolygon(param);
				result.gid = tractObj["gid"];
				result.taxpin = tractObj["taxpin"];
				result.acres = tractObj["acres"];
				result.calculated = tractObj["calculated"];
				result.parceltype = tractObj["parceltype"];
				result.area = tractObj["area"];
				result.len = tractObj["len"];
				result.pidn = tractObj["pidn"];
			
			return result;
		}
		
		override public function remove():void 
		{
			editable = false;
			super.remove();
		}
		
		private function setEditable():void 
		{
			var corePoints:Array = points[0] as Array;
			if (corePoints != null && corePoints.length < 2)
				return;
			
			initMarkerLayers();
			
			initVertexMarkers();
			initMidpointMarkers();
		}
		
		private function setNonEditable():void 
		{
			draggable = false;

			if (vertexMarkers != null && vertexMarkers.length > 0)
			{
				while (vertexMarkers.length > 0) 
				{
					var vMarker:VertexMarker = vertexMarkers.pop() as VertexMarker;
					vMarker.remove();
				}
				vertexMarkers = null;
			}

			if (midpointMarkers != null && midpointMarkers.length > 0)
			{
				while (midpointMarkers.length > 0) 
				{
					var mMarker:MiddleMarker = midpointMarkers.pop() as MiddleMarker;
					mMarker.remove();
				}
				vertexMarkers = null;
			}
			lastMidpoint.remove();
		}
		
		private function initMarkerLayers():void 
		{
			if (vertexLayer == null)
				vertexLayer = new Layer();
			map.addOverlay(vertexLayer);
			
			vertexLayer.addEventListener(OverlayEvent.DRAG, vertexMarkerDragHandler, true); 
			vertexLayer.addEventListener(OverlayEvent.DRAG_STOP, vertexMarkerDragHandler, true); 
			
			if (midpointLayer == null)
				midpointLayer = new Layer();
			map.addOverlay(midpointLayer);

			midpointLayer.addEventListener(OverlayEvent.DRAG_START, middleMarkerDragStartHandler, true); 
			midpointLayer.addEventListener(OverlayEvent.DRAG, middleMarkerDragHandler, true); 
			midpointLayer.addEventListener(OverlayEvent.DRAG_STOP, middleMarkerDragStopHandler, true); 
		}
		
		private function initVertexMarkers():void 
		{
			vertexMarkers = new Array();
			
			var corePoints:Array = points[0] as Array;
			var len:int = corePoints.length; 
			for (var idx:int = 0; idx < len; idx++) 
			{ 
			   var markerPosition:LatLng = corePoints[idx]; 
			   var vertex:VertexMarker = createVertexMarker(markerPosition, idx); 
			   vertexLayer.addOverlay(vertex); 
			   vertexMarkers.push(vertex); 
			} 
		}
		
		private function createVertexMarker(latlng:LatLng, idx:int):VertexMarker 
		{
			var vertex:VertexMarker = new VertexMarker({data:idx}, vertexStyle, iconVertex);
				vertex.position = latlng;
				vertex.draggable = true;
				vertex.smartPosition = false;
				vertex.autoInfo = false;
				vertex.addEventListener(VertexEvent.DELETE, vertexDeleteHandler);

			return vertex; 
		} 
		
		private function initMidpointMarkers():void 
		{ 
			midpointMarkers = new Array();
			
			var midpoint:MiddleMarker;
			var corePoints:Array = points[0] as Array;
			var len:uint = corePoints.length; 
			for (var idx:uint = 1; idx < len; idx++) 
			{ 
				var point1:LatLng = corePoints[idx-1]; 
				var point2:LatLng = corePoints[idx]; 
				midpoint = createMidpointMarker(point1, point2, idx); 
				midpointLayer.addOverlay(midpoint); 
				midpointMarkers.push(midpoint); 
			}
			createLastMidpoint(); 
		}
		
		private var lastMidpoint:MiddleMarker;
		private function createLastMidpoint():void 
		{
			var corePoints:Array = points[0] as Array;
			lastMidpoint = createMidpointMarker(endPoint, startPoint, corePoints.length); 
			midpointLayer.addOverlay(lastMidpoint);
		}
		
		private function get startPoint():LatLng 
		{
			var corePoints:Array = points[0] as Array;
			return corePoints[0] as LatLng;
		}  

		private function get endPoint():LatLng 
		{
			var corePoints:Array = points[0] as Array;
			return corePoints[corePoints.length-1] as LatLng;
		}  

		private function getMidpointLatLng(prev:LatLng, next:LatLng):LatLng 
		{ 
			var prevPoint:Point = map.getBitmapXYFromLatLng(prev); 
			var nextPoint:Point = map.getBitmapXYFromLatLng(next); 
			return map.getLatLngFromBitmapXY(Point.interpolate(prevPoint, nextPoint, 0.5)); 
		} 

		private function createMidpointMarker(prev:LatLng, next:LatLng, idx:uint):MiddleMarker 
		{ 
			var latlng:LatLng = getMidpointLatLng(prev, next); 
			var midpoint:MiddleMarker = new MiddleMarker({data:idx}, midpointStyle, iconMidpoint);
				midpoint.position = latlng;
				midpoint.draggable = true;
				midpoint.smartPosition = false;
				midpoint.autoInfo = false;

			return midpoint; 
		} 
		
		override public function EditablePolygon(arg0:Object=null, arg1:Object=null, vertexLayer:Layer=null, midpointLayer:Layer=null)
		{
			this.map = this.core;
			this.vertexLayer = vertexLayer;
			this.midpointLayer = midpointLayer;
			
			super(arg0, arg1);
		}
		
		private function refreshMarkers():void 
		{
			setNonEditable();
			setEditable();
		}
		
		private function deleteVertex(vertex:VertexMarker):void 
		{
			if (vertex == null)
				return;
			
			if (!vertexExists(vertex))
				return;
				
			var corePoints:Array = points[0] as Array;

			if (corePoints.length < 4) 
			{
				setNonEditable();
				remove();
				dispatchEvent(new Event("removeRequest"));
				return;
			}
			
			var idx:uint = vertex.data as uint;
			
			corePoints.splice(idx, 1);
			refresh(true);

			if (!isDirty)	
				isDirty = true;

			refreshMarkers();
		}
		
		private function vertexExists(vertex:VertexMarker):Boolean 
		{
			var v:VertexMarker;
			for each (v in vertexMarkers) 
			{
				if (vertex == v)
					return true;
			}
			return false;
		}
		
		// handlers
		
		private function vertexMarkerDragHandler(event:OverlayEvent):void 
		{ 
			var idx:uint = uint(event.target.data);
			var latlng:LatLng = event.target.position; 
			var corePoints:Array = points[0] as Array;
			corePoints[idx] = latlng;
			refresh(true);

			if (idx == 0) 
				lastMidpoint.position = getMidpointLatLng(latlng, endPoint);
			
			if (idx == corePoints.length-1) 
				lastMidpoint.position = getMidpointLatLng(startPoint, latlng);

			if (idx > 0) 
				midpointMarkers[idx-1].position = getMidpointLatLng(vertexMarkers[idx-1].position, latlng); 

			if (idx < corePoints.length-1) 
				midpointMarkers[idx].position = getMidpointLatLng(latlng, vertexMarkers[idx+1].position);
			
			if (!isDirty)	
				isDirty = true;
		} 
		
		private function middleMarkerDragStartHandler(event:OverlayEvent):void 
		{ 
			var idx:uint = uint(event.target.data);
			var corePoints:Array = points[0] as Array;
			var newPoint:LatLng = new LatLng(MiddleMarker(event.target).position.lat, MiddleMarker(event.target).position.lng);

			if (idx == corePoints.length) 
				corePoints.push(newPoint);
			else 
				corePoints.splice(idx, 0, newPoint);

			points[0] = corePoints;
			refresh(true);

			if (!isDirty)	
				isDirty = true;
		} 

		private function middleMarkerDragHandler(event:OverlayEvent):void 
		{ 
		   	var corePoints:Array = points[0] as Array; 
		   	corePoints[uint(event.target.data)] = event.target.position; 
		   	points[0] = corePoints; 
			refresh(true);
		} 

		private function middleMarkerDragStopHandler(event:OverlayEvent):void 
		{
			var idx:uint = uint(event.target.data); 
			var latlng:LatLng = event.target.position; 

			var corePoints:Array = points[0] as Array;
			corePoints[idx] = latlng; 
			points[0] = corePoints;
			refresh(true); 
			
			var len:uint = vertexMarkers.length; 
			for (var i:uint = idx; i < len; i++)
			{ 
				vertexMarkers[i].data = i+1; 
			} 

			var vertex:VertexMarker = createVertexMarker(latlng, idx); 
			vertexMarkers.splice(idx, 0, vertex); 
			vertexLayer.addOverlay(vertex); 

			len = midpointMarkers.length; 
			for (var j:uint = idx; j < len; j++) 
			{ 
				midpointMarkers[j].data = j+2; 
			}
			lastMidpoint.data = uint(lastMidpoint.data) + 1; 
			event.target.remove(); 

			var midpoint:MiddleMarker = createMidpointMarker(vertexMarkers[idx-1].position, vertexMarkers[idx].position, idx);
			midpointMarkers.splice(idx-1, 1, midpoint); 
			midpointLayer.addOverlay(midpoint); 
			
			if (idx == corePoints.length-1) 
			{
				lastMidpoint = createMidpointMarker(startPoint, endPoint, corePoints.length);
				midpointLayer.addOverlay(lastMidpoint); 
			} else 
			{
				midpoint = createMidpointMarker(vertexMarkers[idx].position, vertexMarkers[idx+1].position, idx+1); 
				midpointMarkers.splice(idx, 0, midpoint); 
				midpointLayer.addOverlay(midpoint); 
			}
		}

		private function vertexDeleteHandler(event:VertexEvent):void 
		{
			deleteVertex(event.vertex);
		}
	}
}
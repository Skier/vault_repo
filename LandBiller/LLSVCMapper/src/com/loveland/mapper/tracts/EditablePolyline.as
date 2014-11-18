package com.loveland.mapper.tracts
{
	import com.afcomponents.umap.core.UMap;
	import com.afcomponents.umap.events.OverlayEvent;
	import com.afcomponents.umap.overlays.Layer;
	import com.afcomponents.umap.overlays.Polyline;
	import com.afcomponents.umap.styles.MarkerStyle;
	import com.afcomponents.umap.types.LatLng;
	import com.loveland.mapper.UI.events.VertexEvent;
	import com.loveland.mapper.markers.MiddleMarker;
	import com.loveland.mapper.markers.VertexMarker;
	
	import flash.geom.Point;

	public class EditablePolyline extends Polyline
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
		
		private function setEditable():void 
		{
			if (points != null && points.length < 2)
				return;
			
			draggable = true;
			
			initMarkerLayers();
			
			initVertexMarkers();
			initMidpointMarkers();
		
		}
		
		private function setNonEditable():void 
		{
			draggable = false;

			if (vertexMarkers != null && vertexMarkers.length > 0)
			{
				trace("total vertexes: " + vertexMarkers.length.toString());
				while (vertexMarkers.length > 0) 
				{
					var vMarker:VertexMarker = vertexMarkers.pop() as VertexMarker;
					trace("remove vertex: " + uint(vMarker.data).toString());
					vMarker.remove();
				}
				vertexMarkers = null;
			}

			if (midpointMarkers != null && midpointMarkers.length > 0)
			{
				trace("total middles: " + midpointMarkers.length.toString());
				while (midpointMarkers.length > 0) 
				{
					var mMarker:MiddleMarker = midpointMarkers.pop() as MiddleMarker;
					trace("remove middle: " + uint(mMarker.data).toString());
					mMarker.remove();
				}
				vertexMarkers = null;
			}
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
			
			var len:int = this.points.length; 
			for (var idx:int = 0; idx < len; idx++) 
			{ 
			   var markerPosition:LatLng = points[idx]; 
			   var marker:VertexMarker = createVertexMarker(markerPosition, idx); 
			   vertexLayer.addOverlay(marker); 
			   vertexMarkers.push(marker); 
			} 
		}
		
		private function createVertexMarker(latlng:LatLng, idx:int):VertexMarker 
		{
			var marker:VertexMarker = new VertexMarker({data:idx}, vertexStyle, iconVertex);
				marker.position = latlng;
				marker.draggable = true;
				marker.smartPosition = false;
				marker.addEventListener(VertexEvent.DELETE, vertexDeleteHandler);

			return marker; 
		} 
		
		private function initMidpointMarkers():void 
		{ 
			midpointMarkers = new Array();
			
			var len:uint = points.length; 
			for (var idx:uint = 1; idx < len; idx++) 
			{ 
				var point1:LatLng = points[idx-1]; 
				var point2:LatLng = points[idx]; 
				var marker:MiddleMarker = createMidpointMarker(point1, point2, idx); 
				midpointLayer.addOverlay(marker); 
				midpointMarkers.push(marker); 
			} 
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
			var marker:MiddleMarker = new MiddleMarker({data:idx}, midpointStyle, iconMidpoint);
				marker.position = latlng;
				marker.draggable = true;
				marker.smartPosition = false;

			return marker; 
		} 
		
		override public function EditablePolyline(arg0:Object=null, arg1:Object=null, vertexLayer:Layer=null, midpointLayer:Layer=null)
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
				
			if (points.length < 3) 
			{
				setNonEditable();
				remove();
				dispatchEvent(new Event("removeRequest"));
				return;
			}
			
			var idx:uint = vertex.data as uint;
			
			removeVertex(idx);
			
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
			points[idx] = latlng;
			
			refresh(true); 

			if (idx > 0) 
				midpointMarkers[idx-1].position = getMidpointLatLng(vertexMarkers[idx-1].position, latlng); 

			if (idx < points.length-1) 
				midpointMarkers[idx].position = getMidpointLatLng(latlng, vertexMarkers[idx+1].position); 
		} 
		
		private function middleMarkerDragStartHandler(event:OverlayEvent):void 
		{ 
			var idx:uint = uint(event.target.data); 
			addVertex(event.target.position, 0, idx); 
		} 

		private function middleMarkerDragHandler(event:OverlayEvent):void 
		{ 
		   var pnts:Array = this.points; 
		   pnts[uint(event.target.data)] = event.target.position; 
		   this.points = pnts; 
		} 

		private function middleMarkerDragStopHandler(event:OverlayEvent):void 
		{ 
			var idx:uint = uint(event.target.data); 
			var latlng:LatLng = event.target.position; 

			var pnts:Array = this.points; 
			pnts[idx] = latlng; 
			this.points = pnts; 
			
			var len:uint = vertexMarkers.length; 
			for (var i:uint = idx; i < len; i++)
			{ 
				vertexMarkers[i].data = i+1; 
			} 

			var vMarker:VertexMarker = createVertexMarker(latlng, idx); 
			vertexMarkers.splice(idx, 0, vMarker); 
			vertexLayer.addOverlay(vMarker); 

			len = midpointMarkers.length; 
			for (var j:uint = idx; j < len; j++) 
			{ 
				midpointMarkers[j].data = j+2; 
			} 
			event.target.remove(); 

			var mMarker:MiddleMarker;
			mMarker = createMidpointMarker(vertexMarkers[idx-1].position, vertexMarkers[idx].position, idx);
			midpointMarkers.splice(idx-1, 1, mMarker); 
			midpointLayer.addOverlay(mMarker); 

			mMarker = createMidpointMarker(vertexMarkers[idx].position, vertexMarkers[idx+1].position, idx+1); 
			midpointMarkers.splice(idx, 0, mMarker); 
			midpointLayer.addOverlay(mMarker); 
		}
		
		private function vertexDeleteHandler(event:VertexEvent):void 
		{
			deleteVertex(event.vertex);
		}
	}
}
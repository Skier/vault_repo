package com.ebs.eroof.mapping.mapClasses
{
	import com.afcomponents.umap.events.OverlayEvent;
	import com.afcomponents.umap.overlays.Layer;
	import com.afcomponents.umap.overlays.Marker;
	import com.afcomponents.umap.overlays.Polygon;
	import com.afcomponents.umap.styles.MarkerStyle;
	import com.afcomponents.umap.types.LatLng;
	import com.ebs.eroof.mapping.events.MarkerEvent;
	
	import flash.geom.Point;

	public class EditablePolygon extends Polygon
	{
        private var vertexMarkers:Array;
        private var middleMarkers:Array;
        
        private var vertexLayer:Layer;
        private var middleLayer:Layer;
            
		public function EditablePolygon(arg0:Object=null, arg1:Object=null)
		{
			super(arg0, arg1);
		}
		
        [Embed(source="/assets/icons16/radio_button.png")]
        [Bindable]
        private var iconVertex:Class;
                
        [Embed(source="/assets/icons16/radio_button_uncheck.png")]
        [Bindable]
        private var iconMiddle:Class;
                
        private var _vertexStyle:MarkerStyle;
        [Bindable]
        public function get vertexStyle():MarkerStyle 
        { 
        	if (!_vertexStyle)
        		_vertexStyle = new MarkerStyle();

        	return _vertexStyle; 
        }
        public function set vertexStyle(value:MarkerStyle):void 
        {
            _vertexStyle = value;
            //updateVertexStyle();
        }
            
        private var _middleStyle:MarkerStyle;
        [Bindable]
        public function get middleStyle():MarkerStyle 
        { 
        	if (!_middleStyle)
        		_middleStyle = new MarkerStyle();

        	return _middleStyle; 
        }
        public function set middleStyle(value:MarkerStyle):void 
        {
            _middleStyle = value;
            //updateMiddleStyle();
        }
        
        private var _editable:Boolean;
        [Bindable]
        public function get editable():Boolean { return _editable; }
        public function set editable(value:Boolean):void 
        {
        	if (value)
        		setEditable();
        	else 
        		setNonEditable();
        }
        
        public function get corePoints():Array 
        {
        	return points[0] as Array;
        }
            
        public function set corePoints(value:Array):void  
        {
        	points[0] = value;
        }
        
        public function get lastPoint():LatLng 
        {
        	if (corePoints.length > 0)
        		return corePoints[corePoints.length-1] as LatLng;
        	else 
        		return null;
        }
            
        public function set lastPoint(value:LatLng):void 
        {
        	if (corePoints.length > 0)
        		corePoints[corePoints.length-1] = value;
        }
            
        public function setEditable(value:Boolean = true):void 
        {
        	if (corePoints != null && corePoints.length < 2)
            	return;
            
            draggable = true;
                
            initMarkerLayers();

            initVertexMarkers();
            initMiddleMarkers();
            
            _editable = true;
        }

        private function initMarkerLayers():void 
        {
        	initMiddleLayer();
        	initVertexLayer();
        }
        
        private function initVertexLayer():void 
        {
        	if (vertexLayer)
        		vertexLayer.remove();

            vertexLayer = new Layer();
            
            vertexLayer.addEventListener(OverlayEvent.DRAG, vertexMarkerDragHandler, true); 
            vertexLayer.addEventListener(OverlayEvent.DRAG_STOP, vertexMarkerDragHandler, true); 
            
            core.addOverlay(vertexLayer);
        }
        
        private function initMiddleLayer():void 
        {
        	if (middleLayer)
        		middleLayer.remove();

            middleLayer = new Layer();
            
            middleLayer.addEventListener(OverlayEvent.DRAG_START, middleMarkerDragStartHandler, true); 
            middleLayer.addEventListener(OverlayEvent.DRAG, middleMarkerDragHandler, true); 
            middleLayer.addEventListener(OverlayEvent.DRAG_STOP, middleMarkerDragStopHandler, true); 
            
            core.addOverlay(middleLayer);
        }
        
        private function initVertexMarkers():void 
        {
			vertexMarkers = new Array();
                
            var len:int = corePoints.length; 
            for (var idx:int = 0; idx < len; idx++) 
            {
            	var markerPosition:LatLng = corePoints[idx]; 
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
                marker.addEventListener(MarkerEvent.DELETE, vertexDeleteHandler);

            return marker; 
        } 
        
        private function initMiddleMarkers():void 
        { 
            middleMarkers = new Array();
            
            var len:uint = corePoints.length; 
            for (var idx:uint = 1; idx < len; idx++) 
            { 
                var point1:LatLng = corePoints[idx-1]; 
                var point2:LatLng = corePoints[idx]; 
                var marker:MiddleMarker = createMiddleMarker(point1, point2, idx); 
                middleLayer.addOverlay(marker); 
                middleMarkers.push(marker); 
            }
            createLastMiddle(); 
        } 

        private var lastMiddle:MiddleMarker;
        private function createLastMiddle():void 
        {
            var corePoints:Array = points[0] as Array;
            lastMiddle = createMiddleMarker(endPoint, startPoint, corePoints.length); 
            middleLayer.addOverlay(lastMiddle);
        }
                
        private function get startPoint():LatLng 
        {
            return corePoints[0] as LatLng;
        }  

        private function get endPoint():LatLng 
        {
            return corePoints[corePoints.length-1] as LatLng;
        }  

        private function getMiddleLatLng(prev:LatLng, next:LatLng):LatLng 
        { 
            var prevPoint:Point = core.getBitmapXYFromLatLng(prev); 
            var nextPoint:Point = core.getBitmapXYFromLatLng(next); 
            return core.getLatLngFromBitmapXY(Point.interpolate(prevPoint, nextPoint, 0.5)); 
        } 

        private function createMiddleMarker(prev:LatLng, next:LatLng, idx:uint):MiddleMarker 
        { 
            var latlng:LatLng = getMiddleLatLng(prev, next); 
            var marker:MiddleMarker = new MiddleMarker({data:idx}, middleStyle, iconMiddle);
                marker.position = latlng;
                marker.draggable = true;
                marker.smartPosition = false;

            return marker; 
        } 
        
		private function setNonEditable():void
		{
			draggable = false;
			
            removeVertexMarkers();
            removeMiddleMarkers();

            removeMarkerLayers();

			_editable = false;
		}
		
		private function removeVertexMarkers():void 
		{
			if (vertexMarkers)
			{
				while (vertexMarkers.length > 0)
				{
					Marker(vertexMarkers.pop()).remove();
                }
            }

            vertexMarkers = null;
		}

		private function removeMiddleMarkers():void 
		{
			if (middleMarkers)
			{
				while (middleMarkers.length > 0)
				{
					Marker(middleMarkers.pop()).remove();
                }
            }
                
            middleMarkers = null;
		}
		
		private function removeMarkerLayers():void 
		{
			vertexLayer.remove();
			vertexLayer = null;

			middleLayer.remove();
			vertexLayer = null;
		}


        private function refreshMarkers():void 
        {
            setNonEditable();
            setEditable();
        }
        
        private function deleteVertex(vertex:Marker):void 
        {
            if (vertex == null)
                return;
            
            if (!vertexExists(vertex))
                return;
                    
            if (corePoints.length < 3) 
            {
                setNonEditable();
                remove();
                return;
            }
            
            var idx:uint = vertex.data as uint;
            corePoints.splice(idx, 1);
            refresh(true);
            refreshMarkers();
        }
            
        private function vertexExists(vertex:Marker):Boolean 
        {
            var v:VertexMarker;
            for each (v in vertexMarkers) 
            {
                if (vertex == v)
                    return true;
            }
            return false;
        }
            
        private function vertexMarkerDragHandler(event:OverlayEvent):void 
        { 
            var idx:uint = uint(event.target.data);
            var latlng:LatLng = event.target.position; 
            corePoints[idx] = latlng;

            refresh(true);

            if (idx == 0) 
                lastMiddle.position = getMiddleLatLng(latlng, endPoint);
            
            if (idx == corePoints.length-1) 
                lastMiddle.position = getMiddleLatLng(startPoint, latlng);

            if (idx > 0) 
                middleMarkers[idx-1].position = getMiddleLatLng(vertexMarkers[idx-1].position, latlng); 

            if (idx < corePoints.length-1) 
                middleMarkers[idx].position = getMiddleLatLng(latlng, vertexMarkers[idx+1].position);
        } 
            
        private function middleMarkerDragStartHandler(event:OverlayEvent):void 
        { 
            var idx:uint = uint(event.target.data);
            var newPoint:LatLng = MiddleMarker(event.target).position;

            if (idx == corePoints.length) 
                corePoints.push(newPoint);
            else 
                corePoints.splice(idx, 0, newPoint);

            refresh(true);
        }
        
        private function middleMarkerDragHandler(event:OverlayEvent):void 
        { 
	        corePoints[uint(event.target.data)] = event.target.position; 
	        refresh(true);
        } 

        private function middleMarkerDragStopHandler(event:OverlayEvent):void 
        { 
            var idx:uint = uint(event.target.data); 
            var latlng:LatLng = event.target.position; 

            corePoints[idx] = latlng; 
            refresh(true); 
            
            var len:uint = vertexMarkers.length; 
            for (var i:uint = idx; i < len; i++)
            { 
                vertexMarkers[i].data = i+1; 
            } 

            var vertex:VertexMarker = createVertexMarker(latlng, idx); 
            vertexMarkers.splice(idx, 0, vertex); 
            vertexLayer.addOverlay(vertex); 

            len = middleMarkers.length; 
            for (var j:uint = idx; j < len; j++) 
            { 
                middleMarkers[j].data = j+2; 
            }
            lastMiddle.data = uint(lastMiddle.data) + 1; 
            event.target.remove(); 

            var middle:MiddleMarker = createMiddleMarker(vertexMarkers[idx-1].position, vertexMarkers[idx].position, idx);
            middleMarkers.splice(idx-1, 1, middle); 
            middleLayer.addOverlay(middle); 
            
            if (idx == corePoints.length-1) 
            {
                lastMiddle = createMiddleMarker(startPoint, endPoint, corePoints.length);
                middleLayer.addOverlay(lastMiddle); 
            } else 
            {
                middle = createMiddleMarker(vertexMarkers[idx].position, vertexMarkers[idx+1].position, idx+1); 
                middleMarkers.splice(idx, 0, middle); 
                middleLayer.addOverlay(middle); 
            }
        }
            
        private function vertexDeleteHandler(event:MarkerEvent):void 
        {
            deleteVertex(event.marker);
        }
	}
}

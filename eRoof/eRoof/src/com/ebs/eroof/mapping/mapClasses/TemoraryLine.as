package com.ebs.eroof.mapping.mapClasses
{
    import com.afcomponents.umap.core.UMap;
    import com.afcomponents.umap.events.OverlayEvent;
    import com.afcomponents.umap.overlays.Layer;
    import com.afcomponents.umap.overlays.Marker;
    import com.afcomponents.umap.overlays.Polyline;
    import com.afcomponents.umap.styles.GeometryStyle;
    import com.afcomponents.umap.styles.MarkerStyle;
    import com.afcomponents.umap.types.LatLng;
    
    import flash.events.Event;

    public class TemporaryLine extends Polyline
    {
	    [Embed(source="/assets/icons16/bullet_green.png")]
	    [Bindable]
	    private var iconStart:Class;
	            
	    [Embed(source="/assets/icons16/bullet_red.png")]
	    [Bindable]
	    private var iconEnd:Class;
        
        private var mainMap:UMap;
        
        private function get defaultTempLineStyle():GeometryStyle 
        {
            var result:GeometryStyle = new GeometryStyle();
                result.stroke = GeometryStyle.RGB;
                result.strokeRGB = 0xff0000;
                result.strokeThickness = 1;
                result.strokeAlpha = 0.8;
            return result;
        }
        
        override public function TemporaryLine(map:UMap, startPoint:LatLng, endPoint:LatLng, style:Object=null)
        {
            this.mainMap = map;
            
            var param:Object = new Object();
                param.points = new Array();
                param.points.push(startPoint);
                param.points.push(endPoint);
                param.autoInfo = false;
            
            if (style == null)
                    style = defaultTempLineStyle;

            super(param, style);
        }
        
        [Bindable]
        public function get startPoint():LatLng 
        {
            if (points.length > 0) 
                return points[0] as LatLng;
            else 
                return null;
        }
        public function set startPoint(value:LatLng):void  
        {
            if (points == null)
                points = new Array();
                    
            if (points.length > 0) 
                points[0] = value;
            else 
                points.push(value);
            
            rebuild();
        }
        
        [Bindable]
        public function get endPoint():LatLng 
        {
            if (points.length > 1) 
                return points[1] as LatLng;
            else 
                return null;
        }
        public function set endPoint(value:LatLng):void  
        {
            if (points == null)
                points = new Array();
                    
            if (points.length > 1) 
                points[1] = value;
            else if (points.length > 0)
                points.push(value);
            else 
            {
                points.push(value.clone());
                points.push(value);
            }
            
            rebuild();
        }
        
        public function move(startPoint:LatLng, endPoint:LatLng):void 
        {
            points = new Array();
            points.push(startPoint);
            points.push(endPoint);
            
            rebuild();
        }
        
        private function rebuild():void 
        {
            redrawMarkers();
            refresh(true);
        }
        
        override public function remove():void 
        {
            markerLayer.remove();
            super.remove();
        }

        private var _markerLayer:Layer;
        private function get markerLayer():Layer 
        {
            if (_markerLayer == null)
            {
                _markerLayer = new Layer();
                mainMap.addOverlay(_markerLayer);
            }

            return _markerLayer; 
        }
        
        private var startMarker:Marker;
        private var endMarker:Marker;
        private function redrawMarkers():void 
        {
            if (endMarker == null) 
            {
                endMarker = createMarker(endPoint, iconEnd);
                endMarker.addEventListener(OverlayEvent.CLICK, 
                    function(event:OverlayEvent):void 
                    {
                        dispatchEvent(new Event("endPointClick"));
                    });
                markerLayer.addOverlay(endMarker);
            } 
            else 
            {
                endMarker.position = endPoint;
            }

            if (startMarker == null)
            {
                startMarker = createMarker(startPoint, iconStart);
                startMarker.addEventListener(OverlayEvent.CLICK, 
                    function(event:OverlayEvent):void 
                    {
                        dispatchEvent(new Event("startPointClick"));
                    });
                startMarker.addEventListener(OverlayEvent.ROLL_OVER, 
                    function(event:OverlayEvent):void 
                    {
                        dispatchEvent(new Event("startPointRollOver"));
                        visible = false;
                        endMarker.visible = false;
                    });
                startMarker.addEventListener(OverlayEvent.ROLL_OUT, 
                    function(event:OverlayEvent):void 
                    {
                        dispatchEvent(new Event("startPointRollOut"));
                        visible = true;
                        endMarker.visible = true;
                    });
                markerLayer.addOverlay(startMarker);
            } 
            else 
            {
                startMarker.position = startPoint;
            }
        }
        
        private var _vertexStyle:MarkerStyle = new MarkerStyle();
        [Bindable]
        public function get vertexStyle():MarkerStyle { return _vertexStyle; }
        public function set vertexStyle(value:MarkerStyle):void 
        {
            _vertexStyle = value;
        }
        private function createMarker(latlng:LatLng, icon:Object):CustomMarker 
        {
            var vertex:CustomMarker = new CustomMarker({data:null}, vertexStyle, icon);
                vertex.position = latlng;
                vertex.draggable = false;
                vertex.smartPosition = false;
                vertex.autoInfo = false;

            return vertex; 
        } 
            
    }
}
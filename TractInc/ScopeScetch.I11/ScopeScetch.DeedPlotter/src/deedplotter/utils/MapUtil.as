package src.deedplotter.utils
{
    import com.modestmaps.flex.Map;
    import com.modestmaps.geo.Location;
    
    import flash.geom.Point;
    
    import mx.events.PropertyChangeEvent;
    
    import src.deedplotter.containers.DraggableCanvas;
    import src.deedplotter.containers.GeoCanvas;
    import src.deedplotter.events.DragEvent;
    import src.deedplotter.events.ScaleEvent;
    
    /**
    * This is temporary class. implementation should be moved into GeoCanvas
    */

    public class MapUtil
    {
        
        private var _map:Map;
        public function get map():Map { return _map; }
        public function set map(value:Map):void 
        {
            _map = value;
        }

        private var _drawingSurface:GeoCanvas;
        public function get drawingSurface():GeoCanvas { return _drawingSurface; }
        public function set drawingSurface(value:GeoCanvas):void 
        {
            if (_drawingSurface == value) return;
            
            if (_drawingSurface) {
                _drawingSurface.removeEventListener(DragEvent.DRAGGED, surface_draggedHandler);
                _drawingSurface.removeEventListener("scaleChanged", surface_scaleChangedHandler)
            }
            
            _drawingSurface = value;
            
            _drawingSurface.addEventListener(DragEvent.DRAGGED, surface_draggedHandler);
            _drawingSurface.addEventListener("scaleChanged", surface_scaleChangedHandler)
        }

        private function surface_scaleChangedHandler(event:ScaleEvent):void
        {
            if (!_map) return;

            map.map.zoom(event.scaleDelta > 0 ? 1 : -1); //TODO: stub
        }
        
        private function surface_draggedHandler(event:DragEvent):void 
        {
            if (!_map) return;

            var delta:Point = new Point(-event.deltaX, -event.deltaY);

            map.map.panFrames = 1;
	        map.map.panMap(delta);
        }
        
    }
}
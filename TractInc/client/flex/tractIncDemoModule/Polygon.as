package
{
    import com.modestmaps.Map;
    import com.modestmaps.events.MapEvent;
    import com.modestmaps.geo.Location;
    
    import flash.display.BlendMode;
    import flash.display.Sprite;
    import flash.events.Event;
    import flash.geom.Point;
    import flash.geom.Rectangle;

    public class Polygon extends Sprite
    {

        // this example uses hard-coded data, omitted for clarity
        public var poly:Array = [ /* list of lots of Locations, e.g. new Location(37.0, 23.0), etc. */ ];

        private var map:Map;
        
        private var panStart:Point;

        private var polySprite:Sprite = new Sprite();
        private var polyCont:Sprite = new Sprite();

        private var bounds:BoundRectangle = null;

        public function Polygon(map:Map)
        {
            this.mouseEnabled = false;
            this.mouseChildren = false;
            this.map = map;
            this.blendMode = BlendMode.DARKEN;

            addChild(polyCont);
            polyCont.addChild(polySprite);
            addEventListener(Event.ADDED_TO_STAGE, onAddedToStage);
        }
        
        private function onAddedToStage(event:Event):void 
        {
            redrawPoly();
            onMapResize(null);
            map.addEventListener(MapEvent.START_PANNING, onMapStartPan);
            map.addEventListener(MapEvent.PANNED, onMapPanned);
            map.addEventListener(MapEvent.STOP_PANNING, onMapStopPan);
            map.addEventListener(MapEvent.ZOOMED_BY, onMapZoomedBy);
            map.addEventListener(MapEvent.STOP_ZOOMING, onEndZoom);
            map.addEventListener(MapEvent.EXTENT_CHANGED, onMapChange);
            map.addEventListener(MapEvent.RESIZED, onMapResize);
        }
        
        public function onMapResize(event:Event):void
        {
            graphics.clear();
            graphics.beginFill(0xcccccc);
            var w:Number = map.getWidth();
            var h:Number = map.getHeight();
            graphics.drawRect(0, 0, w, h);
            redrawPoly();
        }

        public function redrawPoly():void 
        {        
            var bpoints:Array = [];

            polyCont.scaleX = polyCont.scaleY = 1.0;
            polyCont.x = stage.stageWidth/2;
            polyCont.y = stage.stageHeight/2;
            polySprite.x = stage.stageWidth/2;
            polySprite.y = stage.stageHeight/2;
            polySprite.graphics.clear();
            
            var p1:Point = map.locationPoint(poly[0],polySprite);
            bpoints.push(p1);
            polySprite.graphics.beginFill(0xcccccc);
            polySprite.graphics.lineStyle(3,0xFF00FF);
            polySprite.graphics.moveTo(p1.x, p1.y);
            for each (var location:Location in poly.slice(1)) {
                var p:Point = map.locationPoint(location,polySprite);
                bpoints.push(p);
                polySprite.graphics.lineTo(p.x,p.y);
            }
            polySprite.graphics.lineTo(p1.x, p1.y);
            polySprite.graphics.endFill();

            bounds = BoundRectangle.createByPoints(bpoints);
        }

        private function onMapZoomedBy(event:MapEvent):void {
/*
            polyCont.scaleX = polyCont.scaleY = Math.pow(2, event.zoomDelta);
*/            
        }
        
        private function onEndZoom(event:MapEvent):void
        {
            redrawPoly();
        }

        private function onMapChange(event:MapEvent):void
        {
            redrawPoly();
        }
    
        private function onMapStartPan(event:MapEvent):void
        {
            panStart = new Point(polyCont.x,polyCont.y);
        }

        private function onMapStopPan(event:MapEvent):void
        {
            redrawPoly();
        }
        
        private function onMapPanned(event:MapEvent):void
        {
/**/
            polyCont.x = panStart.x + event.panDelta.x;
            polyCont.y = panStart.y + event.panDelta.y;
/**/
        }

        public function getComponentBounds():Rectangle 
        {
            return bounds.toRectangle();
        }

    }
}
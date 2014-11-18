package
{
    import com.modestmaps.Map;
    import com.modestmaps.events.MapEvent;
    import com.modestmaps.geo.Location;
    
    import flash.display.BlendMode;
    import flash.display.BitmapData;
    import flash.display.Sprite;
    import flash.events.Event;
    import flash.geom.Matrix;
    import flash.geom.Point;
    import flash.geom.Rectangle;

	import mx.controls.Image;
	
    public class Picture extends Sprite
    {
        private var point:Location;
        private var point2:Location;
        
        private var img:Image;
		private var imgBData:BitmapData;
		
        private var map:Map;
        
        private var panStart:Point;

        private var polySprite:Sprite = new Sprite();

        public function Picture(map:Map, p:Location, p2:Location, image:Image)
        {
            this.mouseEnabled = false;
            this.mouseChildren = false;
            this.map = map;
            this.point = p;
            this.point2 = p2;
            this.img = image;
			var m : Matrix = new Matrix();
            this.imgBData = getBitmapDataMatrix(img, m);
            this.blendMode = BlendMode.MULTIPLY;

            addChild(polySprite);
			polySprite.addChild(img.content);
			
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

		private function getBitmapDataMatrix(i:Image, matrix:Matrix) : BitmapData
		{
		   var bd:BitmapData = new BitmapData(img.content.width, i.content.height);
		   bd.draw(i.content, matrix);
		   return bd;
		}
		
        public function redrawPoly():void 
        {        
            var p1:Point = map.locationPoint(point,this);
            var p2:Point = map.locationPoint(point2,this);
            polySprite.x = p1.x;
            polySprite.y = p1.y;
            polySprite.width = Math.abs(p1.x - p2.x);
            polySprite.height = Math.abs(p1.y - p2.y);
/*            
            polySprite.width = img.content.width;
            polySprite.height = img.content.height;
*/            
/*        	
            polySprite.x = stage.stageWidth/2;
            polySprite.y = stage.stageHeight/2;
            polySprite.graphics.clear();
*/          
/*  
            this.graphics.clear();
            var p1:Point = map.locationPoint(point,this);
            this.graphics.beginBitmapFill(this.imgBData,null,true,false);
            this.graphics.drawRect(p1.x, p1.y, this.imgBData.width+10, imgBData.height+10);
            this.graphics.endFill();
*/            
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
            panStart = new Point(polySprite.x,polySprite.y);
        }

        private function onMapStopPan(event:MapEvent):void
        {
            redrawPoly();
        }
        
        private function onMapPanned(event:MapEvent):void
        {
/**/
            polySprite.x = panStart.x + event.panDelta.x;
            polySprite.y = panStart.y + event.panDelta.y;
/**/
        }

    }
}
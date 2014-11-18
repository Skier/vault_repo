package
{
    import com.modestmaps.flex.Map;
    import com.modestmaps.geo.Location;
    
    import flash.geom.Point;
    
    import mx.core.UIComponent;

    public class Line extends UIComponent
    {
        public var map:Map;
        public var startLoc:Location;
        public var endLoc:Location;

        override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
        {
            super.updateDisplayList(unscaledWidth, unscaledHeight);
            
            var startPoint:Point = map.map.locationPoint(startLoc, map);
            var endPoint:Point = map.map.locationPoint(endLoc, map);
            
            this.move(startPoint.x, startPoint.y);

            graphics.clear();
            graphics.lineStyle(1, 0xFF0000);
            graphics.lineTo(endPoint.x - startPoint.x, endPoint.y - startPoint.y);
            
            graphics.drawRect(-1, -1, 2, 2);
            graphics.drawRect(endPoint.x - startPoint.x - 1, endPoint.y - startPoint.y - 1, 2, 2);
            
        }
        
    }
}
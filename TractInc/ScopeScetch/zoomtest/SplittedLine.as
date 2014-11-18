package
{
    import mx.core.UIComponent;
    import com.modestmaps.geo.Location;
    import flash.geom.Point;

    public class SplittedLine extends Line
    {
        public var distance:Number = 0;
        public var startBearing:Number = 0;

        override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
        {
            var startPoint:Point = map.map.locationPoint(startLoc, map);

            var linePoints:Array = GeoUtil.splitLine(startLoc, startBearing, distance, 50);
            var color:uint = 0xdb6217;
            
            graphics.clear();
            graphics.lineStyle(1, 0xFF0000);

            this.move(startPoint.x, startPoint.y);
            
            for each (var loc:Location in linePoints)
            {
                var point:Point = map.map.locationPoint(loc, map);
                var localPoint:Point = new Point(point.x - startPoint.x, point.y - startPoint.y);
                
                graphics.lineTo(localPoint.x, localPoint.y);
                graphics.drawRect(localPoint.x - 1, localPoint.y - 1, 2, 2);
                graphics.lineTo(localPoint.x, localPoint.y);
            }
        }
    }
}
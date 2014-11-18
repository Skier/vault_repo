package
{
    import mx.core.UIComponent;
    import com.modestmaps.geo.Location;
    import flash.geom.Point;

    public class Curve extends Line
    {
        public var radius:Number = 0;
        public var delta:Number = 90;
        public var tangentIn:Number = 0;
        
        override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
        {
            super.updateDisplayList(unscaledWidth, unscaledHeight);

            var startPoint:Point = map.map.locationPoint(startLoc, map);
            var endPoint:Point = map.map.locationPoint(endLoc, map);

    		var h:Number = endPoint.y - startPoint.y;
    		var w:Number = endPoint.x - startPoint.x;
    
    		var distance:Number = Math.sqrt( (h * h) + (w * w) );

            radius = distance / 2;

            this.move(startPoint.x, startPoint.y);

            graphics.clear();
            graphics.lineStyle(1, 0xFF0000);
            var ep:Point = GraphicsUtil.drawArc(graphics, new Point(), tangentIn, radius, delta);
            
            graphics.drawRect(- 1, - 1, 2, 2);
            graphics.drawRect(ep.x - 1, ep.y - 1, 2, 2);
        }
    }
}
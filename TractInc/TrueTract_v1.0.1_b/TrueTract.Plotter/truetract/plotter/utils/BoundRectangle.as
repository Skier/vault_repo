package truetract.plotter.utils
{
    import flash.geom.Point;
    import flash.geom.Rectangle;
    
    import mx.effects.easing.Bounce;
    
    public class BoundRectangle
    {
        public var minX:Number = 0;
        public var minY:Number = 0;
        public var maxX:Number = 0;
        public var maxY:Number = 0;
        
        public function get width():Number {
            return Math.abs(maxX - minX);
        }
        
        public function get height():Number {
            return Math.abs(maxY - minY);
        }
        
        public function get center():Point {
            return new Point( minX + width / 2, minY + height / 2);
        }
        
        public function get topLeft():Point {
            return new Point(minX, maxY);
        }
        
        public function get bottomRight():Point {
            return new Point(maxX, minY);
        }

        public static function createByPoints(points:Array):BoundRectangle {
            var result:BoundRectangle = new BoundRectangle();
            if (points.length == 0) return result;
            
            result.minX = result.maxX = points[0].x;
            result.minY = result.maxY = points[0].y;
            
            for (var i:int = 1; i < points.length; i++) {
                var p:Point = points[i];
                
                if (p.x < result.minX) result.minX = p.x;
                else if (p.x > result.maxX) result.maxX = p.x;
                
                if (p.y < result.minY) result.minY = p.y;
                else if (p.y > result.maxY) result.maxY = p.y;
            }
            
            return result;
        }

        public function addPoint(point:Point):void {
            minX = Math.min(minX, point.x);
            maxX = Math.max(maxX, point.x);
            minY = Math.min(minY, point.y);
            maxY = Math.max(maxY, point.y);
        }
        
		public function addRectangle(rectangle:BoundRectangle):void {
		    if (rectangle.minX < minX) minX = rectangle.minX;
		    if (rectangle.maxX > maxX) maxX = rectangle.maxX;
		    if (rectangle.minY < minY) minY = rectangle.minY;
		    if (rectangle.maxY > maxY) maxY = rectangle.maxY;
		}
        
        public function get area():Number {
            return width * height;
        }
        
        public function toRectangle():Rectangle {
            return new Rectangle( minX, minY, width, height);
        }
    }
}
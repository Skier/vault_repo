package src.deedplotter.utils
{
    import mx.formatters.NumberFormatter;
    import flash.geom.Point;
    
    public class GeoPosition
    {
        public var Northing:Number;
        public var Easting:Number;
        
        private var nf:NumberFormatter;
        
        public function GeoPosition(easting:Number = 0, northnig:Number = 0){
            this.Northing = northnig;
            this.Easting = easting;
            
            nf = new NumberFormatter();
            nf.precision = 2;
            nf.rounding = "nearest";
        }

        public static function Parse(s:String):GeoPosition {

            var split:Array = s.split(",");

            if (split.length != 2){
                throw new Error ("Wrong GeoPosition");
            }

            var northing:Number = Number(split[0]);
            var easting:Number = Number(split[1]);

            if ( isNaN(northing) || isNaN(easting) ) {
                throw new Error ("Wrong GeoPosition");
            }

            return new GeoPosition(easting, northing);
        }

        public static function CreateFromPoint(p:Point):GeoPosition {
            return new GeoPosition(p.x, p.y);
        }
        
        public function toString():String {
            return "N=" + nf.format(Northing) + ", E=" + nf.format(Easting);
        }

        public function toPoint():Point {
            return new Point(Easting, Northing);
        }
        
        public function Equals(other:GeoPosition):Boolean {
            var northingDiff:Number = Math.abs(Northing - other.Northing);
            var eastingDiff:Number = Math.abs(Easting - other.Easting);

            return (northingDiff < 0.0001 && eastingDiff < 0.0001);
        }
        
        public function clone():GeoPosition {
            return new GeoPosition(Easting, Northing);
        }
        
        public function InsidePolygon(points:Array):Boolean {
            var result:Boolean = false;
            var i:int = 0;
            var j:int = points.length - 1;
            
            for (i= 0; i < points.length; j = i++) {
                if (    (
                          ((points[i].y <= Northing) && (Northing < points[j].y))   
                          ||
                          ((points[j].y <= Northing) && (Northing < points[i].y))
                        ) 
                        &&
                        (Easting < (points[j].x - points[i].x) * (Northing - points[i].y) / (points[j].y - points[i].y) + points[i].x))
                    result = !result;
            }
            
            return result;
        }
        
/*         public function InsidePolygon(polygonPoints:Array):Boolean {
            var counter:Number = 0;
            var p1:Point; var p2:Point;
            var xinters:Number;
            
            p1 = polygonPoints[0];
            
            for (var i:int = 1; i < polygonPoints.length; i++) {
                p2 = polygonPoints[i % polygonPoints.length];
                if (Northing > MIN(p1.y, p2.y)) {
                  if (Northing <= MAX(p1.y, p2.y)) {
                    if (Easting <= MAX(p1.x, p2.x)) {
                      if (p1.y != p2.y) {
                        xinters = (Easting-p1.y)*(p2.x-p1.x)/(p2.y-p1.y)+p1.x;
                        if (p1.x == p2.x || Easting <= xinters)
                          counter++;
                      }
                    }
                  }
                }
                p1 = p2;
              }
            
              if (counter % 2 == 0)
                return false;
              else
                return true;
                
        }
*/
         
        private function MIN(x:Number, y:Number):Number {
            return x < y ? x : y;
        }
        
        private function MAX(x:Number, y:Number):Number {
            return x > y ? x : y;
        }
        
    }
}
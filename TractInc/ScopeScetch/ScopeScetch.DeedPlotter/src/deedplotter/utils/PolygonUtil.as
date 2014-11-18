package src.deedplotter.utils
{
    import flash.geom.Point;
    
    public class PolygonUtil
    {

        public static function area(polyPoints:Array):Number 
        {
    		var result:Number = 0;
            var j:Number;
            var n:int = polyPoints.length;

    		for (var i:int = 0; i < n; i++) {
    			j = (i + 1) % n;
    			result += polyPoints[i].x * polyPoints[j].y;
    			result -= polyPoints[j].x * polyPoints[i].y;
    		}

    		result /= 2;
    		
    		return result;
        }

        public static function centerOfMass(polyPoints:Array):Point 
        {
    		var cx:Number = 0;
    		var cy:Number = 0;
    		var area:Number = area(polyPoints) * 6;
    		var j:Number;
    		var factor:Number = 0;
    		var n:Number = polyPoints.length;

    		for (var i:int = 0; i < n; i++) 
    		{
    			j = (i + 1) % n;
    			
    			factor = ( polyPoints[i].x * polyPoints[j].y
    					- polyPoints[j].x * polyPoints[i].y );
    					
    			cx += ( polyPoints[i].x + polyPoints[j].x ) * factor;
    			cy += ( polyPoints[i].y + polyPoints[j].y ) * factor;
    		}

            factor = 1/area;
            
    		cx *= factor;
    		cy *= factor;

    		return new Point(cx, cy);
        }
        
    }
}
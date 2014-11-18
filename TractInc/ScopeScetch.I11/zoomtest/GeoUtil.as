package
{
    import com.modestmaps.geo.Location;
    
    public class GeoUtil
    {
        
        private static const RAD_PER_DEG:Number = Math.PI / 180.0;			

        /**
         *  Given the start location, distance and azimuth of the line.
         *  Returns the end location of that line
         *
         *  @param loc1     The start location
         *  @param distance The distance between points in meters
         *  @param azimuth  The starting angel of the line that connect two points in degrees
         */
        public static function getEndPoint(loc1:Location, distance:Number, azimuth:Number):Location
        {
            var earthRadius:Number = 6372795; //in meters
            var lat:Number = loc1.lat * Math.PI / 180; //radian
            var lon:Number = loc1.lon * Math.PI / 180;
            
            var d:Number = distance / earthRadius; //angular distance covered on earth's surface
            var brng:Number = azimuth * Math.PI / 180; //radian
            var loc2:Location = new Location(0,0);

            loc2.lat = Math.asin(Math.sin(lat) * Math.cos(d) + Math.cos(lat)*Math.sin(d)*Math.cos(brng));
            loc2.lon = ((lon + Math.atan2(Math.sin(brng)*Math.sin(d)*Math.cos(lat), Math.cos(d) - 
                       Math.sin(lat)*Math.sin(loc2.lat))) * 180) / Math.PI;
            loc2.lat = loc2.lat * 180 / Math.PI;
            
            return loc2;
        }
        
        /**
         *  Returns the distance between two points in meters
         *
         *  @param loc1 The start location (lat, lon)
         *  @param loc2 The end location (lat, lon)
         */
        public static function getDistance(loc1:Location, loc2:Location):Number
        {
            var dlon:Number = loc2.lon - loc1.lon;
            var dlat:Number = loc2.lat - loc1.lat;

            var a:Number = Math.pow(Math.sin((dlat/2) * Math.PI / 180), 2) + 
                           Math.cos(loc1.lat * Math.PI / 180) * 
                           Math.cos(loc2.lat * Math.PI / 180) * 
                           Math.pow(Math.sin((dlon/2) * Math.PI / 180), 2);
                           
            var c:Number = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
            
            return 6372795 * c;
        }

        /**
         *  Returns the course between points in degrees
         *
         *  @param loc1 The start location (lat, lon)
         *  @param loc2 The end location (lat, lon)
         */
        public static function getBearing(loc1:Location, loc2:Location):Number
        {
            var lat1:Number = loc1.lat * Math.PI / 180;
            var lat2:Number = loc2.lat * Math.PI / 180;
            var lon1:Number = -loc1.lon * Math.PI / 180;
            var lon2:Number = -loc2.lon * Math.PI / 180;
            
            var result:Number = mod(Math.atan2(
                Math.sin(lon1-lon2) * Math.cos(lat2), 
                Math.cos(lat1) * Math.sin(lat2) - Math.sin(lat1) * Math.cos(lat2) * Math.cos(lon1 - lon2)
                ), 2 * Math.PI);

            return result * 180/Math.PI;
        }
        
		//найдено здесь (http://blogs.msdn.com/virtualearth/archive/2006/02/25/539239.aspx)
		//не уверен что считает правильно.
        //не вижу логики.. или не могу её понять..
        //почему в рассчетах учавствует центральная точка ?
        //
        //Решение подобной задачи компанией Yahoo можно посмотреть здесь 
        //(http://www.jibbering.com/2005/11/%23c%23). См. функцию mpp()
        public static function metersPerPixel(zoomLevel:Number, centerLoc:Location):Number
        {
            return 156543.04 * Math.cos(centerLoc.lat * RAD_PER_DEG) / Math.pow(2, zoomLevel);
        }
        
        public static function splitLine(startLocation:Location, bearing:Number, 
                                            distance:Number, splitCount:Number):Array
        {
            var result:Array = [];
            
		    for (var i:int = 1; i <= splitCount; i++)
		    {
		        var endLocation:Location = getEndPoint(startLocation, distance / splitCount, bearing);

		        result.push(endLocation);
		        
		        startLocation = endLocation;
		    }

            return result;
        }
        
        private static function mod(y:Number, x:Number):Number
        {
             var result:Number = y - x * int(y / x);
             
             if ( result < 0) result = result + x;

             return result;
        }
    }
}
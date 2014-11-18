package truetract.utils
{
import flash.geom.Point;

public class GeoLine implements IGeoShape
{
    //--------------------------------------------------------------------------
    //
    //  Type Methods
    //
    //--------------------------------------------------------------------------
    
    public static function createByEndPosition(
        startPosition:GeoPosition, endPosition:GeoPosition):GeoLine 
    {
        var distance:Number = calculateDistance(startPosition, endPosition);
		var azimuth:Number = 0;

		if (distance > 0)
		{
            azimuth = Math.acos((endPosition.Northing - startPosition.Northing) / distance ) / (Math.PI/180);

            if ( endPosition.Easting < startPosition.Easting )
            {
                azimuth = 360 - azimuth;
            }
		}

        var result:GeoLine = new GeoLine(GeoBearing.CreateByAzimuth(azimuth), distance);
        result.startPosition = startPosition;
        
        return result;
    }

    public static function calculateDistance(startPos:GeoPosition, endPos:GeoPosition):Number
    {
		var h:Number = endPos.Northing - startPos.Northing;
		var w:Number = endPos.Easting - startPos.Easting;

		return Math.sqrt((h * h) + (w * w));
    }

    public static function calculateEndPosition(
        startPosition:GeoPosition, bearing:GeoBearing, distance:Number):GeoPosition
    {
        var radian:Number = bearing.Radian;

  		var endPosition:GeoPosition = new GeoPosition();
  		endPosition.Easting = startPosition.Easting + (distance * Math.sin(radian));
        endPosition.Northing = startPosition.Northing + (distance * Math.cos(radian));
        
        return endPosition;
    }

    //--------------------------------------------------------------------------
    //
    //  Constructor
    //
    //--------------------------------------------------------------------------

    public function GeoLine(bearing:GeoBearing, distance:Number):void {
        _startPosition = new GeoPosition();
        _bearing = bearing;
        _distance = distance;
        
        _endPosition = calculateEndPosition(_startPosition, _bearing, _distance);
    }
    
    //--------------------------------------------------------------------------
    //
    //  Class properties
    //
    //--------------------------------------------------------------------------
    
    private var _startPosition:GeoPosition;
    public function get startPosition():GeoPosition { return _startPosition; }
    public function set startPosition(value:GeoPosition):void 
    {
        _startPosition = value;
        
        _endPosition = calculateEndPosition(_startPosition, _bearing, _distance);
    }

    private var _endPosition:GeoPosition;
    public function get endPosition():GeoPosition { return _endPosition; }

    private var _distance:Number;
    public function get distance():Number { return _distance; }
    
    private var _bearing:GeoBearing;
    public function get bearing():GeoBearing { return _bearing; }

    public function get bearingOut():GeoBearing {
        return bearing;
    }
    
    public function get middlePosition():GeoPosition 
    {
        var radian:Number = bearing.Radian;
        
  		var result:GeoPosition = new GeoPosition();
  		result.Easting = startPosition.Easting + (distance/2 * Math.sin(radian));
        result.Northing = startPosition.Northing + (distance/2 * Math.cos(radian));
        
        return result;
    }
    
    public function get boundWidth():Number 
    {
        return endPosition.Easting - startPosition.Easting;
    }
    
    public function get boundHight():Number 
    {
        return endPosition.Northing - startPosition.Northing;
    }
    
    //--------------------------------------------------------------------------
    //
    //  Class methods
    //
    //--------------------------------------------------------------------------
    
	public function getBounds():BoundRectangle 
	{
        var points:Array = [];

        points.push(new Point(startPosition.Easting, startPosition.Northing));
        points.push(new Point(endPosition.Easting, endPosition.Northing));

        return BoundRectangle.createByPoints( points );
	}

}
}
package truetract.plotter.utils
{

import flash.geom.Point;

import mx.core.mx_internal;

public class GeoCurve implements IGeoShape
{
    //--------------------------------------------------------------------------
    //
    //  Constants
    //
    //--------------------------------------------------------------------------

    public static const LEFT:String = "left";
    public static const RIGHT:String = "right";
    
    //--------------------------------------------------------------------------
    //
    //  Constructor
    //
    //--------------------------------------------------------------------------
    
    public function GeoCurve(radius:Number, delta:Number, tangentIn:GeoBearing, 
        startPosition:GeoPosition = null, direction:String = RIGHT) 
    {
        if (!startPosition)
            startPosition = new GeoPosition();

        _startPosition = startPosition;
        _radius = radius;
        _delta = delta;
        _tangentIn = tangentIn;
        _direction = direction;

        refresh();
    }

    //--------------------------------------------------------------------------
    //
    //  Class properties
    //
    //--------------------------------------------------------------------------

    //----------------------------------
    //  direction
    //----------------------------------

    private var _direction:String;
    public function get direction():String { return _direction; }
    public function set direction(value:String):void 
    {
        if (value != LEFT && value != RIGHT)
        {
            throw new Error("Invalid direction");
        }
        
        _direction = value;
        refresh();
    }

    //----------------------------------
    //  radius
    //----------------------------------

    private var _radius:Number;
    public function get radius():Number { return _radius; }
    public function set radius(value:Number):void 
    {
        _radius = value;
        refresh();
    }

    //----------------------------------
    //  startPosition (IGeoShape implements)
    //----------------------------------

    private var _startPosition:GeoPosition;
    public function get startPosition():GeoPosition { return _startPosition; }
    public function set startPosition(value:GeoPosition):void 
    {
        _startPosition = value;
        refresh();
    }

    //----------------------------------
    //  bearingOut (IGeoShape implements)
    //----------------------------------

    public function get bearingOut():GeoBearing {
        return tangentOut;
    }

    //----------------------------------
    //  endPosition (IGeoShape implements)
    //----------------------------------

    private var _endPosition:GeoPosition;
    public function get endPosition():GeoPosition { return _endPosition; }

    //----------------------------------
    //  centerPosition
    //----------------------------------

    private var _centerPosition:GeoPosition;
    public function get centerPosition():GeoPosition { return _centerPosition; }

    //----------------------------------
    //  tangentIn
    //----------------------------------

    private var _tangentIn:GeoBearing;
    public function get tangentIn():GeoBearing { return _tangentIn; }

    //----------------------------------
    //  tangentOut
    //----------------------------------

    private var _tangentOut:GeoBearing;
    public function get tangentOut():GeoBearing { return _tangentOut; }

    //----------------------------------
    //  radialIn
    //----------------------------------

    private var _radialIn:GeoBearing;
    public function get radialIn():GeoBearing { return _radialIn; }

    //----------------------------------
    //  radialOut
    //----------------------------------

    private var _radialOut:GeoBearing;
    public function get radialOut():GeoBearing { return _radialOut; }
    
    //----------------------------------
    //  chord
    //----------------------------------

    private var _chord:GeoLine;
    public function get chord():GeoLine { return _chord; }

    //----------------------------------
    //  delta
    //----------------------------------

    private var _delta:Number;
    public function get delta():Number { return _delta; }

    //--------------------------------------------------------------------------
    //
    //  Class methods
    //
    //--------------------------------------------------------------------------

    /**
    * Returns the Area of Circle Sector
    *
    * The Sector Area formula: S = αr² / 2
    * The Segment Area formula S = r²(α - sin α) / 2
    * where α - angular value of arc in radians
    *       r - radius
    **/
    public function getArea():Number 
    {
        var angValue:Number = delta * Math.PI / 180;
        
        var sectorArea:Number = angValue * (radius * radius) / 2;
        
        var segmentArea:Number = (radius * radius) * (angValue - Math.sin(angValue)) / 2;
        
        return segmentArea;
    }

    /**
     *  @public
     *  Returns geo coordinates of the point that lays out on the curve arc center
     *  Возвращает геодезические координаты точки, которая находится в центре дуги кривой
     */
    public function getArcMiddlePosition():GeoPosition 
    {
        var arcRadianBearing:GeoBearing = GeoBearing.CreateByAzimuth( (direction == RIGHT)
            ? (radialOut.Azimuth - delta/2)
            : (radialOut.Azimuth + delta/2));
        
        //the radian from center of circle to center of arc 
        var radian:GeoLine = new GeoLine(arcRadianBearing, radius);

        radian.startPosition = centerPosition;
        
        return radian.endPosition;
    }

    /**
     *  @public
     *  Returns geo coordinates of the point that lays out in the center of the circle sector,
     *  which describes this curve
     *  Возвращает геодезические координаты точки, которая находится в центре сектора круга, 
     *  который описывает данная кривая.
     */
    public function getMiddlePosition():GeoPosition 
    {
        
        if (_delta > 180)
        {
            return centerPosition;
        }
        
        //the line from center of circle to center of chord
        var lineToChordMiddlePosition:GeoLine = GeoLine.createByEndPosition(
            centerPosition, chord.middlePosition);
        
        //the line from the middle point of the curve to the middle of the chord
        var middleOrdinateLine:GeoLine = new GeoLine(
            lineToChordMiddlePosition.bearing, radius - lineToChordMiddlePosition.distance);
        middleOrdinateLine.startPosition = lineToChordMiddlePosition.endPosition;

        return middleOrdinateLine.middlePosition;
    }

    /**
     *  @public
     *  Returns geo coordinates of points that lay out on the curve throuch each quarter 
     *  of a graduse (degreeDelta = 0.25)
     *  Возвращает геодезические координаты точек лежащих на кривой через каждый четверть
     *  градуса (degreeDelta = 0.25)
     */
    public function getPoints():Array 
    {
        var result:Array = [ startPosition.toPoint() ];

        var degreeDelta:Number = 0.25;

        var startPos:GeoPosition = startPosition;
        var tngtIn:GeoBearing = tangentIn;

        var curve:GeoCurve;

        for (var i:Number = 0; i < delta; i+= degreeDelta) 
        {
            curve = new GeoCurve(radius, degreeDelta, tngtIn, startPos, direction);

            result.push(curve.endPosition.toPoint());
            startPos = curve.endPosition;
            
            if (direction == RIGHT) {
                tngtIn = GeoBearing.CreateByAzimuth(tngtIn.Azimuth + degreeDelta);
            } else {
                tngtIn = GeoBearing.CreateByAzimuth(tngtIn.Azimuth - degreeDelta);
            }
            
        }

        if (i != delta) {
            result.push(endPosition.toPoint());
        }

        return result;
    }

    /**
     *  @public
     *  Returns geo coordinates of the bounds where this curve is
     *  Возвращает геодезические координаты прямоугольника в котором находится кривая
     */
    public function getBounds():BoundRectangle {
        var result:BoundRectangle = BoundRectangle.createByPoints([_startPosition.toPoint(), _endPosition.toPoint()]);
        
        var startAngle:Number;
        var endAngle:Number;
        
        if (_direction == RIGHT) {
            startAngle = 360 - (180 - _radialIn.Azimuth);
            endAngle = _radialOut.Azimuth;
        } else {
            startAngle = _radialOut.Azimuth;
            endAngle = 360 - (180 - _radialIn.Azimuth);
        }
        
        while (startAngle > 360) startAngle-=360;
        while (endAngle > 360) endAngle-=360;
        
        if (startAngle < 90 && endAngle > 90)
            result.maxX = _centerPosition.Easting + radius;

        if (startAngle < 90 && startAngle > endAngle)
            result.maxX = _centerPosition.Easting + radius;
        
        if (startAngle > 90 && endAngle > 90 && startAngle > endAngle )
            result.maxX = _centerPosition.Easting + radius;

        if (startAngle < 180 && endAngle > 180)
            result.minY = _centerPosition.Northing - radius;

        if (startAngle < 180 && startAngle > endAngle)
            result.minY = _centerPosition.Northing - radius;

        if (startAngle > 180 && endAngle > 180 && startAngle > endAngle )
            result.minY = _centerPosition.Northing - radius;

        if (startAngle < 270 && endAngle > 270)
            result.minX = _centerPosition.Easting - radius;

        if (startAngle < 270 && startAngle > endAngle )
            result.minX = _centerPosition.Easting - radius;

        if (startAngle > 270 && endAngle > 270 && startAngle > endAngle )
            result.minX = _centerPosition.Easting - radius;

        if (startAngle > endAngle)
            result.maxY = _centerPosition.Northing + radius;
 
        if (_delta == 360){
            result.minX = _centerPosition.Easting - radius;
            result.maxX = _centerPosition.Easting + radius;

            result.minY = _centerPosition.Northing - radius;
            result.maxY = _centerPosition.Northing + radius;
        }
        
        return result;
    }

    /**
     *  @private
     *  Recalculates curve properties
     *  Пересчитывет свойства кривой
     */
     private function refresh():void {
        var chordLength:Number = 2 * (radius * Math.sin( _delta / 2 * (Math.PI/180) ) );
        
        var chordBearing:GeoBearing = (_direction == RIGHT)
            ? GeoBearing.CreateByAzimuth(tangentIn.Azimuth + _delta / 2)
            : GeoBearing.CreateByAzimuth(tangentIn.Azimuth - _delta / 2);
        
        _chord = new GeoLine(chordBearing, chordLength);
        _chord.startPosition = _startPosition;
        
  		_endPosition = new GeoPosition();
  		_endPosition.Easting = startPosition.Easting + (chord.distance * Math.sin(chord.bearing.Radian));
        _endPosition.Northing = startPosition.Northing - (- chord.distance * Math.cos(chord.bearing.Radian));
        
        _tangentOut = (_direction == RIGHT) 
            ? GeoBearing.CreateByAzimuth(_tangentIn.Azimuth + _delta)
            : GeoBearing.CreateByAzimuth(_tangentIn.Azimuth - _delta);
     
        _radialIn = (_direction == RIGHT) 
            ? GeoBearing.CreateByAzimuth(_tangentIn.Azimuth + 90)
            : GeoBearing.CreateByAzimuth(_tangentIn.Azimuth - 90);
        
        _radialOut = (_direction == RIGHT)
            ? GeoBearing.CreateByAzimuth(_radialIn.Azimuth + 180 + _delta)
            : GeoBearing.CreateByAzimuth(_radialIn.Azimuth - 180 - _delta);
        
	    _centerPosition = new GeoPosition();
	    _centerPosition.Easting = _startPosition.Easting + (radius * Math.sin(_radialIn.Radian) );
	    _centerPosition.Northing = -(-radius * Math.cos(_radialIn.Radian)) + _startPosition.Northing;
    }

}
}
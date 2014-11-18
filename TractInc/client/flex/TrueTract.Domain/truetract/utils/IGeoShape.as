package truetract.utils
{
    import flash.display.Graphics;
    import flash.geom.Point;

    public interface IGeoShape
    {
        function get startPosition():GeoPosition;
        function set startPosition(value:GeoPosition):void;
        function get endPosition():GeoPosition;
        
        function get bearingOut():GeoBearing;
        
        function getBounds():BoundRectangle;
    }
}
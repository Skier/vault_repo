package truetract.plotter.utils
{
    import truetract.plotter.utils.BoundRectangle;

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
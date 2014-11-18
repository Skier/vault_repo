package src.deedplotter.domain
{
    import src.deedplotter.utils.GeoPosition;
    
    [Bindable]
	[RemoteClass(alias="TractInc.DeedPro.Entity.TractTextObjectInfo")]
    public class TractTextObject
    {
        
        public var TractTextObjectId:int;
        public var TractId:int;
        public var Text:String;
        public var Easting:Number;
        public var Northing:Number;
        public var Rotation:Number = 0;
        
        public function get Position():GeoPosition {
            return new GeoPosition(Easting, Northing);
        }

        public function set Position(value:GeoPosition):void {
            Easting = value.Easting;
            Northing = value.Northing;
        }        
    }
}
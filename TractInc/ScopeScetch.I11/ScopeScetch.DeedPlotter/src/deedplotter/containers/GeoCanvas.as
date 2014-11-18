package src.deedplotter.containers
{
    
    import flash.events.Event;
    import flash.geom.Point;
    
    import mx.binding.utils.ChangeWatcher;
    import mx.containers.Canvas;
    import mx.events.PropertyChangeEvent;
    
    import src.deedplotter.utils.GeoPosition;
    import src.deedplotter.utils.ScaleValue;
    import src.deedplotter.events.ScaleEvent;

    [Event(name="scaleChanged", type="src.deedplotter.events.ScaleEvent")]

    public class GeoCanvas extends DraggableCanvas
    {

        public var StartPoint:Point;

        private var m_scale:ScaleValue;
        private var pointsPerFeet:Number;

        private var resetStartPointPosition:Boolean = true;
        
        public function GeoCanvas()
        {
            Scale = ScaleValue.Default.clone();
            StartPoint = new Point();
        }

        public function get Scale():ScaleValue
        {
            return m_scale;
        }

        [Bindable(event="scaleChanged")]
        public function set Scale(value:ScaleValue):void 
        {
            var scaleDelta:Number = 0;

            if (value) scaleDelta = value.PointsInOneFeet;
            if (m_scale) scaleDelta -= m_scale.PointsInOneFeet;

            m_scale = value;

            dispatchEvent(new ScaleEvent(scaleDelta));
        }

        public function ResetStartPointPosition():void
        {
            resetStartPointPosition = true;
            invalidateDisplayList();
        }
        
        override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void {
            super.updateDisplayList(unscaledWidth, unscaledHeight);
            
            if (resetStartPointPosition) {
                resetStartPointPosition = false;
                
                var widthDelta:Number = (this.width / 2) - StartPoint.x;
                var heihtDelta:Number = (this.height / 2) - StartPoint.y;
                
                scroll(widthDelta, heihtDelta);
            }
        }

        override public function scroll(deltaX:Number, deltaY:Number):void {
            super.scroll(deltaX, deltaY);

            StartPoint.x += deltaX;
            StartPoint.y += deltaY;
            
            invalidateDisplayList();
        }
        
        public function GetLocalX(easting:Number):Number {
            return StartPoint.x + ( easting * m_scale.PointsInOneFeet );
        }

        public function GetLocalY(northing:Number):Number {
            return - ( northing * m_scale.PointsInOneFeet ) + StartPoint.y;
        }
        
        public function GetLocalPosition(position:GeoPosition):Point {
            return new Point(GetLocalX(position.Easting), GetLocalY(position.Northing));
        }

        public function GetGeoPosition(p:Point):GeoPosition {
            var pointsPerFeet:Number = m_scale.PointsInOneFeet;

            var easting:Number = (p.x - StartPoint.x) / pointsPerFeet;
            var northing:Number = -(p.y - StartPoint.y) / pointsPerFeet;
            
            return new GeoPosition(easting, northing);
        }
        
    }
}
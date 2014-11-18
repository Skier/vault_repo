package truetract.plotter.components.tractViewClasses.events
{
    import flash.events.Event;
    
    import truetract.plotter.components.tractViewClasses.ControlPointView;
    import truetract.plotter.components.tractViewClasses.TractPointView;

    public class TractPointEvent extends Event
    {
        public static const TRACT_START_POINT:String = "TRACT_START_POINT";
        public static const TRACT_END_POINT:String = "TRACT_END_POINT";
        public static const TRACT_CONTROL_POINT:String = "TRACT_CONTROL_POINT";
        
        public static const POINT_MOUSE_DOWN:String = "pointMouseDown";
        
        public var pointView:ControlPointView;
        public var pointType:String;

        public function TractPointEvent(type:String, pointType:String, point:ControlPointView,
           bubbles:Boolean=true, cancelable:Boolean=true)
        {
            super(type, bubbles, cancelable);
            
            this.pointView = point;
            this.pointType = pointType;            
        }
                
        override public function clone():Event {
            return new TractPointEvent(type, pointType, pointView, bubbles, cancelable);
        }
        
    }
}
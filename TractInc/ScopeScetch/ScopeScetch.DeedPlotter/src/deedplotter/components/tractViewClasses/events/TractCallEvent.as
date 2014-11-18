package src.deedplotter.components.tractViewClasses.events
{
    import flash.events.Event;
    
    import src.deedplotter.components.tractViewClasses.call.CallView;

    public class TractCallEvent extends Event
    {
        public static const CALL_CLICK:String = "callClick";
        public static const CALL_DOUBLE_CLICK:String = "callDoubleClick";
        
        public var callView:CallView;
        
        public function TractCallEvent(type:String, callView:CallView,
           bubbles:Boolean=true, cancelable:Boolean=false)
        {
            super(type, bubbles, cancelable);
            
            this.callView = callView;
        }
                
        override public function clone():Event {
            return new TractCallEvent(type, callView, bubbles, cancelable);
        }
        
    }
}
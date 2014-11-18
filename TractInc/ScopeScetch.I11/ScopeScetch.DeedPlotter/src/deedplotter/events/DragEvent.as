package src.deedplotter.events
{
import flash.events.Event;

public class DragEvent extends Event
{
    public static const DRAGGED:String = "dragged";

    public var deltaX:Number = 0;
    public var deltaY:Number = 0;
    
    public function DragEvent(type:String, deltaX:Number, deltaY:Number,
       bubbles:Boolean=true, cancelable:Boolean=false)
    {
        super(type, bubbles, cancelable);
        
        this.deltaX = deltaX;
        this.deltaY = deltaY;
    }
            
    override public function clone():Event
    {
        return new DragEvent(type, deltaX, deltaY, bubbles, cancelable);
    }

}
}
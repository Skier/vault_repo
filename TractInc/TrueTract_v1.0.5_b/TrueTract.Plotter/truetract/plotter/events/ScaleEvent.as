package truetract.plotter.events
{
import flash.events.Event;

public class ScaleEvent extends Event
{
    public static const SCALE_CHANGED:String = "scaleChanged";

    public var scaleDelta:Number = 0;

    public function ScaleEvent(scaleDelta:Number,
       bubbles:Boolean=true, cancelable:Boolean=false)
    {
        super(SCALE_CHANGED, bubbles, cancelable);
        
        this.scaleDelta = scaleDelta;
    }
            
    override public function clone():Event
    {
        return new ScaleEvent(scaleDelta, bubbles, cancelable);
    }

}
}
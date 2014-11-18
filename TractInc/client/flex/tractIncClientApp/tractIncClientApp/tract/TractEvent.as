package tractIncClientApp.tract
{
import flash.events.Event;
import truetract.domain.Tract;

public class TractEvent extends Event
{
    public static const OPEN_TRACT:String = "openTract";

    public var tract:Tract;

    public function TractEvent(type:String, tract:Tract, 
        bubbles:Boolean=false, cancelable:Boolean=false)
    {
        super(type, bubbles, cancelable);

        this.tract = tract;
    }
    
    override public function clone():Event
    {
        return new TractEvent(type, tract, bubbles, cancelable);
    }
    
}
}
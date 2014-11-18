package src.deedplotter.events
{
import flash.events.Event;
import src.deedplotter.domain.Tract;
import flash.display.BitmapData;

public class TractExportEvent extends Event
{
    public static const EXPORT_EVENT:String = "export";

    public var format:String; // "pdf"

    public var tract:Tract;
    public var tractBitmapData:BitmapData;
    public var scaleBarBitmapData:BitmapData;

    public function TractExportEvent(tract:Tract, tractBitmapData:BitmapData, 
        scaleBarBitmapData:BitmapData, bubbles:Boolean=true, cancelable:Boolean=false)
    {
        super(EXPORT_EVENT, bubbles, cancelable);
        
        this.tract = tract;
        this.tractBitmapData = tractBitmapData;
        this.scaleBarBitmapData = scaleBarBitmapData;
    }
            
    override public function clone():Event
    {
        return new TractExportEvent(tract, tractBitmapData, scaleBarBitmapData, bubbles, cancelable);
    }

}
}
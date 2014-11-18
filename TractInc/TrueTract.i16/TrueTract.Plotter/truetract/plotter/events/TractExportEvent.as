package truetract.plotter.events
{
import flash.events.Event;
import truetract.plotter.domain.Tract;
import flash.display.BitmapData;

public class TractExportEvent extends Event
{
    public static const PDF_EXPORT_REQUEST_EVENT:String = "pdfExportRequest";

    public var format:String; // "pdf"

    public var tract:Tract;
    public var tractBitmapData:BitmapData;
    public var scaleBarBitmapData:BitmapData;

    public function TractExportEvent(tract:Tract, tractBitmapData:BitmapData, 
        scaleBarBitmapData:BitmapData, bubbles:Boolean=false, cancelable:Boolean=false)
    {
        super(PDF_EXPORT_REQUEST_EVENT, bubbles, cancelable);
        
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
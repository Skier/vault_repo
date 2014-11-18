package truetract.web.dashboard.events
{
import flash.events.Event;
import truetract.domain.Document;

public class DocumentEvent extends Event
{
    public static const OPEN_DOCUMENT:String = "openDocument";

    public var document:Document;

    public function DocumentEvent(type:String, document:Document, 
        bubbles:Boolean=false, cancelable:Boolean=false)
    {
        super(type, bubbles, cancelable);

        this.document = document;
    }
    
    override public function clone():Event
    {
        return new DocumentEvent(type, document, bubbles, cancelable);
    }
    
}
}
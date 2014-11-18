package tractIncClientApp.document
{
import flash.events.Event;
import truetract.domain.DocumentReference;

public class DocumentReferenceEvent extends Event
{
    public static const ADD_REFERENCE:String = "addReference";
    public static const OPEN_REFERENCE:String = "openReference";
    public static const DELETE_REFERENCE:String = "deleteReference";
    
    public var reference:DocumentReference;

    public function DocumentReferenceEvent(type:String, reference:DocumentReference = null, 
        bubbles:Boolean=false, cancelable:Boolean=false)
    {
        super(type, bubbles, cancelable);

        this.reference = reference;
    }
    
    override public function clone():Event
    {
        return new DocumentReferenceEvent(type, reference, bubbles, cancelable);
    }
    
}
}
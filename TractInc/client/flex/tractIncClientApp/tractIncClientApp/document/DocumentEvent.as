package tractIncClientApp.document
{
import flash.events.Event;
import truetract.domain.Document;

public class DocumentEvent extends Event
{
    public static const OPEN_DOCUMENT:String = "openDocument";
    public static const ADD_DOCUMENT:String = "addDocument";
    public static const SAVE_DOCUMENT:String = "saveDocument";

    public var document:Document;
    public var docTarget:*;
    public var docEditorMode:String;

    public function DocumentEvent(type:String, document:Document, 
    	docTarget:* = null, docEditorMode:String = null,
        bubbles:Boolean=false, cancelable:Boolean=false)
    {
        super(type, bubbles, cancelable);

        this.document = document;
        this.docTarget = docTarget;
        this.docEditorMode = docEditorMode;
    }
    
    override public function clone():Event
    {
        return new DocumentEvent(type, document, docTarget, docEditorMode, bubbles, cancelable);
    }
    
}
}
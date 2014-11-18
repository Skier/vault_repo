package truetract.domain
{
import truetract.domain.mementos.FileMemento;
import truetract.domain.mementos.DocumentAttachmentMemento;
    
[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.DocumentAttachmentInfo")]
public class DocumentAttachment implements IFileAttachment
{
    private static const PDF_COPY_TYPE:String = "Document PDF Copy";

    public var DocumentAttachmentId:int;
    public var DocumentAttachmentTypeId:int;
    public var DocumentId:int;
    public var FileId:int;

    public var FileRef:File;
    public var DocumentRef:Document;

    private var dict:DictionaryRegistry = DictionaryRegistry.getInstance();
    
    public function get TypeName():String
    {
        return dict.getDocumentAttachmentType(DocumentAttachmentTypeId).@Name;
    }

    public function IsPdfCopy():Boolean
    {
        return TypeName == PDF_COPY_TYPE;
    }

    public function get fileId():int { return FileId; };
    public function set fileId(value:int):void {
        FileId = value;
    };
    
    public function get file():File { return FileRef; };
    public function set file(value:File):void {
        FileRef = value;
    };
    
    public function get attachmentTypeId():int { return DocumentAttachmentTypeId; };
    public function set attachmentTypeId(value:int):void {
        DocumentAttachmentTypeId = value;
    };
    
    public function getMemento():Object
    {
        var memento:DocumentAttachmentMemento = new DocumentAttachmentMemento();

        memento.documentAttachmentId = DocumentAttachmentId;
        memento.documentAttachmentTypeId = DocumentAttachmentTypeId;
        memento.documentId = DocumentId;
        memento.fileId = FileId;

        if (FileRef)
        {
            memento.fileMemento = FileMemento(FileRef.getMemento());
        }
        
        return memento;
    }

    public function setMemento(value:Object):void
    {
        var memento:DocumentAttachmentMemento = DocumentAttachmentMemento(value);

        DocumentAttachmentId = memento.documentAttachmentId;
        DocumentAttachmentTypeId = memento.documentAttachmentTypeId;
        DocumentId = memento.documentId;
        FileId = memento.fileId;

        if (memento.fileMemento)
            FileRef.setMemento(memento.fileMemento);
        else
            FileRef = null;
    }

    public function clone():DocumentAttachment
    {
        var clone:DocumentAttachment = new DocumentAttachment();
        
        clone.DocumentAttachmentId = DocumentAttachmentId;
        clone.DocumentAttachmentTypeId = DocumentAttachmentTypeId;
        clone.DocumentId = DocumentId;
        clone.FileId = FileId;

        clone.FileRef = FileRef ? FileRef.clone() : null;

        return clone;
    }
}
}
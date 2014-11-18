package truetract.domain
{
import truetract.domain.mementos.ProjectAttachmentMemento;
import truetract.domain.mementos.FileMemento;
    
[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.ProjectAttachmentInfo")]
public class ProjectAttachment implements IFileAttachment
{
    public var ProjectAttachmentId:int;
    public var ProjectAttachmentTypeId:int;
    public var ProjectId:int;
    public var FileId:int;

    public var FileRef:File;
    public var ProjectRef:Project;

    private var dict:DictionaryRegistry = DictionaryRegistry.getInstance();

    public function get TypeName():String
    {
        return dict.getProjectAttachmentType(ProjectAttachmentTypeId).@Name;
    }
    
    public function get fileId():int { return FileId; };
    public function set fileId(value:int):void {
        FileId = value;
    };
    
    public function get file():File { return FileRef; };
    public function set file(value:File):void {
        FileRef = value;
    };
    
    public function get attachmentTypeId():int { return ProjectAttachmentTypeId; };
    public function set attachmentTypeId(value:int):void {
        ProjectAttachmentTypeId = value;
    };
    
    public function getMemento():Object
    {
        var memento:ProjectAttachmentMemento = new ProjectAttachmentMemento();

        memento.projectAttachmentId = ProjectAttachmentId;
        memento.projectAttachmentTypeId = ProjectAttachmentTypeId;
        memento.projectId = ProjectId;
        memento.fileId = FileId;

        if (FileRef)
        {
            memento.fileMemento = FileMemento(FileRef.getMemento());
        }
        
        return memento;
    }

    public function setMemento(value:Object):void
    {
        var memento:ProjectAttachmentMemento = ProjectAttachmentMemento(value);

        ProjectAttachmentId = memento.projectAttachmentId;
        ProjectAttachmentTypeId = memento.projectAttachmentTypeId;
        ProjectId = memento.projectId;
        FileId = memento.fileId;

        if (memento.fileMemento)
            FileRef.setMemento(memento.fileMemento);
        else
            FileRef = null;
    }
}
}
package truetract.domain
{
import truetract.domain.mementos.FileMemento;
    
[Bindable] [RemoteClass(alias="TractInc.TrueTract.Entity.FileInfo")]
public class File implements IMemento
{
    public var FileId:int;
    public var FileName:String;
    public var FileUrl:String;
    public var FilePath:String;
    public var Description:String;
    public var Created:Date;
    public var CreatedBy:int;
    public var CreatedByName:String;

    public function get fileFullUrl():String
    {
        return FileUrl + "\\" + FileName;
    }

    public function getMemento():Object
    {
        var memento:FileMemento = new FileMemento();

        memento.fileId = FileId;
        memento.fileName = FileName;
        memento.fileUrl = FileUrl;
        memento.filePath = FilePath;
        memento.description = Description;
        memento.created = Created;
        memento.createdBy = CreatedBy;
        memento.createdByName = CreatedByName;

        return memento;
    }

    public function setMemento(value:Object):void
    {
        var memento:FileMemento = FileMemento(value);

        FileId = memento.fileId;
        FileName = memento.fileName;
        FileUrl = memento.fileUrl;
        FilePath = memento.filePath;
        Description = memento.description;
        Created = memento.created;
        CreatedBy = memento.createdBy;
        CreatedByName = memento.createdByName;
    }
    
    public function clone():File 
    {
    	var clone:File = new File();
    	
    	clone.FileId = FileId;
    	clone.FileName = FileName;
    	clone.FileUrl = FileUrl;
    	clone.FilePath = FilePath;
    	clone.Description = Description;
    	clone.Created = Created;
    	clone.CreatedBy = CreatedBy;
    	clone.CreatedByName = CreatedByName;
    	
    	return clone;
    }

}
}
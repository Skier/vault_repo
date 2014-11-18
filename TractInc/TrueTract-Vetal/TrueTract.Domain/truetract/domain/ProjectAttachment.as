package truetract.domain
{
[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.ProjectAttachmentInfo")]
public class ProjectAttachment
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
}
}
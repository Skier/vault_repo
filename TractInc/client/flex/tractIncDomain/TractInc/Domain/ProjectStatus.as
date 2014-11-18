package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.ProjectStatus")]
    public class ProjectStatus
    {
        public var ProjectStatusId:int;      
        public var StatusName:String;
    }
}
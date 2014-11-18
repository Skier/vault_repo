package TractInc.Domain
{
    [Bindable]
	[RemoteClass(alias="TractInc.Server.Domain.Permission")]
	public class Permission
	{
        public var PermissionId:int;
        public var ModuleId:int;
        public var Description:String;
        public var Code:String;
        
        public var Selected:Boolean;
	}
}
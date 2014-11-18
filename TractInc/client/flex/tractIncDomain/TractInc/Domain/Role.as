package TractInc.Domain
{
	import mx.collections.ArrayCollection;
	
    [Bindable]
	[RemoteClass(alias="TractInc.Server.Domain.Role")]
	public class Role
	{
        public var RoleId:int;
        public var Name:String;

	    private var _permissions:Array;
	    
	    public function get PermissionList():Array 
	    { 
	        return _permissions; 
	    }
	    
	    public function set PermissionList(value:Array):void 
	    { 
	        _permissions = value;
	    }
        
        public var Selected:Boolean;
	}
}
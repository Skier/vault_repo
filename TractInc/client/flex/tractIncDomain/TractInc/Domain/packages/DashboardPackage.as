package TractInc.Domain.packages
{
	import TractInc.Domain.User;
	import mx.collections.ArrayCollection;
	
    [Bindable]
	[RemoteClass(alias="TractInc.Server.Domain.Package.Dashboard.DashboardPackage")]
	public class DashboardPackage
	{
		public var user:User;
		
	    private var _modules:ArrayCollection = new ArrayCollection();
	    public function get Modules():ArrayCollection { return _modules; }
	
	    private var _moduleList:Array;
	    public function get ModuleList():Array { return _modules.source; }
	    public function set ModuleList(value:Array):void 
	    { 
	        Modules.source = _moduleList = value;
	    }
	}
}
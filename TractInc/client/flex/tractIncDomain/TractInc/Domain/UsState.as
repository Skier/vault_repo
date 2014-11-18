package TractInc.Domain
{
	import mx.collections.ArrayCollection;
	
    [Bindable]
	[RemoteClass(alias="TractInc.Server.Domain.State")]
	public class UsState
	{
        public var StateId:int;
        public var Name:String;
        public var StateFips:String;
        public var StateAbbr:String;

	    private var _countysList:ArrayCollection = new ArrayCollection();
	    public function get CountysList():ArrayCollection { return _countysList; }
	
	    private var _countys:Array;
	    public function get Countys():Array { return _countys; }
	    public function set Countys(value:Array):void 
	    { 
	        CountysList.source = _countys = value;
	    }
	}
}
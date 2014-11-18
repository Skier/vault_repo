package App.Entity
{

    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.ProjectDataObject")]
	public class ProjectDataObject
	{

		public static const SUBAFE_STATUS_ISSUED: String   = "ISSUED";
		public static const SUBAFE_STATUS_EXPIRED: String  = "EXPIRED";
		public static const SUBAFE_STATUS_LOCKED: String   = "LOCKED";
		public static const SUBAFE_STATUS_UNLOCKED: String = "UNLOCKED";

        public var SubAFE:String;

        public var AFE:String;

        public var SubAFEStatus:String;

        public var ShortName:String;

        public var Deleted:Boolean;

        private var _temporary:Boolean;
        public function get Temporary():Boolean {
        	return _temporary;
        }
        public function set Temporary(value:Boolean):void {
        	_temporary = value;
        }
        
        public function get TemporaryString():String {
        	return (_temporary)? "Yes": "No";
        }
        
        public var IsNew:Boolean;
        
        public var Assignments:Array;

	}

}

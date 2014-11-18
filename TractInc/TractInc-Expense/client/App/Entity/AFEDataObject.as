package App.Entity
{
	import mx.collections.ArrayCollection;
	
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.AFEDataObject")]
	public class AFEDataObject
	{

		public static const AFE_STATUS_ISSUED: String   = "ISSUED";
		public static const AFE_STATUS_EXPIRED: String  = "EXPIRED";
		public static const AFE_STATUS_LOCKED: String   = "LOCKED";
		public static const AFE_STATUS_UNLOCKED: String = "UNLOCKED";
			
        public var AFE:String;

        public var ClientId:int;

        public var AFEName:String;

        public var AFEStatus:String;

        public var Deleted:Boolean;
        
        public var IsNew:Boolean = false;
        
	}
	
}

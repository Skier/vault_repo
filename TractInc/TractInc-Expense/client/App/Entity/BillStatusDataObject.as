package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.BillStatusDataObject")]
	public class BillStatusDataObject
	{
		
		public static const BILL_STATUS_NEW: String       = "NEW";
		public static const BILL_STATUS_SUBMITTED: String = "SUBMITTED";
		public static const BILL_STATUS_REJECTED: String  = "REJECTED";
		public static const BILL_STATUS_CHANGED: String   = "CHANGED";
		public static const BILL_STATUS_CORRECTED: String = "CORRECTED";
		public static const BILL_STATUS_APPROVED: String  = "APPROVED";
		public static const BILL_STATUS_CONFIRMED: String = "CONFIRMED";
		public static const BILL_STATUS_DECLINED: String  = "DECLINED";
		public static const BILL_STATUS_VERIFIED: String  = "VERIFIED";
			
		public var Status:String;
		
	}
	
}

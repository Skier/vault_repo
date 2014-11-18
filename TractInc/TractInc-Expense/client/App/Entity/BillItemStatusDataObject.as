package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.BillItemStatusDataObject")]
	public class BillItemStatusDataObject
	{
		
		public static const BILL_ITEM_STATUS_NEW: String       = "NEW";
		public static const BILL_ITEM_STATUS_SUBMITTED: String = "SUBMITTED";
		public static const BILL_ITEM_STATUS_REJECTED: String  = "REJECTED";
		public static const BILL_ITEM_STATUS_CHANGED: String   = "CHANGED";
		public static const BILL_ITEM_STATUS_CORRECTED: String = "CORRECTED";
		public static const BILL_ITEM_STATUS_APPROVED: String  = "APPROVED";
		public static const BILL_ITEM_STATUS_CONFIRMED: String = "CONFIRMED";
		public static const BILL_ITEM_STATUS_DECLINED: String  = "DECLINED";
		public static const BILL_ITEM_STATUS_VERIFIED: String  = "VERIFIED";
		
		public var Status:String;
		
		public static function isLandmanStatus(status:String):Boolean {
			return (BILL_ITEM_STATUS_NEW == status)
				|| (BILL_ITEM_STATUS_REJECTED == status)
				|| (BILL_ITEM_STATUS_CHANGED == status);
		}
		
		public static function isCrewChiefStatus(status:String):Boolean {
			return (BILL_ITEM_STATUS_SUBMITTED == status)
				|| (BILL_ITEM_STATUS_CORRECTED == status)
				|| (BILL_ITEM_STATUS_DECLINED == status);
		}
		
		public static function isManagerStatus(status:String):Boolean {
			return (BILL_ITEM_STATUS_APPROVED == status);
		}
		
	}
	
}

package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.InvoiceItemStatusDataObject")]
	public class InvoiceItemStatusDataObject
	{
		
		public static const INVOICE_ITEM_STATUS_NEW: String       = "NEW";
		public static const INVOICE_ITEM_STATUS_SUBMITTED: String = "SUBMITTED";
		public static const INVOICE_ITEM_STATUS_VOID: String      = "VOID";
		public static const INVOICE_ITEM_STATUS_PAID: String      = "PAID";
			
		public var Status:String;
		
	}
	
}

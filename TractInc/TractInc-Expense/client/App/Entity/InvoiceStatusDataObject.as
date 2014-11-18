package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.InvoiceStatusDataObject")]
	public class InvoiceStatusDataObject
	{
		
		public static const INVOICE_STATUS_NEW: String       = "NEW";
		public static const INVOICE_STATUS_SUBMITTED: String = "SUBMITTED";
		public static const INVOICE_STATUS_PAID: String      = "PAID";
		public static const INVOICE_STATUS_VOID: String      = "VOID";
			
		public var Status:String;
		
	}
	
}

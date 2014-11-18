package App.Domain
{
	import App.Domain.Codegen.*;      
	[Bindable]
	[RemoteClass(alias="TractInc.Expense.Domain.Note")]
	public dynamic class Note extends _Note 
	{
		public static const NOTE_TYPE_BILL:String            = "BILL";
		public static const NOTE_TYPE_BILL_ITEM:String       = "BILL_ITEM";
		public static const NOTE_TYPE_INVOICE:String         = "INVOICE";
		public static const NOTE_TYPE_INVOICE_ITEM:String    = "INVOICE_ITEM";
	}
}
    
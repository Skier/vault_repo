package UI.manager.invoice
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class InvoiceManagerModel
	{
        public static const VIEW_STATE_INVOICE_LIST:int   = 0;
        public static const VIEW_STATE_INVOICE_DETAIL:int = 1;

		public var isLoaded:Boolean = false;
		public var invoices:ArrayCollection = new ArrayCollection();
	}
}
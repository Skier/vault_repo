package UI.manager.invoice
{
	[Bindable]
	public class InvoiceItemGroupByType extends InvoiceItemGroup
	{

		public function InvoiceItemGroupByType(controller:InvoiceManagerController) {
			super(controller);
		}

		public var invoiceItemTypeId:int;

	}
}
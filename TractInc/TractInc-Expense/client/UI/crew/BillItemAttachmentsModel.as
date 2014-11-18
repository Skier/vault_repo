package UI.crew
{
	import App.Entity.BillItemDataObject;
	
	
	[Bindable]
	public class BillItemAttachmentsModel
	{
		public var currentBillItem:BillItemDataObject;

		public var isLoaded:Boolean = false;

		public var isReadOnly:Boolean;
		
		public var uploadingInProgress:Boolean = false;
		
		public function BillItemAttachmentsModel(billItem:BillItemDataObject, isRO:Boolean) {
			currentBillItem = billItem;
			isReadOnly = isRO;
		}
	}
	
}

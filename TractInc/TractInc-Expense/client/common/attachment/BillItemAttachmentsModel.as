package common.attachment
{

	import App.Entity.BillItemDataObject;
	
	[Bindable]
	public class BillItemAttachmentsModel
	{
		public var currentBillItem:BillItemDataObject;

		public var isReadOnly:Boolean;
		
		public var uploadingInProgress:Boolean = false;
		
		public function BillItemAttachmentsModel(billItem:BillItemDataObject, isRO:Boolean) {
			currentBillItem = billItem;
			isReadOnly = isRO;
		}
	}
	
}

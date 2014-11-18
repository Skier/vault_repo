package common.attachment
{
	import App.Domain.BillItem;
	
	[Bindable]
	public class BillItemAttachmentsModel
	{
		public static const BILL_ITEMS_STORAGE_DIRECTORY:String = "BillItems";
		
		public var currentBillItem:BillItem;

		public var isLoaded:Boolean = false;

		public var isReadOnly:Boolean;
		
		public var uploaderUrl:String;
		public var uploaderUrlLoaded:Boolean = false;

		public var storageBaseUrl:String;
		public var storageBaseUrlLoaded:Boolean = false;
		
		public var uploadingInProgress:Boolean = false;
		
		public function BillItemAttachmentsModel(billItem:BillItem, isRO:Boolean) {
			currentBillItem = billItem;
			isReadOnly = isRO;
		}
	}
}
package UI.crew
{
	
	import mx.collections.ArrayCollection;
	import App.Entity.BillDataObject;
	
	[Bindable]
	public class AttachmentsModel
	{
		
		public var bill:BillDataObject;
		
		public var isLoaded:Boolean = false;

		public var itemAttachments:ArrayCollection;
		
		public var compositeAttachments:ArrayCollection;
		
		public function AttachmentsModel(bill:BillDataObject) {
			this.bill = bill;
			this.itemAttachments = new ArrayCollection();
			this.compositeAttachments = new ArrayCollection();
		}
		
	}
	
}

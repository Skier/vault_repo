package App.Entity
{
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.BillItemAttachmentDataObject")]
	public dynamic class BillItemAttachmentDataObject
	{
		
        public var BillItemAttachmentId:int;

        public var BillItemId:int;

        public var FileName:String;

        public var OriginalFileName:String;
		
        public var IsDeleted:Boolean;
        
        public var BillItemInfo:BillItemDataObject;
        
        public var CompositionInfo:BillItemCompositionDataObject;
        
        public function clone():BillItemAttachmentDataObject {
			var attachmentInfo:BillItemAttachmentDataObject = new BillItemAttachmentDataObject();
			attachmentInfo.BillItemAttachmentId = BillItemAttachmentId;
			attachmentInfo.BillItemId = BillItemId;
			attachmentInfo.FileName = FileName;
			attachmentInfo.IsDeleted = IsDeleted;
			attachmentInfo.OriginalFileName = OriginalFileName;
			return attachmentInfo;
        }
		
        public function assign(info:BillItemAttachmentDataObject):void {
			BillItemAttachmentId = info.BillItemAttachmentId;
			BillItemId = info.BillItemId;
			FileName = info.FileName;
			IsDeleted = info.IsDeleted;
			OriginalFileName = info.OriginalFileName;
        }
		
	}
}
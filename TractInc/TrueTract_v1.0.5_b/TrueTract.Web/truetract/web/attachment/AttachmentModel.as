package truetract.web.attachment
{
	import truetract.plotter.domain.DocAttachment;
	import truetract.plotter.domain.Document;
	
	[Bindable]
	public class AttachmentModel
	{
		public static const ATTACHMENTS_STORAGE_DIRECTORY:String = "DocAttachments";
		
		public var currentDocument:Document;

		public var currentAttachment:DocAttachment;
		public var newAttachment:DocAttachment;

		public var uploaderUrl:String;
		public var uploaderUrlLoaded:Boolean = false;

		public var storageBaseUrl:String;
		public var storageBaseUrlLoaded:Boolean = false;
		
		public var uploadingInProgress:Boolean = false;
		
		public function AttachmentModel(doc:Document):void 
		{
			currentDocument = doc;
			reset();
		}
		
		public function reset():void 
		{
			if (currentDocument.Attachment) {
				currentAttachment = new DocAttachment();
				currentAttachment.DocId = currentDocument.DocID;
				currentAttachment.FileName = currentDocument.Attachment.FileName;
				currentAttachment.OriginalFileName = currentDocument.Attachment.OriginalFileName;
			} else {
				currentAttachment = null;
			}
			uploadingInProgress = false;
		}
		
	}
}
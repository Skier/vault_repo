package truetract.web.attachment
{
	import truetract.plotter.domain.IAttach;
	import truetract.plotter.domain.DocAttachment;
	
	[Bindable]
	public class AttachmentsModel
	{
		public static const ATTACHMENTS_STORAGE_DIRECTORY:String = "DocAttachments";
		
		public var currentDoc:IAttach;
		
		public var currentAttachment:DocAttachment;

		public var uploaderUrl:String;
		public var uploaderUrlLoaded:Boolean = false;

		public var storageBaseUrl:String;
		public var storageBaseUrlLoaded:Boolean = false;
		
		public var uploadingInProgress:Boolean = false;
		
		public function AttachmentsModel(doc:IAttach):void 
		{
			currentDoc = doc;
		}
		
		public function reset():void 
		{
			uploadingInProgress = false;
		}
		
	}
}
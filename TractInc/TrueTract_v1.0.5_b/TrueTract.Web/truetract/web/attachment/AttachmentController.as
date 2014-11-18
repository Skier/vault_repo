package truetract.web.attachment
{
	import truetract.plotter.domain.DocAttachment;
	import flash.net.FileReference;
	import mx.rpc.remoting.RemoteObject;
	import mx.rpc.events.*;
	import flash.events.*;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	import mx.controls.Alert;
	import flash.net.URLVariables;
	import truetract.plotter.domain.Document;
	import flash.net.FileFilter;
	
	public class AttachmentController
	{
		private var fileRef:FileReference;
		private var service:RemoteObject;
		
		[Bindable]
		public var model:AttachmentModel;
		public var view:AttachmentView;
		
		public function AttachmentController(view:AttachmentView, doc:Document) 
		{
			this.view = view;

            service = new RemoteObject("GenericDestination");
            service.source = "TractInc.Expense.BaseService";

			service.GetStorageUrl.addEventListener(ResultEvent.RESULT, onGetStorageUrl);
			service.GetUploaderUrl.addEventListener(ResultEvent.RESULT, onGetUploaderUrl);
			service.addEventListener(FaultEvent.FAULT, onFault);

			fileRef = new FileReference();
			
			fileRef.addEventListener(Event.SELECT, onSelectLoad);
			fileRef.addEventListener(Event.COMPLETE, onCompleteLoad);
        	fileRef.addEventListener(IOErrorEvent.IO_ERROR, onErrorUpload);
        	fileRef.addEventListener(ProgressEvent.PROGRESS, onProgressLoad);
        	
			model = new AttachmentModel(doc);

			getServerUrls();
		}
		
		private function getServerUrls():void 
		{
			service.GetStorageUrl();
			service.GetUploaderUrl();
		}
		
		public function onUpload_clickHandler():void 
		{
			model.newAttachment = new DocAttachment();
			
			fileRef.browse( [ new FileFilter("PDF Documents (*.pdf)", "*.pdf")] );
		}

		public function onOpen_clickHandler():void 
		{
			var attachment:DocAttachment = model.currentAttachment;
			
			var url:URLRequest = new URLRequest(model.storageBaseUrl + AttachmentModel.ATTACHMENTS_STORAGE_DIRECTORY + "/" + attachment.FileName);
			navigateToURL(url, "_blank");
		}
			
		public function onRemove_clickHandler():void 
		{
			model.currentAttachment = null;
			model.uploadingInProgress = false;
		}
			
		public function onCancelUpload_clickHandler():void 
		{
			fileRef.cancel();
			model.uploadingInProgress = false;
		}
			
		private function onGetStorageUrl(event:ResultEvent):void 
		{
			model.storageBaseUrl = event.result as String;
			model.storageBaseUrlLoaded = true;
		}

		private function onGetUploaderUrl(event:ResultEvent):void 
		{
			model.uploaderUrl = event.result as String;
			model.uploaderUrlLoaded = true;
		}
			
		private function onFault(event:FaultEvent):void 
		{
			Alert.show(event.fault.message);
		}
			
		private function onSelectLoad(event:Event):void 
		{
			fileRef = FileReference(event.target);
			if (fileRef.name.substr(fileRef.name.length-4, 4).toUpperCase() != ".PDF") {
				Alert.show("Sorry, you can upload only PDF files.", "Unsupported format");
				return;
			}

            var requestURL:URLRequest = new URLRequest;
            requestURL.url = model.uploaderUrl;
            requestURL.method = "POST";

            var variables:URLVariables = new URLVariables();
            var uid:String = UIDUtil.createUID().toString() + "__";

            variables.UploadDir = AttachmentModel.ATTACHMENTS_STORAGE_DIRECTORY;
            variables.UniqueKey = uid;
            
            requestURL.data = variables;

			model.newAttachment.FileName = uid + fileRef.name;
			model.newAttachment.OriginalFileName = fileRef.name;

			model.uploadingInProgress = true;
			fileRef.upload(requestURL);
		}

		private function onProgressLoad(event:ProgressEvent):void 
		{
			view.pbUpload.setProgress(event.bytesLoaded, event.bytesTotal);
		}

		private function onCompleteLoad(event:Event):void 
		{
			model.uploadingInProgress = false;
			model.currentAttachment = model.newAttachment;
		}
			
		private function onErrorUpload(event:IOErrorEvent):void 
		{
			model.uploadingInProgress = false;
			Alert.show(event.text, "Uploading Error");
		}
		
	}
}
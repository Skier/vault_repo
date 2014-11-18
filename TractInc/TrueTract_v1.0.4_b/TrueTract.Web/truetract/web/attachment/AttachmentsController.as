package truetract.web.attachment
{
	import flash.display.DisplayObject;
	import truetract.plotter.domain.IAttach;
	import truetract.plotter.domain.DocAttachment;
	import flash.net.URLRequest;
	import flash.net.URLVariables;
	import mx.utils.UIDUtil;
	import mx.controls.Alert;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.events.FaultEvent;
	import flash.events.ProgressEvent;
	import flash.events.IOErrorEvent;
	import flash.net.FileReference;
	import mx.rpc.remoting.RemoteObject;
	import mx.managers.PopUpManager;
	import flash.net.navigateToURL;
	
	public class AttachmentsController
	{
		private var fileRef:FileReference;
		private var service:RemoteObject;
		
		[Bindable]
		public var model:AttachmentsModel;
		public var view:AttachmentsView;
		
		public function AttachmentsController(view:AttachmentsView, doc:IAttach) 
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
        	
			model = new AttachmentsModel(doc);

			getServerUrls();
		}
		
		private function getServerUrls():void 
		{
			service.GetStorageUrl();
			service.GetUploaderUrl();
		}
		
		private function Close():void 
		{
			PopUpManager.removePopUp(view);
		}
		
		public function onUpload_clickHandler():void 
		{
			model.currentAttachment = new DocAttachment();
			
			fileRef.browse();
		}

		public function onOpen_clickHandler():void 
		{
			var attachment:DocAttachment = view.dgAttachments.selectedItem as DocAttachment;
			
			var url:URLRequest = new URLRequest(model.storageBaseUrl + AttachmentsModel.ATTACHMENTS_STORAGE_DIRECTORY + "/" + attachment.FileName);
			navigateToURL(url, "_blank");
		}
			
		public function onRemove_clickHandler():void 
		{
			var attachment:DocAttachment = view.dgAttachments.selectedItem as DocAttachment;
			
			var index:int = model.currentDoc.Attachments.getItemIndex(attachment);
			if (index != -1) {
				model.currentDoc.Attachments.removeItemAt(index);
			}
		}
			
		public function onCancelUpload_clickHandler():void 
		{
			fileRef.cancel();
			model.reset();
		}
			
		public function onClose_clickHandler():void 
		{
			Close();
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
			Close();
		}
			
		private function onSelectLoad(event:Event):void 
		{
            var requestURL:URLRequest = new URLRequest;
            requestURL.url = model.uploaderUrl;
            requestURL.method = "POST";

            var variables:URLVariables = new URLVariables();
            var uid:String = UIDUtil.createUID().toString() + "__";

            variables.UploadDir = AttachmentsModel.ATTACHMENTS_STORAGE_DIRECTORY;
            variables.UniqueKey = uid;
            
            requestURL.data = variables;

			fileRef = FileReference(event.target);

			model.currentAttachment.FileName = uid + fileRef.name;
			model.currentAttachment.OriginalFileName = fileRef.name;

			model.uploadingInProgress = true;
			fileRef.upload(requestURL);
		}

		private function onProgressLoad(event:ProgressEvent):void 
		{
			view.pbUpload.setProgress(event.bytesLoaded, event.bytesTotal);
		}

		private function onCompleteLoad(event:Event):void 
		{
			model.reset();
			model.currentDoc.Attachments.addItem(model.currentAttachment);
		}
			
		private function onErrorUpload(event:IOErrorEvent):void 
		{
			model.reset();
			Alert.show(event.text, "Uploading Error");
		}
		
	}
}
package common.attachment
{
	import flash.net.FileReference;
	import mx.rpc.remoting.RemoteObject;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	import mx.managers.PopUpManager;
	import flash.events.MouseEvent;
	import flash.net.URLRequest;
	import flash.net.URLVariables;
	import mx.utils.UIDUtil;
	import App.Domain.BillItemAttachment;
	import App.Domain.BillItem;
	import mx.rpc.events.ResultEvent;
	import flash.events.Event;
	import flash.events.ProgressEvent;
	import flash.events.IOErrorEvent;
	import flash.net.navigateToURL;
	import weborb.data.DynamicLoadEvent;
	
	public class BillItemAttachmentsController
	{
		private var fileRef:FileReference;
		private var service:RemoteObject;
		
		private var attachment:BillItemAttachment;
		
		private var view:BillItemAttachmentsView;

		[Bindable]
		public var model:BillItemAttachmentsModel;
		
		public function BillItemAttachmentsController(view:BillItemAttachmentsView, billItem:BillItem, isReadOnly:Boolean) 
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
        	
        	init(billItem, isReadOnly);
		}
		
		public function init(billItem:BillItem, isReadOnly:Boolean):void 
		{
			model = new BillItemAttachmentsModel(billItem, isReadOnly);
			if (model.currentBillItem.RelatedBillItemAttachment.IsLoaded || model.currentBillItem.BillItemId == 0) {
				model.isLoaded = true;
				getServerUrls();
			} else {
				model.currentBillItem.RelatedBillItemAttachment.addEventListener("loaded", onBillItemAttachmentsLoaded);
			}
		}
		
		public function onClickUpload():void 
		{
			attachment = new BillItemAttachment();
			
			fileRef.browse();
		}

		public function onClickOpen():void 
		{
			var attachment:BillItemAttachment = view.dgBillItemAttachments.selectedItem as BillItemAttachment;
			
			var url:URLRequest = new URLRequest(model.storageBaseUrl + BillItemAttachmentsModel.BILL_ITEMS_STORAGE_DIRECTORY + "/" + attachment.FileName);
			navigateToURL(url, "_blank");
		}
			
		public function onClickCancelUpload():void 
		{
			fileRef.cancel();
			model.uploadingInProgress = false;
		}
			
		public function onClickClose():void 
		{
			PopUpManager.removePopUp(view);
		}
		
		private function onBillItemAttachmentsLoaded(event:DynamicLoadEvent):void 
		{
			model.currentBillItem.RelatedBillItemAttachment.removeEventListener("loaded", onBillItemAttachmentsLoaded);
			model.isLoaded = true;
			getServerUrls();
		}
		
		private function getServerUrls():void 
		{
			service.GetStorageUrl();
			service.GetUploaderUrl();
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
            var requestURL:URLRequest = new URLRequest;
            requestURL.url = model.uploaderUrl;
            requestURL.method = "POST";

            var variables:URLVariables = new URLVariables();
            var uid:String = UIDUtil.createUID().toString() + "__";

            variables.UploadDir = BillItemAttachmentsModel.BILL_ITEMS_STORAGE_DIRECTORY;
            variables.UniqueKey = uid;
            
            requestURL.data = variables;

			fileRef = FileReference(event.target);
			attachment.FileName = uid + fileRef.name;
			attachment.OriginalFileName = fileRef.name;

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
			model.currentBillItem.RelatedBillItemAttachment.addItem(attachment);
		}
			
		private function onErrorUpload(event:IOErrorEvent):void 
		{
			model.uploadingInProgress = false;
			Alert.show(event.text, "Uploading Error");
		}
		
	}
}
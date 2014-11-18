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
	import UI.landman.Composition;
	import App.Entity.BillItemAttachmentDataObject;
	import App.Entity.BillItemDataObject;
	import flash.net.FileFilter;
	
	[Bindable]
	public class BillItemAttachmentsController
	{
		
		private static function onFault(event:FaultEvent):void 
		{
			Alert.show(event.fault.message);
		}
			
		private var fileRef:FileReference;
		
		private var attachment:BillItemAttachmentDataObject;
		
		private var view:BillItemAttachmentsView;

		public var model:BillItemAttachmentsModel;
		
		private var composition:Composition = null;
		
		public function BillItemAttachmentsController(view:BillItemAttachmentsView, billItem:BillItemDataObject, isReadOnly:Boolean, composition:Composition = null) 
		{
			this.view = view;
			this.composition = composition;

			fileRef = new FileReference();
			
			fileRef.addEventListener(Event.SELECT, onSelectLoad);
			fileRef.addEventListener(Event.CANCEL, onCancelLoad);
			fileRef.addEventListener(Event.COMPLETE, onCompleteLoad);
        	fileRef.addEventListener(IOErrorEvent.IO_ERROR, onErrorUpload);
        	fileRef.addEventListener(ProgressEvent.PROGRESS, onProgressLoad);
        	
        	init(billItem, isReadOnly);
		}
		
		public function init(billItem:BillItemDataObject, isReadOnly:Boolean):void 
		{
			model = new BillItemAttachmentsModel(billItem, isReadOnly);
			onClickUpload();
		}
		
		public function onClickUpload():void 
		{
			attachment = new BillItemAttachmentDataObject();
			
			fileRef.browse([new FileFilter("PDF Documents", "*.pdf")]);
		}

		public function onClickCancelUpload():void 
		{
			if (model.uploadingInProgress) {
				model.uploadingInProgress = false;
				fileRef.cancel();
				model.uploadingInProgress = false;
			}
			onClickClose();
		}
			
		public function onClickClose():void 
		{
			PopUpManager.removePopUp(view);
		}
		
		private function onSelectLoad(event:Event):void 
		{
            var requestURL:URLRequest = new URLRequest;
            requestURL.url = AppController.uploaderUrl;
            requestURL.method = "POST";

            var variables:URLVariables = new URLVariables();
            var uid:String = UIDUtil.createUID().toString() + "__";

            variables.UploadDir = AppController.BILL_ITEMS_STORAGE_DIRECTORY;
            variables.UniqueKey = uid;
            
            requestURL.data = variables;

			fileRef = FileReference(event.target);
			attachment.FileName = uid + fileRef.name;
			attachment.OriginalFileName = fileRef.name;
			attachment.BillItemId = model.currentBillItem.BillItemId;
			attachment.BillItemInfo = model.currentBillItem;
			model.currentBillItem.AttachmentInfoTemp = attachment;

			model.uploadingInProgress = true;
			fileRef.upload(requestURL);
		}

		private function onProgressLoad(event:ProgressEvent):void 
		{
			view.pbUpload.setProgress(event.bytesLoaded, event.bytesTotal);
		}

		private function onCancelLoad(event:Event):void 
		{
			onClickClose();
		}
			
		private function onCompleteLoad(event:Event):void 
		{
			model.uploadingInProgress = false;
			model.currentBillItem.AttachmentInfoTemp = attachment;
			
			if (null != composition) {
				composition.isChanged = true;
			}
			
			onClickClose();
		}
			
		private function onErrorUpload(event:IOErrorEvent):void 
		{
			model.uploadingInProgress = false;
			Alert.show(event.text, "Uploading Error");
		}
		
	}
}
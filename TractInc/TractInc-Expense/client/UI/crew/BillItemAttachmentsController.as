package UI.crew
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
	import mx.rpc.events.ResultEvent;
	import flash.events.Event;
	import flash.events.ProgressEvent;
	import flash.events.IOErrorEvent;
	import flash.net.navigateToURL;
	import UI.landman.Composition;
	
	[Bindable]
	{
		
		private static function onFault(event:FaultEvent):void 
		{
			Alert.show(event.fault.message);
		}
			
		private var fileRef:FileReference;
		
		private var attachment:BillItemAttachment;
		
		private var view:BillItemAttachmentsView;

		public var model:BillItemAttachmentsModel;
		
		private var composition:Composition = null;
		
		public function BillItemAttachmentsController(view:BillItemAttachmentsView, billItem:BillItem, isReadOnly:Boolean, composition:Composition = null) 
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
		
		public function init(billItem:BillItem, isReadOnly:Boolean):void 
		{
			model = new BillItemAttachmentsModel(billItem, isReadOnly);
			if (model.currentBillItem.RelatedBillItemAttachment.IsLoaded || model.currentBillItem.BillItemId == 0) {
				model.isLoaded = true;
			} else {
				model.currentBillItem.RelatedBillItemAttachment.addEventListener("loaded", onBillItemAttachmentsLoaded);
			}
		}
		
		public function onClickUpload():void 
		{
			attachment = new BillItemAttachment();
			
			fileRef.browse();
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
		
		private function onBillItemAttachmentsLoaded(event:DynamicLoadEvent):void 
		{
			model.currentBillItem.RelatedBillItemAttachment.removeEventListener("loaded", onBillItemAttachmentsLoaded);
			model.isLoaded = true;
		}
		
		private function onSelectLoad(event:Event):void 
		{
            var requestURL:URLRequest = new URLRequest;
            requestURL.url = BillItemAttachmentsController.uploaderUrl;
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

		private function onCancelLoad(event:Event):void 
		{
			onClickClose();
		}
			
		private function onCompleteLoad(event:Event):void 
		{
			model.uploadingInProgress = false;
			model.currentBillItem.RelatedBillItemAttachment.addItem(attachment);
			
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
/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI.ProcessManager.Upload
{
	import Domain.*;
	import Domain.Common.FtpConnectionInfo;

	import UI.ProcessManager.*;

	import mx.core.Application;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.RemoteObject;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.controls.Alert;
	import flash.utils.Timer;
	import flash.events.TimerEvent;
	import flash.net.URLRequest;
	import flash.net.URLVariables;
	import flash.net.FileReference;
	
	
	public class UploadController
	{
		[Bindable]
        public var Model:UploadModel;
        
        public var View:UploadView;
        public var Parent:ProcessManagerController;
        public var Service:RemoteObject;
        
        private var timer:Timer;
        private var receivingStatuses:Boolean = false;
        
		public function UploadController(view:UploadView, parent:ProcessManagerController):void
		{
			View = view;
			Parent = parent;
			Model = new UploadModel();
			
			Service = new RemoteObject(UploadModel.WEBORB_SERVICE_NAME);
			Service.StartUploadToFtp.addEventListener(ResultEvent.RESULT, OnStartUploadToFtp);
			Service.GetUploadStatuses.addEventListener(ResultEvent.RESULT, OnGetUploadStatuses);
			Service.addEventListener(FaultEvent.FAULT, OnFault);
			
            Application.application.addEventListener(AddUploadEvent.EVENT_ADD_UPLOAD, OnAddUpload);
			
			timer = new Timer(1000, 0);
            timer.addEventListener(TimerEvent.TIMER, GetStatuses);
			
			InitUploaderUrl();
		}

		public function OnFault(event:FaultEvent):void 
		{
			Alert.show(event.fault.faultString, "Fault");
		}

		public function CloseUpload(item:UploadItemInfo):void 
		{
            var asyncToken:AsyncToken = Service.AbortUpload(item.Status.ProcessId);
            asyncToken.addResponder (new Responder(
                function(event:ResultEvent):void {
					RemoveItem(item);
                }, 
				function(event:FaultEvent):void {
					RemoveItem(item);
				}
            ));
		}
		
		public function RemoveItem(item:UploadItemInfo):void
		{
			var index:int = Model.Items.getItemIndex(item);
			
			if (index != -1){
				Model.Items.removeItemAt(index);
			}
		}

		public function InitUploaderUrl():void
		{
            var asyncToken:AsyncToken = Service.GetFileUploaderURL();
            asyncToken.addResponder (new Responder(
                function(event:ResultEvent):void {
					Model.UploaderUrl = String(event.result);
                }, 
				function(event:FaultEvent):void {
					Alert.show("Cannot get uploader URL.\n" + event.fault.faultString, "Error");
				}
            ));
		}

		public function StartUploadToFtp(item:UploadItemInfo):void	
		{
			item.ErrorMessage = null;
			item.Status.ProcessedBytes = 0;
			item.Status.State = ProcessStatus.LOAD_COMPLETED;
		
			Service.StartUploadToFtp(item.Status.ProcessId, item.File.Name, item.ConnectionInfo);
		}

		private function OnAddUpload(event:AddUploadEvent):void 
		{
			if (Model.UploaderUrl == null) {
				return;
			} else {
				AddUploadProcess(event.fileRef, event.connectionInfo);
			}
		}

        private function AddUploadProcess(fileRef:FileReference, connectionInfo:FtpConnectionInfo):void 
        {

	        var uploadItem:UploadItemInfo = new UploadItemInfo(fileRef, connectionInfo);
			
			uploadItem.Status.State = ProcessStatus.INIT;
			uploadItem.addEventListener(UploadEvent.COMPLETE_LOAD_PROCESS, OnCompleteLoading);
			uploadItem.addEventListener(UploadEvent.ERROR_LOAD_PROCESS, OnErrorLoading);

            Model.Items.addItem(uploadItem);
            
            var requestURL:URLRequest = new URLRequest;
            requestURL.url = Model.UploaderUrl;
            requestURL.method = "POST";

            var variables:URLVariables = new URLVariables();
            variables.UploadDirUid = uploadItem.Status.ProcessId;
            requestURL.data = variables;
                        
            try {
            	uploadItem.Status.State = ProcessStatus.LOADING;
                uploadItem.StartLoad(requestURL);
            } catch (error:Error) {
                Alert.show("Unable to upload file: " + error.message);
                RemoveItem(uploadItem);
            }                   
        }

		private function OnCompleteLoading(event:UploadEvent):void
		{
			var item:UploadItemInfo = event.Item;
			item.Status.State = ProcessStatus.LOAD_COMPLETED;
			
			StartUploadToFtp(item);
		}

		private function OnErrorLoading(event:UploadEvent):void
		{
			var item:UploadItemInfo = event.Item;
			item.Status.State = ProcessStatus.LOADING_ERROR;
		}

		private function OnStartUploadToFtp(event:ResultEvent):void
		{
			var item:UploadItemInfo = GetItemById(event.result as String);
			if (item != null){
				item.Status.State = ProcessStatus.UPLOADING;
			}
			if (!timer.running){
				timer.start();
			}
		}

		private function GetStatuses(event:TimerEvent):void
		{
			if (receivingStatuses){
				return;
			}
			
			timer.stop();
			
			var arr:Array = new Array();
			
			for each (var item:UploadItemInfo in Model.Items){
				if (item.Status.State == ProcessStatus.UPLOADING) {
					arr.push(item.Status.ProcessId);
				}
			}
			
			if (arr.length > 0){
				Service.GetUploadStatuses(arr);
				receivingStatuses = true;
			}
		}

		private function OnGetUploadStatuses(event:ResultEvent):void
		{
			receivingStatuses = false;
			
			var arr:Array = event.result as Array;
			
			for each (var status:ProcessStatus in arr){
				var item:UploadItemInfo = GetItemById(status.ProcessId);
				if (item != null) {
					item.Status = status;
					if (status.ExceptionMessage != null){
						item.ErrorMessage = status.ExceptionMessage;
					}
					if (item.Status.State == ProcessStatus.UPLOAD_COMPLETED){
						Application.application.dispatchEvent(new CommitUploadEvent(item.ConnectionInfo));
						CloseUpload(item);
					}
				}
			}
			
			timer.start();
		}

		private function GetItemById(uploadId:String):UploadItemInfo
		{
			var result:UploadItemInfo = null;
			
			for each (var item:UploadItemInfo in Model.Items) {
				if (item.Status.ProcessId == uploadId){
					result = item;
					break;
				}
			}
			
			return result;
		}

	}

}
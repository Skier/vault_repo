/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI.ProcessManager.Download
{
	import Domain.AddDownloadEvent;

	import UI.ProcessManager.*;

	import mx.core.Application;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.remoting.RemoteObject;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.controls.Alert;
	import flash.utils.Timer;
	import flash.events.TimerEvent;
	
	

	public class DownloadController
	{
		[Bindable]
        public var Model:DownloadModel;

        public var View:DownloadView;
        public var Parent:ProcessManagerController;
        public var Service:RemoteObject;
        
        private var timer:Timer;
        private var receivingStatuses:Boolean = false;
        
		public function DownloadController(view:DownloadView, parent:ProcessManagerController):void
		{
			View = view;
			Parent = parent;
			Model = new DownloadModel();
			
			Service = new RemoteObject(DownloadModel.WEBORB_SERVICE_NAME);
			Service.GetDownloadStatuses.addEventListener(ResultEvent.RESULT, OnGetDownloadStatuses);
			Service.addEventListener(FaultEvent.FAULT, OnFault);

            Application.application.addEventListener(AddDownloadEvent.EVENT_ADD_DOWNLOAD, AddDownloadProcess);
            
   			timer = new Timer(1000, 0);
            timer.addEventListener(TimerEvent.TIMER, GetStatuses);

		}

		public function CloseDownload(item:DownloadItemInfo):void 
		{
            var asyncToken:AsyncToken = Service.AbortDownload(item.Status.ProcessId);
            asyncToken.addResponder (new Responder(
                function(event:ResultEvent):void{
					RemoveItem(item);
                }, 
                function(event:FaultEvent):void{
					RemoveItem(item);
                }
            ));
			Service.AbortDownload(item.Status.ProcessId);
		}
		
        public function RemoveItem(item:DownloadItemInfo):void
        {
			var index:int = Model.Items.getItemIndex(item);
			if (index != -1){
				Model.Items.removeItemAt(index);
			}
        }
        
        private function AddDownloadProcess(event:AddDownloadEvent):void 
        {

	        var item:DownloadItemInfo = new DownloadItemInfo(event.ftpFile, event.connectionInfo);
			item.addEventListener(DownloadEvent.ERROR_LOAD_PROCESS, OnErrorLoading); 

            Model.Items.addItem(item);
            
            StartDownload(item);
        }

		private function StartDownload(item:DownloadItemInfo):void 
		{
            var asyncToken:AsyncToken = Service.StartDownload(item.File, item.ConnectionInfo);
            asyncToken.addResponder (new Responder(
                function(event:ResultEvent):void{
					item.Status.State = ProcessStatus.DOWNLOADING;
					item.Status.ProcessId = String(event.result);
					
					if (!timer.running){
						timer.start();
					}
                }, 
                function(event:FaultEvent):void{
                }
            ));
		}

		private function GetStatuses(event:TimerEvent):void
		{
			if (receivingStatuses){
				return;
			}
			
			timer.stop();
			
			var arr:Array = new Array();
			
			for each (var item:DownloadItemInfo in Model.Items){
				if ( item.Status.State == ProcessStatus.DOWNLOADING
					|| item.Status.State == ProcessStatus.COMPRESSING
					|| item.Status.State == ProcessStatus.CHECKING_DIRECTORY_SIZE ) {
					arr.push(item.Status.ProcessId);
				}
			}
			
			if (arr.length > 0){
				Service.GetDownloadStatuses(arr);
				receivingStatuses = true;
			}
		}
		
		private function OnGetDownloadStatuses(event:ResultEvent):void
		{
			receivingStatuses = false;
			var arr:Array = event.result as Array;
			
			for each (var status:ProcessStatus in arr){
				var item:DownloadItemInfo = GetItemById(status.ProcessId);
				if (item != null) {
					item.Status = status;
					if (status.ExceptionMessage != null){
						item.ErrorMessage = status.ExceptionMessage;
					} else if (item.Status.State == ProcessStatus.DOWNLOAD_COMPLETED) {
						GetFileUrl(item);
					} else if (item.Status.State == ProcessStatus.TERMINATED) {
						CloseDownload(item);
					}
				}
			}
			
			timer.start();
		}

		private function GetFileUrl(item:DownloadItemInfo):void 
		{
            var asyncToken:AsyncToken = Service.GetFileUrl(item.Status.ProcessId);
            asyncToken.addResponder (new Responder(
                function(event:ResultEvent):void{
					item.FileUrl = String(event.result);
					item.Status.ProcessedBytes = 0;
					item.Status.State = ProcessStatus.URL_RETRIEVED;
                }, 
                function(event:FaultEvent):void{
                }
            ));
		}

		private function OnErrorLoading(event:DownloadEvent):void
		{
			var item:DownloadItemInfo = event.Item;
			item.ErrorMessage = ProcessStatus.LOADING_ERROR;
		}

		public function OnFault(event:FaultEvent):void 
		{
			Alert.show(event.fault.faultString, "Fault");
		}

		private function GetItemById(downloadId:String):DownloadItemInfo
		{
			var result:DownloadItemInfo = null;
			for each (var item:DownloadItemInfo in Model.Items) {
				if (item.Status.ProcessId == downloadId){
					result = item;
					break;
				}
			}
			
			return result;
		}

	}

}
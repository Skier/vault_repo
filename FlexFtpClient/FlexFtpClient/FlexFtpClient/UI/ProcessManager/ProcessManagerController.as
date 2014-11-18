/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI.ProcessManager
{
	import Domain.*;
	import Domain.Common.*;

	import UI.*;

	import mx.core.Application;
	import mx.core.UIComponent;
	import mx.events.DragEvent;
	import mx.controls.DataGrid;
	import mx.managers.DragManager;
	
	public class ProcessManagerController
	{
		public var View:ProcessManagerView;
		public var Parent:AppController;
		
		public function ProcessManagerController(view:ProcessManagerView, parent:AppController):void
		{
			View = view;
			Parent = parent;
		}
		
		public function OnCreationComplete():void
		{
			View.selectedChild = View.upload;
        	Application.application.addEventListener(AddUploadEvent.EVENT_ADD_UPLOAD, SelectUpload);
        	Application.application.addEventListener(AddDownloadEvent.EVENT_ADD_DOWNLOAD, SelectDownload);
		}

		public function SelectUpload(event:AddUploadEvent):void
		{
			if (View.selectedChild != View.upload){
				View.selectedChild = View.upload;
			}
		}
		
		public function SelectDownload(event:AddDownloadEvent):void
		{
			if (View.selectedChild != View.download){
				View.selectedChild = View.download;
			}
		}
		
        public function OnDragEnter(event:DragEvent):void
        {
            if (event.dragInitiator is DataGrid) {
            	var dg:DataGrid = event.dragInitiator as DataGrid;
            	
            	if (dg.selectedItems[0] is FtpFile || dg.selectedItems[0] is FtpDirectory) {
            		View.selectedChild = View.download;
                	DragManager.acceptDragDrop(event.currentTarget as UIComponent);
            	}
            }
        }

        public function OnDragDrop(ev:DragEvent):void
        {
        	var dg:DataGrid = ev.dragInitiator as DataGrid;
        	var connectionInfo:FtpConnectionInfo = Parent.GetCurrentConnection();
        	
        	for each (var file:FtpFile in dg.selectedItems) {
	        	var event:AddDownloadEvent = new AddDownloadEvent(file, connectionInfo);
	        	Application.application.dispatchEvent(event);
        	}
        }  

	}
}
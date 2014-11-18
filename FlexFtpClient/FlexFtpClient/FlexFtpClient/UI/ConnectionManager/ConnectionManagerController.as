/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI.ConnectionManager
{
	import Domain.*;
	import Domain.Common.FtpConnectionInfo;

	import UI.*;
	import UI.ConnectionManager.FtpConnection.*;

	import mx.core.Application;
	import mx.rpc.events.FaultEvent;
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import flash.net.SharedObject;
	
	public class ConnectionManagerController
	{
		public var View:ConnectionManagerView;
		public var Model:ConnectionManagerModel;
		public var Parent:AppController;
		
		public function ConnectionManagerController(view:ConnectionManagerView, parent:AppController):void
		{
			View = view;
			Parent = parent;
			Model = new ConnectionManagerModel();
			
        	Application.application.addEventListener(AddConnectionEvent.EVENT_ADD_CONNECTION, AddConnection);
        	Application.application.addEventListener(DisconnectEvent.EVENT_DISCONNECT, RemoveConnection);
		}
		
		public function SetCurrentFtpConnection():void
		{
			Model.CurrentFtpConnectionModel = 
				Model.FtpConnectionModels.getItemAt(View.tnConnections.selectedIndex) as FtpConnectionModel;
		}
        
        public function OnCreationComplete():void
        {
			var ftpConnections:ArrayCollection = new ArrayCollection();
			
			if (Model.sharedObject.data.ftpConnections != null){
				for each (var ftpConnection:FtpConnectionInfo in Model.sharedObject.data.ftpConnections){
					ftpConnection.CurrentDir = "";
					
					var ftpConnectionModel:FtpConnectionModel = new FtpConnectionModel(ftpConnection);
					Model.FtpConnectionModels.addItem(ftpConnectionModel);
					
					var newTabBar:FtpConnectionView = new FtpConnectionView();
					newTabBar.ParentController = this;
					newTabBar.Controller.Init( ftpConnectionModel );
					View.tnConnections.addChild(newTabBar);
					View.tnConnections.selectedChild = newTabBar;
					
					Model.CurrentFtpConnectionModel = ftpConnectionModel;
				}
			}
        }
        
        public function OnFault(event:FaultEvent):void 
        {
        	Alert.show(event.fault.faultString, "Fault");
        }
        
		private function BackupModel():void
		{
			var ftpConnections:ArrayCollection = new ArrayCollection();
			
			for each (var connectionModel:FtpConnectionModel in Model.FtpConnectionModels){
				ftpConnections.addItem(connectionModel.ConnectionInfo);
			}
			
			Model.sharedObject.data.ftpConnections = ftpConnections;
			Model.sharedObject.flush();
		}

		private function AddConnection(event:AddConnectionEvent):void
		{
			var connectionModel:FtpConnectionModel = new FtpConnectionModel(event.connectionInfo);
			
			Model.FtpConnectionModels.addItem(connectionModel);
			Model.CurrentFtpConnectionModel = connectionModel;
            
            BackupModel();

			var newTabBar:FtpConnectionView = new FtpConnectionView;
			newTabBar.ParentController = this;
			newTabBar.Controller.Init(connectionModel);
			
			View.tnConnections.addChild(newTabBar);
			View.tnConnections.selectedChild = newTabBar;
		}
		
		private function RemoveConnection(event:DisconnectEvent):void
		{
			View.tnConnections.removeChild(event.ftpView);
			
			var modelIndex:int = Model.FtpConnectionModels.getItemIndex(event.ftpView.Controller.Model);
			
			if(modelIndex > -1){
				Model.FtpConnectionModels.removeItemAt(modelIndex);
			}
			else {
				Model.FtpConnectionModels = null;
			}

            BackupModel();
		}
		
	}
}
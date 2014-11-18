/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI
{
	import Domain.Common.FtpConnectionInfo;
	import Domain.*;

	import UI.Login.LoginView;

	import mx.core.Application;
	import mx.collections.ArrayCollection;
	import mx.rpc.remoting.RemoteObject;
	import mx.managers.PopUpManager;
	import flash.net.SharedObject;
	import flash.ui.ContextMenu;
	import mx.events.ValidationResultEvent;
	
	public class AppController
	{
		private var contextMenu:ContextMenu;
		
		[Bindable]
		public var Model:AppModel;

		public var View:AppView;
		
		public function AppController(view:AppView):void 
		{
			Model = new AppModel();
			View = view;
			contextMenu = new ContextMenu();
            contextMenu.hideBuiltInItems();
            Application.application.contextMenu = contextMenu;
		}

		public function Login():void
		{
			LoginView(PopUpManager.createPopUp(View, LoginView, true));
		}

		public function QuickConnect():void
		{
        	var vResult:ValidationResultEvent = View.checkHost.validate();
            if (vResult.type==ValidationResultEvent.INVALID) {
                return;
            }
			
            var connection:FtpConnectionInfo = new FtpConnectionInfo();
            connection.Host = View.hostname.text;
            connection.User = View.username.text;
            connection.Password = View.password.text;
            connection.CurrentDir = "";
        	
        	Application.application.dispatchEvent(new AddConnectionEvent(connection));
		}

		public function DoAnonym():void 
		{
			if (View.isAnonymous.selected) {
				View.username.text = AppModel.ANONYMOUS_USER;
				View.password.text = AppModel.ANONYMOUS_PASSWORD;
			} else {
				View.username.text = View.password.text = "";
			}
		}
		
		public function GetCurrentConnection():FtpConnectionInfo {
			return View.cManager.Controller.Model.CurrentFtpConnectionModel.ConnectionInfo;
		}
	
	}

}
/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI.Login
{
	import Domain.Common.FtpConnectionInfo;
	import Domain.AddConnectionEvent;

	import UI.AppModel;

	import mx.core.Application;
	import mx.events.ValidationResultEvent;
	import mx.managers.PopUpManager;
	
	public class LoginController
	{
		public var View:LoginView;
		public var Model:LoginModel;
		
		public function LoginController(view:LoginView):void
		{
			View = view;
			Model = new LoginModel();
		}

        public function Init():void 
        {
            PopUpManager.centerPopUp(View);
        }

        public function CloseWin():void 
        {
            PopUpManager.removePopUp(View);
        }
        
        public function DoConnect():void 
        {
        	var vResult:ValidationResultEvent = View.checkHost.validate();
            if (vResult.type==ValidationResultEvent.INVALID) {
                return;
            }
            Model.connectionInfo = new FtpConnectionInfo();
            Model.connectionInfo.Host = View.hostname.text;
            Model.connectionInfo.User = View.username.text;
            Model.connectionInfo.Password = View.password.text;
            Model.connectionInfo.CurrentDir = "";

        	Application.application.dispatchEvent(new AddConnectionEvent(Model.connectionInfo));

            PopUpManager.removePopUp(View);
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
	}
}
package App.Login
{
	import App.Domain.User;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.controls.Alert;
	import mx.collections.ArrayCollection;
	import mx.rpc.events.ResultEvent;
	import mx.events.ValidationResultEvent;
	import mx.rpc.remoting.RemoteObject;
	import mx.events.ResizeEvent;
	import App.AppController;
	import mx.managers.CursorManager;
	
	
	[Bindable]
	public class LoginController
	{
		private var userService:RemoteObject;

		public var view:LoginView;
		public var model:LoginModel = new LoginModel();
		
		public var Parent:AppController;
		
		public function LoginController(view:LoginView, parent:AppController):void {
			this.view = view;
			Parent = parent;

            userService = new RemoteObject( "GenericDestination" );
            userService.source = "TractInc.Lease.UserService";
            userService.Login.addEventListener(ResultEvent.RESULT, onLoginOk);
            userService.Login.addEventListener(FaultEvent.FAULT, onFault);
			userService.RestorePassword.addEventListener(ResultEvent.RESULT, onRestorePassword);
            userService.RestorePassword.addEventListener(FaultEvent.FAULT, onFault);
		}

		public function AutoLogin(userName:String, userPassword:String):void {
			model.user.Login = userName;
			model.user.Password = userPassword;
            DoLogin();		    
		}
		
		public function DoLogin():void {

			if (!isFormValid) {
				return;
			}

			model.user.Login = view.username.text;
			model.user.Password = view.password.text;
			
			view.content.enabled = false;
			CursorManager.setBusyCursor();
			userService.Login(model.user.Login, model.user.Password);
			
		}
		
		public function RestorePassword():void {
			view.content.enabled = false;
			userService.RestorePassword(view.username.text);
		}

		public function SwitchToRecovery():void {

			view.currentState = "passwordRecovery";
			view.msgLabel.text = "";
		}
		
		private function onRestorePassword(event:ResultEvent):void {
			
			view.content.enabled = true;

			var exists:Boolean = event.result as Boolean;

			if ( exists ) {
				view.currentState = "login";
				view.msgLabel.text = "Password was sent to your email";
			} else {
				view.msgLabel.text = "User with current login not found";
			}
		}
		
		private function onLoginOk(event:ResultEvent):void {
			CursorManager.removeBusyCursor();
			view.content.enabled = true;
		    Parent.OnUserLoggedIn(User(event.result));
		}
		
 		private function get isFormValid():Boolean {

 			var result:Boolean = true;
 			
 			if (view.usernameV.validate().type == ValidationResultEvent.INVALID) {
 				result = false;
 			}

 			if (view.passwordV.validate().type == ValidationResultEvent.INVALID) {
 				result = false;
 			}

			return result;

 		}
		
		private function onFault(event:FaultEvent):void {
			CursorManager.removeBusyCursor();
			view.content.enabled = true;
			Alert.show(event.fault.faultString);
			Parent.showLogin();
		}

	}
}
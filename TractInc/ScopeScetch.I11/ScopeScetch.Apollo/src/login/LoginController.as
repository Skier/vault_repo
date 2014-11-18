package src.login
{
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import mx.events.ResizeEvent;
	import mx.events.ValidationResultEvent;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	import src.AppController;
	import src.common.UserTractRegistry;
	import src.deedplotter.domain.User;
	
	
	[Bindable]
	public class LoginController
	{

		public function LoginController(view:LoginView, parent:AppController):void 
		{
			this.view = view;
			appController = parent;

            userService = new RemoteObject( "GenericDestination" );
            userService.source = "TractInc.ScopeScetch.UserService";
            userService.addEventListener(FaultEvent.FAULT, onFaultHandler);
            userService.Login.addEventListener(ResultEvent.RESULT, login_onResultHandler);
			userService.RestorePassword.addEventListener(ResultEvent.RESULT, restorePassword_onResultHandler);
		}

		public var view:LoginView;
		public var model:LoginModel = new LoginModel();
		public var appController:AppController;

		private var userService:RemoteObject;
		
		public function autoLogin(userName:String, userPassword:String):void 
		{
			model.user.Login = userName;
			model.user.Password = userPassword;
            doLogin();		    
		}
		
		public function doLogin():void 
		{
			if (!isFormValid())
				return;

			model.user.Login = view.username.text;
			model.user.Password = view.password.text;
			
			if (appController.IsOnline) 
			{
				view.content.enabled = false;
				userService.Login(model.user.Login, model.user.Password);
			} 
			else if (UserTractRegistry.hasUser(model.user.Login)) 
			{
				appController.InitStorage(model.user.Login);

				if (model.attempts == 0) {
					appController.setCurrentUserInactive();
				}

				if ( !appController.CurrentUser.IsActive) {
					Alert.show("User is inactive. Please connect to Internet and try again");
					return;
				}

				if (appController.CurrentUser.Password == model.user.Password) {
					appController.OnUserLoggedIn(appController.CurrentUser);
					model.attempts = 5;
				} else {
					Alert.show("Password is incorrect");
					model.attempts--;
				}
			} 
			else 
			{
			    Alert.show("Selected user is not found on this computer. Please connect to Internet and try again");
			}
		}
		
		public function restorePassword():void 
		{
			view.content.enabled = false;
			userService.RestorePassword(view.username.text);
		}

		public function switchToRecovery():void 
		{
			if (!appController.IsOnline)
				return;

			view.currentState = "passwordRecovery";
			view.msgLabel.text = "";
		}

 		private function isFormValid():Boolean 
 		{
 			var result:Boolean = true;
 			
 			if (view.usernameV.validate().type == ValidationResultEvent.INVALID)
 				result = false;

 			if (view.passwordV.validate().type == ValidationResultEvent.INVALID)
 				result = false;

			return result;
 		}

		private function restorePassword_onResultHandler(event:ResultEvent):void 
		{
			view.content.enabled = true;

			var exists:Boolean = event.result as Boolean;

			if ( exists )
			{
				view.currentState = "login";
				view.msgLabel.text = "Password was sent to your email";
			} 
			else 
			{
				view.msgLabel.text = "User with current login not found";
			}
		}
		
		private function login_onResultHandler(event:ResultEvent):void 
		{
			view.content.enabled = true;
		    appController.OnUserLoggedIn(User(event.result));
		}
		
		private function onFaultHandler(event:FaultEvent):void 
		{
			view.content.enabled = true;
			Alert.show(event.fault.faultString);
			appController.Logout();
		}

	}
}
package truetract.web.login
{
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.InvokeEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;

    import truetract.web.AppController;
    import mx.controls.Alert;
    import truetract.domain.User;
    import truetract.web.signUp.SignUpView;
    import truetract.web.services.TrueTractService;
    import mx.rpc.AsyncToken;
    import mx.rpc.Responder;
    import truetract.web.services.UserService;

	[Bindable]
	public class LoginController
	{
		public function LoginController(view:LoginView, appController:AppController):void 
		{
            this.view = view;
            this.appController = appController;
		}

		public var view:LoginView;

		public var appController:AppController;

        public var serviceIsBusy:Boolean = false;

		private var userService:UserService = UserService.getInstance();
		
		public function doSignUp():void 
		{
		    SignUpView.open(view, true);
		}

		public function autoLogin(userId:int):void 
		{
			var asyncToken:AsyncToken = userService.loginById(userId);
			asyncToken.addResponder(new Responder(login_onResultHandler, onFaultHandler));
		}

/*
		public function restorePassword():void 
		{
			if (!view.restoreFormValidator.validate(true))
				return;

			var asyncToken:AsyncToken = userService.sendPassword(view.username.text);
			asyncToken.addResponder(new Responder(sendPassword_onResultHandler, onFaultHandler));
		}
*/
		private function login_onResultHandler(event:ResultEvent):void 
		{
			serviceIsBusy = false;

		    appController.logIn(User(event.result));
		}
/*
		private function sendPassword_onResultHandler(event:ResultEvent):void 
		{
			serviceIsBusy = false;

			var isSucces:Boolean = event.result as Boolean;

			if (isSucces)
			{
				view.currentState = "login";
				view.msgLabel.text = "Password was sent to your email";
			} 
			else 
			{
				view.msgLabel.text = "User with current login not found";
			}
		}
*/		
		private function onFaultHandler(event:FaultEvent):void 
		{
		    Alert.show(event.fault.faultString);
		}
	}
}
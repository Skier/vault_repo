package truetract.web.login
{
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.InvokeEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;

    import truetract.web.AppController;
    import mx.controls.Alert;
    import truetract.plotter.domain.User;
    import truetract.web.signUp.SignUpView;

	[Bindable]
	public class LoginController
	{
		public function LoginController(view:LoginView, appController:AppController):void 
		{
            this.view = view;
            this.appController = appController;

            trueTractService = new RemoteObject( "GenericDestination" );
			trueTractService.showBusyCursor = true;
            trueTractService.source = "TractInc.TrueTract.TrueTractService";

            trueTractService.addEventListener(FaultEvent.FAULT, service_onFaultHandler);

            trueTractService.Login.addEventListener(ResultEvent.RESULT, login_onResultHandler);
            
			trueTractService.SendPassword.addEventListener(ResultEvent.RESULT, 
			    restorePassword_onResultHandler);
			    
			trueTractService.addEventListener(InvokeEvent.INVOKE, 
			    function(event:InvokeEvent):void { serviceIsBusy = true });
		}

		public var view:LoginView;

		public var appController:AppController;

        public var serviceIsBusy:Boolean = false;

		private var trueTractService:RemoteObject;
		
		public function autoLogin(userName:String, userPassword:String):void 
		{
			view.username.text = userName;
			view.password.text = userPassword;

            doLogin();		    
		}

		public function doSignUp():void 
		{
		    SignUpView.open(view, true);
		}
		
		public function doLogin():void 
		{
			if (!view.loginFormValidator.validate(true))
				return;

			var userLogin:String = view.username.text;
			var userPassword:String = view.password.text;

			trueTractService.Login(userLogin, userPassword);
		}
		
		public function restorePassword():void 
		{
			if (!view.restoreFormValidator.validate(true))
				return;

			trueTractService.SendPassword(view.username.text);
		}

		private function login_onResultHandler(event:ResultEvent):void 
		{
			serviceIsBusy = false;

		    appController.logIn(User(event.result));
		}

		private function restorePassword_onResultHandler(event:ResultEvent):void 
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

		private function service_onFaultHandler(event:FaultEvent):void 
		{
			serviceIsBusy = false;

			Alert.show(event.fault.faultString);
		}
	}
}
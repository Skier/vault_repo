package UI.login
{
    import flash.events.EventDispatcher;

	import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.InvokeEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;
    import mx.controls.Alert;
    
    import common.events.LoginEvent;
    import common.events.LoginFaultEvent;
    
    import UI.AppController;
    import TractInc.Domain.User;
    import TractInc.Domain.Person;
	import TractInc.Domain.storage.ITractStorage;
	import TractInc.Domain.storage.RemoteStorage;

	[Bindable]
	public class LoginController extends EventDispatcher
	{
        public var serviceIsBusy:Boolean = false;
		public var view:LoginView = null;
		public var signUpView:SignUpView = null;
		
		public function LoginController(view:LoginView):void 
		{
            this.view = view;
		}

		public function autoLogin(userName:String, userPassword:String):void 
		{
			view.username.text = userName;
			view.password.text = userPassword;
            doLogin();		    
		}

		public function doLogin():void 
		{
			if (!view.loginFormValidator.validate(true))
				return;

			var userLogin:String = view.username.text;
			var userPassword:String = view.password.text;

            var responder:Responder = new Responder(login_onResultHandler, login_onFaultHandler);
            var storage:ITractStorage = RemoteStorage.instance;
            storage.login(userLogin, userPassword, responder);
		}

		public function doSignUpView():void 
		{
		    signUpView = SignUpView.open(view, true);
		    signUpView.setController(this);
		}

		public function doSignUp(person:Person, login:String, password:String):void 
		{
            var responder:Responder = new Responder(signup_onResultHandler, signup_onFaultHandler);
            var storage:ITractStorage = RemoteStorage.instance;
            storage.signup(person, login, password, responder);
		}

		public function restorePassword():void 
		{
			if (!view.restoreFormValidator.validate(true)) {
				return;
			}

            var responder:Responder = new Responder(restorePassword_onResultHandler, restorePassword_onFaultHandler);
            var storage:ITractStorage = RemoteStorage.instance;
            storage.restorePassword(view.username.text, responder);
		}
		
		private function login_onResultHandler(event:ResultEvent):void 
		{
			serviceIsBusy = false;
			view.dispatchEvent(new LoginEvent(LoginEvent.LOGIN_COMPLETE, event.result as User));
		}
		
		private function login_onFaultHandler(event:FaultEvent):void 
		{
			serviceIsBusy = false;
			view.dispatchEvent(new LoginFaultEvent(LoginFaultEvent.LOGIN_FAULT, event.fault));
		}

		private function signup_onResultHandler(event:ResultEvent):void 
		{
			serviceIsBusy = false;
            Alert.show("Sign Up succeded. Now login");
            signUpView.close();
		}
		
		private function signup_onFaultHandler(event:FaultEvent):void 
		{
			serviceIsBusy = false;
            Alert.show(event.fault.faultString);
		}
		
		private function restorePassword_onResultHandler(event:ResultEvent):void 
		{
			serviceIsBusy = false;
			var isSucces:Boolean = event.result as Boolean;

			if (isSucces) {
				view.loginMode = true;
				Alert.show("Password was sent to your email");
			} else {
				Alert.show("User with current login not found");
			}
		}
		
		private function restorePassword_onFaultHandler(event:FaultEvent):void 
		{
			serviceIsBusy = false;
			Alert.show(event.fault.faultString);
		}
	}
}
package App.Login
{

    import mx.rpc.events.FaultEvent;
    import mx.rpc.AsyncToken;
    import mx.rpc.Responder;
    import mx.controls.Alert;
    import mx.collections.ArrayCollection;
    import mx.rpc.events.ResultEvent;
    import mx.events.ValidationResultEvent;
    import mx.rpc.remoting.RemoteObject;
    import mx.events.ResizeEvent;
    import mx.binding.utils.BindingUtils;
    import mx.binding.utils.ChangeWatcher;
    import mx.events.PropertyChangeEvent;
    import App.Entity.UserDataObject;
    
    [Bindable]
    public class LoginController
    {
        private var userService:RemoteObject;

        public var view:LoginView;
        public var model:LoginModel = new LoginModel();
        
        public var mainApp:AppController;
        
        public function LoginController(view:LoginView, parent:AppController):void {
            this.view = view;
            mainApp = parent;

            userService = new RemoteObject( "GenericDestination" );
            userService.source = "TractInc.Expense.UserService";
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
            
            view.enabled = false;
            userService.Login(model.user.Login, model.user.Password);
        }
        
        public function RestorePassword():void {
            view.enabled = false;
            userService.RestorePassword(view.username.text);
        }

        public function SwitchToRecovery():void {
            view.currentState = "passwordRecovery";
        }
        
        private function onRestorePassword(event:ResultEvent):void {
            view.enabled = true;

            var exists:Boolean = event.result as Boolean;

            if ( exists ) {
                view.currentState = "login";
                Alert.show("Password was sent to your email");
            } else {
                Alert.show("User with current login not found");
            }
        }
        
        private function onLoginOk(event:ResultEvent):void {
           	mainApp.OnUserLoggedIn(UserDataObject(event.result).UserId);
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
            view.enabled = true;
            Alert.show(event.fault.faultString);
        }

    }

}

package UI.Login
{
    import Domain.Account;
    
    import UI.AppController;
    import UI.Registration.RegistrationView;
    
    import flash.display.DisplayObject;
    import flash.net.SharedObject;
    
    import mx.controls.Alert;
    import mx.events.ResizeEvent;
    import mx.events.ValidationResultEvent;
    import mx.managers.PopUpManager;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;
    
    public class LoginController
    {
        public var Parent:AppController;
        public var View:LoginView;
        
        private var m_sharedObject:SharedObject;
        private var m_remoteObject:RemoteObject;
        
        public function LoginController(view:LoginView, parentController:AppController):void {
            this.View = view;
            this.Parent = parentController;
            
            m_remoteObject = new RemoteObject("FlexMailServer");
            m_remoteObject.addEventListener(ResultEvent.RESULT, OnLoginReceipt);
            m_remoteObject.addEventListener(FaultEvent.FAULT, OnLoginFault);
            m_remoteObject.showBusyCursor = true;
            
            m_sharedObject = SharedObject.getLocal("FlexMailSO");
        }
        
        public function OnCreated():void
        {
             if (m_sharedObject.data.lastEmail != null)
                View.emailTxt.text = String(m_sharedObject.data.lastEmail);
        }
        
        public function OnLogin():void 
        {
            if (!IsFormValid())
                return;

            var email:String = View.emailTxt.text;
            var password:String = View.pwdTxt.text;
            
            m_remoteObject.Login(email, password);
        }

        public function OnRegister():void 
        {
            Parent.OnShowRegistration();
        }
        
        private function IsFormValid():Boolean 
        {
            return View.emailValidator.validate().type == ValidationResultEvent.VALID;
        }
        
        private function OnLoginReceipt(event:ResultEvent):void 
        {
            Parent.Login(Account(event.result));
            RememberCurrentLogin();
        }
        
        private function OnLoginFault(event:FaultEvent):void
        {
            Alert.show(event.fault.faultString);
        }
        
        private function RememberCurrentLogin():void 
        {
            m_sharedObject.data.lastEmail = View.emailTxt.text;
            m_sharedObject.flush();
        }
        
    }
}
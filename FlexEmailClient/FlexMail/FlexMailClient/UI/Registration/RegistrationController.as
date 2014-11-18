package UI.Registration
{
    import Domain.Account;
    import Domain.ServerSettings;
    
    import UI.AppController;
    
    import mx.controls.Alert;
    import mx.events.ValidationResultEvent;
    import mx.managers.PopUpManager;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;
    import flash.events.FocusEvent;
    
    public class RegistrationController
    {
        private var View:RegistrationView;
        
        private var m_remoteObject:RemoteObject;
        
        private var m_predefinedSettings:Array = [
            {Domain: "gmail.com", AuthRequired: true, Ssl:true,
                SmtpHost: "smtp.gmail.com", SmtpPort: 587, Pop3Host: "pop.gmail.com", Pop3Port: 995},
            {Domain: "yahoo.com", AuthRequired: true, Ssl:false,
                SmtpHost: "smtp.mail.yahoo.com", SmtpPort: 587, Pop3Host: "pop.mail.yahoo.com", Pop3Port: 110}]
        
        public function RegistrationController(view:RegistrationView)
        {
            this.View = view;

            m_remoteObject = new RemoteObject(AppController.FLEX_MAIL_SERVER_OBJECT_NAME);
            m_remoteObject.addEventListener(ResultEvent.RESULT, OnRegistered);
            m_remoteObject.addEventListener(FaultEvent.FAULT, OnFault);
            m_remoteObject.showBusyCursor = true;
        }
        
        public function OnCreated():void
        {
            PopUpManager.centerPopUp(View);
        }
        
        public function OnSave():void 
        {
            if (!IsFormValid())
            {
                Alert.show("Some of required fields are not filled or have wrong values.");
                return;
            }
                                                
            var account:Account = new Account();
            account.Pop3Settings = new ServerSettings();
            account.SmtpSettings = new ServerSettings();
                            
            account.Email = View.userEmail.text;                             

            account.Pop3Settings.Host = View.pop3Server.text;
            account.Pop3Settings.Port = int(View.pop3Port.text);
            account.Pop3Settings.ConnectionType = (View.pop3ConnectionType.selectedLabel == "Regular")
                ? ServerSettings.CONNECTION_TYPE_REGULAR
                : ServerSettings.CONNECTION_TYPE_SECURE_TLS;
            
            account.Pop3Settings.UserName = View.pop3UserName.text;
            account.Pop3Settings.UserPassword = View.pop3Password.text;
            
            account.SmtpSettings.Host = View.smtpServer.text;
            account.SmtpSettings.Port = int(View.smtpPort.text);
            account.SmtpSettings.ConnectionType = (View.smtpConnectionType.selectedLabel == "Regular")
                ? ServerSettings.CONNECTION_TYPE_REGULAR
                : ServerSettings.CONNECTION_TYPE_SECURE_TLS;
            
            if (View.smtpAuthCheckBox.selected)
            {
                account.SmtpSettings.UserName = View.smtpUserName.text;
                account.SmtpSettings.UserPassword = View.smtpPassword.text;
            }
                        
            m_remoteObject.CreateAccount(account);
        }

        public function OnUserEmailFocusOut():void {
            var email:String = View.userEmail.text;
            
            var atPosition:int = email.indexOf("@");
            if (atPosition == -1) return;
            
            var emailDomain:String = email.substring(atPosition + 1);
            var emailUser:String = email.substr(0, atPosition);
            
            var settings:Object = null;
            
            for (var i:int = 0; i < m_predefinedSettings.length; i++){
                if (emailDomain == m_predefinedSettings[i].Domain) {
                    settings = m_predefinedSettings[i];
                    break;
                }
            }
            
            if (!settings) return;
            
            View.smtpServer.text = settings.SmtpHost;
            View.smtpPort.text = settings.SmtpPort;
            View.pop3Server.text = settings.Pop3Host;
            View.pop3Port.text = settings.Pop3Port;
            View.smtpAuthCheckBox.selected = settings.AuthRequired;
            View.smtpConnectionType.selectedIndex = (settings.Ssl) ? 1 : 0;
            View.pop3ConnectionType.selectedIndex = (settings.Ssl) ? 1 : 0;
            View.pop3UserName.text = emailUser;
            
            if (settings.AuthRequired)
                View.smtpUserName.text = emailUser;
        }
        
        public function OnClose():void
        {
            PopUpManager.removePopUp(View);
        }
        
        public function OnFault(event:FaultEvent):void
        {
            Alert.show(event.fault.faultString);
        }
        
        private function OnRegistered(event:ResultEvent):void 
        {
            Alert.show('New account has been created. Please login.');
            OnClose();
        }
        
        public function IsFormValid():Boolean 
        {
            var result:Boolean = true;
            
            if ((View.userEmailV.validate().type == ValidationResultEvent.INVALID) ||
                (View.popServerV.validate().type == ValidationResultEvent.INVALID) ||
                (View.popPortV.validate().type == ValidationResultEvent.INVALID) ||
                (View.pop3UserNameV.validate().type == ValidationResultEvent.INVALID) ||
                (View.pop3PasswordV.validate().type == ValidationResultEvent.INVALID)) 
            {
                result = false;
            }
            
            if (View.smtpAuthCheckBox.selected && (
               (View.smtpUserNameV.validate().type == ValidationResultEvent.INVALID) ||
               (View.smtpPasswordV.validate().type == ValidationResultEvent.INVALID)))
            {
                result= false;
            }
            
            return result;
        }        
        
    }
}
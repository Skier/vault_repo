package UI.Settings
{
    import Domain.Account;
    import Domain.ServerSettings;
    
    import UI.AppController;
    import UI.AppModel;
    
    import mx.controls.Alert;
    import mx.events.ValidationResultEvent;
    import mx.managers.PopUpManager;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;
    import mx.events.CloseEvent;
    
    public class SettingsController
    {
        [Bindable]
        public var Model:Account;
        
        private var Parent:AppController;
        private var View:SettingsView;
        
        private var m_remoteObject:RemoteObject;
        private var m_pop3SettingsChanged:Boolean = false;
        private var m_tmpModel:Account;
        
        public function SettingsController(view:SettingsView, parent:AppController)
        {
            this.Parent = parent;
            this.View = view;

            Model = Parent.Model.account;

            m_remoteObject = new RemoteObject(AppController.FLEX_MAIL_SERVER_OBJECT_NAME);
            m_remoteObject.addEventListener(ResultEvent.RESULT, OnUpdateReceipt);
            m_remoteObject.addEventListener(FaultEvent.FAULT, OnFault);
            m_remoteObject.showBusyCursor = true;
            
        }
        
        public function OnCreated():void
        {
            PopUpManager.centerPopUp(View);
        }
        
        public function OnSave():void {
            if (!IsFormValid()){
                Alert.show("Some of required fields are not filled or have wrong values.");
                return;
            }
                                                
            m_tmpModel = new Account();
            m_tmpModel.Pop3Settings = new ServerSettings();
            m_tmpModel.SmtpSettings = new ServerSettings();
                            
            m_tmpModel.Email = View.userEmail.text;                             

            m_tmpModel.Pop3Settings.Host = View.pop3Server.text;
            m_tmpModel.Pop3Settings.Port = int(View.pop3Port.text);
            m_tmpModel.Pop3Settings.ConnectionType = (View.pop3ConnectionType.selectedLabel == "Regular")
                ? ServerSettings.CONNECTION_TYPE_REGULAR
                : ServerSettings.CONNECTION_TYPE_SECURE_TLS;
            
            m_tmpModel.Pop3Settings.UserName = View.pop3UserName.text;
            m_tmpModel.Pop3Settings.UserPassword = View.pop3Password.text;
            
            m_tmpModel.SmtpSettings.Host = View.smtpServer.text;
            m_tmpModel.SmtpSettings.Port = int(View.smtpPort.text);
            m_tmpModel.SmtpSettings.ConnectionType = (View.smtpConnectionType.selectedLabel == "Regular")
                ? ServerSettings.CONNECTION_TYPE_REGULAR
                : ServerSettings.CONNECTION_TYPE_SECURE_TLS;

            if (View.smtpAuthCheckBox.selected)
            {
                m_tmpModel.SmtpSettings.UserName = View.smtpUserName.text;
                m_tmpModel.SmtpSettings.UserPassword = View.smtpPassword.text;
            } 
            
            if (m_tmpModel.Pop3Settings.ConnectionType != Model.Pop3Settings.ConnectionType ||
                m_tmpModel.Pop3Settings.Host != Model.Pop3Settings.Host ||
                m_tmpModel.Pop3Settings.Port != Model.Pop3Settings.Port ||
                m_tmpModel.Pop3Settings.UserName != Model.Pop3Settings.UserName ||
                m_tmpModel.Pop3Settings.UserPassword != Model.Pop3Settings.UserPassword)
            {
                //User has changed Pop3 Settings. This requires relogin. Promt user for this action
                Alert.show('You are changed Pop3Settings. This requires relogin. Do you want to apply new settings?', 
                    'Alert', Alert.YES | Alert.NO, View, UpdateConfirmationListener, null, Alert.NO);    
            }
            else
            {
                m_pop3SettingsChanged = false;
                m_remoteObject.UpdateAccount(m_tmpModel);
            }
        }

        public function UpdateConfirmationListener(event:CloseEvent):void
        {
            if (event.detail == Alert.YES) 
            {
                m_pop3SettingsChanged = true;
                m_remoteObject.UpdateAccount(m_tmpModel);
            }
        }

        public function OnClose():void
        {
            PopUpManager.removePopUp(View);
        }
        
        private function OnUpdateReceipt(event:ResultEvent):void 
        {
            OnClose();
            
            if (m_pop3SettingsChanged)
            {
                Parent.Logout();
                Alert.show("Settings are updated. Now relogin please.")
            }
            else
            {
                Model = Parent.Model.account = m_tmpModel;
                Alert.show("Settings are updated.")
            }
        }
        
        private function OnFault(event:FaultEvent):void
        {
            Alert.show(event.fault.faultString);

            if (event.fault.faultString == "Your session has expired"){
                OnClose();
                Parent.Logout();
            }
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
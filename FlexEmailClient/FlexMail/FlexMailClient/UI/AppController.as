package UI
{
    import Domain.Account;
    import Domain.MailBoxStatus;
    import Domain.Message;
    import Domain.MessageBody;
    
    import UI.Registration.RegistrationView;
    import UI.Settings.SettingsView;
    
    import flash.display.DisplayObject;
    import flash.events.TimerEvent;
    import flash.utils.Timer;
    
    import mx.collections.ArrayCollection;
    import mx.controls.Alert;
    import mx.formatters.DateFormatter;
    import mx.managers.PopUpManager;
    import mx.rpc.AsyncToken;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;
        
    public class AppController
    {
        public static const FLEX_MAIL_SERVER_OBJECT_NAME:String = "FlexMailServer";
        private const MAX_PACKET_SIZE:int = 10;
        
        [Bindable]
        public var Model:AppModel;

        [Bindable]
        public var IsPop3Busy:Boolean = false;
        
        [Bindable]
        public var IsSmtpBusy:Boolean = false;
        
        private var View:AppView;
        
        private var m_remoteObject:RemoteObject;
        private var m_ticker:Timer;
        private var m_listeners:Array;
        
        public function AppController( view:AppView ):void
        {
            this.View = view;
            this.Model = new AppModel();

            m_remoteObject = new RemoteObject(FLEX_MAIL_SERVER_OBJECT_NAME);
            m_remoteObject.GetMailBoxStatus.addEventListener(ResultEvent.RESULT, OnMailBoxStatusReturn);
            m_remoteObject.RetrieveNewMessages.addEventListener(ResultEvent.RESULT, OnNewMessagesReceived);
            m_remoteObject.RetrieveMoreMessages.addEventListener(ResultEvent.RESULT, OnNewMessagesReceived);
            m_remoteObject.SendMessage.addEventListener(ResultEvent.RESULT, OnMessageSent);
            m_remoteObject.GetContacts.addEventListener(ResultEvent.RESULT, OnContactsReceived);
            
            m_remoteObject.addEventListener(FaultEvent.FAULT, OnFault);
            m_remoteObject.showBusyCursor = false;
                        
            m_ticker = new Timer(60000 * 2); //2 minutes timer
            m_ticker.addEventListener(TimerEvent.TIMER, OnTick);
            
            m_listeners = [];
        }
                
        public function AddListener(listener:IAppControllerListener):void 
        {
            m_listeners.push(listener);
        }
                
        public function Login(account:Account):void 
        {
            Reset();
            Model.account = account;
            SetAppWorkflowState( AppModel.WORKFLOWSTATE_INBOX_VIEWING );
            m_ticker.start();

            m_remoteObject.GetContacts();
            UpdateMailBoxStatus();
        }
        
        public function Reset():void
        {
            Model.Reset();
            
            for each(var listener:IAppControllerListener in m_listeners)
                listener.OnReset();
        }
        
        public function OnCreated():void        
        {
            SetAppWorkflowState( AppModel.WORKFLOWSTATE_LOGIN_VIEWING );
        }
         
        public function Logout():void
        {
            m_ticker.stop();
            SetAppWorkflowState( AppModel.WORKFLOWSTATE_LOGIN_VIEWING );
        }
        
        public function ShowInbox():void
        {
            SetAppWorkflowState( AppModel.WORKFLOWSTATE_INBOX_VIEWING );
        }
        
        public function ComposeMessage(initMessage:Message = null): void 
        {
            if (!initMessage)
                initMessage = new Message();
                
            View.composeView.Controller.Initialize( initMessage );
            SetAppWorkflowState( AppModel.WORKFLOWSTATE_COMPOSING );
        }

        public function SendMessage( message:Message ):void 
        {
            m_remoteObject.SendMessage( message );
            IsSmtpBusy = true;
            Model.AddHistoryEvent("Sending message");
        }
        
        public function DeleteMessages( messages:Array):void{
            
            var msgUIDs:Array = [];
            for (var i:int=0; i < messages.length;i++)
                msgUIDs.push (messages[i].Uid);

            var asyncToken:AsyncToken = m_remoteObject.DeleteMessages(msgUIDs);
            asyncToken.addResponder ( new Responder(
            
                //onResult
                function(e:ResultEvent):void
                {
                    OnMessagesDeleted(messages);
                },
                
                OnFault
                
            ));
            
            IsPop3Busy = true;
            Model.AddHistoryEvent("Deleting messages");
        }

        public function UpdateMailBoxStatus():void 
        {
            IsPop3Busy = true;
            m_remoteObject.GetMailBoxStatus();
            Model.AddHistoryEvent("Checking for new messages");
        }
        
        /**
        * Retrieve the only new messages that have arrived since the last check.
        */
        public function RetrieveNewMessages():void
        {
            m_remoteObject.RetrieveNewMessages(MAX_PACKET_SIZE);
            
            IsPop3Busy = true;
            Model.AddHistoryEvent("Receiving mail messages");
        }

        public function RetrieveMoreMessages():void
        {
            m_remoteObject.RetrieveMoreMessages(MAX_PACKET_SIZE);
            
            IsPop3Busy = true;
            Model.AddHistoryEvent("Receiving mail messages");
        }
        
        public function RetrieveMessageBody(message:Message):void
        {
            var asyncToken:AsyncToken = m_remoteObject.RetrieveMessageBody(message.Uid);
            
            asyncToken.addResponder ( new Responder(
            
                // onResult
                function(event:ResultEvent):void 
                {
                    IsPop3Busy = false;
                    message.Body = MessageBody( event.result );
                    message.Status = Message.NORMAL;
                },
                
                function (event:FaultEvent):void
                {
                    OnFault( event );
                    message.Status = Message.BODY_ERROR;
                }
            ));
            
            IsPop3Busy = true;
            message.Status = Message.BODY_RETRIEVING;
        }
        
        public function OnContactsReceived(event:ResultEvent):void
        {
            var list:Array = event.result as Array;
            
            if (list && list.length > 0)
            {
                for (var i:int = 0; i < list.length; i++)
                {
                    Model.contactList.addItem(list[i]);
                }
            }
        }
        
        public function OnShowSettings(): void 
        {
            var settingsView:SettingsView = SettingsView( 
                PopUpManager.createPopUp( DisplayObject(View.parentApplication), SettingsView, true ));

            settingsView.ParentController = this;
        }
        
        public function OnShowRegistration():void
        {
            var registrationView:RegistrationView = RegistrationView( 
                PopUpManager.createPopUp( DisplayObject(View.parentApplication), RegistrationView, true ));
        }
        
        public function OnComposeDiscard():void
        {
            SetAppWorkflowState( AppModel.WORKFLOWSTATE_INBOX_VIEWING );
        }
        
        public function OnCheckMail():void
        {
            UpdateMailBoxStatus();
        }
                
        public function OnFault(event:FaultEvent):void 
        {
            IsPop3Busy = false;
            IsSmtpBusy = false;

            Alert.show(event.fault.faultString);
            
            if (Model && Model.history){
                Model.AddHistoryEvent("Error has occured: " + event.fault.faultString);
            }
            
            if (event.fault.faultString == "Your session has expired") {
                Logout();
            }
        }

        private function OnMailBoxStatusReturn(event:ResultEvent):void 
        {
            IsPop3Busy = false;
            Model.mailBoxStatus = MailBoxStatus(event.result);

            Model.AddHistoryEvent(Model.mailBoxStatus.TotalMessages + " messages in the mailbox, " + Model.mailBoxStatus.NewMessages + " new");
            
            if (Model.mailBoxStatus.NewMessages > 0)
                RetrieveNewMessages();
        }
        
        private function OnMessagesDeleted(messageList:Array):void
        {
            IsPop3Busy = false;
            
            for each (var message:Message in messageList)
            {
                var index:int = Model.inbox.getItemIndex( message );
                Model.inbox.removeItemAt( index );
            }
            
            for each(var listener:IAppControllerListener in m_listeners)
                listener.OnMessagesDeleted( messageList );

            Model.AddHistoryEvent( "Connection finished. " + messageList.length + " messages deleted" );
            
            UpdateMailBoxStatus();
        }
                
        private function OnNewMessagesReceived(event:ResultEvent):void
        {
            IsPop3Busy = false;
            var messageList:Array = event.result as Array;
            
            for each (var message:Message in messageList)
            {
                Model.inbox.addItem(message);
                Model.AddHistoryEvent("Received message from " + message.From.DisplayValue + ", subject: " + message.Subject);
            }
                
            for each(var listener:IAppControllerListener in m_listeners)
                listener.OnNewMessagesReceipt(messageList);

            Model.AddHistoryEvent("Connection finished. " + messageList.length + " messages received");
            
        }
        
        private function OnMessageSent(event:ResultEvent):void
        {
            IsSmtpBusy = false;
            SetAppWorkflowState( AppModel.WORKFLOWSTATE_INBOX_VIEWING );
            Model.AddHistoryEvent("Message has been sent");
        }

        private function OnTick(event:TimerEvent):void 
        {
            UpdateMailBoxStatus();
        }

        private function SetAppWorkflowState(state:int):void 
        {
            Model.workflowState = state;
            
            switch (state)
            {
                case AppModel.WORKFLOWSTATE_LOGIN_VIEWING :
                    View.m_appViewStack.selectedChild = View.loginView;
                    break;
                                                                
                case AppModel.WORKFLOWSTATE_INBOX_VIEWING :
                    View.m_appViewStack.selectedChild = View.mainView;
                    View.m_mainViewStack.selectedChild = View.inboxView;
                    break;
                        
                case AppModel.WORKFLOWSTATE_COMPOSING :
                    View.m_appViewStack.selectedChild = View.mainView;
                    View.m_mainViewStack.selectedChild = View.composeView;
                    break;
                        
                default :
                    throw new Error("Workflow state is invalid");
            }
        }        
    }
}
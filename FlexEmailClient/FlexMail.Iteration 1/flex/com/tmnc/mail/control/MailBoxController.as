package com.tmnc.mail.control
{
    import com.tmnc.mail.business.*;
    import com.tmnc.mail.control.events.*;
    import com.tmnc.mail.model.MailBoxModel;
    import com.tmnc.mail.view.controls.Hourglass;
    import com.tmnc.mail.vo.*;
    
    import flash.events.EventDispatcher;
    import flash.net.URLRequest;
    import flash.net.URLRequestMethod;
    
    import mx.collections.ArrayCollection;
    import mx.controls.Alert;
    import mx.controls.Text;
    import mx.core.Application;
    import mx.core.IFlexDisplayObject;
    import mx.managers.PopUpManager;
    import mx.rpc.AsyncToken;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;
    import mx.utils.UIDUtil;
    import com.tmnc.mail.view.helpers.ViewHelper;
    import flash.utils.Timer;
    import flash.events.TimerEvent;
    import flash.events.Event;
    
    public class MailBoxController
    {
        private const MAX_PACKET_SIZE:int = 50;
        
        private var _ecService : RemoteObject;
        private var _model:MailBoxModel;
        private var _app:Application = Application(Application.application);
        
        private var ticker:Timer;
        
        public function MailBoxController(){
            _ecService = new RemoteObject("FlexMailClient");
            _ecService.showBusyCursor = false;

            _model = MailBoxModel.getInstance();

            _app.addEventListener(LoginEvent.EVENT_LOGIN, login);
            _app.addEventListener(MessageEvent.EVENT_SEND, sendMessage);
            _app.addEventListener(MessageEvent.EVENT_REPLY, replyMessage);
            _app.addEventListener(MessageEvent.EVENT_VIEW, viewMessage);
            _app.addEventListener(MessageEvent.EVENT_FORWARD, forwardMessage);
            _app.addEventListener(MessageEvent.EVENT_COMPOSE, composeMessage);
            _app.addEventListener(DeleteMessagesEvent.EVENT_DELETE_MESSAGES, deleteMessages);
            _app.addEventListener(AccountEvent.EVENT_REGISTER, registerAccount);
            _app.addEventListener(AccountEvent.EVENT_UPDATE, updateAccount);
            _app.addEventListener(AccountEvent.EVENT_CANCEL_CHANGES, hideSettingsView);
            _app.addEventListener(DisplayDialogEvent.EVENT_DISPLAY_REGISTRATION_DIALOG, displayAccountRegistrationDialog);
            _app.addEventListener(DisplayDialogEvent.EVENT_DISPLAY_SETTINGS_DIALOG, displayAccountSettingsDialog);
            _app.addEventListener("getMessages", getMessagesHandler);
            
            //a Timer that fires an event once per 5 minutes
            ticker = new Timer(60000 * 5);
            ticker.addEventListener(TimerEvent.TIMER, onTick);
        }

        private function onTick(event:TimerEvent):void {
            if (!_model.serverActivity){
                updateMailBoxStatus(false);
            }
        }
        
        private function login(event:LoginEvent):void{
            if (event.email == null || event.email == ""){
                _model.clearUserState();
                ticker.stop();
                return;
            }
            
            var asyncToken:AsyncToken = _ecService.Login(event.email, event.password);
            asyncToken.addResponder (new Responder( 
            
                // onResult
                function(event:ResultEvent):void {
                    Hourglass.remove();
                    
                    _model.accountInfo = AccountInfo(event.result);
                    _model.workflowState = MailBoxModel.VIEWING_INBOX_MAIL_SCREEN;
                    
                    _app.dispatchEvent(new CheckMessagesEvent());

                    updateMailBoxStatus(true);
                    ticker.start();
                    refreshContactList();
                    retrieveUploaderUrlRequest();
                    getPop3Settings();
                    getSmtpSettings();
                },
                
                // onFault
                function(event:FaultEvent):void{
                    Hourglass.remove();                    
                    showServerFaultError(event, "Unable to login.");
                }
            ));
            
            Hourglass.addHourglass();
        }
        
        private function refreshContactList():void{

            var asyncToken:AsyncToken = _ecService.GetContacts();
            asyncToken.addResponder ( new Responder(
            
                // onResult
                function(event:ResultEvent):void {
                    Hourglass.remove();                    
                    _model.accountContactEmails = event.result as Array;
                },
                
                // onFault
                function(event:FaultEvent):void{
                    Hourglass.remove();
                    showServerFaultError(event, "Unable to retrieve contact list.");
                }

            ));
            
            Hourglass.addHourglass();
         }

        private function getPop3Settings():void{

            var asyncToken:AsyncToken = _ecService.GetPop3Settings();
            asyncToken.addResponder ( new Responder(
            
                // onResult
                function(event:ResultEvent):void {
                    Hourglass.remove();                    
                    _model.pop3Settings = ServerSettingsInfo(event.result);
                },
                
                // onFault
                function(event:FaultEvent):void{
                    Hourglass.remove();                    
                    showServerFaultError(event, "Unable to retrieve Server Settings information.");
                }

            ));
            
            Hourglass.addHourglass();            
         }

        private function getSmtpSettings():void{

            var asyncToken:AsyncToken = _ecService.GetSmtpSettings();
            asyncToken.addResponder ( new Responder(
            
                // onResult
                function(event:ResultEvent):void {
                    Hourglass.remove();
                    _model.smtpSettings = ServerSettingsInfo(event.result);
                },
                
                // onFault
                function(event:FaultEvent):void{
                    Hourglass.remove();                    
                    showServerFaultError(event, "Unable to retrieve Server Settings information.");
                }

            ));
            
            Hourglass.addHourglass();
         }

        private function updateMailBoxStatus(loadNewMessagesRequired:Boolean=false):void {
            var asyncToken:AsyncToken = _ecService.GetMailBoxStatus();
            
            asyncToken.addResponder (new Responder(
            
                // onResult
                function(event:ResultEvent):void{
                    Hourglass.remove();

                    _model.mailBoxStatus = MailBoxStatus(event.result);
                    
                    var retrievedMessagesCount:int = _model.messageListDescriptor.messagesCount;

                    if (_model.mailBoxStatus.NewMessages > 0 && loadNewMessagesRequired) {
                        retrieveMessages();
                        return;
                    }

                    ViewHelper.UpdateMailBoxStatus();
                }, 
                
                // onFault
                function(event:FaultEvent):void {
                    showServerFaultError(event, "Unable to get count of messages on POP3 server.");
                    Hourglass.remove();
                }
            ));

            Hourglass.addHourglass();
        }
        
        private function getMessagesHandler(event:Event):void {
            updateMailBoxStatus(true);
        }
        
        private function retrieveMessages():void{
            
            var asyncToken:AsyncToken = _ecService.GetMissingMessages(MAX_PACKET_SIZE);
            asyncToken.addResponder (new Responder(
            
                // onResult
                function(event:ResultEvent):void {
                    _model.serverActivity = false;
                    
                    var newMessages:Array = event.result as Array;
                    if (newMessages.length > 0){
                        _model.messageListDescriptor.addItems(newMessages);
                        
                        ViewHelper.UpdateMailBoxStatus();
                    }
                    
                    Hourglass.remove();
                },
                
                // onFault
                function(event:FaultEvent):void{
                    _model.serverActivity = false;
                    showServerFaultError(event, "Unable to load message.");
                    Hourglass.remove();
                }
            ));

            Hourglass.addHourglass();
            _model.serverActivity = true;
        }
 
        private function deleteMessages(event:DeleteMessagesEvent):void{
            
            var msgUIDs:Array = [];
            for (var i:int=0; i < event.messageList.length;i++){
                msgUIDs.push (event.messageList[i].Uid);
            }

            var asyncToken:AsyncToken = _ecService.DeleteMessages(msgUIDs);
            asyncToken.addResponder (new Responder(
            
                //onResult
                function(e:ResultEvent):void{
                    _model.messageListDescriptor.deleteItems(event.messageList);
                    _model.currentMessage = null;
                    
                    Hourglass.remove();
                    updateMailBoxStatus();
                },
                
                // onFault
                function(event:FaultEvent):void{
                    Hourglass.remove();
                    showServerFaultError(event, "Unable to delete message(s).");
                }
                
            ));
            
            Hourglass.addHourglass();
        }
                
        private function viewMessage(event:MessageEvent):void {
            
            _model.currentMessage = event.message;

            if (event.message && !event.message.Body){
                loadMessageBody(event.message);
            }
            
        }
        
        private function loadMessageBody(message:MessageInfo):void {
            var asyncToken:AsyncToken = _ecService.RetrieveMessageBody(message.Uid);
            
            asyncToken.addResponder ( new Responder(
            
                // onResult
                function(event:ResultEvent):void {
                    Hourglass.remove();
                    message.Body = MessageBodyInfo(event.result);
                },
                
                // onFault
                function(event:FaultEvent):void{
                    Hourglass.remove();                    
                    showServerFaultError(event, "Unable to get message body.");
                }

            ));
            
            Hourglass.addHourglass();                
        }
        
        private function composeMessage(event:MessageEvent):void {
            ViewHelper.CleanComposeView();
            _model.workflowState = MailBoxModel.VIEWING_MAIL_CREATION_SCREEN;
        }
        
        private function replyMessage(event:MessageEvent):void {
            ViewHelper.PrepareMessageReply(event.message);
            _model.workflowState = MailBoxModel.VIEWING_MAIL_CREATION_SCREEN;
        }

        private function forwardMessage(event:MessageEvent):void {
            ViewHelper.PrepareMessageForward(event.message);
            _model.workflowState = MailBoxModel.VIEWING_MAIL_CREATION_SCREEN;
        }
        
        private function sendMessage(event:MessageEvent):void{

            var asyncToken:AsyncToken = _ecService.SendMessage(event.message);
            asyncToken.addResponder (new Responder(
            
                // onResult
                function(event:ResultEvent):void {
                    Hourglass.remove();
                    Alert.show('Message has been sent.');    
                    
                    _model.workflowState = MailBoxModel.VIEWING_INBOX_MAIL_SCREEN;
                    _app.dispatchEvent(new CheckMessagesEvent());                    
                },
                
                // onFault
                function(event:FaultEvent):void{
                    Hourglass.remove();                    
                    showServerFaultError(event, "Unable to send message.");
                }
            ));
            
            Hourglass.addHourglass();
        }

        private function registerAccount(event:AccountEvent):void {
            var initializeEvent:AccountEvent = event;
            
            var asyncToken:AsyncToken = _ecService.CreateAccount(event.accountInfo.Email, 
                event.pop3Settings, event.smtpSettings);
                
            asyncToken.addResponder (new Responder(
            
                // onResult
                function(event:ResultEvent):void {
                    Hourglass.remove();
                    Alert.show('New account has been created. Please login.');
                    _model.workflowState = MailBoxModel.VIEWING_LOGIN_SCREEN;
                },
                
                // onFault
                function(event:FaultEvent):void{
                    Hourglass.remove();
                    showServerFaultError(event, "Unable to create account.");
                }
            ));
            
            Hourglass.addHourglass();
        }

        private function updateAccount(event:AccountEvent):void {
            var initializeEvent:AccountEvent = event;
            
            var asyncToken:AsyncToken = _ecService.UpdateAccount(event.accountInfo, 
                event.pop3Settings, event.smtpSettings);
                
            asyncToken.addResponder (new Responder(
            
                // onResult
                function(event:ResultEvent):void {
                    Hourglass.remove();

                    _model.clearUserState();
                    
                    _app.dispatchEvent(new LoginEvent(
                        initializeEvent.accountInfo.Email,
                        initializeEvent.pop3Settings.UserPassword));
                        
                },
                
                // onFault
                function(event:FaultEvent):void {
                    Hourglass.remove();
                    showServerFaultError(event, "Unable to update account.");
                }
            ));
            
            Hourglass.addHourglass();
        }

        private function hideSettingsView(event:AccountEvent):void {
            if (_model.accountInfo){
                _model.workflowState = MailBoxModel.VIEWING_INBOX_MAIL_SCREEN;
            } else {
                _model.workflowState = MailBoxModel.VIEWING_LOGIN_SCREEN;
            }
        }
        
        private function displayAccountRegistrationDialog(event:DisplayDialogEvent):void {
            ViewHelper.PrepareRegistrationView();
            _model.workflowState = MailBoxModel.VIEWING_SETTINGS_SCREEN;
        }

        private function displayAccountSettingsDialog(event:DisplayDialogEvent):void {
            ViewHelper.PrepareAccountSettingsView();
            _model.workflowState = MailBoxModel.VIEWING_SETTINGS_SCREEN;
        }
        
        private function retrieveUploaderUrlRequest():void{
            
            var asyncToken:AsyncToken = _ecService.GetFileUploaderURL();
            asyncToken.addResponder (new Responder(
            
                // onResult
                function(event:ResultEvent):void {
                    Hourglass.remove();
                    
                    _model.uploadRequestURL = new URLRequest(event.result as String)
                    _model.uploadRequestURL.method = URLRequestMethod.POST;
                },
                
                // onFault
                function(event:FaultEvent):void{
                    Hourglass.remove();
                    
                    showServerFaultError(event, "Unable to get file uploader URL.");
                }
            ));
            
            Hourglass.addHourglass();
        }
        
        private function showServerFaultError(event:FaultEvent, errorMessage:String = "Unable to complete operation."):void {
            errorMessage += "\n\n[Error Detail: " + event.fault.faultString +"]";
            Alert.show(errorMessage, "Error");
            
            if (event.fault.faultString == "Your session has expired."){
                Application.application.dispatchEvent(new LoginEvent("", ""));
            }
        }
        
    }
}
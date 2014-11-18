package com.tmnc.mail.control
{
	import com.tmnc.mail.business.*;
	import com.tmnc.mail.business.messages.MessageInfo;	
	import com.tmnc.mail.control.events.*;
	import com.tmnc.mail.model.MailBoxModel;
	
	import flash.net.URLRequest;
	import flash.net.URLRequestMethod;
	
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	import mx.utils.UIDUtil;
	import flash.events.EventDispatcher;
	import mx.core.Application;
	import mx.controls.Text;
	import mx.core.IFlexDisplayObject;
	import com.tmnc.mail.view.dialogs.AccountSettingsDialog;
	import mx.managers.PopUpManager;
	import com.tmnc.mail.view.controls.Hourglass;
	
	public class MailBoxController
	{
        private var _ecService : RemoteObject;
		private var _model:MailBoxModel;
		private var _app:Application = Application(Application.application);
		
		public function  MailBoxController(){
            _ecService = new RemoteObject("email_client");
			_ecService.showBusyCursor = false;
			
        	_model = MailBoxModel.getInstance();

            _app.addEventListener(LoginEvent.EVENT_LOGIN, login);
            _app.addEventListener(MessageEvent.EVENT_SEND, sendMessage);
            _app.addEventListener(MessageEvent.EVENT_REPLY, replyMessage);
            _app.addEventListener(MessageEvent.EVENT_FORWARD, forwardMessage);
            _app.addEventListener(MessageEvent.EVENT_COMPOSE, composeMessage);
            _app.addEventListener(CheckMessagesEvent.EVENT_CHECK_MESSAGES, checkMessages);
            _app.addEventListener(DeleteMessagesEvent.EVENT_DELETE_MESSAGES, deleteMessages);
            _app.addEventListener(AccountEvent.EVENT_REGISTER, registerAccount);
            _app.addEventListener(AccountEvent.EVENT_UPDATE, updateAccount);
            _app.addEventListener(DisplayDialogEvent.EVENT_DISPLAY_REGISTRATION_DIALOG, displayAccountRegistrationDialog);
            _app.addEventListener(DisplayDialogEvent.EVENT_DISPLAY_SETTINGS_DIALOG, displayAccountSettingsDialog);
		}

		private function login(event:LoginEvent):void{
			if (event.email == null || event.email == ""){
				_model.clearUserState();
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

                    refreshContactList();
		            retrieveUploaderUrlRequest();
		            getPop3Settings();
		            getSmtpSettings();		            
            	},
            	
            	// onFault
            	function(event:FaultEvent):void{
                    Hourglass.remove();            	    
            		showServerFaultError(event, "Unable to login. Check your username and password.");
            	}
            ));
            
            Hourglass.addHourglass();
        }
		
		private function refreshContactList():void{

            var asyncToken:AsyncToken = _ecService.GetContacts(_model.accountInfo.Id);
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

            var asyncToken:AsyncToken = _ecService.GetSettings(_model.accountInfo.Pop3SettingsId);
            asyncToken.addResponder ( new Responder(
            
	            // onResult
	            function(event:ResultEvent):void {
                    Hourglass.remove();	                
	        		_model.pop3Settings = ServerSettingsInfo(event.result);
	            },
	            
	            // onFault
            	function(event:FaultEvent):void{
                    Hourglass.remove();            	    
            		showServerFaultError(event, "Unable to retrieve Srver Settings information.");
            	}

			));
			
            Hourglass.addHourglass();			
 		}

		private function getSmtpSettings():void{

            var asyncToken:AsyncToken = _ecService.GetSettings(_model.accountInfo.SmtpSettingsId);
            asyncToken.addResponder ( new Responder(
            
	            // onResult
	            function(event:ResultEvent):void {
                    Hourglass.remove();	                
	        		_model.smtpSettings = ServerSettingsInfo(event.result);
	            },
	            
	            // onFault
            	function(event:FaultEvent):void{
                    Hourglass.remove();            	    
            		showServerFaultError(event, "Unable to retrieve Srver Settings information.");
            	}

			));
			
            Hourglass.addHourglass();			
 		}

        private function checkMessages(event:CheckMessagesEvent):void{

        	var asyncToken:AsyncToken = _ecService.CheckMessages(_model.accountInfo.Id);
			
			//we use an animated icon on mailBox toolbar to show server activity instead 
			//of replacing cursor to hourglass. Thus, user can continue work while messages are loading.
			_model.serverActivity = true; 
			_model.statusMessage = "Retrieving messages count...";
			        	
            asyncToken.addResponder (new Responder(
            
            	// onResult
            	function(event:ResultEvent):void{
					_model.serverActivity = false;

					if (Number(event.result) > _model.messageListDescriptor.messageList.length){
						_model.serversMessageCount = Number(event.result);
						loadMissingMessages();
					} else {
						_model.statusMessage = "No new messages.";
					}
					
            	}, 
            	
            	// onFault
            	function(event:FaultEvent):void{
            		showServerFaultError(event, "Unable to check new messages.");
					_model.serverActivity = false;
			    	_model.statusMessage = "Stoped by failure.";
            	}
            ));
        	
        }

        private function getMessageByNumber(msgNumber:int):void{

            var asyncToken:AsyncToken = _ecService.Receive(_model.accountInfo.Id, msgNumber);
            asyncToken.addResponder (new Responder(
            
	            // onResult
	            function(event:ResultEvent):void{
					_model.serverActivity = false;
					
		        	var message:MessageInfo = MessageInfo(event.result);
		    		_model.messageListDescriptor.addItem(message);
		    		
		    		//Continue message loading
		    		loadMissingMessages();		    		
	            },
	            
            	// onFault
            	function(event:FaultEvent):void{
            		showServerFaultError(event, "Unable to load message.");
					_model.serverActivity = false;
			    	_model.statusMessage = "Stoped by failure.";
            	}
            ));

			_model.statusMessage = "Retrieving " + msgNumber + " from " + _model.serversMessageCount + "...";        	
			_model.serverActivity = true; 
        }

        
        private function deleteMessages(event:DeleteMessagesEvent):void{
        	
        	var msgUIDs:Array = [];
        	for (var i:int=0; i < event.messageList.length;i++){
				msgUIDs.push (event.messageList[i].Uid);
        	}

            var asyncToken:AsyncToken = _ecService.DeleteMessages(_model.accountInfo.Id, msgUIDs);
            asyncToken.addResponder (new Responder(
            
	            //onResult
	            function(e:ResultEvent):void{
	                _model.messageListDescriptor.deleteItems(event.messageList);
			    	_model.serversMessageCount = _model.serversMessageCount - event.messageList.length;
			    	_model.currentMessage = null;
			    	
                    Hourglass.remove();
	            },
	            
            	// onFault
            	function(event:FaultEvent):void{
                    Hourglass.remove();
            		showServerFaultError(event, "Unable to delete message(s).");
            	}
	            
	        ));
	        
            Hourglass.addHourglass();
        }
                
        private function composeMessage(event:MessageEvent):void{
			_model.newMessage = new MessageInfo();
			_model.newMessage.AttachmentDir = UIDUtil.createUID();
			
        	_model.workflowState = MailBoxModel.VIEWING_MAIL_CREATION_SCREEN;
        }

        private function replyMessage(event:MessageEvent):void{
        	var originalMsg:MessageInfo = event.message;
			_model.newMessage = new MessageInfo();
			_model.newMessage.AttachmentDir = UIDUtil.createUID();
			_model.newMessage.To = originalMsg.From;
			_model.newMessage.Subject = "Re: " + originalMsg.Subject;
			_model.newMessage.BodyPlainText = originalMsg.BodyPlainText;
			_model.newMessage.InReplyTo = originalMsg.Uid;
           	_model.workflowState = MailBoxModel.VIEWING_MAIL_CREATION_SCREEN;
        }

        private function forwardMessage(event:MessageEvent):void{
        	var originalMsg:MessageInfo = event.message;
			_model.newMessage = new MessageInfo();
			_model.newMessage.AttachmentDir = UIDUtil.createUID();
			_model.newMessage.Subject = "Fw: " + originalMsg.Subject;
			_model.newMessage.BodyPlainText = originalMsg.BodyPlainText;
			_model.workflowState = MailBoxModel.VIEWING_MAIL_CREATION_SCREEN;
        }
        
        private function sendMessage(event:MessageEvent):void{

            var asyncToken:AsyncToken = _ecService.Send(_model.accountInfo.Id, new Array(event.message));
            asyncToken.addResponder (new Responder(
            
            	//onResult
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
            
            	//onResult
            	function(event:ResultEvent):void {
                    Hourglass.remove();
                    
                    PopUpManager.removePopUp(initializeEvent.popup);
		            Alert.show('New account has been created. Please login.');	
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
            
            	//onResult
            	function(event:ResultEvent):void {
                    Hourglass.remove();
                    PopUpManager.removePopUp(initializeEvent.popup);

                    _model.clearUserState();
                    
                    _app.dispatchEvent(new LoginEvent(
                        initializeEvent.pop3Settings.UserName,
                        initializeEvent.pop3Settings.Password));
                        
            	},
            	
            	// onFault
            	function(event:FaultEvent):void {
                    Hourglass.remove();
            		showServerFaultError(event, "Unable to update account.");
            	}
            ));
            
            Hourglass.addHourglass();
        }

        private function displayAccountRegistrationDialog(event:DisplayDialogEvent):void {
            var initialEvent:DisplayDialogEvent = DisplayDialogEvent(event);
            
            var popup:IFlexDisplayObject = PopUpManager.createPopUp(
                initialEvent.dialogParent, AccountSettingsDialog, true);
                popup.alpha = 1;

            AccountSettingsDialog(popup).title = "New user registration.";
            PopUpManager.centerPopUp(popup);
        }

        private function displayAccountSettingsDialog(event:DisplayDialogEvent):void {
            var initialEvent:DisplayDialogEvent = DisplayDialogEvent(event);
            
            var popup:IFlexDisplayObject = PopUpManager.createPopUp(
                initialEvent.dialogParent, AccountSettingsDialog,true);
                
                AccountSettingsDialog(popup).title = "Account Settings";
                AccountSettingsDialog(popup).setData(
                    _model.accountInfo,
                    _model.pop3Settings,
                    _model.smtpSettings);
                
            PopUpManager.centerPopUp(popup);
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
        
        private function loadMissingMessages():void{
            var currentMessageCount:int = _model.messageListDescriptor.messageList.length;
      		if ( _model.serversMessageCount > currentMessageCount ){

	        	_model.statusMessage = (_model.serversMessageCount - currentMessageCount) + " new messages.";
	        	
	        	getMessageByNumber(currentMessageCount + 1 );
      		} else {
	        	_model.statusMessage = "All messages retrieved.";
      		}
        }

        private function showServerFaultError(event:FaultEvent, errorMessage:String = "Unable to complete operation."):void{
            errorMessage += "\n\n[Error Detail: " + event.fault.faultString +"]";
	        Alert.show(errorMessage, "Error");
        }
		
	}
}
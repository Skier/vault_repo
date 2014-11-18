/**
 * $id:

 * Copyright(c) 2006 The Midnight Coders Company. All Rights Reserved.
 */

package com.tmnc.mail.model 
{
    import com.tmnc.mail.business.*;
    import com.tmnc.mail.vo.*;
    
    import flash.net.URLRequest;
    import flash.net.URLRequestMethod;
    
    import mx.collections.ArrayCollection;
    import mx.controls.Alert;
    import mx.managers.CursorManager;
    import mx.rpc.AsyncToken;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.RemoteObject;
    import mx.utils.UIDUtil;
    
    [Bindable]
    public class MailBoxModel
    {       
        private static var instance : MailBoxModel;

        public static function getInstance() : MailBoxModel {
            if (instance == null){
                instance = new MailBoxModel();
            }
                
            return instance;
        }
       
        public function MailBoxModel() {   
            if (MailBoxModel.instance != null){
                throw new Error("Only one MailBoxModel instance should be instantiated!");
            }
            
            clearUserState();
        }
        
        
        public function clearUserState():void{
            this.accountInfo = null;
            this.workflowState = VIEWING_LOGIN_SCREEN;
            this.currentMessage = null;
            this.messageListDescriptor = new MessageListDescriptor();
            this.mailBoxStatus = null;
            this.statusMessage = "";
            this.serverActivity = false;
        }
        
        
        //-------------------------------------------------------------------------

        public static const VIEWING_LOGIN_SCREEN:int = 0;
        public static const VIEWING_INBOX_MAIL_SCREEN:int = 1;
        public static const VIEWING_MAIL_CREATION_SCREEN:int = 2;
        public static const VIEWING_SETTINGS_SCREEN:int = 3;
        
        /** Information about the current account. */
        public var accountInfo:AccountInfo;
        
        public var pop3Settings:ServerSettingsInfo;
        public var smtpSettings:ServerSettingsInfo;
        
        /** The currently-selected MailMessage. */
        public var currentMessage:MessageInfo;

        /** The current state of workflow, defines which View is active. */
        public var workflowState:Number;
        
        /** The current workflof process description. e.g. "Checking new messages.." */        
        public var statusMessage:String;
        
        /** The mail message list descriptor that keep email messages we are working with **/
        public var messageListDescriptor:MessageListDescriptor;

        /** The user's contact list email adresses. 
        * We use them to autofill To, Cc and Bcc email input fields **/
        public var accountContactEmails:Array;

        /** The flag of server activity state. Shows Is remote object busy now. **/
        public var serverActivity:Boolean=false;

        /** The Server Request URL that can upload files for us **/
        public var uploadRequestURL:URLRequest;

        /** The counts of total and new messages on POP3 server **/
        public var mailBoxStatus:MailBoxStatus;
        
    }
}


package com.tmnc.mail.control.events
{
    import com.tmnc.mail.vo.*;
    
    import flash.events.Event;
    
    import mx.core.IFlexDisplayObject;
    import mx.rpc.IResponder;
    
    public class AccountEvent extends Event {

        public static const EVENT_REGISTER:String = "register";
        public static const EVENT_UPDATE:String = "update";
        public static const EVENT_CANCEL_CHANGES:String = "cancel_changes";
                        
        public var accountInfo:AccountInfo;
        public var pop3Settings:ServerSettingsInfo;
        public var smtpSettings:ServerSettingsInfo;
                
        public function AccountEvent(type:String, accountInfo:AccountInfo, pop3Settings:ServerSettingsInfo, 
            smtpSettings:ServerSettingsInfo,
            bubbles:Boolean=true, cancelable:Boolean=false):void 
        {
            super(type, bubbles, cancelable);
            this.accountInfo = accountInfo;
            this.pop3Settings = pop3Settings;
            this.smtpSettings = smtpSettings;
        }
        
        override public function clone():Event {
            return new AccountEvent(type, accountInfo, pop3Settings, smtpSettings, bubbles, cancelable);
        }

    }
}
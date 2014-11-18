package com.tmnc.mail.control.events
{
	import flash.events.Event;
	import com.tmnc.mail.business.*;
	import mx.rpc.IResponder;
	import mx.core.IFlexDisplayObject;
	
	public class AccountEvent extends Event {

        public static const EVENT_REGISTER:String = "register";
        public static const EVENT_UPDATE:String = "update";
        		
		public var accountInfo:AccountInfo;
		public var pop3Settings:ServerSettingsInfo;
		public var smtpSettings:ServerSettingsInfo;
				
        public function AccountEvent(type:String, accountInfo:AccountInfo, pop3Settings:ServerSettingsInfo, 
        	smtpSettings:ServerSettingsInfo, popup:IFlexDisplayObject, 
        	bubbles:Boolean=true, cancelable:Boolean=false):void 
        {
            super(type, bubbles, cancelable);
			this.accountInfo = accountInfo;
			this.pop3Settings = pop3Settings;
			this.smtpSettings = smtpSettings;
			this.popup = popup;
			
        }
		
	    override public function clone():Event {
	        return new AccountEvent(type, accountInfo, pop3Settings, smtpSettings, popup, bubbles, cancelable);
	    }

        public var popup:IFlexDisplayObject;
 
	}
}
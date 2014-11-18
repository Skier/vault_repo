
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _PhoneSms implements IDomainEntity
    {
        public function _PhoneSms()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var trackingPhoneId:int;
        public function get TrackingPhoneId():int { return trackingPhoneId; }
        public function set TrackingPhoneId(value:int):void 
        {
            trackingPhoneId = value;
        }
      
        private var leadSourceId:int;
        public function get LeadSourceId():int { return leadSourceId; }
        public function set LeadSourceId(value:int):void 
        {
            leadSourceId = value;
        }
      
        private var smsSid:String;
        public function get SmsSid():String { return smsSid; }
        public function set SmsSid(value:String):void 
        {
            smsSid = value;
        }
      
        private var accountSid:String;
        public function get AccountSid():String { return accountSid; }
        public function set AccountSid(value:String):void 
        {
            accountSid = value;
        }
      
        private var message:String;
        public function get Message():String { return message; }
        public function set Message(value:String):void 
        {
            message = value;
        }
      
        private var dateCreated:Date;
        public function get DateCreated():Date { return dateCreated; }
        public function set DateCreated(value:Date):void 
        {
            dateCreated = value;
        }
      
        private var phoneFrom:String;
        public function get PhoneFrom():String { return phoneFrom; }
        public function set PhoneFrom(value:String):void 
        {
            phoneFrom = value;
        }
      
        private var phoneTo:String;
        public function get PhoneTo():String { return phoneTo; }
        public function set PhoneTo(value:String):void 
        {
            phoneTo = value;
        }
      

        public function prepareToSend():PhoneSms
        {
            var result:PhoneSms = new PhoneSms();
      
            result.Id = this.Id;
      
            result.TrackingPhoneId = this.TrackingPhoneId;
      
            result.LeadSourceId = this.LeadSourceId;
      
            result.SmsSid = this.SmsSid;
      
            result.AccountSid = this.AccountSid;
      
            result.Message = this.Message;
      
            result.DateCreated = this.DateCreated;
      
            result.PhoneFrom = this.PhoneFrom;
      
            result.PhoneTo = this.PhoneTo;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new PhoneSms();
      
            this.Id = value["Id"];
      
            this.TrackingPhoneId = value["TrackingPhoneId"];
      
            this.LeadSourceId = value["LeadSourceId"];
      
            this.SmsSid = value["SmsSid"];
      
            this.AccountSid = value["AccountSid"];
      
            this.Message = value["Message"];
      
            this.DateCreated = value["DateCreated"];
      
            this.PhoneFrom = value["PhoneFrom"];
      
            this.PhoneTo = value["PhoneTo"];
      
        }
    }
}
      
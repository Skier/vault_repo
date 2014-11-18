
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _TrackingPhoneRotation implements IDomainEntity
    {
        public function _TrackingPhoneRotation()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var timeDisplay:Date;
        public function get TimeDisplay():Date { return timeDisplay; }
        public function set TimeDisplay(value:Date):void 
        {
            timeDisplay = value;
        }
      
        private var userHostAddress:String;
        public function get UserHostAddress():String { return userHostAddress; }
        public function set UserHostAddress(value:String):void 
        {
            userHostAddress = value;
        }
      
        private var parentReferralUri:String;
        public function get ParentReferralUri():String { return parentReferralUri; }
        public function set ParentReferralUri(value:String):void 
        {
            parentReferralUri = value;
        }
      
        private var referralUri:String;
        public function get ReferralUri():String { return referralUri; }
        public function set ReferralUri(value:String):void 
        {
            referralUri = value;
        }
      
        private var sessionIdUid:String;
        public function get SessionIdUid():String { return sessionIdUid; }
        public function set SessionIdUid(value:String):void 
        {
            sessionIdUid = value;
        }
      
        private var trackingPhoneId:int;
        public function get TrackingPhoneId():int { return trackingPhoneId; }
        public function set TrackingPhoneId(value:int):void 
        {
            trackingPhoneId = value;
        }
      
        private var phoneCallId:int;
        public function get PhoneCallId():int { return phoneCallId; }
        public function set PhoneCallId(value:int):void 
        {
            phoneCallId = value;
        }
      
        private var phoneSmsId:int;
        public function get PhoneSmsId():int { return phoneSmsId; }
        public function set PhoneSmsId(value:int):void 
        {
            phoneSmsId = value;
        }
      
        private var leadFormId:int;
        public function get LeadFormId():int { return leadFormId; }
        public function set LeadFormId(value:int):void 
        {
            leadFormId = value;
        }
      
        private var leadSourceId:int;
        public function get LeadSourceId():int { return leadSourceId; }
        public function set LeadSourceId(value:int):void 
        {
            leadSourceId = value;
        }
      

        public function prepareToSend():TrackingPhoneRotation
        {
            var result:TrackingPhoneRotation = new TrackingPhoneRotation();
      
            result.Id = this.Id;
      
            result.TimeDisplay = this.TimeDisplay;
      
            result.UserHostAddress = this.UserHostAddress;
      
            result.ParentReferralUri = this.ParentReferralUri;
      
            result.ReferralUri = this.ReferralUri;
      
            result.SessionIdUid = this.SessionIdUid;
      
            result.TrackingPhoneId = this.TrackingPhoneId;
      
            result.PhoneCallId = this.PhoneCallId;
      
            result.PhoneSmsId = this.PhoneSmsId;
      
            result.LeadFormId = this.LeadFormId;
      
            result.LeadSourceId = this.LeadSourceId;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new TrackingPhoneRotation();
      
            this.Id = value["Id"];
      
            this.TimeDisplay = value["TimeDisplay"];
      
            this.UserHostAddress = value["UserHostAddress"];
      
            this.ParentReferralUri = value["ParentReferralUri"];
      
            this.ReferralUri = value["ReferralUri"];
      
            this.SessionIdUid = value["SessionIdUid"];
      
            this.TrackingPhoneId = value["TrackingPhoneId"];
      
            this.PhoneCallId = value["PhoneCallId"];
      
            this.PhoneSmsId = value["PhoneSmsId"];
      
            this.LeadFormId = value["LeadFormId"];
      
            this.LeadSourceId = value["LeadSourceId"];
      
        }
    }
}
      
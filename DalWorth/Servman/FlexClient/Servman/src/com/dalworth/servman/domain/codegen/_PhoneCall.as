
package com.dalworth.servman.domain.codegen
{
    import com.dalworth.servman.domain.*;
    
    [Bindable]
    public class _PhoneCall implements IDomainEntity
    {
        public function _PhoneCall()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var toPhoneId:int;
        public function get ToPhoneId():int { return toPhoneId; }
        public function set ToPhoneId(value:int):void 
        {
            toPhoneId = value;
        }
      
        private var isAnsweredByUser:Boolean;
        public function get IsAnsweredByUser():Boolean { return isAnsweredByUser; }
        public function set IsAnsweredByUser(value:Boolean):void 
        {
            isAnsweredByUser = value;
        }
      
        private var answeredByUserId:int;
        public function get AnsweredByUserId():int { return answeredByUserId; }
        public function set AnsweredByUserId(value:int):void 
        {
            answeredByUserId = value;
        }
      
        private var callSid:String;
        public function get CallSid():String { return callSid; }
        public function set CallSid(value:String):void 
        {
            callSid = value;
        }
      
        private var accountSid:String;
        public function get AccountSid():String { return accountSid; }
        public function set AccountSid(value:String):void 
        {
            accountSid = value;
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
      
        private var callStatus:String;
        public function get CallStatus():String { return callStatus; }
        public function set CallStatus(value:String):void 
        {
            callStatus = value;
        }
      
        private var apiVersion:String;
        public function get ApiVersion():String { return apiVersion; }
        public function set ApiVersion(value:String):void 
        {
            apiVersion = value;
        }
      
        private var direction:String;
        public function get Direction():String { return direction; }
        public function set Direction(value:String):void 
        {
            direction = value;
        }
      
        private var forwardedFrom:String;
        public function get ForwardedFrom():String { return forwardedFrom; }
        public function set ForwardedFrom(value:String):void 
        {
            forwardedFrom = value;
        }
      
        private var fromCity:String;
        public function get FromCity():String { return fromCity; }
        public function set FromCity(value:String):void 
        {
            fromCity = value;
        }
      
        private var fromState:String;
        public function get FromState():String { return fromState; }
        public function set FromState(value:String):void 
        {
            fromState = value;
        }
      
        private var fromZip:String;
        public function get FromZip():String { return fromZip; }
        public function set FromZip(value:String):void 
        {
            fromZip = value;
        }
      
        private var fromCountry:String;
        public function get FromCountry():String { return fromCountry; }
        public function set FromCountry(value:String):void 
        {
            fromCountry = value;
        }
      
        private var toCity:String;
        public function get ToCity():String { return toCity; }
        public function set ToCity(value:String):void 
        {
            toCity = value;
        }
      
        private var toState:String;
        public function get ToState():String { return toState; }
        public function set ToState(value:String):void 
        {
            toState = value;
        }
      
        private var toZip:String;
        public function get ToZip():String { return toZip; }
        public function set ToZip(value:String):void 
        {
            toZip = value;
        }
      
        private var toCountry:String;
        public function get ToCountry():String { return toCountry; }
        public function set ToCountry(value:String):void 
        {
            toCountry = value;
        }
      
        private var callDuration:String;
        public function get CallDuration():String { return callDuration; }
        public function set CallDuration(value:String):void 
        {
            callDuration = value;
        }
      
        private var recordingUrl:String;
        public function get RecordingUrl():String { return recordingUrl; }
        public function set RecordingUrl(value:String):void 
        {
            recordingUrl = value;
        }
      
        private var callerName:String;
        public function get CallerName():String { return callerName; }
        public function set CallerName(value:String):void 
        {
            callerName = value;
        }
      
        private var businessPartnerId:int;
        public function get BusinessPartnerId():int { return businessPartnerId; }
        public function set BusinessPartnerId(value:int):void 
        {
            businessPartnerId = value;
        }
      
        private var salesRepId:int;
        public function get SalesRepId():int { return salesRepId; }
        public function set SalesRepId(value:int):void 
        {
            salesRepId = value;
        }
      
        private var dateCreated:Date;
        public function get DateCreated():Date { return dateCreated; }
        public function set DateCreated(value:Date):void 
        {
            dateCreated = value;
        }
      

        public function prepareToSend():PhoneCall
        {
            var result:PhoneCall = new PhoneCall();
      
            result.Id = this.Id;
      
            result.ToPhoneId = this.ToPhoneId;
      
            result.IsAnsweredByUser = this.IsAnsweredByUser;
      
            result.AnsweredByUserId = this.AnsweredByUserId;
      
            result.CallSid = this.CallSid;
      
            result.AccountSid = this.AccountSid;
      
            result.PhoneFrom = this.PhoneFrom;
      
            result.PhoneTo = this.PhoneTo;
      
            result.CallStatus = this.CallStatus;
      
            result.ApiVersion = this.ApiVersion;
      
            result.Direction = this.Direction;
      
            result.ForwardedFrom = this.ForwardedFrom;
      
            result.FromCity = this.FromCity;
      
            result.FromState = this.FromState;
      
            result.FromZip = this.FromZip;
      
            result.FromCountry = this.FromCountry;
      
            result.ToCity = this.ToCity;
      
            result.ToState = this.ToState;
      
            result.ToZip = this.ToZip;
      
            result.ToCountry = this.ToCountry;
      
            result.CallDuration = this.CallDuration;
      
            result.RecordingUrl = this.RecordingUrl;
      
            result.CallerName = this.CallerName;
      
            result.BusinessPartnerId = this.BusinessPartnerId;
      
            result.SalesRepId = this.SalesRepId;
      
            result.DateCreated = this.DateCreated;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new PhoneCall();
      
            this.Id = value["Id"];
      
            this.ToPhoneId = value["ToPhoneId"];
      
            this.IsAnsweredByUser = value["IsAnsweredByUser"];
      
            this.AnsweredByUserId = value["AnsweredByUserId"];
      
            this.CallSid = value["CallSid"];
      
            this.AccountSid = value["AccountSid"];
      
            this.PhoneFrom = value["PhoneFrom"];
      
            this.PhoneTo = value["PhoneTo"];
      
            this.CallStatus = value["CallStatus"];
      
            this.ApiVersion = value["ApiVersion"];
      
            this.Direction = value["Direction"];
      
            this.ForwardedFrom = value["ForwardedFrom"];
      
            this.FromCity = value["FromCity"];
      
            this.FromState = value["FromState"];
      
            this.FromZip = value["FromZip"];
      
            this.FromCountry = value["FromCountry"];
      
            this.ToCity = value["ToCity"];
      
            this.ToState = value["ToState"];
      
            this.ToZip = value["ToZip"];
      
            this.ToCountry = value["ToCountry"];
      
            this.CallDuration = value["CallDuration"];
      
            this.RecordingUrl = value["RecordingUrl"];
      
            this.CallerName = value["CallerName"];
      
            this.BusinessPartnerId = value["BusinessPartnerId"];
      
            this.SalesRepId = value["SalesRepId"];
      
            this.DateCreated = value["DateCreated"];
      
        }
    }
}
      
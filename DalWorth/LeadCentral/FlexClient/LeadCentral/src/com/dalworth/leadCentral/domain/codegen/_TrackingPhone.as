
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _TrackingPhone implements IDomainEntity
    {
        public function _TrackingPhone()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var number:String;
        public function get Number():String { return number; }
        public function set Number(value:String):void 
        {
            number = value;
        }
      
        private var twilioId:String;
        public function get TwilioId():String { return twilioId; }
        public function set TwilioId(value:String):void 
        {
            twilioId = value;
        }
      
        private var description:String;
        public function get Description():String { return description; }
        public function set Description(value:String):void 
        {
            description = value;
        }
      
        private var isTollFree:Boolean;
        public function get IsTollFree():Boolean { return isTollFree; }
        public function set IsTollFree(value:Boolean):void 
        {
            isTollFree = value;
        }
      
        private var isSuspended:Boolean;
        public function get IsSuspended():Boolean { return isSuspended; }
        public function set IsSuspended(value:Boolean):void 
        {
            isSuspended = value;
        }
      
        private var isRemoved:Boolean;
        public function get IsRemoved():Boolean { return isRemoved; }
        public function set IsRemoved(value:Boolean):void 
        {
            isRemoved = value;
        }
      
        private var screenNumber:String;
        public function get ScreenNumber():String { return screenNumber; }
        public function set ScreenNumber(value:String):void 
        {
            screenNumber = value;
        }
      
        private var timeLastDisplay:Date;
        public function get TimeLastDisplay():Date { return timeLastDisplay; }
        public function set TimeLastDisplay(value:Date):void 
        {
            timeLastDisplay = value;
        }
      
        private var smsResponse:String;
        public function get SmsResponse():String { return smsResponse; }
        public function set SmsResponse(value:String):void 
        {
            smsResponse = value;
        }
      
        private var denyTranscription:Boolean;
        public function get DenyTranscription():Boolean { return denyTranscription; }
        public function set DenyTranscription(value:Boolean):void 
        {
            denyTranscription = value;
        }
      
        private var denyCallerId:Boolean;
        public function get DenyCallerId():Boolean { return denyCallerId; }
        public function set DenyCallerId(value:Boolean):void 
        {
            denyCallerId = value;
        }
      

        public function prepareToSend():TrackingPhone
        {
            var result:TrackingPhone = new TrackingPhone();
      
            result.Id = this.Id;
      
            result.Number = this.Number;
      
            result.TwilioId = this.TwilioId;
      
            result.Description = this.Description;
      
            result.IsTollFree = this.IsTollFree;
      
            result.IsSuspended = this.IsSuspended;
      
            result.IsRemoved = this.IsRemoved;
      
            result.ScreenNumber = this.ScreenNumber;
      
            result.TimeLastDisplay = this.TimeLastDisplay;
      
            result.SmsResponse = this.SmsResponse;
      
            result.DenyTranscription = this.DenyTranscription;
      
            result.DenyCallerId = this.DenyCallerId;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new TrackingPhone();
      
            this.Id = value["Id"];
      
            this.Number = value["Number"];
      
            this.TwilioId = value["TwilioId"];
      
            this.Description = value["Description"];
      
            this.IsTollFree = value["IsTollFree"];
      
            this.IsSuspended = value["IsSuspended"];
      
            this.IsRemoved = value["IsRemoved"];
      
            this.ScreenNumber = value["ScreenNumber"];
      
            this.TimeLastDisplay = value["TimeLastDisplay"];
      
            this.SmsResponse = value["SmsResponse"];
      
            this.DenyTranscription = value["DenyTranscription"];
      
            this.DenyCallerId = value["DenyCallerId"];
      
        }
    }
}
      
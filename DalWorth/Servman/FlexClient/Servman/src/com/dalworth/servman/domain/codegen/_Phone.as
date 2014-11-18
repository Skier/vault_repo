
package com.dalworth.servman.domain.codegen
{
    import com.dalworth.servman.domain.*;
    
    [Bindable]
    public class _Phone implements IDomainEntity
    {
        public function _Phone()
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
      
        private var incomingCallUrl:String;
        public function get IncomingCallUrl():String { return incomingCallUrl; }
        public function set IncomingCallUrl(value:String):void 
        {
            incomingCallUrl = value;
        }
      
        private var incomingSmsUrl:String;
        public function get IncomingSmsUrl():String { return incomingSmsUrl; }
        public function set IncomingSmsUrl(value:String):void 
        {
            incomingSmsUrl = value;
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
      

        public function prepareToSend():Phone
        {
            var result:Phone = new Phone();
      
            result.Id = this.Id;
      
            result.Number = this.Number;
      
            result.TwilioId = this.TwilioId;
      
            result.IncomingCallUrl = this.IncomingCallUrl;
      
            result.IncomingSmsUrl = this.IncomingSmsUrl;
      
            result.Description = this.Description;
      
            result.IsTollFree = this.IsTollFree;
      
            result.IsSuspended = this.IsSuspended;
      
            result.IsRemoved = this.IsRemoved;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new Phone();
      
            this.Id = value["Id"];
      
            this.Number = value["Number"];
      
            this.TwilioId = value["TwilioId"];
      
            this.IncomingCallUrl = value["IncomingCallUrl"];
      
            this.IncomingSmsUrl = value["IncomingSmsUrl"];
      
            this.Description = value["Description"];
      
            this.IsTollFree = value["IsTollFree"];
      
            this.IsSuspended = value["IsSuspended"];
      
            this.IsRemoved = value["IsRemoved"];
      
        }
    }
}
      
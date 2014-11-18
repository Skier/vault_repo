
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _LeadSourcePhone implements IDomainEntity
    {
        public function _LeadSourcePhone()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var leadSourceId:int;
        public function get LeadSourceId():int { return leadSourceId; }
        public function set LeadSourceId(value:int):void 
        {
            leadSourceId = value;
        }
      
        private var phoneNumber:String;
        public function get PhoneNumber():String { return phoneNumber; }
        public function set PhoneNumber(value:String):void 
        {
            phoneNumber = value;
        }
      
        private var simplePhoneNumber:String;
        public function get SimplePhoneNumber():String { return simplePhoneNumber; }
        public function set SimplePhoneNumber(value:String):void 
        {
            simplePhoneNumber = value;
        }
      
        private var description:String;
        public function get Description():String { return description; }
        public function set Description(value:String):void 
        {
            description = value;
        }
      
        private var isRemoved:Boolean;
        public function get IsRemoved():Boolean { return isRemoved; }
        public function set IsRemoved(value:Boolean):void 
        {
            isRemoved = value;
        }
      

        public function prepareToSend():LeadSourcePhone
        {
            var result:LeadSourcePhone = new LeadSourcePhone();
      
            result.Id = this.Id;
      
            result.LeadSourceId = this.LeadSourceId;
      
            result.PhoneNumber = this.PhoneNumber;
      
            result.SimplePhoneNumber = this.SimplePhoneNumber;
      
            result.Description = this.Description;
      
            result.IsRemoved = this.IsRemoved;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new LeadSourcePhone();
      
            this.Id = value["Id"];
      
            this.LeadSourceId = value["LeadSourceId"];
      
            this.PhoneNumber = value["PhoneNumber"];
      
            this.SimplePhoneNumber = value["SimplePhoneNumber"];
      
            this.Description = value["Description"];
      
            this.IsRemoved = value["IsRemoved"];
      
        }
    }
}
      
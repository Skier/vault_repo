
package com.dalworth.servman.domain.codegen
{
    import com.dalworth.servman.domain.*;
    
    [Bindable]
    public class _PhoneToBusinessPartner implements IDomainEntity
    {
        public function _PhoneToBusinessPartner()
        {
        }
      
        private var phoneId:int;
        public function get PhoneId():int { return phoneId; }
        public function set PhoneId(value:int):void 
        {
            phoneId = value;
        }
      
        private var businessPartnerId:int;
        public function get BusinessPartnerId():int { return businessPartnerId; }
        public function set BusinessPartnerId(value:int):void 
        {
            businessPartnerId = value;
        }
      
        private var notes:String;
        public function get Notes():String { return notes; }
        public function set Notes(value:String):void 
        {
            notes = value;
        }
      
        private var isIncoming:Boolean;
        public function get IsIncoming():Boolean { return isIncoming; }
        public function set IsIncoming(value:Boolean):void 
        {
            isIncoming = value;
        }
      

        public function prepareToSend():PhoneToBusinessPartner
        {
            var result:PhoneToBusinessPartner = new PhoneToBusinessPartner();
      
            result.PhoneId = this.PhoneId;
      
            result.BusinessPartnerId = this.BusinessPartnerId;
      
            result.Notes = this.Notes;
      
            result.IsIncoming = this.IsIncoming;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new PhoneToBusinessPartner();
      
            this.PhoneId = value["PhoneId"];
      
            this.BusinessPartnerId = value["BusinessPartnerId"];
      
            this.Notes = value["Notes"];
      
            this.IsIncoming = value["IsIncoming"];
      
        }
    }
}
      
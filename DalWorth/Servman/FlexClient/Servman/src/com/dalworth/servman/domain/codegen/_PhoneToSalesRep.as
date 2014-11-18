
package com.dalworth.servman.domain.codegen
{
    import com.dalworth.servman.domain.*;
    
    [Bindable]
    public class _PhoneToSalesRep implements IDomainEntity
    {
        public function _PhoneToSalesRep()
        {
        }
      
        private var phoneId:int;
        public function get PhoneId():int { return phoneId; }
        public function set PhoneId(value:int):void 
        {
            phoneId = value;
        }
      
        private var salesRepId:int;
        public function get SalesRepId():int { return salesRepId; }
        public function set SalesRepId(value:int):void 
        {
            salesRepId = value;
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
      

        public function prepareToSend():PhoneToSalesRep
        {
            var result:PhoneToSalesRep = new PhoneToSalesRep();
      
            result.PhoneId = this.PhoneId;
      
            result.SalesRepId = this.SalesRepId;
      
            result.Notes = this.Notes;
      
            result.IsIncoming = this.IsIncoming;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new PhoneToSalesRep();
      
            this.PhoneId = value["PhoneId"];
      
            this.SalesRepId = value["SalesRepId"];
      
            this.Notes = value["Notes"];
      
            this.IsIncoming = value["IsIncoming"];
      
        }
    }
}
      
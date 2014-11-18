
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _ApplicationStatus implements IDomainEntity
    {
        public function _ApplicationStatus()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var servmanCustomerId:int;
        public function get ServmanCustomerId():int { return servmanCustomerId; }
        public function set ServmanCustomerId(value:int):void 
        {
            servmanCustomerId = value;
        }
      
        private var billingStatus:String;
        public function get BillingStatus():String { return billingStatus; }
        public function set BillingStatus(value:String):void 
        {
            billingStatus = value;
        }
      
        private var lastPaymentDate:Date;
        public function get LastPaymentDate():Date { return lastPaymentDate; }
        public function set LastPaymentDate(value:Date):void 
        {
            lastPaymentDate = value;
        }
      

        public function prepareToSend():ApplicationStatus
        {
            var result:ApplicationStatus = new ApplicationStatus();
      
            result.Id = this.Id;
      
            result.ServmanCustomerId = this.ServmanCustomerId;
      
            result.BillingStatus = this.BillingStatus;
      
            result.LastPaymentDate = this.LastPaymentDate;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new ApplicationStatus();
      
            this.Id = value["Id"];
      
            this.ServmanCustomerId = value["ServmanCustomerId"];
      
            this.BillingStatus = value["BillingStatus"];
      
            this.LastPaymentDate = value["LastPaymentDate"];
      
        }
    }
}
      
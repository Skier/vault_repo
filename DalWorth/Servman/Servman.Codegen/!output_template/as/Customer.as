
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.Customer")]
    public class Customer
    {
        public function Customer()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var businessPartnerId:int;
        public function get BusinessPartnerId():int { return businessPartnerId; }
        public function set BusinessPartnerId(value:int):void 
        {
            businessPartnerId = value;
        }
      
        private var qbCustomerId:String;
        public function get QbCustomerId():String { return qbCustomerId; }
        public function set QbCustomerId(value:String):void 
        {
            qbCustomerId = value;
        }
      

        public function clone():Customer
        {
            var result:Customer = new Customer();
      
            result.Id = this.Id;
      
            result.BusinessPartnerId = this.BusinessPartnerId;
      
            result.QbCustomerId = this.QbCustomerId;
      
            return result;
        }

        public function updateFields(value:Customer):void 
        {
            if (value == null)
                value = new Customer();
      
            this.Id = value.Id;
      
            this.BusinessPartnerId = value.BusinessPartnerId;
      
            this.QbCustomerId = value.QbCustomerId;
      
        }
    }
}
      
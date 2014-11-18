
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.Customer")]
    public class Customer
    {
        public function Customer()
        {
        }
      
        private var _id:int;
        public function get id():int { return _id; }
        public function set id(value:int):void 
        {
            _id = value;
        }
      
        private var _businessPartnerId:int;
        public function get businessPartnerId():int { return _businessPartnerId; }
        public function set businessPartnerId(value:int):void 
        {
            _businessPartnerId = value;
        }
      
        private var _qbCustomerId:String;
        public function get qbCustomerId():String { return _qbCustomerId; }
        public function set qbCustomerId(value:String):void 
        {
            _qbCustomerId = value;
        }
      
    }
}
      
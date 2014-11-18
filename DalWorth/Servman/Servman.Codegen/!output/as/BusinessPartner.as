
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.BusinessPartner")]
    public class BusinessPartner
    {
        public function BusinessPartner()
        {
        }
      
        private var _id:int;
        public function get id():int { return _id; }
        public function set id(value:int):void 
        {
            _id = value;
        }
      
        private var _vendorId:int;
        public function get vendorId():int { return _vendorId; }
        public function set vendorId(value:int):void 
        {
            _vendorId = value;
        }
      
        private var _employeeId:int;
        public function get employeeId():int { return _employeeId; }
        public function set employeeId(value:int):void 
        {
            _employeeId = value;
        }
      
        private var _name:String;
        public function get name():String { return _name; }
        public function set name(value:String):void 
        {
            _name = value;
        }
      
        private var _address:String;
        public function get address():String { return _address; }
        public function set address(value:String):void 
        {
            _address = value;
        }
      
        private var _email:String;
        public function get email():String { return _email; }
        public function set email(value:String):void 
        {
            _email = value;
        }
      
        private var _phone:String;
        public function get phone():String { return _phone; }
        public function set phone(value:String):void 
        {
            _phone = value;
        }
      
    }
}
      
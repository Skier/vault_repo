
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.Lead")]
    public class Lead
    {
        public function Lead()
        {
        }
      
        private var _id:int;
        public function get id():int { return _id; }
        public function set id(value:int):void 
        {
            _id = value;
        }
      
        private var _leadStatusId:int;
        public function get leadStatusId():int { return _leadStatusId; }
        public function set leadStatusId(value:int):void 
        {
            _leadStatusId = value;
        }
      
        private var _businessPartnerId:int;
        public function get businessPartnerId():int { return _businessPartnerId; }
        public function set businessPartnerId(value:int):void 
        {
            _businessPartnerId = value;
        }
      
        private var _projectTypeId:int;
        public function get projectTypeId():int { return _projectTypeId; }
        public function set projectTypeId(value:int):void 
        {
            _projectTypeId = value;
        }
      
        private var _employeeId:int;
        public function get employeeId():int { return _employeeId; }
        public function set employeeId(value:int):void 
        {
            _employeeId = value;
        }
      
        private var _firstName:String;
        public function get firstName():String { return _firstName; }
        public function set firstName(value:String):void 
        {
            _firstName = value;
        }
      
        private var _lastName:String;
        public function get lastName():String { return _lastName; }
        public function set lastName(value:String):void 
        {
            _lastName = value;
        }
      
        private var _phone:String;
        public function get phone():String { return _phone; }
        public function set phone(value:String):void 
        {
            _phone = value;
        }
      
        private var _customerNotes:String;
        public function get customerNotes():String { return _customerNotes; }
        public function set customerNotes(value:String):void 
        {
            _customerNotes = value;
        }
      
        private var _employeeNotes:String;
        public function get employeeNotes():String { return _employeeNotes; }
        public function set employeeNotes(value:String):void 
        {
            _employeeNotes = value;
        }
      
    }
}
      

package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.LeadToPartner")]
    public class LeadToPartner
    {
        public function LeadToPartner()
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
      
        private var _partnerProjectTypeId:int;
        public function get partnerProjectTypeId():int { return _partnerProjectTypeId; }
        public function set partnerProjectTypeId(value:int):void 
        {
            _partnerProjectTypeId = value;
        }
      
        private var _closedAmount:Number;
        public function get closedAmount():Number { return _closedAmount; }
        public function set closedAmount(value:Number):void 
        {
            _closedAmount = value;
        }
      
        private var _commissionAmount:Number;
        public function get commissionAmount():Number { return _commissionAmount; }
        public function set commissionAmount(value:Number):void 
        {
            _commissionAmount = value;
        }
      
    }
}
      

package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.LeadToPartner")]
    public class LeadToPartner
    {
        public function LeadToPartner()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var leadStatusId:int;
        public function get LeadStatusId():int { return leadStatusId; }
        public function set LeadStatusId(value:int):void 
        {
            leadStatusId = value;
        }
      
        private var businessPartnerId:int;
        public function get BusinessPartnerId():int { return businessPartnerId; }
        public function set BusinessPartnerId(value:int):void 
        {
            businessPartnerId = value;
        }
      
        private var firstName:String;
        public function get FirstName():String { return firstName; }
        public function set FirstName(value:String):void 
        {
            firstName = value;
        }
      
        private var lastName:String;
        public function get LastName():String { return lastName; }
        public function set LastName(value:String):void 
        {
            lastName = value;
        }
      
        private var phone:String;
        public function get Phone():String { return phone; }
        public function set Phone(value:String):void 
        {
            phone = value;
        }
      
        private var customerNotes:String;
        public function get CustomerNotes():String { return customerNotes; }
        public function set CustomerNotes(value:String):void 
        {
            customerNotes = value;
        }
      
        private var employeeNotes:String;
        public function get EmployeeNotes():String { return employeeNotes; }
        public function set EmployeeNotes(value:String):void 
        {
            employeeNotes = value;
        }
      
        private var partnerProjectTypeId:int;
        public function get PartnerProjectTypeId():int { return partnerProjectTypeId; }
        public function set PartnerProjectTypeId(value:int):void 
        {
            partnerProjectTypeId = value;
        }
      
        private var closedAmount:Number;
        public function get ClosedAmount():Number { return closedAmount; }
        public function set ClosedAmount(value:Number):void 
        {
            closedAmount = value;
        }
      
        private var commissionAmount:Number;
        public function get CommissionAmount():Number { return commissionAmount; }
        public function set CommissionAmount(value:Number):void 
        {
            commissionAmount = value;
        }
      

        public function clone():LeadToPartner
        {
            var result:LeadToPartner = new LeadToPartner();
      
            result.Id = this.Id;
      
            result.LeadStatusId = this.LeadStatusId;
      
            result.BusinessPartnerId = this.BusinessPartnerId;
      
            result.FirstName = this.FirstName;
      
            result.LastName = this.LastName;
      
            result.Phone = this.Phone;
      
            result.CustomerNotes = this.CustomerNotes;
      
            result.EmployeeNotes = this.EmployeeNotes;
      
            result.PartnerProjectTypeId = this.PartnerProjectTypeId;
      
            result.ClosedAmount = this.ClosedAmount;
      
            result.CommissionAmount = this.CommissionAmount;
      
            return result;
        }

        public function updateFields(value:LeadToPartner):void 
        {
            if (value == null)
                value = new LeadToPartner();
      
            this.Id = value.Id;
      
            this.LeadStatusId = value.LeadStatusId;
      
            this.BusinessPartnerId = value.BusinessPartnerId;
      
            this.FirstName = value.FirstName;
      
            this.LastName = value.LastName;
      
            this.Phone = value.Phone;
      
            this.CustomerNotes = value.CustomerNotes;
      
            this.EmployeeNotes = value.EmployeeNotes;
      
            this.PartnerProjectTypeId = value.PartnerProjectTypeId;
      
            this.ClosedAmount = value.ClosedAmount;
      
            this.CommissionAmount = value.CommissionAmount;
      
        }
    }
}
      
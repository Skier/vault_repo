
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.Lead")]
    public class Lead
    {
        public function Lead()
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
      
        private var projectTypeId:int;
        public function get ProjectTypeId():int { return projectTypeId; }
        public function set ProjectTypeId(value:int):void 
        {
            projectTypeId = value;
        }
      
        private var employeeId:int;
        public function get EmployeeId():int { return employeeId; }
        public function set EmployeeId(value:int):void 
        {
            employeeId = value;
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
      
        private var customerId:int;
        public function get CustomerId():int { return customerId; }
        public function set CustomerId(value:int):void 
        {
            customerId = value;
        }
      

        public function clone():Lead
        {
            var result:Lead = new Lead();
      
            result.Id = this.Id;
      
            result.LeadStatusId = this.LeadStatusId;
      
            result.BusinessPartnerId = this.BusinessPartnerId;
      
            result.ProjectTypeId = this.ProjectTypeId;
      
            result.EmployeeId = this.EmployeeId;
      
            result.FirstName = this.FirstName;
      
            result.LastName = this.LastName;
      
            result.Phone = this.Phone;
      
            result.CustomerNotes = this.CustomerNotes;
      
            result.EmployeeNotes = this.EmployeeNotes;
      
            result.CustomerId = this.CustomerId;
      
            return result;
        }

        public function updateFields(value:Lead):void 
        {
            if (value == null)
                value = new Lead();
      
            this.Id = value.Id;
      
            this.LeadStatusId = value.LeadStatusId;
      
            this.BusinessPartnerId = value.BusinessPartnerId;
      
            this.ProjectTypeId = value.ProjectTypeId;
      
            this.EmployeeId = value.EmployeeId;
      
            this.FirstName = value.FirstName;
      
            this.LastName = value.LastName;
      
            this.Phone = value.Phone;
      
            this.CustomerNotes = value.CustomerNotes;
      
            this.EmployeeNotes = value.EmployeeNotes;
      
            this.CustomerId = value.CustomerId;
      
        }
    }
}
      
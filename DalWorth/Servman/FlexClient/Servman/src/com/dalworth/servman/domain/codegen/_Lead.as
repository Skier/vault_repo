
package com.dalworth.servman.domain.codegen
{
    import com.dalworth.servman.domain.*;
    
    [Bindable]
    public class _Lead implements IDomainEntity
    {
        public function _Lead()
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
      
        private var salesRepId:int;
        public function get SalesRepId():int { return salesRepId; }
        public function set SalesRepId(value:int):void 
        {
            salesRepId = value;
        }
      
        private var businessPartnerId:int;
        public function get BusinessPartnerId():int { return businessPartnerId; }
        public function set BusinessPartnerId(value:int):void 
        {
            businessPartnerId = value;
        }
      
        private var assignedToUser:int;
        public function get AssignedToUser():int { return assignedToUser; }
        public function set AssignedToUser(value:int):void 
        {
            assignedToUser = value;
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
      
        private var address:String;
        public function get Address():String { return address; }
        public function set Address(value:String):void 
        {
            address = value;
        }
      
        private var customerNotes:String;
        public function get CustomerNotes():String { return customerNotes; }
        public function set CustomerNotes(value:String):void 
        {
            customerNotes = value;
        }
      
        private var createdByUserId:int;
        public function get CreatedByUserId():int { return createdByUserId; }
        public function set CreatedByUserId(value:int):void 
        {
            createdByUserId = value;
        }
      
        private var dateCreated:Date;
        public function get DateCreated():Date { return dateCreated; }
        public function set DateCreated(value:Date):void 
        {
            dateCreated = value;
        }
      
        private var dateContacted:Date;
        public function get DateContacted():Date { return dateContacted; }
        public function set DateContacted(value:Date):void 
        {
            dateContacted = value;
        }
      
        private var isImportant:Boolean;
        public function get IsImportant():Boolean { return isImportant; }
        public function set IsImportant(value:Boolean):void 
        {
            isImportant = value;
        }
      
        private var phoneCallId:int;
        public function get PhoneCallId():int { return phoneCallId; }
        public function set PhoneCallId(value:int):void 
        {
            phoneCallId = value;
        }
      
        private var leadTypeId:int;
        public function get LeadTypeId():int { return leadTypeId; }
        public function set LeadTypeId(value:int):void 
        {
            leadTypeId = value;
        }
      
        private var dateLastUpdated:Date;
        public function get DateLastUpdated():Date { return dateLastUpdated; }
        public function set DateLastUpdated(value:Date):void 
        {
            dateLastUpdated = value;
        }
      

        public function prepareToSend():Lead
        {
            var result:Lead = new Lead();
      
            result.Id = this.Id;
      
            result.LeadStatusId = this.LeadStatusId;
      
            result.SalesRepId = this.SalesRepId;
      
            result.BusinessPartnerId = this.BusinessPartnerId;
      
            result.AssignedToUser = this.AssignedToUser;
      
            result.FirstName = this.FirstName;
      
            result.LastName = this.LastName;
      
            result.Phone = this.Phone;
      
            result.Address = this.Address;
      
            result.CustomerNotes = this.CustomerNotes;
      
            result.CreatedByUserId = this.CreatedByUserId;
      
            result.DateCreated = this.DateCreated;
      
            result.DateContacted = this.DateContacted;
      
            result.IsImportant = this.IsImportant;
      
            result.PhoneCallId = this.PhoneCallId;
      
            result.LeadTypeId = this.LeadTypeId;
      
            result.DateLastUpdated = this.DateLastUpdated;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new Lead();
      
            this.Id = value["Id"];
      
            this.LeadStatusId = value["LeadStatusId"];
      
            this.SalesRepId = value["SalesRepId"];
      
            this.BusinessPartnerId = value["BusinessPartnerId"];
      
            this.AssignedToUser = value["AssignedToUser"];
      
            this.FirstName = value["FirstName"];
      
            this.LastName = value["LastName"];
      
            this.Phone = value["Phone"];
      
            this.Address = value["Address"];
      
            this.CustomerNotes = value["CustomerNotes"];
      
            this.CreatedByUserId = value["CreatedByUserId"];
      
            this.DateCreated = value["DateCreated"];
      
            this.DateContacted = value["DateContacted"];
      
            this.IsImportant = value["IsImportant"];
      
            this.PhoneCallId = value["PhoneCallId"];
      
            this.LeadTypeId = value["LeadTypeId"];
      
            this.DateLastUpdated = value["DateLastUpdated"];
      
        }
    }
}
      
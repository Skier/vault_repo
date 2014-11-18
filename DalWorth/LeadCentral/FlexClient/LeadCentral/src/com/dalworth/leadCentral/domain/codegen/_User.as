
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _User implements IDomainEntity
    {
        public function _User()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var qbUserId:String;
        public function get QbUserId():String { return qbUserId; }
        public function set QbUserId(value:String):void 
        {
            qbUserId = value;
        }
      
        private var email:String;
        public function get Email():String { return email; }
        public function set Email(value:String):void 
        {
            email = value;
        }
      
        private var name:String;
        public function get Name():String { return name; }
        public function set Name(value:String):void 
        {
            name = value;
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
      
        private var photoFileId:int;
        public function get PhotoFileId():int { return photoFileId; }
        public function set PhotoFileId(value:int):void 
        {
            photoFileId = value;
        }
      
        private var isActive:Boolean;
        public function get IsActive():Boolean { return isActive; }
        public function set IsActive(value:Boolean):void 
        {
            isActive = value;
        }
      
        private var qbEmployeeRecordId:String;
        public function get QbEmployeeRecordId():String { return qbEmployeeRecordId; }
        public function set QbEmployeeRecordId(value:String):void 
        {
            qbEmployeeRecordId = value;
        }
      
        private var qbVendorRecordId:String;
        public function get QbVendorRecordId():String { return qbVendorRecordId; }
        public function set QbVendorRecordId(value:String):void 
        {
            qbVendorRecordId = value;
        }
      
        private var qbSalesRepRecordId:String;
        public function get QbSalesRepRecordId():String { return qbSalesRepRecordId; }
        public function set QbSalesRepRecordId(value:String):void 
        {
            qbSalesRepRecordId = value;
        }
      
        private var roleName:String;
        public function get RoleName():String { return roleName; }
        public function set RoleName(value:String):void 
        {
            roleName = value;
        }
      
        private var dateLastAccess:Date;
        public function get DateLastAccess():Date { return dateLastAccess; }
        public function set DateLastAccess(value:Date):void 
        {
            dateLastAccess = value;
        }
      

        public function prepareToSend():User
        {
            var result:User = new User();
      
            result.Id = this.Id;
      
            result.QbUserId = this.QbUserId;
      
            result.Email = this.Email;
      
            result.Name = this.Name;
      
            result.FirstName = this.FirstName;
      
            result.LastName = this.LastName;
      
            result.Phone = this.Phone;
      
            result.Address = this.Address;
      
            result.PhotoFileId = this.PhotoFileId;
      
            result.IsActive = this.IsActive;
      
            result.QbEmployeeRecordId = this.QbEmployeeRecordId;
      
            result.QbVendorRecordId = this.QbVendorRecordId;
      
            result.QbSalesRepRecordId = this.QbSalesRepRecordId;
      
            result.RoleName = this.RoleName;
      
            result.DateLastAccess = this.DateLastAccess;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new User();
      
            this.Id = value["Id"];
      
            this.QbUserId = value["QbUserId"];
      
            this.Email = value["Email"];
      
            this.Name = value["Name"];
      
            this.FirstName = value["FirstName"];
      
            this.LastName = value["LastName"];
      
            this.Phone = value["Phone"];
      
            this.Address = value["Address"];
      
            this.PhotoFileId = value["PhotoFileId"];
      
            this.IsActive = value["IsActive"];
      
            this.QbEmployeeRecordId = value["QbEmployeeRecordId"];
      
            this.QbVendorRecordId = value["QbVendorRecordId"];
      
            this.QbSalesRepRecordId = value["QbSalesRepRecordId"];
      
            this.RoleName = value["RoleName"];
      
            this.DateLastAccess = value["DateLastAccess"];
      
        }
    }
}
      

package com.dalworth.servman.domain.codegen
{
    import com.dalworth.servman.domain.*;
    
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
      
        }
    }
}
      
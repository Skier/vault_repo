
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.BusinessPartner")]
    public class BusinessPartner
    {
        public function BusinessPartner()
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
      
        private var name:String;
        public function get Name():String { return name; }
        public function set Name(value:String):void 
        {
            name = value;
        }
      
        private var address:String;
        public function get Address():String { return address; }
        public function set Address(value:String):void 
        {
            address = value;
        }
      
        private var email:String;
        public function get Email():String { return email; }
        public function set Email(value:String):void 
        {
            email = value;
        }
      
        private var phone:String;
        public function get Phone():String { return phone; }
        public function set Phone(value:String):void 
        {
            phone = value;
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
      
        private var canLogin:Boolean;
        public function get CanLogin():Boolean { return canLogin; }
        public function set CanLogin(value:Boolean):void 
        {
            canLogin = value;
        }
      

        public function clone():BusinessPartner
        {
            var result:BusinessPartner = new BusinessPartner();
      
            result.Id = this.Id;
      
            result.QbUserId = this.QbUserId;
      
            result.Name = this.Name;
      
            result.Address = this.Address;
      
            result.Email = this.Email;
      
            result.Phone = this.Phone;
      
            result.FirstName = this.FirstName;
      
            result.LastName = this.LastName;
      
            result.CanLogin = this.CanLogin;
      
            return result;
        }

        public function updateFields(value:BusinessPartner):void 
        {
            if (value == null)
                value = new BusinessPartner();
      
            this.Id = value.Id;
      
            this.QbUserId = value.QbUserId;
      
            this.Name = value.Name;
      
            this.Address = value.Address;
      
            this.Email = value.Email;
      
            this.Phone = value.Phone;
      
            this.FirstName = value.FirstName;
      
            this.LastName = value.LastName;
      
            this.CanLogin = value.CanLogin;
      
        }
    }
}
      
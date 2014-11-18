
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _ServmanCustomer implements IDomainEntity
    {
        public function _ServmanCustomer()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var realmId:String;
        public function get RealmId():String { return realmId; }
        public function set RealmId(value:String):void 
        {
            realmId = value;
        }
      
        private var creationDate:Date;
        public function get CreationDate():Date { return creationDate; }
        public function set CreationDate(value:Date):void 
        {
            creationDate = value;
        }
      
        private var lastLoginDate:Date;
        public function get LastLoginDate():Date { return lastLoginDate; }
        public function set LastLoginDate(value:Date):void 
        {
            lastLoginDate = value;
        }
      
        private var dbName:String;
        public function get DbName():String { return dbName; }
        public function set DbName(value:String):void 
        {
            dbName = value;
        }
      
        private var login:String;
        public function get Login():String { return login; }
        public function set Login(value:String):void 
        {
            login = value;
        }
      
        private var password:String;
        public function get Password():String { return password; }
        public function set Password(value:String):void 
        {
            password = value;
        }
      
        private var name:String;
        public function get Name():String { return name; }
        public function set Name(value:String):void 
        {
            name = value;
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
      
        private var description:String;
        public function get Description():String { return description; }
        public function set Description(value:String):void 
        {
            description = value;
        }
      
        private var appDbId:String;
        public function get AppDbId():String { return appDbId; }
        public function set AppDbId(value:String):void 
        {
            appDbId = value;
        }
      
        private var isLeadSourcesInited:Boolean;
        public function get IsLeadSourcesInited():Boolean { return isLeadSourcesInited; }
        public function set IsLeadSourcesInited(value:Boolean):void 
        {
            isLeadSourcesInited = value;
        }
      
        private var isOAuthInited:Boolean;
        public function get IsOAuthInited():Boolean { return isOAuthInited; }
        public function set IsOAuthInited(value:Boolean):void 
        {
            isOAuthInited = value;
        }
      
        private var isWorkflowsInited:Boolean;
        public function get IsWorkflowsInited():Boolean { return isWorkflowsInited; }
        public function set IsWorkflowsInited(value:Boolean):void 
        {
            isWorkflowsInited = value;
        }
      

        public function prepareToSend():ServmanCustomer
        {
            var result:ServmanCustomer = new ServmanCustomer();
      
            result.Id = this.Id;
      
            result.RealmId = this.RealmId;
      
            result.CreationDate = this.CreationDate;
      
            result.LastLoginDate = this.LastLoginDate;
      
            result.DbName = this.DbName;
      
            result.Login = this.Login;
      
            result.Password = this.Password;
      
            result.Name = this.Name;
      
            result.Email = this.Email;
      
            result.Phone = this.Phone;
      
            result.Description = this.Description;
      
            result.AppDbId = this.AppDbId;
      
            result.IsLeadSourcesInited = this.IsLeadSourcesInited;
      
            result.IsOAuthInited = this.IsOAuthInited;
      
            result.IsWorkflowsInited = this.IsWorkflowsInited;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new ServmanCustomer();
      
            this.Id = value["Id"];
      
            this.RealmId = value["RealmId"];
      
            this.CreationDate = value["CreationDate"];
      
            this.LastLoginDate = value["LastLoginDate"];
      
            this.DbName = value["DbName"];
      
            this.Login = value["Login"];
      
            this.Password = value["Password"];
      
            this.Name = value["Name"];
      
            this.Email = value["Email"];
      
            this.Phone = value["Phone"];
      
            this.Description = value["Description"];
      
            this.AppDbId = value["AppDbId"];
      
            this.IsLeadSourcesInited = value["IsLeadSourcesInited"];
      
            this.IsOAuthInited = value["IsOAuthInited"];
      
            this.IsWorkflowsInited = value["IsWorkflowsInited"];
      
        }
    }
}
      
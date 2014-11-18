
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _LeadSource implements IDomainEntity
    {
        public function _LeadSource()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var name:String;
        public function get Name():String { return name; }
        public function set Name(value:String):void 
        {
            name = value;
        }
      
        private var userId:int;
        public function get UserId():int { return userId; }
        public function set UserId(value:int):void 
        {
            userId = value;
        }
      
        private var isActive:Boolean;
        public function get IsActive():Boolean { return isActive; }
        public function set IsActive(value:Boolean):void 
        {
            isActive = value;
        }
      
        private var ownedByUserId:int;
        public function get OwnedByUserId():int { return ownedByUserId; }
        public function set OwnedByUserId(value:int):void 
        {
            ownedByUserId = value;
        }
      

        public function prepareToSend():LeadSource
        {
            var result:LeadSource = new LeadSource();
      
            result.Id = this.Id;
      
            result.Name = this.Name;
      
            result.UserId = this.UserId;
      
            result.IsActive = this.IsActive;
      
            result.OwnedByUserId = this.OwnedByUserId;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new LeadSource();
      
            this.Id = value["Id"];
      
            this.Name = value["Name"];
      
            this.UserId = value["UserId"];
      
            this.IsActive = value["IsActive"];
      
            this.OwnedByUserId = value["OwnedByUserId"];
      
        }
    }
}
      
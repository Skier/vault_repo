
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _LeadStatus implements IDomainEntity
    {
        public function _LeadStatus()
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
      

        public function prepareToSend():LeadStatus
        {
            var result:LeadStatus = new LeadStatus();
      
            result.Id = this.Id;
      
            result.Name = this.Name;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new LeadStatus();
      
            this.Id = value["Id"];
      
            this.Name = value["Name"];
      
        }
    }
}
      
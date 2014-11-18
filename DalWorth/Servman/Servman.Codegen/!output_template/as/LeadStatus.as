
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.LeadStatus")]
    public class LeadStatus
    {
        public function LeadStatus()
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
      

        public function clone():LeadStatus
        {
            var result:LeadStatus = new LeadStatus();
      
            result.Id = this.Id;
      
            result.Name = this.Name;
      
            return result;
        }

        public function updateFields(value:LeadStatus):void 
        {
            if (value == null)
                value = new LeadStatus();
      
            this.Id = value.Id;
      
            this.Name = value.Name;
      
        }
    }
}
      
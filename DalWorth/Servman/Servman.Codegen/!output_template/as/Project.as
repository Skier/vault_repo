
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.Project")]
    public class Project
    {
        public function Project()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var leadId:int;
        public function get LeadId():int { return leadId; }
        public function set LeadId(value:int):void 
        {
            leadId = value;
        }
      
        private var customerId:int;
        public function get CustomerId():int { return customerId; }
        public function set CustomerId(value:int):void 
        {
            customerId = value;
        }
      
        private var qbJobId:String;
        public function get QbJobId():String { return qbJobId; }
        public function set QbJobId(value:String):void 
        {
            qbJobId = value;
        }
      

        public function clone():Project
        {
            var result:Project = new Project();
      
            result.Id = this.Id;
      
            result.LeadId = this.LeadId;
      
            result.CustomerId = this.CustomerId;
      
            result.QbJobId = this.QbJobId;
      
            return result;
        }

        public function updateFields(value:Project):void 
        {
            if (value == null)
                value = new Project();
      
            this.Id = value.Id;
      
            this.LeadId = value.LeadId;
      
            this.CustomerId = value.CustomerId;
      
            this.QbJobId = value.QbJobId;
      
        }
    }
}
      
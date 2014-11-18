
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _Job implements IDomainEntity
    {
        public function _Job()
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
      
        private var qbJobRecordId:String;
        public function get QbJobRecordId():String { return qbJobRecordId; }
        public function set QbJobRecordId(value:String):void 
        {
            qbJobRecordId = value;
        }
      

        public function prepareToSend():Job
        {
            var result:Job = new Job();
      
            result.Id = this.Id;
      
            result.LeadId = this.LeadId;
      
            result.QbJobRecordId = this.QbJobRecordId;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new Job();
      
            this.Id = value["Id"];
      
            this.LeadId = value["LeadId"];
      
            this.QbJobRecordId = value["QbJobRecordId"];
      
        }
    }
}
      
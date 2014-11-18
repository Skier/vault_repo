
package com.dalworth.servman.domain.codegen
{
    import com.dalworth.servman.domain.*;
    
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
      
        private var businessPartnerId:int;
        public function get BusinessPartnerId():int { return businessPartnerId; }
        public function set BusinessPartnerId(value:int):void 
        {
            businessPartnerId = value;
        }
      
        private var salesRepId:int;
        public function get SalesRepId():int { return salesRepId; }
        public function set SalesRepId(value:int):void 
        {
            salesRepId = value;
        }
      

        public function prepareToSend():Job
        {
            var result:Job = new Job();
      
            result.Id = this.Id;
      
            result.LeadId = this.LeadId;
      
            result.QbJobRecordId = this.QbJobRecordId;
      
            result.BusinessPartnerId = this.BusinessPartnerId;
      
            result.SalesRepId = this.SalesRepId;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new Job();
      
            this.Id = value["Id"];
      
            this.LeadId = value["LeadId"];
      
            this.QbJobRecordId = value["QbJobRecordId"];
      
            this.BusinessPartnerId = value["BusinessPartnerId"];
      
            this.SalesRepId = value["SalesRepId"];
      
        }
    }
}
      
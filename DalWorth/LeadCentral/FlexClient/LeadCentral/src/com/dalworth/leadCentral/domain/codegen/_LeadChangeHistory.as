
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _LeadChangeHistory implements IDomainEntity
    {
        public function _LeadChangeHistory()
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
      
        private var leadStatusId:int;
        public function get LeadStatusId():int { return leadStatusId; }
        public function set LeadStatusId(value:int):void 
        {
            leadStatusId = value;
        }
      
        private var dateChanged:Date;
        public function get DateChanged():Date { return dateChanged; }
        public function set DateChanged(value:Date):void 
        {
            dateChanged = value;
        }
      
        private var userId:int;
        public function get UserId():int { return userId; }
        public function set UserId(value:int):void 
        {
            userId = value;
        }
      
        private var action:String;
        public function get Action():String { return action; }
        public function set Action(value:String):void 
        {
            action = value;
        }
      
        private var description:String;
        public function get Description():String { return description; }
        public function set Description(value:String):void 
        {
            description = value;
        }
      

        public function prepareToSend():LeadChangeHistory
        {
            var result:LeadChangeHistory = new LeadChangeHistory();
      
            result.Id = this.Id;
      
            result.LeadId = this.LeadId;
      
            result.LeadStatusId = this.LeadStatusId;
      
            result.DateChanged = this.DateChanged;
      
            result.UserId = this.UserId;
      
            result.Action = this.Action;
      
            result.Description = this.Description;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new LeadChangeHistory();
      
            this.Id = value["Id"];
      
            this.LeadId = value["LeadId"];
      
            this.LeadStatusId = value["LeadStatusId"];
      
            this.DateChanged = value["DateChanged"];
      
            this.UserId = value["UserId"];
      
            this.Action = value["Action"];
      
            this.Description = value["Description"];
      
        }
    }
}
      

package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _LeadAction implements IDomainEntity
    {
        public function _LeadAction()
        {
        }
      
        private var sequence:int;
        public function get Sequence():int { return sequence; }
        public function set Sequence(value:int):void 
        {
            sequence = value;
        }
      
        private var fromLeadStatusId:int;
        public function get FromLeadStatusId():int { return fromLeadStatusId; }
        public function set FromLeadStatusId(value:int):void 
        {
            fromLeadStatusId = value;
        }
      
        private var toLeadStatusId:int;
        public function get ToLeadStatusId():int { return toLeadStatusId; }
        public function set ToLeadStatusId(value:int):void 
        {
            toLeadStatusId = value;
        }
      
        private var message:String;
        public function get Message():String { return message; }
        public function set Message(value:String):void 
        {
            message = value;
        }
      

        public function prepareToSend():LeadAction
        {
            var result:LeadAction = new LeadAction();
      
            result.Sequence = this.Sequence;
      
            result.FromLeadStatusId = this.FromLeadStatusId;
      
            result.ToLeadStatusId = this.ToLeadStatusId;
      
            result.Message = this.Message;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new LeadAction();
      
            this.Sequence = value["Sequence"];
      
            this.FromLeadStatusId = value["FromLeadStatusId"];
      
            this.ToLeadStatusId = value["ToLeadStatusId"];
      
            this.Message = value["Message"];
      
        }
    }
}
      
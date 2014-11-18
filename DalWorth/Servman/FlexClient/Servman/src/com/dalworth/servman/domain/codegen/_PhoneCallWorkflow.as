
package com.dalworth.servman.domain.codegen
{
    import com.dalworth.servman.domain.*;
    
    [Bindable]
    public class _PhoneCallWorkflow implements IDomainEntity
    {
        public function _PhoneCallWorkflow()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var fromPhoneId:int;
        public function get FromPhoneId():int { return fromPhoneId; }
        public function set FromPhoneId(value:int):void 
        {
            fromPhoneId = value;
        }
      
        private var toPhoneId:int;
        public function get ToPhoneId():int { return toPhoneId; }
        public function set ToPhoneId(value:int):void 
        {
            toPhoneId = value;
        }
      
        private var callWorkflowId:int;
        public function get CallWorkflowId():int { return callWorkflowId; }
        public function set CallWorkflowId(value:int):void 
        {
            callWorkflowId = value;
        }
      
        private var fromPhoneNumber:String;
        public function get FromPhoneNumber():String { return fromPhoneNumber; }
        public function set FromPhoneNumber(value:String):void 
        {
            fromPhoneNumber = value;
        }
      
        private var fromWeekDay:int;
        public function get FromWeekDay():int { return fromWeekDay; }
        public function set FromWeekDay(value:int):void 
        {
            fromWeekDay = value;
        }
      
        private var toWeekDay:int;
        public function get ToWeekDay():int { return toWeekDay; }
        public function set ToWeekDay(value:int):void 
        {
            toWeekDay = value;
        }
      
        private var fromTime:String;
        public function get FromTime():String { return fromTime; }
        public function set FromTime(value:String):void 
        {
            fromTime = value;
        }
      
        private var toTime:String;
        public function get ToTime():String { return toTime; }
        public function set ToTime(value:String):void 
        {
            toTime = value;
        }
      
        private var priority:int;
        public function get Priority():int { return priority; }
        public function set Priority(value:int):void 
        {
            priority = value;
        }
      

        public function prepareToSend():PhoneCallWorkflow
        {
            var result:PhoneCallWorkflow = new PhoneCallWorkflow();
      
            result.Id = this.Id;
      
            result.FromPhoneId = this.FromPhoneId;
      
            result.ToPhoneId = this.ToPhoneId;
      
            result.CallWorkflowId = this.CallWorkflowId;
      
            result.FromPhoneNumber = this.FromPhoneNumber;
      
            result.FromWeekDay = this.FromWeekDay;
      
            result.ToWeekDay = this.ToWeekDay;
      
            result.FromTime = this.FromTime;
      
            result.ToTime = this.ToTime;
      
            result.Priority = this.Priority;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new PhoneCallWorkflow();
      
            this.Id = value["Id"];
      
            this.FromPhoneId = value["FromPhoneId"];
      
            this.ToPhoneId = value["ToPhoneId"];
      
            this.CallWorkflowId = value["CallWorkflowId"];
      
            this.FromPhoneNumber = value["FromPhoneNumber"];
      
            this.FromWeekDay = value["FromWeekDay"];
      
            this.ToWeekDay = value["ToWeekDay"];
      
            this.FromTime = value["FromTime"];
      
            this.ToTime = value["ToTime"];
      
            this.Priority = value["Priority"];
      
        }
    }
}
      
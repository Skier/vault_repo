
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _Session implements IDomainEntity
    {
        public function _Session()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var userId:int;
        public function get UserId():int { return userId; }
        public function set UserId(value:int):void 
        {
            userId = value;
        }
      
        private var sessionKey:String;
        public function get SessionKey():String { return sessionKey; }
        public function set SessionKey(value:String):void 
        {
            sessionKey = value;
        }
      
        private var ticket:String;
        public function get Ticket():String { return ticket; }
        public function set Ticket(value:String):void 
        {
            ticket = value;
        }
      
        private var sessionStart:Date;
        public function get SessionStart():Date { return sessionStart; }
        public function set SessionStart(value:Date):void 
        {
            sessionStart = value;
        }
      
        private var sessionEnd:Date;
        public function get SessionEnd():Date { return sessionEnd; }
        public function set SessionEnd(value:Date):void 
        {
            sessionEnd = value;
        }
      

        public function prepareToSend():Session
        {
            var result:Session = new Session();
      
            result.Id = this.Id;
      
            result.UserId = this.UserId;
      
            result.SessionKey = this.SessionKey;
      
            result.Ticket = this.Ticket;
      
            result.SessionStart = this.SessionStart;
      
            result.SessionEnd = this.SessionEnd;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new Session();
      
            this.Id = value["Id"];
      
            this.UserId = value["UserId"];
      
            this.SessionKey = value["SessionKey"];
      
            this.Ticket = value["Ticket"];
      
            this.SessionStart = value["SessionStart"];
      
            this.SessionEnd = value["SessionEnd"];
      
        }
    }
}
      
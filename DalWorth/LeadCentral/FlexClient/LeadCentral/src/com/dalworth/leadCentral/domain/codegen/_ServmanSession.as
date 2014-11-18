
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _ServmanSession implements IDomainEntity
    {
        public function _ServmanSession()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var servmanCustomerId:int;
        public function get ServmanCustomerId():int { return servmanCustomerId; }
        public function set ServmanCustomerId(value:int):void 
        {
            servmanCustomerId = value;
        }
      
        private var qbUserId:String;
        public function get QbUserId():String { return qbUserId; }
        public function set QbUserId(value:String):void 
        {
            qbUserId = value;
        }
      
        private var sessionId:String;
        public function get SessionId():String { return sessionId; }
        public function set SessionId(value:String):void 
        {
            sessionId = value;
        }
      
        private var dbId:String;
        public function get DbId():String { return dbId; }
        public function set DbId(value:String):void 
        {
            dbId = value;
        }
      
        private var appToken:String;
        public function get AppToken():String { return appToken; }
        public function set AppToken(value:String):void 
        {
            appToken = value;
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
      
        private var isActive:Boolean;
        public function get IsActive():Boolean { return isActive; }
        public function set IsActive(value:Boolean):void 
        {
            isActive = value;
        }
      

        public function prepareToSend():ServmanSession
        {
            var result:ServmanSession = new ServmanSession();
      
            result.Id = this.Id;
      
            result.ServmanCustomerId = this.ServmanCustomerId;
      
            result.QbUserId = this.QbUserId;
      
            result.SessionId = this.SessionId;
      
            result.DbId = this.DbId;
      
            result.AppToken = this.AppToken;
      
            result.Ticket = this.Ticket;
      
            result.SessionStart = this.SessionStart;
      
            result.IsActive = this.IsActive;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new ServmanSession();
      
            this.Id = value["Id"];
      
            this.ServmanCustomerId = value["ServmanCustomerId"];
      
            this.QbUserId = value["QbUserId"];
      
            this.SessionId = value["SessionId"];
      
            this.DbId = value["DbId"];
      
            this.AppToken = value["AppToken"];
      
            this.Ticket = value["Ticket"];
      
            this.SessionStart = value["SessionStart"];
      
            this.IsActive = value["IsActive"];
      
        }
    }
}
      

package com.dalworth.servman.domain.codegen
{
    import com.dalworth.servman.domain.*;
    
    [Bindable]
    public class _SessionLog implements IDomainEntity
    {
        public function _SessionLog()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var sessionId:int;
        public function get SessionId():int { return sessionId; }
        public function set SessionId(value:int):void 
        {
            sessionId = value;
        }
      
        private var dateLog:Date;
        public function get DateLog():Date { return dateLog; }
        public function set DateLog(value:Date):void 
        {
            dateLog = value;
        }
      
        private var description:String;
        public function get Description():String { return description; }
        public function set Description(value:String):void 
        {
            description = value;
        }
      
        private var userAgent:String;
        public function get UserAgent():String { return userAgent; }
        public function set UserAgent(value:String):void 
        {
            userAgent = value;
        }
      
        private var remoteAddress:String;
        public function get RemoteAddress():String { return remoteAddress; }
        public function set RemoteAddress(value:String):void 
        {
            remoteAddress = value;
        }
      

        public function prepareToSend():SessionLog
        {
            var result:SessionLog = new SessionLog();
      
            result.Id = this.Id;
      
            result.SessionId = this.SessionId;
      
            result.DateLog = this.DateLog;
      
            result.Description = this.Description;
      
            result.UserAgent = this.UserAgent;
      
            result.RemoteAddress = this.RemoteAddress;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new SessionLog();
      
            this.Id = value["Id"];
      
            this.SessionId = value["SessionId"];
      
            this.DateLog = value["DateLog"];
      
            this.Description = value["Description"];
      
            this.UserAgent = value["UserAgent"];
      
            this.RemoteAddress = value["RemoteAddress"];
      
        }
    }
}
      

package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _QbmsTransaction implements IDomainEntity
    {
        public function _QbmsTransaction()
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
      
        private var ticket:String;
        public function get Ticket():String { return ticket; }
        public function set Ticket(value:String):void 
        {
            ticket = value;
        }
      
        private var opId:String;
        public function get OpId():String { return opId; }
        public function set OpId(value:String):void 
        {
            opId = value;
        }
      
        private var amount:Number;
        public function get Amount():Number { return amount; }
        public function set Amount(value:Number):void 
        {
            amount = value;
        }
      
        private var opType:String;
        public function get OpType():String { return opType; }
        public function set OpType(value:String):void 
        {
            opType = value;
        }
      
        private var status:String;
        public function get Status():String { return status; }
        public function set Status(value:String):void 
        {
            status = value;
        }
      
        private var statusCode:String;
        public function get StatusCode():String { return statusCode; }
        public function set StatusCode(value:String):void 
        {
            statusCode = value;
        }
      
        private var statusMessage:String;
        public function get StatusMessage():String { return statusMessage; }
        public function set StatusMessage(value:String):void 
        {
            statusMessage = value;
        }
      
        private var txnType:String;
        public function get TxnType():String { return txnType; }
        public function set TxnType(value:String):void 
        {
            txnType = value;
        }
      
        private var txnTimestamp:String;
        public function get TxnTimestamp():String { return txnTimestamp; }
        public function set TxnTimestamp(value:String):void 
        {
            txnTimestamp = value;
        }
      
        private var maskedCCN:String;
        public function get MaskedCCN():String { return maskedCCN; }
        public function set MaskedCCN(value:String):void 
        {
            maskedCCN = value;
        }
      
        private var authCode:String;
        public function get AuthCode():String { return authCode; }
        public function set AuthCode(value:String):void 
        {
            authCode = value;
        }
      
        private var txnId:String;
        public function get TxnId():String { return txnId; }
        public function set TxnId(value:String):void 
        {
            txnId = value;
        }
      

        public function prepareToSend():QbmsTransaction
        {
            var result:QbmsTransaction = new QbmsTransaction();
      
            result.Id = this.Id;
      
            result.ServmanCustomerId = this.ServmanCustomerId;
      
            result.Ticket = this.Ticket;
      
            result.OpId = this.OpId;
      
            result.Amount = this.Amount;
      
            result.OpType = this.OpType;
      
            result.Status = this.Status;
      
            result.StatusCode = this.StatusCode;
      
            result.StatusMessage = this.StatusMessage;
      
            result.TxnType = this.TxnType;
      
            result.TxnTimestamp = this.TxnTimestamp;
      
            result.MaskedCCN = this.MaskedCCN;
      
            result.AuthCode = this.AuthCode;
      
            result.TxnId = this.TxnId;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new QbmsTransaction();
      
            this.Id = value["Id"];
      
            this.ServmanCustomerId = value["ServmanCustomerId"];
      
            this.Ticket = value["Ticket"];
      
            this.OpId = value["OpId"];
      
            this.Amount = value["Amount"];
      
            this.OpType = value["OpType"];
      
            this.Status = value["Status"];
      
            this.StatusCode = value["StatusCode"];
      
            this.StatusMessage = value["StatusMessage"];
      
            this.TxnType = value["TxnType"];
      
            this.TxnTimestamp = value["TxnTimestamp"];
      
            this.MaskedCCN = value["MaskedCCN"];
      
            this.AuthCode = value["AuthCode"];
      
            this.TxnId = value["TxnId"];
      
        }
    }
}
      
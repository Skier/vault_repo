
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _Transaction implements IDomainEntity
    {
        public function _Transaction()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var transactionDate:Date;
        public function get TransactionDate():Date { return transactionDate; }
        public function set TransactionDate(value:Date):void 
        {
            transactionDate = value;
        }
      
        private var transactionTypeId:int;
        public function get TransactionTypeId():int { return transactionTypeId; }
        public function set TransactionTypeId(value:int):void 
        {
            transactionTypeId = value;
        }
      
        private var trackingPhoneId:int;
        public function get TrackingPhoneId():int { return trackingPhoneId; }
        public function set TrackingPhoneId(value:int):void 
        {
            trackingPhoneId = value;
        }
      
        private var phoneCallId:int;
        public function get PhoneCallId():int { return phoneCallId; }
        public function set PhoneCallId(value:int):void 
        {
            phoneCallId = value;
        }
      
        private var phoneSmsId:int;
        public function get PhoneSmsId():int { return phoneSmsId; }
        public function set PhoneSmsId(value:int):void 
        {
            phoneSmsId = value;
        }
      
        private var quantity:Number;
        public function get Quantity():Number { return quantity; }
        public function set Quantity(value:Number):void 
        {
            quantity = value;
        }
      
        private var amount:Number;
        public function get Amount():Number { return amount; }
        public function set Amount(value:Number):void 
        {
            amount = value;
        }
      
        private var currentBalance:Number;
        public function get CurrentBalance():Number { return currentBalance; }
        public function set CurrentBalance(value:Number):void 
        {
            currentBalance = value;
        }
      
        private var qbmsTransactionId:int;
        public function get QbmsTransactionId():int { return qbmsTransactionId; }
        public function set QbmsTransactionId(value:int):void 
        {
            qbmsTransactionId = value;
        }
      

        public function prepareToSend():Transaction
        {
            var result:Transaction = new Transaction();
      
            result.Id = this.Id;
      
            result.TransactionDate = this.TransactionDate;
      
            result.TransactionTypeId = this.TransactionTypeId;
      
            result.TrackingPhoneId = this.TrackingPhoneId;
      
            result.PhoneCallId = this.PhoneCallId;
      
            result.PhoneSmsId = this.PhoneSmsId;
      
            result.Quantity = this.Quantity;
      
            result.Amount = this.Amount;
      
            result.CurrentBalance = this.CurrentBalance;
      
            result.QbmsTransactionId = this.QbmsTransactionId;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new Transaction();
      
            this.Id = value["Id"];
      
            this.TransactionDate = value["TransactionDate"];
      
            this.TransactionTypeId = value["TransactionTypeId"];
      
            this.TrackingPhoneId = value["TrackingPhoneId"];
      
            this.PhoneCallId = value["PhoneCallId"];
      
            this.PhoneSmsId = value["PhoneSmsId"];
      
            this.Quantity = value["Quantity"];
      
            this.Amount = value["Amount"];
      
            this.CurrentBalance = value["CurrentBalance"];
      
            this.QbmsTransactionId = value["QbmsTransactionId"];
      
        }
    }
}
      
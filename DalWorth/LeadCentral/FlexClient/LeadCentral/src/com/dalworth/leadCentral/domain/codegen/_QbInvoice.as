
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _QbInvoice implements IDomainEntity
    {
        public function _QbInvoice()
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
      
        private var qbInvoiceId:String;
        public function get QbInvoiceId():String { return qbInvoiceId; }
        public function set QbInvoiceId(value:String):void 
        {
            qbInvoiceId = value;
        }
      
        private var amount:Number;
        public function get Amount():Number { return amount; }
        public function set Amount(value:Number):void 
        {
            amount = value;
        }
      
        private var taxAmount:Number;
        public function get TaxAmount():Number { return taxAmount; }
        public function set TaxAmount(value:Number):void 
        {
            taxAmount = value;
        }
      
        private var totalAmount:Number;
        public function get TotalAmount():Number { return totalAmount; }
        public function set TotalAmount(value:Number):void 
        {
            totalAmount = value;
        }
      
        private var status:String;
        public function get Status():String { return status; }
        public function set Status(value:String):void 
        {
            status = value;
        }
      

        public function prepareToSend():QbInvoice
        {
            var result:QbInvoice = new QbInvoice();
      
            result.Id = this.Id;
      
            result.LeadId = this.LeadId;
      
            result.QbInvoiceId = this.QbInvoiceId;
      
            result.Amount = this.Amount;
      
            result.TaxAmount = this.TaxAmount;
      
            result.TotalAmount = this.TotalAmount;
      
            result.Status = this.Status;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new QbInvoice();
      
            this.Id = value["Id"];
      
            this.LeadId = value["LeadId"];
      
            this.QbInvoiceId = value["QbInvoiceId"];
      
            this.Amount = value["Amount"];
      
            this.TaxAmount = value["TaxAmount"];
      
            this.TotalAmount = value["TotalAmount"];
      
            this.Status = value["Status"];
      
        }
    }
}
      
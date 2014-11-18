
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.QbCustomer")]
    public class QbCustomer
    {
        public function QbCustomer()
        {
        }
      
        private var recordId:String;
        public function get RecordId():String { return recordId; }
        public function set RecordId(value:String):void 
        {
            recordId = value;
        }
      
        private var showAs:String;
        public function get ShowAs():String { return showAs; }
        public function set ShowAs(value:String):void 
        {
            showAs = value;
        }
      

        public function clone():QbCustomer
        {
            var result:QbCustomer = new QbCustomer();
      
            result.RecordId = this.RecordId;
      
            result.ShowAs = this.ShowAs;
      
            return result;
        }

        public function updateFields(value:QbCustomer):void 
        {
            if (value == null)
                value = new QbCustomer();
      
            this.RecordId = value.RecordId;
      
            this.ShowAs = value.ShowAs;
      
        }
    }
}
      
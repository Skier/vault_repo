
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.QbJob")]
    public class QbJob
    {
        public function QbJob()
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
      

        public function clone():QbJob
        {
            var result:QbJob = new QbJob();
      
            result.RecordId = this.RecordId;
      
            result.ShowAs = this.ShowAs;
      
            return result;
        }

        public function updateFields(value:QbJob):void 
        {
            if (value == null)
                value = new QbJob();
      
            this.RecordId = value.RecordId;
      
            this.ShowAs = value.ShowAs;
      
        }
    }
}
      
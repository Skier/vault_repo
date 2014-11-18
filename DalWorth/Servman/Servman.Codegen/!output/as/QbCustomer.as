
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.QbCustomer")]
    public class QbCustomer
    {
        public function QbCustomer()
        {
        }
      
        private var _recordId:String;
        public function get recordId():String { return _recordId; }
        public function set recordId(value:String):void 
        {
            _recordId = value;
        }
      
    }
}
      
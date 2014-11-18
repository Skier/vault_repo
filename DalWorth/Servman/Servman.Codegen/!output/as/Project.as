
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.Project")]
    public class Project
    {
        public function Project()
        {
        }
      
        private var _id:int;
        public function get id():int { return _id; }
        public function set id(value:int):void 
        {
            _id = value;
        }
      
        private var _leadId:int;
        public function get leadId():int { return _leadId; }
        public function set leadId(value:int):void 
        {
            _leadId = value;
        }
      
        private var _customerId:int;
        public function get customerId():int { return _customerId; }
        public function set customerId(value:int):void 
        {
            _customerId = value;
        }
      
        private var _qbJobId:String;
        public function get qbJobId():String { return _qbJobId; }
        public function set qbJobId(value:String):void 
        {
            _qbJobId = value;
        }
      
    }
}
      
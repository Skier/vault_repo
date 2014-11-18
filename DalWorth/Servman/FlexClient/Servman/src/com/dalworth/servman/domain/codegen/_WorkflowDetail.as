
package com.dalworth.servman.domain.codegen
{
    import com.dalworth.servman.domain.*;
    
    [Bindable]
    public class _WorkflowDetail implements IDomainEntity
    {
        public function _WorkflowDetail()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var callWorkflowId:int;
        public function get CallWorkflowId():int { return callWorkflowId; }
        public function set CallWorkflowId(value:int):void 
        {
            callWorkflowId = value;
        }
      
        private var propertyName:String;
        public function get PropertyName():String { return propertyName; }
        public function set PropertyName(value:String):void 
        {
            propertyName = value;
        }
      
        private var propertyValue:String;
        public function get PropertyValue():String { return propertyValue; }
        public function set PropertyValue(value:String):void 
        {
            propertyValue = value;
        }
      

        public function prepareToSend():WorkflowDetail
        {
            var result:WorkflowDetail = new WorkflowDetail();
      
            result.Id = this.Id;
      
            result.CallWorkflowId = this.CallWorkflowId;
      
            result.PropertyName = this.PropertyName;
      
            result.PropertyValue = this.PropertyValue;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new WorkflowDetail();
      
            this.Id = value["Id"];
      
            this.CallWorkflowId = value["CallWorkflowId"];
      
            this.PropertyName = value["PropertyName"];
      
            this.PropertyValue = value["PropertyValue"];
      
        }
    }
}
      
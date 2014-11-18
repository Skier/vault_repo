
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _CallWorkflow implements IDomainEntity
    {
        public function _CallWorkflow()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var description:String;
        public function get Description():String { return description; }
        public function set Description(value:String):void 
        {
            description = value;
        }
      

        public function prepareToSend():CallWorkflow
        {
            var result:CallWorkflow = new CallWorkflow();
      
            result.Id = this.Id;
      
            result.Description = this.Description;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new CallWorkflow();
      
            this.Id = value["Id"];
      
            this.Description = value["Description"];
      
        }
    }
}
      
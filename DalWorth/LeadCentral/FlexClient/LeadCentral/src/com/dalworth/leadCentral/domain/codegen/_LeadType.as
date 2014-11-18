
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _LeadType implements IDomainEntity
    {
        public function _LeadType()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var name:String;
        public function get Name():String { return name; }
        public function set Name(value:String):void 
        {
            name = value;
        }
      
        private var qbJobTypeRecordId:String;
        public function get QbJobTypeRecordId():String { return qbJobTypeRecordId; }
        public function set QbJobTypeRecordId(value:String):void 
        {
            qbJobTypeRecordId = value;
        }
      
        private var isActive:Boolean;
        public function get IsActive():Boolean { return isActive; }
        public function set IsActive(value:Boolean):void 
        {
            isActive = value;
        }
      

        public function prepareToSend():LeadType
        {
            var result:LeadType = new LeadType();
      
            result.Id = this.Id;
      
            result.Name = this.Name;
      
            result.QbJobTypeRecordId = this.QbJobTypeRecordId;
      
            result.IsActive = this.IsActive;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new LeadType();
      
            this.Id = value["Id"];
      
            this.Name = value["Name"];
      
            this.QbJobTypeRecordId = value["QbJobTypeRecordId"];
      
            this.IsActive = value["IsActive"];
      
        }
    }
}
      
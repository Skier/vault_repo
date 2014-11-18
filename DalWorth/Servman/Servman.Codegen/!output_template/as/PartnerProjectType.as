
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.PartnerProjectType")]
    public class PartnerProjectType
    {
        public function PartnerProjectType()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var businessPartnerId:int;
        public function get BusinessPartnerId():int { return businessPartnerId; }
        public function set BusinessPartnerId(value:int):void 
        {
            businessPartnerId = value;
        }
      
        private var name:String;
        public function get Name():String { return name; }
        public function set Name(value:String):void 
        {
            name = value;
        }
      

        public function clone():PartnerProjectType
        {
            var result:PartnerProjectType = new PartnerProjectType();
      
            result.Id = this.Id;
      
            result.BusinessPartnerId = this.BusinessPartnerId;
      
            result.Name = this.Name;
      
            return result;
        }

        public function updateFields(value:PartnerProjectType):void 
        {
            if (value == null)
                value = new PartnerProjectType();
      
            this.Id = value.Id;
      
            this.BusinessPartnerId = value.BusinessPartnerId;
      
            this.Name = value.Name;
      
        }
    }
}
      
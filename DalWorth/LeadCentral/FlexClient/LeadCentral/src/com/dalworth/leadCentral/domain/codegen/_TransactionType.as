
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _TransactionType implements IDomainEntity
    {
        public function _TransactionType()
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
      
        private var cost:Number;
        public function get Cost():Number { return cost; }
        public function set Cost(value:Number):void 
        {
            cost = value;
        }
      

        public function prepareToSend():TransactionType
        {
            var result:TransactionType = new TransactionType();
      
            result.Id = this.Id;
      
            result.Name = this.Name;
      
            result.Cost = this.Cost;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new TransactionType();
      
            this.Id = value["Id"];
      
            this.Name = value["Name"];
      
            this.Cost = value["Cost"];
      
        }
    }
}
      
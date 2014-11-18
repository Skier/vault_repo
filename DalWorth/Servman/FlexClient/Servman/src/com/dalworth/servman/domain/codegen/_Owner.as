
package com.dalworth.servman.domain.codegen
{
    import com.dalworth.servman.domain.*;
    
    [Bindable]
    public class _Owner implements IDomainEntity
    {
        public function _Owner()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var userId:int;
        public function get UserId():int { return userId; }
        public function set UserId(value:int):void 
        {
            userId = value;
        }
      
        private var showAs:String;
        public function get ShowAs():String { return showAs; }
        public function set ShowAs(value:String):void 
        {
            showAs = value;
        }
      
        private var isActive:Boolean;
        public function get IsActive():Boolean { return isActive; }
        public function set IsActive(value:Boolean):void 
        {
            isActive = value;
        }
      

        public function prepareToSend():Owner
        {
            var result:Owner = new Owner();
      
            result.Id = this.Id;
      
            result.UserId = this.UserId;
      
            result.ShowAs = this.ShowAs;
      
            result.IsActive = this.IsActive;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new Owner();
      
            this.Id = value["Id"];
      
            this.UserId = value["UserId"];
      
            this.ShowAs = value["ShowAs"];
      
            this.IsActive = value["IsActive"];
      
        }
    }
}
      
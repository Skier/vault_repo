
package com.dalworth.servman.domain.codegen
{
    import com.dalworth.servman.domain.*;
    
    [Bindable]
    public class _SalesRep implements IDomainEntity
    {
        public function _SalesRep()
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
      
        private var qbSalesRepRecordId:String;
        public function get QbSalesRepRecordId():String { return qbSalesRepRecordId; }
        public function set QbSalesRepRecordId(value:String):void 
        {
            qbSalesRepRecordId = value;
        }
      
        private var isActive:Boolean;
        public function get IsActive():Boolean { return isActive; }
        public function set IsActive(value:Boolean):void 
        {
            isActive = value;
        }
      

        public function prepareToSend():SalesRep
        {
            var result:SalesRep = new SalesRep();
      
            result.Id = this.Id;
      
            result.UserId = this.UserId;
      
            result.ShowAs = this.ShowAs;
      
            result.QbSalesRepRecordId = this.QbSalesRepRecordId;
      
            result.IsActive = this.IsActive;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new SalesRep();
      
            this.Id = value["Id"];
      
            this.UserId = value["UserId"];
      
            this.ShowAs = value["ShowAs"];
      
            this.QbSalesRepRecordId = value["QbSalesRepRecordId"];
      
            this.IsActive = value["IsActive"];
      
        }
    }
}
      
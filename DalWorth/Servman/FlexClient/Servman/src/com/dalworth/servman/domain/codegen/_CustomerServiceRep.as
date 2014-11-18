
package com.dalworth.servman.domain.codegen
{
    import com.dalworth.servman.domain.*;
    
    [Bindable]
    public class _CustomerServiceRep implements IDomainEntity
    {
        public function _CustomerServiceRep()
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
      
        private var qbEmployeeRecordId:String;
        public function get QbEmployeeRecordId():String { return qbEmployeeRecordId; }
        public function set QbEmployeeRecordId(value:String):void 
        {
            qbEmployeeRecordId = value;
        }
      
        private var qbVendorRecordId:String;
        public function get QbVendorRecordId():String { return qbVendorRecordId; }
        public function set QbVendorRecordId(value:String):void 
        {
            qbVendorRecordId = value;
        }
      
        private var isActive:Boolean;
        public function get IsActive():Boolean { return isActive; }
        public function set IsActive(value:Boolean):void 
        {
            isActive = value;
        }
      

        public function prepareToSend():CustomerServiceRep
        {
            var result:CustomerServiceRep = new CustomerServiceRep();
      
            result.Id = this.Id;
      
            result.UserId = this.UserId;
      
            result.ShowAs = this.ShowAs;
      
            result.QbEmployeeRecordId = this.QbEmployeeRecordId;
      
            result.QbVendorRecordId = this.QbVendorRecordId;
      
            result.IsActive = this.IsActive;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new CustomerServiceRep();
      
            this.Id = value["Id"];
      
            this.UserId = value["UserId"];
      
            this.ShowAs = value["ShowAs"];
      
            this.QbEmployeeRecordId = value["QbEmployeeRecordId"];
      
            this.QbVendorRecordId = value["QbVendorRecordId"];
      
            this.IsActive = value["IsActive"];
      
        }
    }
}
      

package com.dalworth.servman.domain.codegen
{
    import com.dalworth.servman.domain.*;
    
    [Bindable]
    public class _BusinessPartner implements IDomainEntity
    {
        public function _BusinessPartner()
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
      
        private var createdByUserId:int;
        public function get CreatedByUserId():int { return createdByUserId; }
        public function set CreatedByUserId(value:int):void 
        {
            createdByUserId = value;
        }
      
        private var dateCreated:Date;
        public function get DateCreated():Date { return dateCreated; }
        public function set DateCreated(value:Date):void 
        {
            dateCreated = value;
        }
      
        private var salesRepId:int;
        public function get SalesRepId():int { return salesRepId; }
        public function set SalesRepId(value:int):void 
        {
            salesRepId = value;
        }
      
        private var showAs:String;
        public function get ShowAs():String { return showAs; }
        public function set ShowAs(value:String):void 
        {
            showAs = value;
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
      
        private var canLogin:Boolean;
        public function get CanLogin():Boolean { return canLogin; }
        public function set CanLogin(value:Boolean):void 
        {
            canLogin = value;
        }
      

        public function prepareToSend():BusinessPartner
        {
            var result:BusinessPartner = new BusinessPartner();
      
            result.Id = this.Id;
      
            result.UserId = this.UserId;
      
            result.CreatedByUserId = this.CreatedByUserId;
      
            result.DateCreated = this.DateCreated;
      
            result.SalesRepId = this.SalesRepId;
      
            result.ShowAs = this.ShowAs;
      
            result.QbVendorRecordId = this.QbVendorRecordId;
      
            result.IsActive = this.IsActive;
      
            result.CanLogin = this.CanLogin;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new BusinessPartner();
      
            this.Id = value["Id"];
      
            this.UserId = value["UserId"];
      
            this.CreatedByUserId = value["CreatedByUserId"];
      
            this.DateCreated = value["DateCreated"];
      
            this.SalesRepId = value["SalesRepId"];
      
            this.ShowAs = value["ShowAs"];
      
            this.QbVendorRecordId = value["QbVendorRecordId"];
      
            this.IsActive = value["IsActive"];
      
            this.CanLogin = value["CanLogin"];
      
        }
    }
}
      
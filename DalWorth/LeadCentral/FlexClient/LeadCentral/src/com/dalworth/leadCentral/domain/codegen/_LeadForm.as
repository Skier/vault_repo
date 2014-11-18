
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _LeadForm implements IDomainEntity
    {
        public function _LeadForm()
        {
        }
      
        private var id:int;
        public function get Id():int { return id; }
        public function set Id(value:int):void 
        {
            id = value;
        }
      
        private var leadSourceId:int;
        public function get LeadSourceId():int { return leadSourceId; }
        public function set LeadSourceId(value:int):void 
        {
            leadSourceId = value;
        }
      
        private var firstName:String;
        public function get FirstName():String { return firstName; }
        public function set FirstName(value:String):void 
        {
            firstName = value;
        }
      
        private var lastName:String;
        public function get LastName():String { return lastName; }
        public function set LastName(value:String):void 
        {
            lastName = value;
        }
      
        private var phone:String;
        public function get Phone():String { return phone; }
        public function set Phone(value:String):void 
        {
            phone = value;
        }
      
        private var message:String;
        public function get Message():String { return message; }
        public function set Message(value:String):void 
        {
            message = value;
        }
      
        private var dateCreated:Date;
        public function get DateCreated():Date { return dateCreated; }
        public function set DateCreated(value:Date):void 
        {
            dateCreated = value;
        }
      
        private var referralUri:String;
        public function get ReferralUri():String { return referralUri; }
        public function set ReferralUri(value:String):void 
        {
            referralUri = value;
        }
      

        public function prepareToSend():LeadForm
        {
            var result:LeadForm = new LeadForm();
      
            result.Id = this.Id;
      
            result.LeadSourceId = this.LeadSourceId;
      
            result.FirstName = this.FirstName;
      
            result.LastName = this.LastName;
      
            result.Phone = this.Phone;
      
            result.Message = this.Message;
      
            result.DateCreated = this.DateCreated;
      
            result.ReferralUri = this.ReferralUri;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new LeadForm();
      
            this.Id = value["Id"];
      
            this.LeadSourceId = value["LeadSourceId"];
      
            this.FirstName = value["FirstName"];
      
            this.LastName = value["LastName"];
      
            this.Phone = value["Phone"];
      
            this.Message = value["Message"];
      
            this.DateCreated = value["DateCreated"];
      
            this.ReferralUri = value["ReferralUri"];
      
        }
    }
}
      
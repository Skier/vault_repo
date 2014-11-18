
package com.dalworth.leadCentral.domain.codegen
{
    import com.dalworth.leadCentral.domain.*;
    
    [Bindable]
    public class _LeadSourceTrackingPhone implements IDomainEntity
    {
        public function _LeadSourceTrackingPhone()
        {
        }
      
        private var leadSourceId:int;
        public function get LeadSourceId():int { return leadSourceId; }
        public function set LeadSourceId(value:int):void 
        {
            leadSourceId = value;
        }
      
        private var trackingPhoneId:int;
        public function get TrackingPhoneId():int { return trackingPhoneId; }
        public function set TrackingPhoneId(value:int):void 
        {
            trackingPhoneId = value;
        }
      
        private var notes:String;
        public function get Notes():String { return notes; }
        public function set Notes(value:String):void 
        {
            notes = value;
        }
      

        public function prepareToSend():LeadSourceTrackingPhone
        {
            var result:LeadSourceTrackingPhone = new LeadSourceTrackingPhone();
      
            result.LeadSourceId = this.LeadSourceId;
      
            result.TrackingPhoneId = this.TrackingPhoneId;
      
            result.Notes = this.Notes;
      
            return result;
        }

        public function applyFields(value:Object):void 
        {
            if (value == null)
                value = new LeadSourceTrackingPhone();
      
            this.LeadSourceId = value["LeadSourceId"];
      
            this.TrackingPhoneId = value["TrackingPhoneId"];
      
            this.Notes = value["Notes"];
      
        }
    }
}
      
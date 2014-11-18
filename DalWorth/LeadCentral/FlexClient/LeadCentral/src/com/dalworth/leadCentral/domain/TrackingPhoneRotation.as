
package com.dalworth.leadCentral.domain
{
    import com.dalworth.leadCentral.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Dalworth.LeadCentral.Domain.TrackingPhoneRotation")]
    public class TrackingPhoneRotation extends _TrackingPhoneRotation
    {
    	public var RelatedPhoneCall:PhoneCall;
    	public var RelatedPhoneSms:PhoneSms;
    	public var RelatedWebForm:LeadForm;
    	
        public function TrackingPhoneRotation()
        {
        }

        public function get timeDisplayed():Number 
        {
        	if (TimeDisplay != null)
        		return TimeDisplay.time;
        	else 
        		return 0;
        }
        
    }
}
      
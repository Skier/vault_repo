
package com.dalworth.servman.domain
{
    import com.dalworth.servman.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Servman.Domain.Lead")]
    public class Lead extends _Lead
    {
        public function Lead()
        {
        }
        
        public var RelatedPhoneCall:PhoneCall;
        
        public function get timeCreated():Number 
        {
        	if (DateCreated != null)
        		return DateCreated.time;
        	else 
        		return 0;
        }
        
        public function get ToPhoneNumber():String 
        {
        	if (RelatedPhoneCall)
        		return RelatedPhoneCall.PhoneTo;
        	else
        		return "";
        }
    }
}
      
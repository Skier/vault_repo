
package com.dalworth.servman.domain
{
    import com.dalworth.servman.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Servman.Domain.PhoneCall")]
    public class PhoneCall extends _PhoneCall
    {
    	public var RelatedBusinessPartner:BusinessPartner;
    	
        public function PhoneCall()
        {
        }
        
        public function get businessPartnerStr():String 
        {
        	if (RelatedBusinessPartner != null)
        		return RelatedBusinessPartner.ShowAs;
        	else 
        		return "";
        }
    }
}
      
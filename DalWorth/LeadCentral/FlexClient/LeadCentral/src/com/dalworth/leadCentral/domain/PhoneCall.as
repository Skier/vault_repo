
package com.dalworth.leadCentral.domain
{
    import com.dalworth.leadCentral.domain.codegen.*;
    
    import mx.collections.ArrayCollection;

    [Bindable]
    [RemoteClass(alias="Dalworth.LeadCentral.Domain.PhoneCall")]
    public class PhoneCall extends _PhoneCall
    {
    	public var RelatedLeadSource:LeadSource;
    	
    	public var relatedTransactions:ArrayCollection;
    	
        public function PhoneCall()
        {
        	relatedTransactions = new ArrayCollection();
        }
        
        public function get leadSourceStr():String 
        {
        	if (RelatedLeadSource != null)
        		return RelatedLeadSource.Name;
        	else 
        		return "";
        }
    }
}
      
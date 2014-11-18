
package com.dalworth.leadCentral.domain
{
    import Intuit.Sb.Cdm.vo.CustomerType;
    
    import com.dalworth.leadCentral.domain.codegen.*;
    
    import mx.collections.ArrayCollection;

    [Bindable]
    [RemoteClass(alias="Dalworth.LeadCentral.Domain.LeadSource")]
    public class LeadSource extends _LeadSource
    {
    	public var RelatedUser:User;
    	
    	public var relatedSalesRep:User;

    	public var qbCustomerType:CustomerType;

        public function LeadSource()
        {
        	
        }

        public static function getEmpty():LeadSource 
        {
        	var result:LeadSource = new LeadSource();
        	
        	result.Id = 0;
        	result.Name = "";
        	
        	return result;
        }
    }
}
      
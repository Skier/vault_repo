
package com.dalworth.leadCentral.domain
{
    import com.dalworth.leadCentral.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Dalworth.LeadCentral.Domain.LeadSourcePhone")]
    public class LeadSourcePhone extends _LeadSourcePhone
    {
        public function LeadSourcePhone()
        {
        }

    	public static function getEmpty():LeadSourcePhone
    	{
    		var result:LeadSourcePhone = new LeadSourcePhone();
	    		result.PhoneNumber = "Any phone number";
	    		result.Description = "Any phone number";
    		return result;
    	}
    	
    }
}
      
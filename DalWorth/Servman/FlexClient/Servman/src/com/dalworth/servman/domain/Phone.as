
package com.dalworth.servman.domain
{
    import com.dalworth.servman.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Servman.Domain.Phone")]
    public class Phone extends _Phone
    {
    	public function get isCompanyPhone():Boolean
    	{
    		if (TwilioId != null && TwilioId.length > 0)
    			return true;
    		else 
    			return false;
    	}
    	
    	public static function getEmpty():Phone
    	{
    		var result:Phone = new Phone();
	    		result.Number = "Any phone number";
	    		result.Description = "Any phone number";
    		return result;
    	}
    	
        public function Phone()
        {
        }
    }
}
      
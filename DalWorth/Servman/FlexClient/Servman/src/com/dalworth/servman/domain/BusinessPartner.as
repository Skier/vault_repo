
package com.dalworth.servman.domain
{
    import com.dalworth.servman.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Servman.Domain.BusinessPartner")]
    public class BusinessPartner extends _BusinessPartner
    {
    	public var RelatedUser:User;
    	
        public function BusinessPartner()
        {
        }
        
        public static function getEmpty():BusinessPartner 
        {
        	var result:BusinessPartner = new BusinessPartner();
        	
        	result.Id = 0;
        	result.ShowAs = "";
        	
        	return result;
        }
    }
}
      
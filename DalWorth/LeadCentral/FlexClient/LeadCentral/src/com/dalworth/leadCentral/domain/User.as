
package com.dalworth.leadCentral.domain
{
    import com.dalworth.leadCentral.domain.codegen.*;
    
    import mx.collections.ArrayCollection;

    [Bindable]
    [RemoteClass(alias="Dalworth.LeadCentral.Domain.User")]
    public class User extends _User
    {
    	public static const ROLE_ADMINISTRATOR:String = "Administrator";
    	public static const ROLE_STAFF:String = "Staff";
    	public static const ROLE_BUSINESS_PARTNER:String = "BusinessPartner";
    	
        public var RelatedImageFile:File;
        public var RelatedCustomer:ServmanCustomer;

        public var relatedLeadSources:ArrayCollection;

        public function User()
        {
        	IsActive = true;
        	relatedLeadSources = new ArrayCollection();
        }
        
        public static function getEmpty(name:String = ""):User 
        {
        	var result:User = new User();
        	
        	result.Id = 0;
        	result.Name = name;
        	
        	return result;
        } 
    }
}
      
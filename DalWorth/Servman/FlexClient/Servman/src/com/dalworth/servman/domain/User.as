
package com.dalworth.servman.domain
{
    import com.dalworth.servman.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Servman.Domain.User")]
    public class User extends _User
    {
    	public static const ROLE_ADMINISTRATOR:String = "Administrator";
    	public static const ROLE_OWNER:String = "Owner";
    	public static const ROLE_BUSINESS_PARTNER:String = "BusinessPartner";
    	public static const ROLE_SALES_REP:String = "SalesRep";
    	public static const ROLE_CUSTOMER_SERVICE_REP:String = "CustomerServiceRep";
    	
        public function User()
        {
        }
        
        public var RoleNames:Array;
        
        public var RelatedCustomerServiceRep:CustomerServiceRep;
        public var RelatedSalesRep:SalesRep;
        public var RelatedBusinessPartner:BusinessPartner;
        public var RelatedOwner:Owner;
        
        public var RelatedImageFile:File;

        public function HasRoleName(name:String):Boolean
        {
        	if (RoleNames != null && RoleNames.length > 0)
        	{
        		for each (var roleName:String in RoleNames)
        		{
        			if (roleName.toUpperCase() == name.toUpperCase())
        				return true;
        		}
        	}
        	
        	return false;
        }
        
        public static function getEmpty():User 
        {
        	var result:User = new User();
        	
        	result.Id = 0;
        	result.Name = "";
        	
        	return result;
        } 
    }
}
      
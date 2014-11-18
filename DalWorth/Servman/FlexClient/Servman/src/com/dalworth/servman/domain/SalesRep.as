
package com.dalworth.servman.domain
{
    import com.dalworth.servman.domain.codegen.*;
    
    import mx.collections.ArrayCollection;

    [Bindable]
    [RemoteClass(alias="Servman.Domain.SalesRep")]
    public class SalesRep extends _SalesRep
    {
    	public var RelatedUser:User;
    	public var BusinessPartners:ArrayCollection;
    	
    	public function set RelatedBusinessPartners(value:Array):void
    	{
    		BusinessPartners.source = value;
    	}
    	
    	public function get RelatedBusinessPartners():Array 
    	{
    		return BusinessPartners.source;
    	}
    	
        public function SalesRep()
        {
        	BusinessPartners = new ArrayCollection();
        }
        
        public static function getEmpty():SalesRep
        {
        	var result:SalesRep = new SalesRep();
        	
        	result.Id = 0;
        	result.ShowAs = "";
        	
        	return result;
        }
    }
}
      
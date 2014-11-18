
package com.dalworth.servman.domain
{
    import com.dalworth.servman.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Servman.Domain.LeadType")]
    public class LeadType extends _LeadType
    {
        public function LeadType()
        {
        }
        
        public static function getEmpty():LeadType
        {
        	var result:LeadType = new LeadType();
        	
        	result.Id = 0;
        	result.Name = "";
        	
        	return result;
        } 
    }
}
      
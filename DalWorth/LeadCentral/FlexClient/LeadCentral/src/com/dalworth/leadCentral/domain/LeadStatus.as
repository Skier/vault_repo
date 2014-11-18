
package com.dalworth.leadCentral.domain
{
    import com.dalworth.leadCentral.domain.codegen.*;
    import com.dalworth.leadCentral.domain.enum.LeadStatusEnum;
    
    import mx.collections.ArrayCollection;

    [Bindable]
    [RemoteClass(alias="Dalworth.LeadCentral.Domain.LeadStatus")]
    public class LeadStatus extends _LeadStatus
    {
    	public static const STATUS_NEW:String = "NEW";
    	public static const STATUS_PENDING:String = "PENDING";
    	public static const STATUS_CANCELLED:String = "CANCELLED";
    	public static const STATUS_CONVERTED:String = "CONVERTED";

        public function LeadStatus()
        {
        }
        
        public static function getStatusName(id:int):String 
        {
        	var result:String = "";
        	switch (id) 
        	{
        		case LeadStatusEnum.NEW:
        			result = STATUS_NEW;
        			break;
        		case LeadStatusEnum.PENDING:
        			result = STATUS_PENDING;
        			break;
        		case LeadStatusEnum.CANCELLED:
        			result = STATUS_CANCELLED;
        			break;
        		case LeadStatusEnum.CONVERTED:
        			result = STATUS_CONVERTED;
        			break;
        	}
        	return result;
        }

        public static function getStatuses():ArrayCollection 
        {
        	var result:ArrayCollection = new ArrayCollection();
	        	result.addItem(getStatus(LeadStatusEnum.NEW));
	        	result.addItem(getStatus(LeadStatusEnum.PENDING));
	        	result.addItem(getStatus(LeadStatusEnum.CANCELLED));
	        	result.addItem(getStatus(LeadStatusEnum.CONVERTED));
        	return result;
        }
        
        public static function getStatus(id:int):LeadStatus 
        {
        	var result:LeadStatus = new LeadStatus();
        	result.Id = id;
        	result.Name = getStatusName(id);
        	return result;
        }
        
        public static function getEmpty():LeadStatus
        {
        	var result:LeadStatus = new LeadStatus;
        	
        	result.Id = 0;
        	result.Name = "";
        	
        	return result;
        }
    }
}
      
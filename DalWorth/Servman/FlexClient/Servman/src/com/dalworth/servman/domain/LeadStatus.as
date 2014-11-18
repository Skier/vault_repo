
package com.dalworth.servman.domain
{
    import com.dalworth.servman.domain.codegen.*;
    
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="Servman.Domain.LeadStatus")]
    public class LeadStatus extends _LeadStatus
    {
    	public static const STATUS_NEW:String = "NEW";
    	public static const STATUS_CONTACTED:String = "CONTACTED";
    	public static const STATUS_CANCELLED:String = "CANCELLED";
    	public static const STATUS_QUALIFIED:String = "QUALIFIED";
    	public static const STATUS_CONVERTED:String = "CONVERTED";

    	public static const STATUS_NEW_ID:int = 1;
    	public static const STATUS_CONTACTED_ID:int = 2;
    	public static const STATUS_CANCELLED_ID:int = 3;
    	public static const STATUS_QUALIFIED_ID:int = 4;
    	public static const STATUS_CONVERTED_ID:int = 5;
    	
        public function LeadStatus()
        {
        }
        
        public static function getStatusName(id:int):String 
        {
        	var result:String;
        	switch (id) 
        	{
        		case STATUS_NEW_ID:
        			result = STATUS_NEW;
        			break;
        		case STATUS_CONTACTED_ID:
        			result = STATUS_CONTACTED;
        			break;
        		case STATUS_CANCELLED_ID:
        			result = STATUS_CANCELLED;
        			break;
        		case STATUS_QUALIFIED_ID:
        			result = STATUS_QUALIFIED;
        			break;
        		case STATUS_CONVERTED_ID:
        			result = STATUS_CONVERTED;
        			break;
        	}
        	return result;
        }

        public static function getStatuses():ArrayCollection 
        {
        	var result:ArrayCollection = new ArrayCollection();
        	var leadStatus:LeadStatus;
        	
        	leadStatus = new LeadStatus();
        	leadStatus.Id = STATUS_NEW_ID;
        	leadStatus.Name = STATUS_NEW;
        	result.addItem(leadStatus);

        	leadStatus = new LeadStatus();
        	leadStatus.Id = STATUS_CONTACTED_ID;
        	leadStatus.Name = STATUS_CONTACTED;
        	result.addItem(leadStatus);

        	leadStatus = new LeadStatus();
        	leadStatus.Id = STATUS_QUALIFIED_ID;
        	leadStatus.Name = STATUS_QUALIFIED;
        	result.addItem(leadStatus);

        	leadStatus = new LeadStatus();
        	leadStatus.Id = STATUS_CANCELLED_ID;
        	leadStatus.Name = STATUS_CANCELLED;
        	result.addItem(leadStatus);

        	leadStatus = new LeadStatus();
        	leadStatus.Id = STATUS_CONVERTED_ID;
        	leadStatus.Name = STATUS_CONVERTED;
        	result.addItem(leadStatus);

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
      
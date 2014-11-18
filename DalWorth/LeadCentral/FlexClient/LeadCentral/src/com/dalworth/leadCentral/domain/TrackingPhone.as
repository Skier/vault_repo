
package com.dalworth.leadCentral.domain
{
    import com.dalworth.leadCentral.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Dalworth.LeadCentral.Domain.TrackingPhone")]
    public class TrackingPhone extends _TrackingPhone
    {
    	public var LeadSourceTrackingPhones:Array;
    	
        public function TrackingPhone()
        {
        }

    	public static function getEmpty():TrackingPhone
    	{
    		var result:TrackingPhone = new TrackingPhone();
	    		result.Number = "Any phone number";
	    		result.Description = "Any phone number";
    		return result;
    	}
    	
    	override public function applyFields(value:Object):void
    	{
    		super.applyFields(value);
    		LeadSourceTrackingPhones = value["LeadSourceTrackingPhones"] as Array;
    	}
    	
    }
}
      
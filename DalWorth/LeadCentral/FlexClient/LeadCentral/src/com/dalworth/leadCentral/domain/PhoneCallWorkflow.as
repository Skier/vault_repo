
package com.dalworth.leadCentral.domain
{
    import com.affilia.util.DateUtil;
    import com.dalworth.leadCentral.domain.codegen.*;
    
    import mx.formatters.PhoneFormatter;

    [Bindable]
    [RemoteClass(alias="Dalworth.LeadCentral.Domain.PhoneCallWorkflow")]
    public class PhoneCallWorkflow extends _PhoneCallWorkflow
    {
    	private var _relatedPhoneTo:TrackingPhone;
    	public function get RelatedPhoneTo():TrackingPhone { return _relatedPhoneTo; }
    	public function set RelatedPhoneTo(value:TrackingPhone):void 
    	{
    		_relatedPhoneTo = value;
    		updateProperties();
    	}

    	private var _relatedWorkflow:CallWorkflow;
    	public function get RelatedWorkflow():CallWorkflow { return _relatedWorkflow; }
    	public function set RelatedWorkflow(value:CallWorkflow):void 
    	{
    		_relatedWorkflow = value;
    		updateProperties();
    	}
    	
    	public function updateProperties():void 
    	{
    		PhoneToStr = phoneFormatter.format(_relatedPhoneTo.Number);

    		if (FromWeekDay != 0 || ToWeekDay != 0)
    			DaysStr = DateUtil.getWeekDayName(FromWeekDay) + "-" + DateUtil.getWeekDayName(ToWeekDay);
    		else 
    			DaysStr = "All";
    			
    		if (FromTime != null && ToTime != null)
    		{
    			var fromTime:Date = new Date(1900,0,1,int(Number(FromTime.split(":")[0])),int(Number(FromTime.split(":")[1]))); 
    			var toTime:Date = new Date(1900,0,1,int(Number(ToTime.split(":")[0])),int(Number(ToTime.split(":")[1]))); 
    			TimeStr = DateUtil.getTimeStr(fromTime) + "-" + DateUtil.getTimeStr(toTime);
    		} else 
    		{
    			TimeStr = "All";
    		} 

    		if (_relatedWorkflow != null)
    			FlowStr = _relatedWorkflow.Description;
    		else 
    			FlowStr = "All";
    	}

		public var PhoneToStr:String;
		public var DaysStr:String;
		public var TimeStr:String;
		public var FlowStr:String;

		private var phoneFormatter:PhoneFormatter;
        
        public function PhoneCallWorkflow()
        {
        	phoneFormatter = new PhoneFormatter();
        	phoneFormatter.formatString = "##(###)###-####";
        }
    }
}
      
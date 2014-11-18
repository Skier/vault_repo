
package com.dalworth.servman.domain
{
    import com.affilia.util.DateUtil;
    import com.dalworth.servman.domain.codegen.*;
    
    import mx.formatters.PhoneFormatter;

    [Bindable]
    [RemoteClass(alias="Servman.Domain.PhoneCallWorkflow")]
    public class PhoneCallWorkflow extends _PhoneCallWorkflow
    {
    	private var _relatedPhoneFrom:Phone;
    	public function get RelatedPhoneFrom():Phone { return _relatedPhoneFrom; }
    	public function set RelatedPhoneFrom(value:Phone):void 
    	{
    		_relatedPhoneFrom = value;
    		updateProperties();
    	}

    	private var _relatedPhoneTo:Phone;
    	public function get RelatedPhoneTo():Phone { return _relatedPhoneTo; }
    	public function set RelatedPhoneTo(value:Phone):void 
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
    		if (_relatedPhoneFrom != null)
    			PhoneFromStr = phoneFormatter.format(_relatedPhoneFrom.Number);
    		else 
    			PhoneFromStr = "All";
    		
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

		public var PhoneFromStr:String;
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
      
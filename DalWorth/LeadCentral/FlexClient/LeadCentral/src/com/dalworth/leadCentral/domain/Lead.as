
package com.dalworth.leadCentral.domain
{
    import com.dalworth.leadCentral.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Dalworth.LeadCentral.Domain.Lead")]
    public class Lead extends _Lead
    {
    	public var transportDetail:Object;
    	
    	private var relatedPhoneCall:PhoneCall;
    	public function get RelatedPhoneCall():PhoneCall{return relatedPhoneCall;}
    	public function set RelatedPhoneCall(value:PhoneCall):void 
    	{
    		relatedPhoneCall = value;
    		transportDetail = value;
    	}

    	private var relatedSms:PhoneSms;
    	public function get RelatedSms():PhoneSms{return relatedSms;}
    	public function set RelatedSms(value:PhoneSms):void 
    	{
    		relatedSms = value;
    		transportDetail = value;
    	}

    	private var relatedForm:LeadForm;
    	public function get RelatedForm():LeadForm{return relatedForm;}
    	public function set RelatedForm(value:LeadForm):void 
    	{
    		relatedForm = value;
    		transportDetail = value;
    	}

    	public var RelatedQbInvoices:Array;

		public var AmountSummary:LeadAmountSummary;

        public function Lead()
        {
        }
        
        override public function applyFields(value:Object):void
        {
        	super.applyFields(value);
        	
        	if (value is Lead) 
        	{
	        	var lead:Lead = value as Lead;

				RelatedPhoneCall = lead.RelatedPhoneCall;
				RelatedSms = lead.RelatedSms;
				RelatedForm = lead.RelatedForm;
				RelatedQbInvoices = lead.RelatedQbInvoices;
        	}
        }

        public function get timeCreated():Number 
        {
        	if (DateCreated != null)
        		return DateCreated.time;
        	else 
        		return 0;
        }
        
        public function get ToPhoneNumber():String 
        {
        	if (RelatedPhoneCall)
        		return RelatedPhoneCall.PhoneTo;
        	else
        		return "";
        }
    }
}
      
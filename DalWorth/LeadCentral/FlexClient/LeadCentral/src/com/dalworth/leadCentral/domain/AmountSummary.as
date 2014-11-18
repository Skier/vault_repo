package com.dalworth.leadCentral.domain
{
	import mx.formatters.CurrencyFormatter;
	
    [Bindable]
    [RemoteClass(alias="Dalworth.LeadCentral.Domain.AmountSummary")]
	public class AmountSummary
	{
        public var SubTotalAmt:Number;
        public var TaxAmt:Number;
        public var TotalAmt:Number;
        
        public function push(summary:AmountSummary):void 
        {
        	if (isNaN(SubTotalAmt)) SubTotalAmt = 0;
        	if (isNaN(TaxAmt)) TaxAmt = 0;
        	if (isNaN(TotalAmt)) TotalAmt = 0;
        	
        	if (summary != null)
        	{
	        	SubTotalAmt += (isNaN(summary.SubTotalAmt) ? 0 : summary.SubTotalAmt);
	        	TaxAmt += (isNaN(summary.TaxAmt) ? 0 : summary.TaxAmt);
	        	TotalAmt += (isNaN(summary.TotalAmt) ? 0 : summary.TotalAmt);
        	}
        }
        
        public function get subTotalStr():String 
        {
        	if (!isNaN(SubTotalAmt))
        		return cf.format(SubTotalAmt);
        	else 
        		return "N/A";
        }
        
        public function get taxStr():String 
        {
        	if (!isNaN(TaxAmt))
        		return cf.format(TaxAmt);
        	else 
        		return "N/A";
        }
        
        public function get totalStr():String 
        {
        	if (!isNaN(TotalAmt))
        		return cf.format(TotalAmt);
        	else 
        		return "N/A";
        }
        
        public function AmountSummary()
        {
        	cf = new CurrencyFormatter();
        	cf.precision = 2;
        }
        
        private var cf:CurrencyFormatter;
	}
}

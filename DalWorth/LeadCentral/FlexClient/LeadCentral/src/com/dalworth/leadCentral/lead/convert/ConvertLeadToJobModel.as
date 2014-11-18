package com.dalworth.leadCentral.lead.convert
{
	import com.dalworth.leadCentral.domain.Lead;
	import com.dalworth.leadCentral.domain.QbInvoice;
	
	[Bindable]
	public class ConvertLeadToJobModel
	{
		public var currentLead:Lead;
		public var currentQbInvoice:QbInvoice;
		
		public var isBusy:Boolean = false;
		
		private static var _instance:ConvertLeadToJobModel;
		public static function getInstance():ConvertLeadToJobModel
		{
			if (_instance == null)
				_instance = new ConvertLeadToJobModel(new Private());
			
			return _instance;
		}
		
		public function ConvertLeadToJobModel(accessPrivate:Private) 
		{
			currentQbInvoice = new QbInvoice();
		}
		
		public function reset():void 
		{
			currentQbInvoice = new QbInvoice();
		}
	}
}

class Private {}

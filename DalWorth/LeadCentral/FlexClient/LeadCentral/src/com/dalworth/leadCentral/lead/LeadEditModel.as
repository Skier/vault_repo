package com.dalworth.leadCentral.lead
{
	import com.dalworth.leadCentral.domain.AmountSummary;
	import com.dalworth.leadCentral.domain.Lead;
	
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class LeadEditModel
	{
		public var lead:Lead;
		
		public var summary:AmountSummary;
		
		public var leadSources:ArrayCollection;
		public var users:ArrayCollection;

		public var leadStatuses:ArrayCollection;
		
		public var relatedInvoices:ArrayCollection;
		
		public var isBusy:Boolean;
		
		public var isReadOnly:Boolean = false;

		private static var _instance:LeadEditModel;
		public static function getInstance():LeadEditModel
		{
			if (_instance == null)
				_instance = new LeadEditModel(new Private());
			
			return _instance;
		}
		
		public function LeadEditModel(accessPrivate:Private) 
		{
			leadSources = new ArrayCollection();
			users = new ArrayCollection();
			relatedInvoices = new ArrayCollection();
			leadStatuses = new ArrayCollection();
		}
	}
}

class Private {}
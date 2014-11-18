package com.dalworth.leadCentral.transport.leadForm
{
	import com.dalworth.leadCentral.domain.LeadForm;
	
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;

	[Bindable]
	public class LeadFormModel
	{
		public var isBusy:Boolean = false;
		
		public var leadForm:LeadForm;
		
		public var rotations:ArrayCollection;
		
		public var cameFrom:String;
		public var callFrom:String;
		public var callTime:Date;
		public var pagesVisited:int;
		public var searchQuery:String;
		
		private static var _instance:LeadFormModel;
		public static function getInstance():LeadFormModel
		{
			if (_instance == null)
				_instance = new LeadFormModel(new Private());
			
			return _instance;
		}
		
		public function LeadFormModel(accessPrivate:Private) 
		{
			rotations = new ArrayCollection();
			
			var sort:Sort = new Sort();
			sort.fields = [new SortField("timeDisplayed", false, false, true)];
			rotations.sort = sort;
		}
	}
}	

class Private {}

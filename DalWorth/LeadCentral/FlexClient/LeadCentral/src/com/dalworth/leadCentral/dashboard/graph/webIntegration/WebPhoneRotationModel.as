package com.dalworth.leadCentral.dashboard.graph.webIntegration
{
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;
	
	[Bindable]
	public class WebPhoneRotationModel
	{
		public var dailyCollection:ArrayCollection;
		public var rotations:ArrayCollection;
		
		public var selectedLeadSources:ArrayCollection; 
		public var startDate:Date;
		public var endDate:Date;
		
		public var isBusy:Boolean;

		private static var _instance:WebPhoneRotationModel;
		public static function getInstance():WebPhoneRotationModel
		{
			if (_instance == null)
				_instance = new WebPhoneRotationModel(new Private());
			
			return _instance;
		}
		
		public function WebPhoneRotationModel(accessPrivate:Private) 
		{
			dailyCollection = new ArrayCollection();
			rotations = new ArrayCollection();

			var sort:Sort = new Sort();
			sort.fields = [new SortField("timeDisplayed", false, true, true)];
			rotations.sort = sort;
		}
		
	}
}

class Private {}

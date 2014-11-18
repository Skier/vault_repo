package com.dalworth.leadCentral.transport.phoneCall
{
	import com.dalworth.leadCentral.domain.PhoneCall;
	
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;

	[Bindable]
	public class PhoneCallModel
	{
		public static const MODEL_INITED:String = "LeadsModelInited";
		
		public var isBusy:Boolean = false;
		
		public var phoneCall:PhoneCall;
		
		public var rotations:ArrayCollection;
		
		public var cameFrom:String;
		public var callFrom:String;
		public var callTime:Date;
		public var pagesVisited:int;
		public var searchQuery:String;
		
		private static var _instance:PhoneCallModel;
		public static function getInstance():PhoneCallModel
		{
			if (_instance == null)
				_instance = new PhoneCallModel(new Private());
			
			return _instance;
		}
		
		public function PhoneCallModel(accessPrivate:Private) 
		{
			rotations = new ArrayCollection();
			
			var sort:Sort = new Sort();
			sort.fields = [new SortField("timeDisplayed", false, false, true)];
			rotations.sort = sort;
		}
	}
}	

class Private {}

package com.dalworth.leadCentral.transport.phoneSms
{
	import com.dalworth.leadCentral.domain.PhoneSms;
	
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;

	[Bindable]
	public class PhoneSmsModel
	{
		public var isBusy:Boolean = false;
		
		public var phoneSms:PhoneSms;
		
		public var rotations:ArrayCollection;
		
		public var cameFrom:String;
		public var callFrom:String;
		public var callTime:Date;
		public var pagesVisited:int;
		public var searchQuery:String;
		
		private static var _instance:PhoneSmsModel;
		public static function getInstance():PhoneSmsModel
		{
			if (_instance == null)
				_instance = new PhoneSmsModel(new Private());
			
			return _instance;
		}
		
		public function PhoneSmsModel(accessPrivate:Private) 
		{
			rotations = new ArrayCollection();
			
			var sort:Sort = new Sort();
			sort.fields = [new SortField("timeDisplayed", false, false, true)];
			rotations.sort = sort;
		}
	}
}	

class Private {}

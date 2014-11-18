package com.dalworth.servman.main.owner.setting.customerServiceRep
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class CustomerServiceRepsModel
	{
		public var customerServiceReps:ArrayCollection;
		public var isBusy:Boolean;
		
		private static var _instance:CustomerServiceRepsModel;
		public static function getInstance():CustomerServiceRepsModel
		{
			if (_instance == null)
				_instance = new CustomerServiceRepsModel(new Private());
			
			return _instance;
		}
		
		public function CustomerServiceRepsModel(accessPrivate:Private) 
		{
			customerServiceReps = new ArrayCollection();
		}
	}
}

class Private {}

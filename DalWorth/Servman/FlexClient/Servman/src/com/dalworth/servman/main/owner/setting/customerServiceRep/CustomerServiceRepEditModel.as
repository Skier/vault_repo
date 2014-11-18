package com.dalworth.servman.main.owner.setting.customerServiceRep
{
	import com.dalworth.servman.domain.CustomerServiceRep;
	
	[Bindable]
	public class CustomerServiceRepEditModel
	{
		public var customerServiceRep:CustomerServiceRep;
		
		public var isBusy:Boolean;

		private static var _instance:CustomerServiceRepEditModel;
		public static function getInstance():CustomerServiceRepEditModel
		{
			if (_instance == null)
				_instance = new CustomerServiceRepEditModel(new Private());
			
			return _instance;
		}
		
		public function CustomerServiceRepEditModel(accessPrivate:Private) 
		{
		}
	}
}

class Private {}
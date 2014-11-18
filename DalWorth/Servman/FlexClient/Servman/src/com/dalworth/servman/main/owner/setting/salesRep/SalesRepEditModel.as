package com.dalworth.servman.main.owner.setting.salesRep
{
	import com.dalworth.servman.domain.SalesRep;
	
	[Bindable]
	public class SalesRepEditModel
	{
		public var salesRep:SalesRep;
		
		public var isBusy:Boolean;

		private static var _instance:SalesRepEditModel;
		public static function getInstance():SalesRepEditModel
		{
			if (_instance == null)
				_instance = new SalesRepEditModel(new Private());
			
			return _instance;
		}
		
		public function SalesRepEditModel(accessPrivate:Private) 
		{
		}
	}
}

class Private {}
package com.dalworth.servman.main.owner.setting.salesRep
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class SalesRepsModel
	{
		public var salesReps:ArrayCollection;
		public var isBusy:Boolean;
		
		private static var _instance:SalesRepsModel;
		public static function getInstance():SalesRepsModel
		{
			if (_instance == null)
				_instance = new SalesRepsModel(new Private());
			
			return _instance;
		}
		
		public function SalesRepsModel(accessPrivate:Private) 
		{
			salesReps = new ArrayCollection();
		}
	}
}

class Private {}

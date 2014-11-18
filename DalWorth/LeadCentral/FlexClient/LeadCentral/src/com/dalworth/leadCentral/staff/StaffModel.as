package com.dalworth.leadCentral.staff
{
	[Bindable]
	public class StaffModel
	{
		public var isBusy:Boolean = false;
		
		private static var _instance:StaffModel;
		public static function getInstance():StaffModel
		{
			if (_instance == null)
				_instance = new StaffModel(new Private());
			
			return _instance;
		}
		
		public function StaffModel(accessPrivate:Private) 
		{
		}
	}
}	

class Private {}

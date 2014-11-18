package com.dalworth.servman.main.breadCrumb
{
	[Bindable]
	public class BreadCrumbModel
	{
		public var breadCrumbString:String = "empty > empty > empty > empty > empty > empty";
		
		private static var _instance:BreadCrumbModel;
		public static function getInstance():BreadCrumbModel
		{
			if (_instance == null)
				_instance = new BreadCrumbModel(new Private());
			
			return _instance;
		}
		
		public function BreadCrumbModel(accessPrivate:Private) 
		{
		}
	}
}

class Private {}
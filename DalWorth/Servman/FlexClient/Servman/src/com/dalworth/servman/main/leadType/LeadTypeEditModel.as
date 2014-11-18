package com.dalworth.servman.main.ptojectType
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class LeadTypeEditModel
	{
		public var leadType:LeadType;

		public var jobTypes:ArrayCollection;

		public var isBusy:Boolean;
		
		private static var _instance:ProjectTypeEditModel;
		public static function getInstance():ProjectTypeEditModel
		{
			if (_instance == null)
				_instance = new ProjectTypeEditModel(new Private());
			
			return _instance;
		}
		
		public function ProjectTypeEditModel(accessPrivate:Private) 
		{
			jobTypes = new ArrayCollection();
		}
	}
}

class Private {}
package com.dalworth.servman.main.project
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class ProjectEditModel
	{
		public var projectTypes:ArrayCollection;
		public var customerList:ArrayCollection;
		public var jobList:ArrayCollection;
		
		private static var _instance:ProjectEditModel;
		public static function getInstance():ProjectEditModel
		{
			if (_instance == null)
				_instance = new ProjectEditModel(new Private());
			
			return _instance;
		}
		
		public function ProjectEditModel(accessPrivate:Private) 
		{
			customerList = new ArrayCollection();
			jobList = new ArrayCollection();
		}
	}
}

class Private {}

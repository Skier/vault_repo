package com.dalworth.servman.main.owner.setting.projectType
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class ProjectTypesModel
	{
		public var projectTypes:ArrayCollection;
		
		private static var _instance:ProjectTypesModel;
		public static function getInstance():ProjectTypesModel
		{
			if (_instance == null)
				_instance = new ProjectTypesModel(new Private());
			
			return _instance;
		}
		
		public function ProjectTypesModel(accessPrivate:Private) 
		{
			projectTypes = new ArrayCollection();
		}
	}
}

class Private {}

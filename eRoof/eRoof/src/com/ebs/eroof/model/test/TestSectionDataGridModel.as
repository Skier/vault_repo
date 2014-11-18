package com.ebs.eroof.model.test
{
   	import com.adobe.cairngorm.model.IModelLocator;
   	import com.ebs.eroof.model.wrapper.Section;
   	
   	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class TestSectionDataGridModel implements IModelLocator
	{
		public var collection:ArrayCollection;
		public var sectionsCount:int;
		public var pageSize:int;
		public var currentPageNo:int;
		public var pages:int;
		
		public var currentSection:Section;
		
		private static var _instance:TestSectionDataGridModel;
        public static function getInstance():TestSectionDataGridModel
        {
			if (_instance == null)
            	_instance = new TestSectionDataGridModel(new Private());
			
			return _instance;
		}
         
		public function TestSectionDataGridModel(accessPrivate:Private) 
		{
			collection = new ArrayCollection();
		}
	}
}

class Private {}

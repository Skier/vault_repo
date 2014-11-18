package com.titus.catalog.model
{
	
	public class CatalogLocation
	{
		
		private var _section:String;
		public function get section():String {
			return _section;
		}
		
		private var _sectionPage:int;
		public function get sectionPage():int {
			return _sectionPage;
		}
		
		private var _catalogPage:int;
		public function get catalogPage():int {
			return _catalogPage;
		}
		
		public function CatalogLocation(aSection:String, aSectionPage:int, aCatalogPage:int)
		{
			_section = aSection;
			_sectionPage = aSectionPage;
			_catalogPage = aCatalogPage;
		}

	}
}
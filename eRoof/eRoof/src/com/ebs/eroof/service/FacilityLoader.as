package com.ebs.eroof.service
{
	import com.ebs.eroof.dto.Sections_DTO;
	import com.ebs.eroof.model.wrapper.Facility;
	import com.ebs.eroof.model.wrapper.Section;
	import com.quickbase.idn.model.QuickBaseDTOArrayCollection;
	
	import flash.events.Event;
	import flash.events.EventDispatcher;

	public class FacilityLoader extends EventDispatcher
	{
		public var facility:Facility;
		
		public function FacilityLoader(facility:Facility)
		{
			super(null);

			this.facility = facility;
		}
		
		private var isSectionsLoaded:Boolean;
		private var sections:QuickBaseDTOArrayCollection;
		public function load():void 
		{
			facility.sectionsCollection.removeAll();
			
			isSectionsLoaded = false;
			sections = new QuickBaseDTOArrayCollection(Sections_DTO, true, "{'29'.CT.'"+facility.facilityDTO.rid+"'}");
			sections.addEventListener(QuickBaseDTOArrayCollection.DATA_INITED, onSectionsLoaded);
			sections.initData();
		}
		
		public function loadSections():void 
		{
			for each (var section:Section in facility.sectionsCollection) 
			{
				section.isLoaded = false;
				var sectionLoader:SectionLoader = new SectionLoader(section);
				sectionLoader.addEventListener("sectionLoaded", onSectionLoaded);
				sectionLoader.load();
			}
		}
		
		private function onSectionLoaded(event:Event):void
		{
			var sectionLoader:SectionLoader = event.target as SectionLoader;
			if (isAllSectionsLoaded) 
			{
				facility.isLoaded = true;
				dispatchEvent(new Event("facilityLoaded"));
			}
		}
		
		private function get isAllSectionsLoaded():Boolean 
		{
			for each (var section:Section in facility.sectionsCollection) 
			{
				if (!section.isLoaded)
					return false;
			}
			
			return true;
		}  
		
		private function onSectionsLoaded():void 
		{
			for each (var sectionDTO:Sections_DTO in sections) 
			{
				var section:Section = new Section(sectionDTO);
				facility.sectionsCollection.addItem(section);
			}
			
			loadSections();
		}
		
	}
}

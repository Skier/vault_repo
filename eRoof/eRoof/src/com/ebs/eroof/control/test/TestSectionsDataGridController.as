package com.ebs.eroof.control.test
{
	import com.ebs.eroof.control.section.SectionServiceController;
	import com.ebs.eroof.dto.Sections_DTO;
	import com.ebs.eroof.model.test.TestSectionDataGridModel;
	import com.ebs.eroof.model.wrapper.Section;
	import com.quickbase.idn.model.QuickBaseDTOArrayCollection;
	
	import flash.events.Event;
	import flash.events.EventDispatcher;
	
	import mx.core.UIComponent;
	import mx.events.DynamicEvent;

	public class TestSectionsDataGridController extends EventDispatcher
	{
		public static const INIT_MODEL:String = "initModel";

		public static const INIT_SECTIONS_COUNT:String = "initSectionsCount";
		public static const SECTIONS_COUNT_INITED:String = "sectionsCountInited";

		public static const GET_SECTIONS_PAGE:String = "getSectionsPage";
		public static const SET_SECTIONS_PAGE_SIZE:String = "setSectionsPageSize";
		
		private var model:TestSectionDataGridModel = TestSectionDataGridModel.getInstance();
		private var view:UIComponent;
		
		public function TestSectionsDataGridController(view:UIComponent)
		{
			this.view = view;
			addEventListener(SECTIONS_COUNT_INITED, sectionsCountInitHandler);
		}
		
		public function initModel():void 
		{
   			if (model.pageSize == 0)
   				model.pageSize = 100;
   			model.currentPageNo = 1;
			initSectionsCount();
		}
		
		public function getFirstPage():void 
		{
			getSectionsPage(1);
		}
		
		public function getPrevPage():void 
		{
			if (model.currentPageNo > 1)
				getSectionsPage(model.currentPageNo - 1);
		}
		
		public function getNextPage():void 
		{
			if (model.currentPageNo < model.pages)
				getSectionsPage(model.currentPageNo + 1);
		}
		
		public function getLastPage():void 
		{
			getSectionsPage(model.pages);
		}
		
		private function sectionsCountInitHandler(event:Event):void 
		{
			getSectionsPage(model.currentPageNo);
		}
		
		private function initSectionsCount():void 
		{
			var sections_DTO:QuickBaseDTOArrayCollection = SectionServiceController.getInstance().getAllNames(); 
			sections_DTO.addEventListener(QuickBaseDTOArrayCollection.DATA_INITED,
           		function (ev:*):void 
           		{
           			model.sectionsCount = sections_DTO.length;
           			model.pages = Math.ceil(model.sectionsCount / model.pageSize);
           			
           			getCurrentPage();
           		});
		}
		
		private function getCurrentPage():void 
		{
			getSectionsPage(model.currentPageNo);
		}
		
		public function getSectionsPage(pageNo:int):void 
		{
			model.collection.removeAll();
			
			var sections:QuickBaseDTOArrayCollection = SectionServiceController.getInstance().getSectionPage(pageNo, model.pageSize); 
			sections.addEventListener(QuickBaseDTOArrayCollection.DATA_INITED,
           		function (ev:*):void 
           		{
           			for each (var s:Sections_DTO in sections)
           			{
           				var section:Section = new Section(s);
           				model.collection.addItem(section);
           			}

           			model.currentPageNo = pageNo;
           		});
		}
		
		private function setSectionsPageSize(event:DynamicEvent):void 
		{
			var pageSize:int = event.pageSize as int;
			model.pageSize = pageSize;
			getSectionsPage(model.currentPageNo);
		}
		
	}
}
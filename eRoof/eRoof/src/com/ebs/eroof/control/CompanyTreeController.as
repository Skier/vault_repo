package com.ebs.eroof.control
{
	import com.ebs.eroof.dto.Clients_DTO;
	import com.ebs.eroof.dto.Consultant_DTO;
	import com.ebs.eroof.dto.Facilities_DTO;
	import com.ebs.eroof.dto.Sections_DTO;
	import com.ebs.eroof.dto.Segments_DTO;
	import com.ebs.eroof.model.company.CompanyTreeModel;
	import com.ebs.eroof.model.wrapper.Client;
	import com.ebs.eroof.model.wrapper.Company;
	import com.ebs.eroof.model.wrapper.Facility;
	import com.ebs.eroof.model.wrapper.Section;
	import com.ebs.eroof.model.wrapper.Segment;
	import com.quickbase.idn.model.QuickBaseDTOArrayCollection;
	import com.quickbase.idn.model.QuickBaseMSAModel;
	
	import flash.events.Event;
	import flash.utils.Dictionary;
	
	import mx.collections.Sort;
	import mx.collections.SortField;
	import mx.core.UIComponent;
	
	public class CompanyTreeController
	{
		public static const INIT_COMPANY_TREE:String = "initCompanyTree";
		public static const COMPANY_TREE_INITED:String = "companyTreeInited";
		
		private var model:CompanyTreeModel = CompanyTreeModel.getInstance();
		private var view:UIComponent;
		
		public function CompanyTreeController(view:UIComponent)
		{
			this.view = view;
			this.view.addEventListener(INIT_COMPANY_TREE, initCompanyTreeHandler);
		}
		
		private var segmentsInited:Boolean = false;
		private var clientsInited:Boolean = false;
		private var facilitiesInited:Boolean = false;
		private var sectionsInited:Boolean = false;
		
		private var segments:QuickBaseDTOArrayCollection;
		private var clients:QuickBaseDTOArrayCollection;
		private var facilities:QuickBaseDTOArrayCollection;
		private var sections:QuickBaseDTOArrayCollection;
		
		private function initCompanyTreeHandler(ev:*):void 
		{
			//model.company.name = "Loading...";
			var mainModel:QuickBaseMSAModel = QuickBaseMSAModel.getInstance();
			//model.company.name = mainModel.
			
			model.isLoaded = false;
			model.isLoading = true;
			
			initSegments();
			initClients();
			initFacilities();
			initSections();
		}
		
		private var segmentsHash:Dictionary;
		private function initSegments():void 
		{
			segments = new QuickBaseDTOArrayCollection(Segments_DTO);
			segments.initData();
           	segments.addEventListener(QuickBaseDTOArrayCollection.DATA_INITED, 
           		function (ev:*):void 
           		{
           			segmentsHash = new Dictionary();
           			for each (var s:Segments_DTO in segments)
           			{
           				segmentsHash[s.rid] = new Segment(s);
           			}
           			segmentsInited = true;
           			tryParseData();
           		});
		}
		
		private var clientsHash:Dictionary;
		private function initClients():void 
		{
			clients = new QuickBaseDTOArrayCollection(Clients_DTO);
			clients.initData();
           	clients.addEventListener(QuickBaseDTOArrayCollection.DATA_INITED, 
           		function (ev:*):void 
           		{
           			clientsHash = new Dictionary();
           			for each (var c:Clients_DTO in clients)
           			{
           				clientsHash[c.rid] = new Client(c);
           			}
           			clientsInited = true;
           			tryParseData();
           		});
		}
		
		private var facilitiesHash:Dictionary;
		private function initFacilities():void 
		{
			facilities = new QuickBaseDTOArrayCollection(Facilities_DTO);
			facilities.initData();
           	facilities.addEventListener(QuickBaseDTOArrayCollection.DATA_INITED, 
           		function (ev:*):void 
           		{
           			facilitiesHash = new Dictionary();
           			for each (var f:Facilities_DTO in facilities)
           			{
           				facilitiesHash[f.rid] = new Facility(f);
           			}
           			facilitiesInited = true;
           			tryParseData();
           		});
		}
		
		private var sectionsHash:Dictionary;
		private function initSections():void 
		{
			sections = new QuickBaseDTOArrayCollection(Sections_DTO);
			sections.initData();
           	sections.addEventListener(QuickBaseDTOArrayCollection.DATA_INITED, 
           		function (ev:*):void 
           		{
           			sectionsHash = new Dictionary();
           			for each (var s:Sections_DTO in sections)
           			{
           				sectionsHash[s.rid] = new Section(s);
           			}
           			sectionsInited = true;
           			tryParseData();
           		});
		}
		
		private function tryParseData():void 
		{
			if (segmentsInited && clientsInited && facilitiesInited && sectionsInited)
				parseData();
		}
		
		private function parseData():void 
		{
			parseSections();
			parseFacilities();
			parseClients();
			parseSegments();
		}
		
		private function parseSections():void 
		{
			for each (var s:Sections_DTO in sections) 
			{
				var section:Section = sectionsHash[s.rid] as Section;
				var facility:Facility = facilitiesHash[Number(section.sectionDTO.RelatedFacility)] as Facility;
				facility.sectionsCollection.addItem(section);
				section.facility = facility;
			}
		}

		private function parseFacilities():void 
		{
			for each (var f:Facilities_DTO in facilities) 
			{
				var facility:Facility = facilitiesHash[f.rid] as Facility;
				var client:Client = clientsHash[Number(facility.facilityDTO.RelatedClient)] as Client;
				client.facilityCollection.addItem(facility);
				facility.client = client;

				var sectionSort:Sort = new Sort();
				sectionSort.fields = [new SortField("name", true)];
				facility.sectionsCollection.sort = sectionSort;
				facility.sectionsCollection.refresh();
			}
		}

		private function parseClients():void 
		{
			for each (var c:Clients_DTO in clients) 
			{
				var client:Client = clientsHash[c.rid] as Client;
				var segment:Segment = segmentsHash[client.clientDTO.RelatedSegment] as Segment;
				segment.clientCollection.addItem(client);
				client.segment = segment;

				var facilitySort:Sort = new Sort();
				facilitySort.fields = [new SortField("name", true)];
				client.facilityCollection.sort = facilitySort;
				client.facilityCollection.refresh();
			}
		}

		private function parseSegments():void 
		{
			initCompany();
			
			for each (var s:Segments_DTO in segments) 
			{
				var segment:Segment = segmentsHash[s.rid] as Segment;
				model.company.segmentCollection.addItem(segment);
				segment.company = model.company;

				var clientSort:Sort = new Sort();
				clientSort.fields = [new SortField("name", true)];
				segment.clientCollection.sort = clientSort;
				segment.clientCollection.refresh();
			}
			
			var segmentSort:Sort = new Sort();
			segmentSort.fields = [new SortField("name", true)];
			model.company.segmentCollection.sort = segmentSort;
			model.company.segmentCollection.refresh();
			
			model.isLoaded = true;
			model.isLoading = false;
			
			view.dispatchEvent(new Event(COMPANY_TREE_INITED));
		}
		
		private function initCompany():void 
		{
			var consultants:QuickBaseDTOArrayCollection = new QuickBaseDTOArrayCollection(Consultant_DTO);
			consultants.initData();
           	consultants.addEventListener(QuickBaseDTOArrayCollection.DATA_INITED, 
           		function (ev:*):void 
           		{
           			if (consultants.length > 0) 
           			{
           				model.company.consultantDTO = consultants[0] as Consultant_DTO;
           				model.company.name = model.company.consultantDTO.CompanyName;
           			} else 
           			{
           				model.company.name = Company.DEFAULT_COMPANY_NAME;
           			}
           		});
		}

	}
}
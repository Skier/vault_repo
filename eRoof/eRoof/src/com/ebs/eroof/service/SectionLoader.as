package com.ebs.eroof.service
{
	import com.ebs.eroof.dto.CorePhotos_DTO;
	import com.ebs.eroof.dto.Expenditures_DTO;
	import com.ebs.eroof.dto.Inspections_DTO;
	import com.ebs.eroof.dto.Layers_DTO;
	import com.ebs.eroof.model.wrapper.CorePhoto;
	import com.ebs.eroof.model.wrapper.Expenditure;
	import com.ebs.eroof.model.wrapper.Inspection;
	import com.ebs.eroof.model.wrapper.Layer;
	import com.ebs.eroof.model.wrapper.Section;
	import com.quickbase.idn.model.QuickBaseDTOArrayCollection;
	
	import flash.events.Event;
	import flash.events.EventDispatcher;

	public class SectionLoader extends EventDispatcher
	{
		public var section:Section;
		
		public function SectionLoader(section:Section)
		{
			super(null);
			
			this.section = section;
		}
		
		private var isCorePhotosLoaded:Boolean;
		private var isLayersLoaded:Boolean;
		private var isInspectionsLoaded:Boolean;
		private var isExpendituresLoaded:Boolean;
		
		public function load():void 
		{
			loadCorePhotos();
			loadLayers();
			loadInspections();
			loadExpenditures();
			
			section.isLoaded = false;
		}
		
		private function loadCorePhotos():void 
		{
			isCorePhotosLoaded = false;
			section.corePhotosCollection.removeAll();
			var corePhotos:QuickBaseDTOArrayCollection = new QuickBaseDTOArrayCollection(CorePhotos_DTO, true, "{'10'.EX.'"+section.sectionDTO.rid+"'}");
			corePhotos.addEventListener(QuickBaseDTOArrayCollection.DATA_INITED, 
				function (event:Event):void 
				{
					for each (var corePhotoDTO:CorePhotos_DTO in corePhotos) 
					{
						var corePhoto:CorePhoto = new CorePhoto(corePhotoDTO);
						corePhoto.section = section;
						section.corePhotosCollection.addItem(corePhoto);
					}
					isCorePhotosLoaded = true;
					tryCommit();
				});
			corePhotos.initData();
		}
		
		private function loadLayers():void 
		{
			isLayersLoaded = false;
			section.layersCollection.removeAll();
			var layers:QuickBaseDTOArrayCollection = new QuickBaseDTOArrayCollection(Layers_DTO, true, "{'10'.EX.'"+section.sectionDTO.rid+"'}","","LayerNumber");
			layers.addEventListener(QuickBaseDTOArrayCollection.DATA_INITED, 
				function (event:Event):void 
				{
					for each (var layerDTO:Layers_DTO in layers) 
					{
						var layer:Layer = new Layer(layerDTO);
						layer.section = section;
						section.layersCollection.addItem(layer);
					}
					isLayersLoaded = true;
					tryCommit();
				});
			layers.initData();
		}
		
		private function loadInspections():void 
		{
			isInspectionsLoaded = false;
			section.inspectionsCollection.removeAll();
			var inspections:QuickBaseDTOArrayCollection = new QuickBaseDTOArrayCollection(Inspections_DTO, true, "{'11'.EX.'"+section.sectionDTO.rid+"'}");
			inspections.addEventListener(QuickBaseDTOArrayCollection.DATA_INITED, 
				function (event:Event):void 
				{
					for each (var inspectionDTO:Inspections_DTO in inspections) 
					{
						var inspection:Inspection = new Inspection(inspectionDTO);
						inspection.section = section;
						section.inspectionsCollection.addItem(inspection);
					}
					isInspectionsLoaded = true;
					tryCommit();
				});
			inspections.initData();
		}
		
		private function loadExpenditures():void 
		{
			isExpendituresLoaded = false;
			section.expendituresCollection.removeAll();
			var expenditures:QuickBaseDTOArrayCollection = new QuickBaseDTOArrayCollection(Expenditures_DTO, true, "{'12'.EX.'"+section.sectionDTO.rid+"'}");
			expenditures.addEventListener(QuickBaseDTOArrayCollection.DATA_INITED, 
				function (event:Event):void 
				{
					for each (var expenditureDTO:Expenditures_DTO in expenditures) 
					{
						var expenditure:Expenditure = new Expenditure(expenditureDTO);
						expenditure.section = section;
						section.expendituresCollection.addItem(expenditure);
					}
					isExpendituresLoaded = true;
					tryCommit();
				});
			expenditures.initData();
		}
		
		private function tryCommit():void 
		{
			if (isCorePhotosLoaded && isLayersLoaded && isInspectionsLoaded && isExpendituresLoaded)
			{
				section.isLoaded = true;
				dispatchEvent(new Event("sectionLoaded"));
			}
				
		}
	}
}
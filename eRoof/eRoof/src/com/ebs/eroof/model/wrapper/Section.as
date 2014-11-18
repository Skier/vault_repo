package com.ebs.eroof.model.wrapper
{
	import com.ebs.eroof.dto.Sections_DTO;
	import com.ebs.eroof.mapping.mapClasses.SectionPolygon;
	
	import flash.events.EventDispatcher;
	
	import mx.collections.ArrayCollection;
	import mx.formatters.CurrencyFormatter;
	import mx.formatters.NumberFormatter;
	import mx.utils.UIDUtil;

	[Bindable]
	public class Section extends EventDispatcher
	{
		private static const POLYGON_DIVIDER:String = "|";
		
		public var isLoaded:Boolean = false;
		public var uniquePhotoId:String;

		public var sectionDTO:Sections_DTO;
		
		private var _facility:Facility;
		public function get facility():Facility { return _facility; }
		public function set facility(value:Facility):void 
		{
			_facility = value;
		}
		
		private var _layersCollection:ArrayCollection;
		public function get layersCollection():ArrayCollection 
		{ 
			return _layersCollection; 
		}
		
		private var _corePhotosCollection:ArrayCollection;
		public function get corePhotosCollection():ArrayCollection 
		{ 
			return _corePhotosCollection; 
		}
		
		private var _inspectionsCollection:ArrayCollection;
		public function get inspectionsCollection():ArrayCollection 
		{ 
			return _inspectionsCollection; 
		}
		
		private var _expendituresCollection:ArrayCollection;
		public function get expendituresCollection():ArrayCollection 
		{ 
			return _expendituresCollection; 
		}
		
		public function get name():String
		{
			return sectionDTO.Designation + " - " + sectionDTO.RoofName;
		}
		
		public function get complexName():String
		{
			return sectionDTO.Facility + "/"  + sectionDTO.RoofName;
		}
		
		public function get sqFt():String
		{
			var nf:NumberFormatter = new NumberFormatter();
			nf.precision = 0;
			nf.useThousandsSeparator = true;

			return nf.format(sectionDTO.SqFt);
		}
		
		public function get estCostPerSqFt():String
		{
			var cf:CurrencyFormatter = new CurrencyFormatter();
			return cf.format(sectionDTO.EstCostPerSqFt);
		}
		
		public function get estCost():String
		{
			var cf:CurrencyFormatter = new CurrencyFormatter();
			return cf.format(sectionDTO.EstCost);
		}
		
		public function Section(dto:Sections_DTO)
		{
			super();

			if (dto == null)
				throw new Error("Section::Section() - DTO object can not be null!");
			
			sectionDTO = dto;
			_layersCollection = new ArrayCollection();
			_corePhotosCollection = new ArrayCollection();
			_inspectionsCollection = new ArrayCollection();
			_expendituresCollection = new ArrayCollection();
			
			uniquePhotoId = UIDUtil.createUID();
		}
		
		public function addToFacility(parentFacility:Facility = null):void 
		{
			if (parentFacility == null)
				parentFacility = facility;
			
			if (parentFacility == null)
				return;
			
			parentFacility.sectionsCollection.addItem(this);
			this.facility = parentFacility;
		}
		
		public function updateGeometry(polygons:Array):void 
		{
			var geometryStr:String = "";
			
			for each (var polygon:SectionPolygon in polygons) 
			{
				if (geometryStr != "")
					geometryStr += POLYGON_DIVIDER;
				
				geometryStr += polygon.toDBString();
			}
			
			sectionDTO.Polygon = geometryStr;
		}
		
		public function getRoofs():ArrayCollection 
		{
			var result:ArrayCollection = new ArrayCollection();
			
			var geometry:String = sectionDTO.Polygon;
			
			if (!geometry)
				return result;
			
			var polygons:Array = geometry.split(POLYGON_DIVIDER);
			for each (var polygonStr:String in polygons) 
			{
				var polygon:SectionPolygon = SectionPolygon.parse(polygonStr);
				if (polygon) 
					result.addItem(polygon);
			}
			
			return result;
		}
	}
}
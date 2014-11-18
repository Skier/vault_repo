package com.ebs.eroof.model.wrapper
{
	import com.afcomponents.umap.types.LatLng;
	import com.ebs.eroof.dto.Facilities_DTO;
	import com.ebs.eroof.mapping.mapClasses.FacilityMarker;
	
	import flash.events.EventDispatcher;
	
	import mx.collections.ArrayCollection;
	import mx.formatters.CurrencyFormatter;
	import mx.formatters.NumberFormatter;
	import mx.utils.UIDUtil;

	[Bindable]
	public class Facility extends EventDispatcher
	{
		private static const LAT_LNG_DIVIDER:String = ",";
		
		public var isLoaded:Boolean = false;
		public var uniquePhotoId:String;
		public var uniqueKeyplanId:String;
		
		public var facilityDTO:Facilities_DTO;
		
		private var _client:Client;
		public function get client():Client { return _client; }
		public function set client(value:Client):void 
		{
			_client = value;
		}
		
		private var _sectionsCollection:ArrayCollection;
		public function get sectionsCollection():ArrayCollection 
		{ 
			return _sectionsCollection; 
		}
		
		public function get clientName():String 
		{
			return client.name;
		} 
		
		public function get name():String
		{
			return facilityDTO.FacilityName;
		}
		
		public function get briefName():String
		{
			return facilityDTO.BriefName;
		}
		
		public function get primaryContact():String
		{
			return facilityDTO.PrimaryContact;
		}
		
		public function get totalSqFt():String
		{
			var nf:NumberFormatter = new NumberFormatter();
			nf.useThousandsSeparator = true;
			nf.precision = 0;
			
			
			return nf.format(facilityDTO.TotalSqFt);
		}
		
		public function get totalValue():String
		{
			var cf:CurrencyFormatter = new CurrencyFormatter();
			cf.useThousandsSeparator = true;
			cf.precision = 2;
			
			return cf.format(facilityDTO.TotalValue);
		}
		
		public function get id():String
		{
			return facilityDTO.rid;
		}
		
		public function addToClient(parentClient:Client = null):void 
		{
			if (parentClient == null)
				parentClient = client;
			
			if (parentClient == null)
				return;
			
			parentClient.facilityCollection.addItem(this);
			this.client = parentClient;
		}
		
		public function getPosition():LatLng
		{
			if (!facilityDTO.MapLatLong)
				return null;
				
			var lat:Number = Number(facilityDTO.MapLatLong.split(LAT_LNG_DIVIDER)[0]);
			var lon:Number = Number(facilityDTO.MapLatLong.split(LAT_LNG_DIVIDER)[1]);
			
			if (isNaN(lat) || isNaN(lon))
				return null;
			else 
				return new LatLng(lat, lon);
		}
		
		public function setPosition(value:LatLng):void
		{
			var latLngStr:String = "";
			
			if (value)
			{
				latLngStr += value.lat.toFixed(6);
				latLngStr += LAT_LNG_DIVIDER;
				latLngStr += value.lng.toFixed(6);
			}
			
			facilityDTO.MapLatLong = latLngStr;
		}
		
		public function setZoom(value:Number):void
		{
			facilityDTO.MapZoom = value;
		}
		
		public function getMarker():FacilityMarker 
		{
			var marker:FacilityMarker = new FacilityMarker();
			
			//marker.index = id;
			marker.name = name;
			marker.infoParam = {title:name, content:getInfo()};
			marker.position = getPosition();
			
			return marker;
		}
			
		public function getInfo():String
		{
			var result:String = "";
			
			result += facilityDTO.Address;
			
			return result;
		}
		
		public function Facility(dto:Facilities_DTO)
		{
			super();

			if (dto == null)
				throw new Error("Facility::Facility() - DTO object can not be null!");
			
			facilityDTO = dto;
			_sectionsCollection = new ArrayCollection();
			
			uniquePhotoId = UIDUtil.createUID();
			uniqueKeyplanId = UIDUtil.createUID();
		}
		
	}
}
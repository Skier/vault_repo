package com.llsvc.maps.layers
{
	public class GeoGeometryLayer extends BaseLayer
	{
		public function GeoGeometryLayer(geoServUrl:String)
		{
			super(geoServUrl);
			layerGroup = "Leases";
			layerName = "Geo Geometry";

			this.minZoomLevel = 1;
			this.maxZoomLevel = 16;
		}
		
		override protected function getGeoServerLayer():String 
		{
			return "topp:geo_geometry";
		} 
	}
}